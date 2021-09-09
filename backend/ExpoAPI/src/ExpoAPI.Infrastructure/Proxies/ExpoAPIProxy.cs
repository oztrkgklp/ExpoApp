using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
// using ExpoAPI.Domain.Commands.Aggregates.User;
using ExpoAPI.Domain.Proxies;


namespace ExpoAPI.Infrastructure.Proxies
{
    [ExcludeFromCodeCoverage]
    public class ExpoAPIProxy : IExpoAPIProxy
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ExpoAPISettings _ExpoAPISettings;
        private readonly ILogger<ExpoAPIProxy> _logger;

        public ExpoAPIProxy(IHttpClientFactory httpClientFactory, IOptions<ExpoAPISettings> ExpoAPISettings, ILogger<ExpoAPIProxy> logger)
        {
            _httpClientFactory = httpClientFactory;
            _ExpoAPISettings = ExpoAPISettings.Value;
            _logger = logger;
        }

        public async Task<ExpoAPIResponse<TResponse>> AuthenticateAsync<TRequest, TResponse>(TRequest @params, string method, CancellationToken cancellationToken)
        {
            var request = GetRequest(@params, method);
            return await SendAsync<TRequest, TResponse>(request, cancellationToken);
        }

        public async Task<ExpoAPIResponse<TResponse>> SendRequestAsync<TRequest, TResponse>(TRequest @params, string method, string token, CancellationToken cancellationToken)
        {
            var request = GetRequest(@params, method, token);
            return await SendAsync<TRequest, TResponse>(request, cancellationToken);
        }

        /// <summary>
        /// todo : use circuit breaker 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <returns></returns>
        private async Task<ExpoAPIResponse<TResponse>> SendAsync<TRequest, TResponse>(Request request, CancellationToken cancellationToken)
        {
            var content = new ByteArrayContent(Serialize(request));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json-rpc");
            var httpClient = _httpClientFactory.CreateClient("ExpoAPIClient");
            var response = await httpClient.PostAsync(_ExpoAPISettings.Url, content, cancellationToken);
            var responseData = await response.Content.ReadAsByteArrayAsync();
            return HandleResponse<TResponse>(responseData);
        }

        private Request GetRequest(object @params, string method, string token = null)
        {
            return new Request
            {
                Method = method,
                Params = @params,
                Id = Guid.NewGuid().ToString(),
                Auth = token
            };
        }

        private ExpoAPIResponse<TResponse> HandleResponse<TResponse>(byte[] responseData)
        {
            var responseString = Encoding.UTF8.GetString(responseData);
            var response = JsonSerializer.Deserialize<ExpoAPIResponse<TResponse>>(responseString);
            return response;
        }

        private byte[] Serialize<T>(T value)
        {
            return Encoding.UTF8.GetBytes(JsonSerializer.Serialize(value));
        }
    }
}