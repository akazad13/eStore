using eStore.Application.Common.Interfaces;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace eStore.Application.Common.Behaviours
{
    public class LoggingBehaviour<TRequest>(
        ILogger<TRequest> logger,
        ICurrentUserService currentUserService,
        IIdentityService identityService
        ) : IRequestPreProcessor<TRequest>
        where TRequest : notnull
    {
        private readonly ILogger _logger = logger;
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly IIdentityService _identityService = identityService;

        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            var userId = _currentUserService.UserId;
            string userName = string.Empty;

            if (userId != 0)
            {
                userName = await _identityService.GetUserNameAsync(userId);
            }

            _logger.LogInformation(
                "CleanArchitecture Request: {Name} {@UserId} {@UserName} {@Request}",
                requestName,
                userId,
                userName,
                request
            );
        }
    }
}
