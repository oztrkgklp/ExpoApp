using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ExpoAPI.UseCases;

namespace ExpoAPI.Infrastructure.Repositories
{
    public interface IExpoAPIQueryRepository
    {
        Task<AdminInformationContract> GetAdminInformationAsync(CancellationToken cancellationToken);
    }
}