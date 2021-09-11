using System.Diagnostics;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ExpoAPI.Infrastructure.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var serializedRequest = JsonSerializer.Serialize(request);
            _logger.LogInformation("Handling request with content @{serializedRequest}", serializedRequest);
            var timer = new Stopwatch();
            timer.Start();

            var response = await next();

            timer.Stop();
            var timeTaken = timer.Elapsed;
            if (timeTaken.Seconds > 3) // if the request is greater than 3 seconds, then log the warnings
            {
                _logger.LogWarning($"[PERF] The request {typeof(TRequest).FullName} took {timeTaken.Seconds} seconds.");
            }

            var serializedResponse = JsonSerializer.Serialize(response);
            _logger.LogInformation("Handled response with content {@serializedResponse}", serializedResponse);
            return response;
        }
    }
}