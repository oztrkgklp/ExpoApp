using System.Threading;
using System.Threading.Tasks;

namespace ExpoAPI.Domain.Proxies
{
    public interface IExpoAPIProxy
    {
        Task<ExpoAPIResponse<TResponse>> AuthenticateAsync<TRequest, TResponse>(TRequest @params, string method,
            CancellationToken cancellationToken);

        Task<ExpoAPIResponse<TResponse>> SendRequestAsync<TRequest, TResponse>(TRequest @params, string method,
            string token, CancellationToken cancellationToken);
    }
}