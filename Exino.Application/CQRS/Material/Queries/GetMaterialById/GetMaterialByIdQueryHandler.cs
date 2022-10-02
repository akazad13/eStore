using AutoMapper;
using Exino.Application.Common.Wrappers;
using Exino.Application.RepositoriesInterface;
using Exino.Domain.Enums;
using MediatR;

namespace Exino.Application.CQRS.Material.Queries.GetMaterialById
{
    public class GetMaterialByIdQueryHandler
        : IRequestHandler<GetMaterialByIdQueryRequest, IResult<GetMaterialByIdQueryResponse>>
    {
        private readonly IMaterialRepository _materialRepository;
        private readonly IMapper _mapper;

        public GetMaterialByIdQueryHandler(IMaterialRepository materialRepository, IMapper mapper)
        {
            _materialRepository = materialRepository;
            _mapper = mapper;
        }

        public async Task<IResult<GetMaterialByIdQueryResponse>> Handle(
            GetMaterialByIdQueryRequest request,
            CancellationToken cancellationToken
        )
        {
            var material = await _materialRepository.GetFilteredFirstOrDefault(
                selector: x =>
                    new GetMaterialByIdQueryResponse
                    {
                        Id = x.Id,
                        Name = x.Name,
                        MaterialImagePath = x.MaterialImagePath,
                    },
                expression: x => x.Id == request.Id && x.Status != Status.Deactive
            );
            return Response<GetMaterialByIdQueryResponse>.SuccessResponese(material);
        }
    }
}
