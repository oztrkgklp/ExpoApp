using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ExpoAPI.UseCases.Accommodation;
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
            queryBuilder.Append(@"INSERT INTO PURCHASE (SellerID,PurchaserID,Product,PurchaseDate,Amount) VALUES (")
                                    .Append(contract.SellerID).Append(",")
                                    .Append(contract.PurchaserID).Append(",")
                                    .Append(contract.Product).Append(",'")
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
                                    .Append("Product").Append("=").Append(contract.Product).Append(",")
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
            queryBuilder.Append(@"SELECT PurchaseID, C2.CompanyName AS SellerName, C1.CompanyName AS PurchaserName,Product,Amount,PurchaseDate FROM PURCHASE P
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
            queryBuilder.Append(@"SELECT * FROM COMPANY WHERE IsGuest=0");
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

        public async Task<IEnumerable<CompanyContract?>?> GetAllAsync(CancellationToken cancellationToken)
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

        public async Task<IEnumerable<CompanyContract?>?> GetGuestsAsync(CancellationToken cancellationToken)
        {
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(@"SELECT * FROM COMPANY WHERE IsGuest = 1");
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

        public async Task<IEnumerable<CompanyContract?>?> GetEnteredCompaniesAsync(CancellationToken cancellationToken)
        {
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(@"SELECT * FROM COMPANY WHERE IsGuest = 0 AND IsEntered = 1");
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

        public async Task<int?> GetNumberOfEnteredCompaniesAsync(CancellationToken cancellationToken)
        {
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(@"SELECT COUNT(CompanyID) FROM COMPANY WHERE IsGuest = 0 AND IsEntered = 1");
            using(Database)
            {
                var getCompanies = await _dapperPolly.QueryAsyncWithRetry<int>(Database, queryBuilder.ToString());
                if (getCompanies.Any())
                {
                    return getCompanies.FirstOrDefault();
                }
                return null;
            }
        }

        public async Task<IEnumerable<CompanyContract?>?> GetEnteredCompaniesWithoutPurchaseAsync(CancellationToken cancellationToken)
        {
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(@"select C.CompanyID,C.CompanyName,C.Phone,C.EMail,C.Endorsement,C.IsEntered from COMPANY C
                                    INNER JOIN (select DISTINCT C.CompanyID from COMPANY C WHERE C.IsEntered = 1 AND C.IsGuest = 1
                                                except
                                                select DISTINCT C.CompanyID AS ID from COMPANY C	
                                                    INNER JOIN (select P.PurchaserID, P.Amount from PURCHASE P) P ON P.PurchaserID = C.CompanyID
                                                    WHERE C.CompanyID = P.PurchaserID) I ON I.CompanyID = C.CompanyID");
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

        public async Task<int?> GetNumberOfEnteredCompaniesWithoutPurchaseAsync(CancellationToken cancellationToken)
        {
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(@"select COUNT(C.CompanyID) from (select C.CompanyID,C.CompanyName,C.Phone,C.EMail,C.Endorsement,C.IsEntered from COMPANY C
                                    INNER JOIN (select DISTINCT C.CompanyID from COMPANY C WHERE C.IsEntered = 1 AND C.IsGuest = 1
                                                except
                                                select DISTINCT C.CompanyID AS ID from COMPANY C	
                                                    INNER JOIN (select P.PurchaserID, P.Amount from PURCHASE P) P ON P.PurchaserID = C.CompanyID
                                                    WHERE C.CompanyID = P.PurchaserID) I ON I.CompanyID = C.CompanyID) C ");
            using(Database)
            {
                var getCompanies = await _dapperPolly.QueryAsyncWithRetry<int>(Database, queryBuilder.ToString());
                if (getCompanies.Any())
                {
                    return getCompanies.FirstOrDefault();
                }
                return null;
            }
        }

        public async Task<IEnumerable<CompanyContract?>?> GetNotEnteredCompaniesAsync(CancellationToken cancellationToken)
        {
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(@"SELECT * FROM COMPANY WHERE IsEntered = 0 AND IsGuest = 0");
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

        public async Task<int?> GetNumberOfNotEnteredCompaniesAsync(CancellationToken cancellationToken)
        {
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(@"SELECT COUNT(CompanyID) FROM COMPANY WHERE IsGuest = 0 AND IsEntered = 0");
            using(Database)
            {
                var getCompanies = await _dapperPolly.QueryAsyncWithRetry<int?>(Database, queryBuilder.ToString());
                if (getCompanies.Any())
                {
                    return getCompanies.FirstOrDefault();
                }
                return null;
            }
        }

        public async Task<int?> GetNumberOfCompaniesAsync(CancellationToken cancellationToken)
        {
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(@"SELECT COUNT(CompanyID) FROM COMPANY WHERE IsGuest = 0");
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

        public async Task<int?> GetNumberOfGuestsAsync(CancellationToken cancellationToken)
        {
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(@"SELECT COUNT(CompanyID) FROM COMPANY WHERE IsGuest = 1");
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
                                    .Append("IsGuest").Append("=").Append(contract.IsGuest?1:0).Append(", ")
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
            queryBuilder.Append(@"INSERT INTO COMPANY (CompanyName,Phone,EMail,Endorsement,IsGuest,IsEntered) VALUES (")
                                    .Append(contract.CompanyName).Append(",")
                                    .Append(contract.Phone).Append(",")
                                    .Append(contract.EMail).Append(",")
                                    .Append(contract.Endorsement).Append(",")
                                    .Append(contract.IsGuest?1:0).Append(",")
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

        public async Task<AccommodationDBContract?> GetAccommodationByIdAsync(int accommodationID, CancellationToken cancellationToken)
        {
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(@"select * from ACCOMMODATION WHERE AccommodationID =" + accommodationID);
            using(Database)
            {
                var getAccommodationById = await _dapperPolly.QueryAsyncWithRetry<AccommodationDBContract?>(Database, queryBuilder.ToString());

                if (getAccommodationById.Any())
                {
                    return getAccommodationById.FirstOrDefault();
                }
                return null;
            }
        }

        public async Task<IEnumerable<AccommodationDBContract?>?> GetAccommodationsAsync(CancellationToken cancellationToken)
        {
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(@"select * from ACCOMMODATION" );
            using(Database)
            {
                var getAccommodations = await _dapperPolly.QueryAsyncWithRetry<AccommodationDBContract?>(Database, queryBuilder.ToString());

                if (getAccommodations.Any())
                {
                    return getAccommodations.ToList();
                }
                return null;
            }
        }

        public async Task<int?> GetNumberOfAccommodationsAsync(CancellationToken cancellationToken)
        {
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(@"select SUM(NumberOfGuests) from ACCOMMODATION" );
            using(Database)
            {
                var getNumberOfAccommodations = await _dapperPolly.QueryAsyncWithRetry<int?>(Database, queryBuilder.ToString());

                if (getNumberOfAccommodations.Any())
                {
                    return getNumberOfAccommodations.FirstOrDefault();
                }
                return null;
            }
        }

        public async Task<object?> UpdateAccommodationByIdAsync(AccommodationContract? contract, CancellationToken cancellationToken)
        {
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(@"UPDATE ACCOMMODATION SET ")
                                    .Append("CompanyName").Append("=").Append(contract.CompanyName == null ? "\'\'" : contract.CompanyName).Append(", ")
                                    .Append("Hotel").Append("=").Append(contract.Hotel == null ? "\'\'" : contract.Hotel).Append(", ")
                                    .Append("CheckInDate").Append("=").Append("CAST(\'").Append(contract.CheckIn.Value.ToShortDateString() == null ? "\'\'" : contract.CheckIn.Value.ToShortDateString()).Append("\' AS DATE)").Append(", ")
                                    .Append("CheckInTime").Append("=").Append("CAST(\'").Append(contract.CheckIn.Value.ToShortTimeString() == null ? "\'\'" : contract.CheckIn.Value.ToShortTimeString()).Append("\' AS TIME)").Append(", ")
                                    .Append("FirstGuest").Append("=").Append(contract.FirstGuest == null ? "\'\'" : contract.FirstGuest).Append(", ")
                                    .Append("SecondGuest").Append("=").Append(contract.SecondGuest == null ? "\'\'" : contract.SecondGuest).Append(", ")
                                    .Append("ThirdGuest").Append("=").Append(contract.ThirdGuest == null ? "\'\'" : contract.ThirdGuest).Append(", ")
                                    .Append("NumberOfGuests").Append("=").Append(contract.NumberOfGuests == null ? "\'\'" : contract.NumberOfGuests).Append(", ")
                                    .Append("GuestCompanyName").Append("=").Append(contract.GuestCompanyName == null ? "\'\'" : contract.GuestCompanyName).Append(", ")
                                    .Append("Phone").Append("=").Append(contract.Phone == null ? "\'\'" : contract.Phone).Append(", ")
                                    .Append("SNG").Append("=").Append(contract.SNG == null ? "\'\'" : contract.SNG).Append(", ")
                                    .Append("DBL").Append("=").Append(contract.DBL == null ? "\'\'" : contract.DBL).Append(", ")
                                    .Append("TRPL").Append("=").Append(contract.TRPL == null ? "\'\'" : contract.TRPL).Append(", ")
                                    .Append("QUAT").Append("=").Append(contract.QUAT == null ? "\'\'" : contract.QUAT).Append(", ")
                                    .Append("SNGCHD").Append("=").Append(contract.SNGCHD == null ? "\'\'" : contract.SNGCHD).Append(", ")
                                    .Append("DBLCHD").Append("=").Append(contract.DBLCHD == null ? "\'\'" : contract.DBLCHD).Append(", ")
                                    .Append("TRPLCHD").Append("=").Append(contract.TRPLCHD == null ? "\'\'" : contract.TRPLCHD).Append(", ")
                                    .Append("CheckOutDate").Append("=").Append("CAST(\'").Append(contract.CheckOut.Value.ToShortDateString() == null ? "\'\'" : contract.CheckOut.Value.ToShortDateString()).Append("\' AS DATE)").Append(", ")
                                    .Append("CheckOutTime").Append("=").Append("CAST(\'").Append(contract.CheckOut.Value.ToShortTimeString() == null ? "\'\'" : contract.CheckOut.Value.ToShortTimeString()).Append("\' AS TIME)").Append(", ")
                                    .Append("_SNG").Append("=").Append(contract._SNG == null ? "\'\'" : contract._SNG).Append(", ")
                                    .Append("_DBL").Append("=").Append(contract._DBL == null ? "\'\'" : contract._DBL).Append(", ")
                                    .Append("_TRPL").Append("=").Append(contract._TRPL == null ? "\'\'" : contract._TRPL).Append(", ")
                                    .Append("_QUAT").Append("=").Append(contract._QUAT == null ? "\'\'" : contract._QUAT).Append(", ")
                                    .Append("_SNGCHD").Append("=").Append(contract._SNGCHD == null ? "\'\'" : contract._SNGCHD).Append(", ")
                                    .Append("_DBLCHD").Append("=").Append(contract._DBLCHD == null ? "\'\'" : contract._DBLCHD).Append(", ")
                                    .Append("_TRPLCHD").Append("=").Append(contract._TRPLCHD == null ? "\'\'" : contract._TRPLCHD).Append(", ")
                                    .Append("Description").Append("=").Append(contract.Description == null ? "\'\'" : contract.Description).Append(" WHERE AccommodationID =").Append(contract.AccommodationID);
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

        public async Task<object?> DeleteAccommodationByIdAsync(int accommodationID, CancellationToken cancellationToken)
        {
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(@"DELETE FROM ACCOMMODATION WHERE AccommodationID = ").Append(accommodationID);
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

        public async Task<object?> CreateAccommodationAsync(AccommodationContract? contract, CancellationToken cancellationToken)
        {
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(@"INSERT INTO ACCOMMODATION (CompanyName,")
                                    .Append("Hotel").Append(",")
                                    .Append("CheckInDate").Append(",")
                                    .Append("CheckInTime").Append(",")
                                    .Append("FirstGuest").Append(",")
                                    .Append("SecondGuest").Append(",")
                                    .Append("ThirdGuest").Append(",")
                                    .Append("NumberOfGuests").Append(",")
                                    .Append("GuestCompanyName").Append(",")
                                    .Append("Phone").Append(",")
                                    .Append("SNG").Append(",")
                                    .Append("DBL").Append(",")
                                    .Append("TRPL").Append(",")
                                    .Append("QUAT").Append(",")
                                    .Append("SNGCHD").Append(",")
                                    .Append("DBLCHD").Append(",")
                                    .Append("TRPLCHD").Append(",")
                                    .Append("CheckOutDate").Append(",")
                                    .Append("CheckOutTime").Append(",")
                                    .Append("_SNG").Append(",")
                                    .Append("_DBL").Append(",")
                                    .Append("_TRPL").Append(",")
                                    .Append("_QUAT").Append(",")
                                    .Append("_SNGCHD").Append(",")
                                    .Append("_DBLCHD").Append(",")
                                    .Append("_TRPLCHD").Append(",")
                                    .Append("Description").Append(") VALUES (")
                                    .Append(contract.CompanyName == null ? "\'\'" : contract.CompanyName).Append(",")
                                    .Append(contract.Hotel == null ? "\'\'" : contract.Hotel).Append(",")
                                    .Append("CAST(\'").Append(contract.CheckOut.Value.ToShortDateString() == null ? "\'\'" : contract.CheckOut).Append("\' AS DATE)").Append(",")
                                    .Append("CAST(\'").Append(contract.CheckOut.Value.ToShortTimeString() == null ? "\'\'" : contract.CheckOut).Append("\' AS TIME)").Append(",")
                                    .Append(contract.FirstGuest == null ? "\'\'" : contract.FirstGuest).Append(",")
                                    .Append(contract.SecondGuest == null ? "\'\'" : contract.SecondGuest).Append(",")
                                    .Append(contract.ThirdGuest == null ? "\'\'" : contract.ThirdGuest).Append(",")
                                    .Append(contract.NumberOfGuests == null ? "\'\'" : contract.NumberOfGuests).Append(",")
                                    .Append(contract.GuestCompanyName == null ? "\'\'" : contract.GuestCompanyName).Append(",")
                                    .Append(contract.Phone == null ? "\'\'" : contract.Phone).Append(",")
                                    .Append(contract.SNG == null ? "\'\'" : contract.SNG).Append(",")
                                    .Append(contract.DBL == null ? "\'\'" : contract.DBL).Append(",")
                                    .Append(contract.TRPL == null ? "\'\'" : contract.TRPL).Append(",")
                                    .Append(contract.QUAT == null ? "\'\'" : contract.QUAT).Append(",")
                                    .Append(contract.SNGCHD == null ? "\'\'" : contract.SNGCHD).Append(",")
                                    .Append(contract.DBLCHD == null ? "\'\'" : contract.DBLCHD).Append(",")
                                    .Append(contract.TRPLCHD == null ? "\'\'" : contract.TRPLCHD).Append(",")
                                    .Append("CAST(\'").Append(contract.CheckOut.Value.ToShortDateString() == null ? "\'\'" : contract.CheckOut).Append("\' AS DATE)").Append(",")
                                    .Append("CAST(\'").Append(contract.CheckOut.Value.ToShortTimeString() == null ? "\'\'" : contract.CheckOut).Append("\' AS TIME)").Append(",")
                                    .Append(contract._SNG == null ? "\'\'" : contract._SNG).Append(",")
                                    .Append(contract._DBL == null ? "\'\'" : contract._DBL).Append(",")
                                    .Append(contract._TRPL == null ? "\'\'" : contract._TRPL).Append(",")
                                    .Append(contract._QUAT == null ? "\'\'" : contract._QUAT).Append(",")
                                    .Append(contract._SNGCHD == null ? "\'\'" : contract._SNGCHD).Append(",")
                                    .Append(contract._DBLCHD == null ? "\'\'" : contract._DBLCHD).Append(",")
                                    .Append(contract._TRPLCHD == null ? "\'\'" : contract._TRPLCHD).Append(",")
                                    .Append(contract.Description == null ? "\'\'" : contract.Description).Append(")");
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
    }
}