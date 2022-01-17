using ExpoAPI.UseCases.Admin;
using ExpoAPI.UseCases.Purchase;
using ExpoAPI.UseCases.Company;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using ExpoAPI.UseCases.Accommodation;
using ExpoAPI.UseCases.ExternalAttendance;
using ExpoAPI.UseCases.OtelInformation;
using ExpoAPI.UseCases.Balance;
using ExpoAPI.UseCases.Cost;
using ExpoAPI.UseCases.Expense;

namespace ExpoAPI.Infrastructure.Repositories
{
    public interface IExpoAPIQueryRepository
    {
        Task<AdminInformationContract?> GetAdminInformationAsync(CancellationToken cancellationToken);

        Task<PurchaseContract?> GetPurchaseByIdAsync(int purchaseID, CancellationToken cancellationToken);
        Task<IEnumerable<PurchaseContract?>?> GetPurchasesAsync(CancellationToken cancellationToken);
        Task<int?> GetNumberOfPurchasesAsync(CancellationToken cancellationToken);
        Task<IEnumerable<PurchaseWithNamesContract?>?> GetPurchasesWithCompanyNamesAsync(CancellationToken cancellationToken);
        Task<object?> UpdatePurchaseByIdAsync(PurchaseContract? contract, CancellationToken cancellationToken);
        Task<object?> DeletePurchaseByIdAsync(int purchaseID, CancellationToken cancellationToken);
        Task<object?> CreatePurchaseAsync(PurchaseContract? contract, CancellationToken cancellationToken);

        Task<CompanyContract?> GetCompanyByIdAsync(int companyID, CancellationToken cancellationToken);
        Task<IEnumerable<CompanyContract?>?> GetCompaniesAsync(CancellationToken cancellationToken);
        Task<IEnumerable<CompanyContract?>?> GetAllAsync(CancellationToken cancellationToken);
        Task<IEnumerable<CompanyContract?>?> GetGuestsAsync(CancellationToken cancellationToken);
        Task<IEnumerable<CompanyContract?>?> GetEnteredCompaniesAsync(CancellationToken cancellationToken);
        Task<int?> GetNumberOfEnteredCompaniesAsync(CancellationToken cancellationToken);
        Task<IEnumerable<CompanyContract?>?> GetEnteredCompaniesWithoutPurchaseAsync(CancellationToken cancellationToken);
        Task<int?> GetNumberOfEnteredCompaniesWithoutPurchaseAsync(CancellationToken cancellationToken);
        Task<IEnumerable<CompanyContract?>?> GetNotEnteredCompaniesAsync(CancellationToken cancellationToken);
        Task<int?> GetNumberOfNotEnteredCompaniesAsync(CancellationToken cancellationToken);
        Task<int?> GetNumberOfCompaniesAsync(CancellationToken cancellationToken);
        Task<int?> GetNumberOfGuestsAsync(CancellationToken cancellationToken);
        Task<int?> GetNumberOfAccommodationsAsync(CancellationToken cancellationToken);
        Task<IEnumerable<string?>?> GetCompanyNamesAsync(CancellationToken cancellationToken);
        Task<object?> UpdateCompanyByIdAsync(CompanyContract? contract, CancellationToken cancellationToken);
        Task<object?> DeleteCompanyByIdAsync(int companyID, CancellationToken cancellationToken);
        Task<object?> CreateCompanyAsync(CompanyContract? contract, CancellationToken cancellationToken);

        Task<AccommodationDBContract?> GetAccommodationByIdAsync(int AccommodationID, CancellationToken cancellationToken);
        Task<IEnumerable<AccommodationDBContract?>?> GetAccommodationsAsync(CancellationToken cancellationToken);
        Task<object?> UpdateAccommodationByIdAsync(AccommodationContract? contract, CancellationToken cancellationToken);
        Task<object?> DeleteAccommodationByIdAsync(int accommodationID, CancellationToken cancellationToken);
        Task<object?> CreateAccommodationAsync(AccommodationContract? contract, CancellationToken cancellationToken);


        Task<List<PurchaseContract?>?> GetPurchasesBySellerIdAsync(int sellerID, CancellationToken cancellationToken);
        Task<List<PurchaseContract?>?> GetPurchasesByPurchaserIdAsync(int PurchaserID, CancellationToken cancellationToken);
        Task<int?> GetCompanyIdByNameAsync(string? name, CancellationToken cancellationToken);        
        Task<string?> GetCompanyNameByIdAsync(int companyID, CancellationToken cancellationToken);        

        Task<decimal?> GetTotalEndorsementAsync(CancellationToken cancellationToken);



        Task<List<ExternalAttendanceContract?>?> GetExternalAttendancesAsync(CancellationToken cancellationToken);
        Task<ExternalAttendanceContract?> GetExternalAttendanceByIdAsync(int externalAttendanceId, CancellationToken cancellationToken);
        Task<object?> DeleteExternalAttendanceByIdAsync(int externalAttendanceId, CancellationToken cancellationToken);
        Task<object?> UpdateExternalAttendanceByIdAsync(ExternalAttendanceContract? contract, CancellationToken cancellationToken);
        Task<object?> CreateExternalAttendanceAsync(ExternalAttendanceContract? contract, CancellationToken cancellationToken);

        Task<OtelInformationContract?> GetOtelInformationAsync(CancellationToken cancellationToken);
        Task<object?> UpdateOtelInformationAsync(OtelInformationContract? contract, CancellationToken cancellationToken);


        Task<BalanceContract?> GetBalanceAsync(CancellationToken cancellationToken);
        Task<object?> UpdateBalanceAsync(BalanceContract? contract, CancellationToken cancellationToken);


        Task<List<CostContract?>?> GetCostsAsync(CancellationToken cancellationToken);
        Task<CostContract?> GetCostByIdAsync(int CostId, CancellationToken cancellationToken);
        Task<object?> DeleteCostByIdAsync(int CostId, CancellationToken cancellationToken);
        Task<object?> UpdateCostByIdAsync(CostContract? contract, CancellationToken cancellationToken);
        Task<object?> CreateCostAsync(CostContract? contract, CancellationToken cancellationToken);

        Task<List<ExpenseContract?>?> GetExpensesAsync(CancellationToken cancellationToken);
        Task<ExpenseContract?> GetExpenseByIdAsync(int ExpenseId, CancellationToken cancellationToken);
        Task<object?> DeleteExpenseByIdAsync(int ExpenseId, CancellationToken cancellationToken);
        Task<object?> UpdateExpenseByIdAsync(ExpenseContract? contract, CancellationToken cancellationToken);
        Task<object?> CreateExpenseAsync(ExpenseContract? contract, CancellationToken cancellationToken);

    }
}