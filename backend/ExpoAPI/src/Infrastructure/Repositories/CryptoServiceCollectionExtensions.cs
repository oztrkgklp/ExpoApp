using ExpoAPI.Infrastructure.Adapters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpoAPI.Api.Infrastructure.Repositories
{
    public static class CryptoServiceCollectionExtensions
    {
        public static IServiceCollection AddCrypto(this IServiceCollection services)
        {
            services.AddSingleton<IHashingAdapter, HashingAdapter>();
            return services;
        }
    }
}
