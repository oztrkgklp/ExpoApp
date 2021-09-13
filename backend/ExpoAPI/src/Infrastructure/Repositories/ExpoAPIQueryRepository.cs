using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ExpoAPI.UseCases.Admin;
using ExpoAPI.UseCases.Purchase;

namespace ExpoAPI.Infrastructure.Repositories
{
    public class ExpoAPIQueryRepository : BaseRepository, IExpoAPIQueryRepository
    {
        private readonly IDapperPolly _dapperPolly;

        public ExpoAPIQueryRepository(IConfiguration configuration, IDapperPolly dapperPolly) : base(configuration)
        {
            _dapperPolly = dapperPolly;
        }

        /// <summary>
        /// todo: use cancellationToken for db
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<AdminInformationContract?> GetAdminInformationAsync(CancellationToken cancellationToken)
        {
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(@"SELECT TOP 1 * FROM ADMIN");
            using(Database)
            {
                var getAdminInformation = await _dapperPolly.QueryAsyncWithRetry<AdminInformationContract>(Database, queryBuilder.ToString());

                if (getAdminInformation.Any())
                {
                    return getAdminInformation.FirstOrDefault();
                }

                return null;
            }
        }

        public async Task<PurchaseDBModel?> GetPurchaseByIdAsync(int purchaseID, CancellationToken cancellationToken)
        {
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(@"SELECT TOP 1 * FROM PURCHASES WHERE PurchaseID = ").Append(purchaseID);
            using(Database)
            {
                var getPurchaseById = await _dapperPolly.QueryAsyncWithRetry<PurchaseDBModel>(Database, queryBuilder.ToString());

                if (getPurchaseById.Any())
                {
                    return getPurchaseById.FirstOrDefault();
                }
                return null;
            }
        }
    }
}