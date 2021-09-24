using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ExpoAPI.UseCases.Admin;
using ExpoAPI.UseCases.Company;
using ExpoAPI.UseCases.Purchase;
using Microsoft.Extensions.Configuration;

namespace ExpoAPI.Infrastructure.Repositories
{
    public class ExpoAPIQueryRepository : BaseRepository, IExpoAPIQueryRepository
    {
        private readonly IDapperPolly _dapperPolly;

        public ExpoAPIQueryRepository(IConfiguration configuration, IDapperPolly dapperPolly) : base(configuration)
        {
            _dapperPolly = dapperPolly;
        }

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

        public async Task<PurchaseContract?> GetPurchaseByIdAsync(int purchaseID, CancellationToken cancellationToken)
        {
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(@"SELECT * FROM PURCHASE WHERE PurchaseID = ").Append(purchaseID);
            using(Database)
            {
                var getPurchaseById = await _dapperPolly.QueryAsyncWithRetry<PurchaseContract>(Database, queryBuilder.ToString());
                
                if (getPurchaseById.Any())
                {
                    return getPurchaseById.FirstOrDefault();
                }
                return null;
            }
        }

        public async Task<object?> CreatePurchaseAsync(PurchaseContract? contract, CancellationToken cancellationToken)
        {
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(@"INSERT INTO PURCHASE (SellerID,PurchaserID,PurchaseDate,Amount) VALUES (")
                                    .Append(contract.SellerID).Append(",")
                                    .Append(contract.PurchaserID).Append(",'")
                                    .Append(contract.PurchaseDate).Append("',")
                                    .Append(contract.Amount).Append(")");
            using(Database)
            {
                var createPurchase = await _dapperPolly.QueryAsyncWithRetry<object>(Database, queryBuilder.ToString());

                if (createPurchase.Any())
                {
                    return createPurchase.FirstOrDefault();
                }
                return null;
            }
        }

        public async Task<object?> DeletePurchaseByIdAsync(int purchaseID, CancellationToken cancellationToken)
        {
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(@"DELETE FROM PURCHASE WHERE PurchaseID = ").Append(purchaseID);
            using(Database)
            {
                var deletePurchaseById = await _dapperPolly.QueryAsyncWithRetry<object>(Database, queryBuilder.ToString());

                if (deletePurchaseById.Any())
                {
                    return deletePurchaseById.FirstOrDefault();
                }
                return null;
            }
        }

        public async Task<object?> UpdatePurchaseByIdAsync(PurchaseContract? contract, CancellationToken cancellationToken)
        {
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(@"UPDATE PURCHASE SET ")
                                    .Append("SellerID").Append("=").Append(contract.SellerID).Append(",")
                                    .Append("PurchaserID").Append("=").Append(contract.PurchaserID).Append(",")
                                    .Append("PurchaseDate").Append("='").Append(contract.PurchaseDate).Append("',")
                                    .Append("Amount").Append("=").Append(contract.Amount).Append(" WHERE PurchaseID=").Append(contract.PurchaseID);
            using(Database)
            {
                var updatePurchaseById = await _dapperPolly.QueryAsyncWithRetry<object>(Database, queryBuilder.ToString());

                if (updatePurchaseById.Any())
                {
                    return updatePurchaseById.FirstOrDefault();
                }
                return null;
            }
        }

        public async Task<IEnumerable<PurchaseContract?>?> GetPurchasesAsync(CancellationToken cancellationToken)
        {
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(@"SELECT * FROM PURCHASE");
            using(Database)
            {
                var getPurchases = await _dapperPolly.QueryAsyncWithRetry<PurchaseContract>(Database, queryBuilder.ToString());

                if (getPurchases.Any())
                {
                    return getPurchases.ToList();
                }
                return null;
            }
        }

        public async Task<int?> GetNumberOfPurchasesAsync(CancellationToken cancellationToken)
        {
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(@"SELECT COUNT(PurchaseID) FROM PURCHASE");
            using(Database)
            {
                var getNumberOfPurchases = await _dapperPolly.QueryAsyncWithRetry<int?>(Database, queryBuilder.ToString());
                if (getNumberOfPurchases.Any())
                {
                    return getNumberOfPurchases.FirstOrDefault();
                }
                return null;
            }
        }

        public async Task<IEnumerable<PurchaseWithNamesContract?>?> GetPurchasesWithCompanyNamesAsync(CancellationToken cancellationToken)
        {
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(@"SELECT PurchaseID, C2.CompanyName AS SellerName, C1.CompanyName AS PurchaserName,Amount,PurchaseDate FROM PURCHASE P
                                    INNER JOIN COMPANY C1 ON C1.CompanyID = P.PurchaserID
                                    INNER JOIN COMPANY C2 ON C2.CompanyID = P.SellerID");
            using(Database)
            {
                var getPurchases = await _dapperPolly.QueryAsyncWithRetry<PurchaseWithNamesContract>(Database, queryBuilder.ToString());

                if (getPurchases.Any())
                {
                    return getPurchases.ToList();
                }
                return null;
            }
        }

        public async Task<CompanyContract?> GetCompanyByIdAsync(int companyID, CancellationToken cancellationToken)
        {
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(@"SELECT * FROM COMPANY WHERE CompanyID = ").Append(companyID);
            using(Database)
            {
                var getCompanyById = await _dapperPolly.QueryAsyncWithRetry<CompanyContract?>(Database, queryBuilder.ToString());

                if (getCompanyById.Any())
                {
                    return getCompanyById.FirstOrDefault();
                }
                return null;
            }
        }

        public async Task<IEnumerable<CompanyContract?>?> GetCompaniesAsync(CancellationToken cancellationToken)
        {
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(@"SELECT * FROM COMPANY");
            using(Database)
            {
                var getCompanies = await _dapperPolly.QueryAsyncWithRetry<CompanyContract>(Database, queryBuilder.ToString());
                if (getCompanies.Any())
                {
                    return getCompanies.ToList();
                }
                return null;
            }
        }

        public async Task<int?> GetNumberOfCompaniesAsync(CancellationToken cancellationToken)
        {
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(@"SELECT COUNT(CompanyID) FROM COMPANY");
            using(Database)
            {
                var getNumberOfCompanies = await _dapperPolly.QueryAsyncWithRetry<int?>(Database, queryBuilder.ToString());
                if (getNumberOfCompanies.Any())
                {
                    return getNumberOfCompanies.FirstOrDefault();
                }
                return null;
            }
        }

        public async Task<IEnumerable<string?>?> GetCompanyNamesAsync(CancellationToken cancellationToken)
        {
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(@"SELECT CompanyName FROM COMPANY");
            using(Database)
            {
                var getCompanyNames = await _dapperPolly.QueryAsyncWithRetry<string?>(Database, queryBuilder.ToString());
                if (getCompanyNames.Any())
                {
                    return getCompanyNames.ToList();
                }
                return null;
            }
        }

        public async Task<object?> UpdateCompanyByIdAsync(CompanyContract? contract, CancellationToken cancellationToken)
        {
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(@"UPDATE COMPANY SET ")
                                    .Append("CompanyName").Append("=").Append(contract.CompanyName).Append(", ")
                                    .Append("Phone").Append("=").Append(contract.Phone).Append(", ")
                                    .Append("EMail").Append("=").Append(contract.EMail).Append(", ")
                                    .Append("Endorsement").Append("=").Append(contract.Endorsement).Append(", ")
                                    .Append("IsEntered").Append("=").Append(contract.IsEntered?1:0).Append(" WHERE CompanyID =").Append(contract.CompanyID);
            using(Database)
            {
                var updateCompanyById = await _dapperPolly.QueryAsyncWithRetry<object>(Database, queryBuilder.ToString());

                if (updateCompanyById.Any())
                {
                    return updateCompanyById.FirstOrDefault();
                }
                return null;
            }
        }

        public async Task<object?> DeleteCompanyByIdAsync(int companyID, CancellationToken cancellationToken)
        {
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(@"DELETE FROM COMPANY WHERE CompanyID = ").Append(companyID);
            using(Database)
            {
                var deleteCompanyById = await _dapperPolly.QueryAsyncWithRetry<object>(Database, queryBuilder.ToString());

                if (deleteCompanyById.Any())
                {
                    return deleteCompanyById.FirstOrDefault();
                }
                return null;
            }
        }

        public async Task<object?> CreateCompanyAsync(CompanyContract? contract, CancellationToken cancellationToken)
        {
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(@"INSERT INTO COMPANY (CompanyName,Phone,EMail,Endorsement,IsEntered) VALUES (")
                                    .Append(contract.CompanyName).Append(",")
                                    .Append(contract.Phone).Append(",")
                                    .Append(contract.EMail).Append(",")
                                    .Append(contract.Endorsement).Append(",")
                                    .Append(contract.IsEntered?1:0).Append(")");
            using(Database)
            {
                var createCompany = await _dapperPolly.QueryAsyncWithRetry<object>(Database, queryBuilder.ToString());

                if (createCompany.Any())
                {
                    return createCompany.FirstOrDefault();
                }
                return null;
            }
        }

        public async Task<List<PurchaseContract?>?> GetPurchasesBySellerIdAsync(int sellerID, CancellationToken cancellationToken)
        {
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(@"SELECT * FROM PURCHASE WHERE SellerID = ").Append(sellerID);
            using(Database)
            {
                var deletePurchaseById = await _dapperPolly.QueryAsyncWithRetry<PurchaseContract?>(Database, queryBuilder.ToString());

                if (deletePurchaseById.Any())
                {
                    return deletePurchaseById.ToList();
                }
                return null;
            }
        }

        public async Task<List<PurchaseContract?>?> GetPurchasesByPurchaserIdAsync(int purchaserID, CancellationToken cancellationToken)
        {
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(@"SELECT * FROM PURCHASE WHERE PurchaserID = ").Append(purchaserID);
            using(Database)
            {
                var deletePurchaseById = await _dapperPolly.QueryAsyncWithRetry<PurchaseContract>(Database, queryBuilder.ToString());

                if (deletePurchaseById.Any())
                {
                    return deletePurchaseById.ToList();
                }
                return null;
            }
        }

        public async Task<int?> GetCompanyIdByNameAsync(string? name, CancellationToken cancellationToken)
        {
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(@"SELECT DISTINCT CompanyID FROM COMPANY WHERE CompanyName = ").Append(name);
            using(Database)
            {
                var deletePurchaseById = await _dapperPolly.QueryAsyncWithRetry<int?>(Database, queryBuilder.ToString());

                if (deletePurchaseById.Any())
                {
                    return deletePurchaseById.FirstOrDefault();
                }
                return null;
            }
        }

        public async Task<string?> GetCompanyNameByIdAsync(int companyID, CancellationToken cancellationToken)
        {
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(@"SELECT DISTINCT CompanyName FROM COMPANY WHERE CompanyID = ").Append(companyID);
            using(Database)
            {
                var deletePurchaseById = await _dapperPolly.QueryAsyncWithRetry<string?>(Database, queryBuilder.ToString());

                if (deletePurchaseById.Any())
                {
                    return deletePurchaseById.FirstOrDefault();
                }
                return null;
            }
        }

        public async Task<decimal?> GetTotalEndorsementAsync(CancellationToken cancellationToken)
        {
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(@"select SUM(CAST(Endorsement AS decimal(19,4))) from COMPANY");
            using(Database)
            {
                var getTotalEndorsement = await _dapperPolly.QueryAsyncWithRetry<decimal?>(Database, queryBuilder.ToString());

                if (getTotalEndorsement.Any())
                {
                    return getTotalEndorsement.FirstOrDefault();
                }
                return null;
            }
        }
    }
}