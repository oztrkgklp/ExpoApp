using ExpoAPI.UseCases.Admin;
using ExpoAPI.UseCases.Purchase;

namespace ExpoAPI.Infrastructure.Repositories
{
    public interface IExpoAPIQueryRepository
    {
        Task<AdminInformationContract?> GetAdminInformationAsync(CancellationToken cancellationToken);

        Task<PurchaseDBModel?> GetPurchaseByIdAsync(int purchaseID, CancellationToken cancellationToken);
    }
}