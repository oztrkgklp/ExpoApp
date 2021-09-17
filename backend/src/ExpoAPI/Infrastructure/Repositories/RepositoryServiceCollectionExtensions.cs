using Microsoft.Extensions.DependencyInjection;

namespace ExpoAPI.Infrastructure.Repositories
{
    public static class RepositoryServiceCollectionExtensions
    {
        public static IServiceCollection AddDapperPolly(this IServiceCollection services)
        {
            services.AddSingleton<IDapperPolly, DapperPolly>();
            return services;
        }
    }
}