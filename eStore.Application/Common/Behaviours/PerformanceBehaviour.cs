using System.Diagnostics;
using eStore.Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace eStore.Application.Common.Behaviours
{
    public class PerformanceBehaviour<TRequest, TResponse>(
        ILogger<TRequest> logger,
        ICurrentUserService currentUserService,
        IIdentityService identityService
        ) : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly Stopwatch _timer = new();
        private readonly ILogger<TRequest> _logger = logger;
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly IIdentityService _identityService = identityService;

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken
        )
        {
            _timer.Start();

            var response = await next();

            _timer.Stop();

            var elapsedMilliseconds = _timer.ElapsedMilliseconds;

            if (elapsedMilliseconds > 500)
            {
                var requestName = typeof(TRequest).Name;
                var userId = _currentUserService.UserId;
                var userName = string.Empty;

                if (userId != 0)
                {
                    userName = await _identityService.GetUserNameAsync(userId);
                }

                _logger.LogWarning(
                    "CleanArchitecture Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@UserId} {@UserName} {@Request}",
                    requestName,
                    elapsedMilliseconds,
                    userId,
                    userName,
                    request
                );
            }

            return response;
        }
    }
}
