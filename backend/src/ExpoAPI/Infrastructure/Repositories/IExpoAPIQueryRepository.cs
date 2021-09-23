using ExpoAPI.UseCases.Admin;
using ExpoAPI.UseCases.Purchase;
using ExpoAPI.UseCases.Company;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;

namespace ExpoAPI.Infrastructure.Repositories
{
    public interface IExpoAPIQueryRepository
    {
        Task<AdminInformationContract?> GetAdminInformationAsync(CancellationToken cancellationToken);

        Task<PurchaseContract?> GetPurchaseByIdAsync(int purchaseID, CancellationToken cancellationToken);
        Task<IEnumerable<PurchaseContract?>?> GetPurchasesAsync(CancellationToken cancellationToken);
        Task<IEnumerable<PurchaseWithNamesContract?>?> GetPurchasesWithCompanyNamesAsync(CancellationToken cancellationToken);
        Task<object?> UpdatePurchaseByIdAsync(PurchaseContract? contract, CancellationToken cancellationToken);
        Task<object?> DeletePurchaseByIdAsync(int purchaseID, CancellationToken cancellationToken);
        Task<object?> CreatePurchaseAsync(PurchaseContract? contract, CancellationToken cancellationToken);

        Task<CompanyContract?> GetCompanyByIdAsync(int companyID, CancellationToken cancellationToken);
        Task<IEnumerable<CompanyContract?>?> GetCompaniesAsync(CancellationToken cancellationToken);
        Task<object?> UpdateCompanyByIdAsync(CompanyContract? contract, CancellationToken cancellationToken);
        Task<object?> DeleteCompanyByIdAsync(int companyID, CancellationToken cancellationToken);
        Task<object?> CreateCompanyAsync(CompanyContract? contract, CancellationToken cancellationToken);

        Task<List<PurchaseContract?>?> GetPurchasesBySellerIdAsync(int sellerID, CancellationToken cancellationToken);
        Task<List<PurchaseContract?>?> GetPurchasesByPurchaserIdAsync(int PurchaserID, CancellationToken cancellationToken);
        Task<int?> GetCompanyIdByNameAsync(string? name, CancellationToken cancellationToken);        
        Task<string?> GetCompanyNameByIdAsync(int companyID, CancellationToken cancellationToken);        

        Task<decimal?> GetTotalEndorsementAsync(CancellationToken cancellationToken);

    }
}