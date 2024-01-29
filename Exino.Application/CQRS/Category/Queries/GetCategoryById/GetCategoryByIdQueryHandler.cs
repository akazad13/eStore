﻿using AutoMapper;
using Exino.Application.Common.Wrappers;
using Exino.Application.RepositoriesInterface;
using Exino.Domain.Enums;
using MediatR;

namespace Exino.Application.CQRS.Category.Queries.GetCategoryById
{
    public class GetCategoryByIdQueryHandler
        : IRequestHandler<GetCategoryByIdQueryRequest, IResult<GetCategoryByIdQueryResponse>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<IResult<GetCategoryByIdQueryResponse>> Handle(
            GetCategoryByIdQueryRequest request,
            CancellationToken cancellationToken
        )
        {
            var category = await _categoryRepository.GetFilteredFirstOrDefault(
                selector: x => new GetCategoryByIdQueryResponse { Id = x.Id, Name = x.Name, },
                expression: x => x.Id == request.Id && x.Status != Status.Deactive
            );

            return Response<GetCategoryByIdQueryResponse>.SuccessResponese(category);
        }
    }
}