import axios from "axios";
import { domain } from "../assets/domain";
import regeneratorRuntime from "regenerator-runtime";

export const companies = async () => {
  const { data } = await axios.get(domain + "all");
  return data;
};

export const purchases = async () => {
  const { data } = await axios.get(domain + "purchases/with-name");
  return data;
};

export const getGuests = async () => {
  const { data } = await axios.get(domain + "guests");
  return data;
};

export const accommodations = async () => {
  const { data } = await axios.get(domain + "accommodations");
  return data;
};
export const deleteAccommodations = async (id) => {
  const { data } = await axios.delete(domain + "accommodations/" + id);
  return data;
};
export const getAccommodationById = async (id) => {
  const result = await axios.get(domain + "accommodations/" + id);
  return result;
};
export const getAccommodationByOrderedDate = async () => {
  const { data } = await axios.get(domain + "accommodations/ordered-by-date/");
  return data;
};
export const enteredCompany = async () => {
  const { data } = await axios.get(domain + "companies/entered");
  return data;
};
/* yeni gelenler*/
/**********Balance************ */
export const getBalance = async () => {
  const { data } = await axios.get(domain + "balance");
  return data;
};
export const updateBalance = async (amount) => {
  const { data } = await axios.put(domain + "balance?balanceID=1&amount=" + amount);
  return data;
};
/**********OtelInformation************ */
export const getOtelInformation = async () => {
  const { data } = await axios.get(domain + "otel-info");
  return data;
};
export const updateOtelInformation = async (infos) => {
  console.log(infos);
  var url =
    domain +
    "otel-info?OtelInformationID=" +
    infos.OtelInformationID +
    "&" +
    "SNG=" +
    (infos.SNG === "undefined" || infos.SNG === undefined || infos.SNG === null
      ? ""
      : infos.SNG) +
    "&" +
    "DBL=" +
    (infos.DBL === "undefined" || infos.DBL === undefined || infos.DBL === null
      ? ""
      : infos.DBL) +
    "&" +
    "TRPL=" +
    (infos.TRPL === "undefined" ||
    infos.TRPL === undefined ||
    infos.TRPL === null
      ? ""
      : infos.TRPL) +
    "&" +
    "QUAT=" +
    (infos.QUAT === "undefined" ||
    infos.QUAT === undefined ||
    infos.QUAT === null
      ? ""
      : infos.QUAT) +
    "&" +
    "SNGCHD=" +
    (infos.SNGCHD === "undefined" ||
    infos.SNGCHD === undefined ||
    infos.SNGCHD === null
      ? ""
      : infos.SNGCHD) +
    "&" +
    "DBLCHD=" +
    (infos.DBLCHD === "undefined" ||
    infos.DBLCHD === undefined ||
    infos.DBLCHD === null
      ? ""
      : infos.DBLCHD) +
    "&" +
    "TRPLCHD=" +
    (infos.TRPLCHD === "undefined" ||
    infos.TRPLCHD === undefined ||
    infos.TRPLCHD === null
      ? ""
      : infos.TRPLCHD) +
    "&";
  console.log(url);
  const { data } = await axios.put(url);
  return data;
};
/**************Expenses***************** */
export const getExpenses = async () => {
  const { data } = await axios.get(domain + "expenses");
  return data;
};
export const getExpenseById = async (id) => {
  const { data } = await axios.get(domain + "expenses/" + id);
  return data;
};
export const deleteExpenseById = async (id) => {
  const { data } = await axios.delete(domain + "expenses/" + id);
  return data;
};
export const updateExpenseById = async (infos) => {
  const { data } = await axios.put(
    domain + "expenses/ " + infos.id + "?amount=" + infos.amount
  );
  return data;
};
export const createExpense = async (infos) => {
  const { data } = await axios.post(domain + "expenses?amount=" + infos.amount);
  return data;
};
/**************Cost***************** */
export const getCosts = async () => {
  const { data } = await axios.get(domain + "costs");
  return data;
};
export const getCostById = async (id) => {
  const { data } = await axios.get(domain + "costs/" + id);
  return data;
};
export const deleteCostById = async (id) => {
  const { data } = await axios.delete(domain + "costs/" + id);
  return data;
};
export const updateCostById = async (infos) => {
  const { data } = await axios.put(
    domain +
      "costs/ " +
      infos.id +
      "?costType=" +
      infos.costType +
      "&description=" +
      infos.description +
      "&costDate=" +
      infos.costDate +
      "&PAX=" +
      infos.PAX +
      "&totalCost=" +
      infos.totalCost
  );
  return data;
};
export const createCost = async (infos) => {
  const { data } = await axios.post(
    domain + "costs"+
      "?costType=" +
      infos.costType +
      "&description=" +
      infos.description +
      "&costDate=" +
      infos.costDate +
      "&PAX=" +
      infos.PAX +
      "&totalCost=" +
      infos.totalCost
  );
  return data;
};
/**************External Attendance***************** */
export const getExternalAttendances = async () => {
  const { data } = await axios.get(domain + "external-attendances");
  return data;
};
export const getExternalAttendanceById = async (id) => {
  const { data } = await axios.get(domain + "external-attendances/" + id);
  return data;
};
export const deleteExternalAttendance = async (id) => {
  const { data } = await axios.delete(domain + "external-attendances/" + id);
  return data;
};
export const updateExternalAttendance = async (infos) => {
  console.log(domain +
    "external-attendances/" +
    infos.id +
    "?" +
    "NameSurname=" +
    infos.NameSurname +
    "&" +
    "CompanyName=" +
    infos.CompanyName +
    "&" +
    "TCID=" +
    infos.TCID +
    "&" +
    "Phone=" +
    infos.Phone +
    "&" +
    "NumberOfPeople=" +
    infos.NumberOfPeople +
    "&" +
    "EntranceTime=" +
    infos.EntranceTime +
    "&" +
    "ExitTime=" +
    infos.ExitTime +
    "&" +
    "Occupancy=" +
    infos.Occupancy +
    "&" +
    "EntranceDate=" +
    infos.EntranceDate +
    "&" +
    "Description=" +
    infos.Description)
  const { data } = await axios.put(
    domain +
      "external-attendances/" +
      infos.id +
      "?" +
      "NameSurname=" +
      infos.NameSurname +
      "&" +
      "CompanyName= " +
      infos.CompanyName +
      "&" +
      "TCID=" +
      infos.TCID +
      "&" +
      "Phone=" +
      infos.Phone +
      "&" +
      "NumberOfPeople=" +
      infos.NumberOfPeople +
      "&" +
      "EntranceTime=" +
      infos.EntranceTime +
      "&" +
      "ExitTime=" +
      infos.ExitTime +
      "&" +
      "Occupancy=" +
      infos.Occupancy +
      "&" +
      "EntranceDate=" +
      infos.EntranceDate +
      "&" +
      "Description=" +
      infos.Description
  );
  return data;
};
export const createExternalAttendance = async (infos) => {
  console.log(
    domain +
      "external-attendances" +
      "?" +
      "NameSurname=" +
      infos.NameSurname +
      "&" +
      "CompanyName=" +
      infos.CompanyName +
      "&" +
      "TCID=" +
      infos.TCID +
      "&" +
      "Phone=" +
      infos.Phone +
      "&" +
      "NumberOfPeople=" +
      infos.NumberOfPeople +
      "&" +
      "EntranceTime=" +
      infos.EntranceTime +
      "&" +
      "ExitTime=" +
      infos.ExitTime +
      "&" +
      "Occupancy=" +
      infos.Occupancy +
      "&" +
      "EntranceDate=" +
      infos.EntranceDate +
      "&" +
      "Description=" +
      infos.Description
  );
  const { data } = await axios.post(
    domain +
      "external-attendances" +
      "?" +
      "NameSurname=" +
      infos.NameSurname +
      "&" +
      "CompanyName=" +
      infos.CompanyName +
      "&" +
      "TCID=" +
      infos.TCID +
      "&" +
      "Phone=" +
      infos.Phone +
      "&" +
      "NumberOfPeople=" +
      infos.NumberOfPeople +
      "&" +
      "EntranceTime=" +
      infos.EntranceTime +
      "&" +
      "ExitTime=" +
      infos.ExitTime +
      "&" +
      "Occupancy=" +
      infos.Occupancy +
      "&" +
      "EntranceDate=" +
      infos.EntranceDate +
      "&" +
      "Description=" +
      infos.Description
  );
  return data;
};
/********************* */
export const notEnteredCompany = async () => {
  const { data } = await axios.get(domain + "companies/not-entered");
  return data;
};
export const noPurchaseCount = async () => {
  const { data } = await axios.get(
    domain + "companies/entered/no-purchase/count"
  );
  return data;
};
export const noPurchaseCompany = async () => {
  const { data } = await axios.get(domain + "companies/entered/no-purchase");
  return data;
};
export const deleteCompany = async (id) => {
  const { data } = await axios.delete(domain + "companies/" + id);
  return data;
};
export const createCompany = async (
  companyName,
  phone,
  eMail,
  isEntered,
  endorsement,
  isGuest
) => {
  
  const url =
    domain +
    "companies?CompanyName='" +
    companyName +
    "'&Phone='" +
    phone +
    "'&EMail='" +
    eMail +
    "'&IsEntered=" +
    (isEntered === true ? "true" : "false") +
    "&Endorsement=" +
    endorsement +
    "&IsGuest=" +
    (isGuest === true ? "true" : "false");
  console.log(url);
  const { data } = await axios.post(url);
  return data;
};
export const createPurchases = async (params) => {
  const { data } = await axios.post(
    domain +
      "purchases?SellerId=" +
      params.sellerId +
      "&PurchaserId=" +
      params.purchaserId +
      "&Amount=" +
      params.amount +
      "&Product='" +
      params.product +
      "'"
  );
  return data;
};

export const createAccommodation = async (params) => {
  var i = 1;
  if (params.secondGuest.length > 1) i++;
  if (params.thirdGuest.length > 1) i++;
  const { data } = await axios.post(
    domain +
    "accommodations?CompanyName=" +
    params.companyName +
    "&Hotel=" +
    params.hotel +
    "&CheckIn=" +
    params.checkInDate +
    " 00:00:00" +
    "&FirstGuest=" +
    params.firstGuest +
    "&SecondGuest=" +
    params.secondGuest +
    "&ThirdGuest=" +
    params.thirdGuest +
    "&NumberOfGuests=" +
    i +
    "&GuestCompanyName=" +
    params.guestCompanyName +
    "&Phone=" +
    params.phone +
    "&SNG=" +
    params.SNG +
    "&DBL=" +
    params.DBL +
    "&TRPL=" +
    params.TRPL +
    "&QUAT=" +
    params.QUAT +
    "&SNGCHD=" +
    params.SNGCHD +
    "&DBLCHD=" +
    params.DBLCHD +
    "&TRPLCHD=" +
    params.TRPLCHD +
    "&CheckOut=" +
    params.checkOutDate +
    " 00:00:00" +
    "&SNG_=" +
    (isNaN(params._SNG) ? "0" : params._SNG)+
    "&DBL_=" +
    (isNaN(params._DBL) ? "0" : params._DBL)+
    "&TRPL_=" +
    (isNaN(params._TRPL) ? "0" : params._TRPL)+
    "&QUAT_=" +
    (isNaN(params._QUAT) ? "0" : params._QUAT)+
    "&SNG_CHD=" +
    (isNaN(params._SNGCHD) ? "0" : params._SNGCHD)+
    "&DBL_CHD=" +
    (isNaN(params._DBLCHD) ? "0" : params._DBLCHD)+
    "&TRPL_CHD=" +
    (isNaN(params._TRPLCHD) ? "0" : params._TRPLCHD)+
    "&Description=" +
    params.description  
  );
  return data;
};
export const updateAcc = async (params) => {
  var i = 1;
  if (params.secondGuest) i++;
  if (params.thirdGuest) i++;
  console.log(params);
  const { data } = await axios.put(
    domain +
      "accommodations/" +
      params.accommodationID +
      "?CompanyName='" +
      params.companyName +
      "'" +
      "&Hotel='" +
      params.hotel +
      "'" +
      "&CheckIn=" +
      params.checkInDate +
      " 00:00:00" +
      "&FirstGuest='" +
      params.firstGuest +
      "'" +
      "&SecondGuest='" +
      params.secondGuest +
      "'" +
      "&ThirdGuest='" +
      params.thirdGuest +
      "'" +
      "&NumberOfGuests=" +
      i +
      "&GuestCompanyName='" +
      params.guestCompanyName +
      "'" +
      "&Phone='" +
      params.phone +
      "'" +
      "&SNG='" +
      params.SNG +
      "'" +
      "&DBL='" +
      params.DBL +
      "'" +
      "&TRPL='" +
      params.TRPL +
      "'" +
      "&QUAT='" +
      params.QUAT +
      "'" +
      "&SNGCHD='" +
      params.SNGCHD +
      "'" +
      "&DBLCHD='" +
      params.DBLCHD +
      "'" +
      "&TRPLCHD='" +
      params.TRPLCHD +
      "'" +
      "&CheckOut=" +
      params.checkOutDate +
      " 00:00:00" +
      "&_SNG='" +
      params._SNG +
      "'" +
      "&_DBL='" +
      params._DBL +
      "'" +
      "&_TRPL='" +
      params._TRPL +
      "'" +
      "&_QUAT='" +
      params._QUAT +
      "'" +
      "&_SNGCHD='" +
      params._SNGCHD +
      "'" +
      "&_DBLCHD='" +
      params._DBLCHD +
      "'" +
      "&_TRPLCHD='" +
      params._TRPLCHD +
      "'" +
      "&Description='" +
      params.description +
      "'"
  );
  return data;
};
export const updateEndorsement = async (
  companyID,
  companyName,
  phone,
  eMail,
  endorsement,
  isEntered,
  isGuest
) => {
  const url =
    domain +
    "companies/" +
    companyID +
    "?CompanyName='" +
    companyName +
    "'&Phone='" +
    phone +
    "'&EMail='" +
    eMail +
    "'&Endorsement=" +
    endorsement +
    "&IsEntered=" +
    (isEntered === true ? "true" : "false") +
    "&IsGuest=" +
    (isGuest === true ? "true" : "false");
  console.log(url);
  const { data } = await axios.put(url);
  return data;
};

export const createGuest = async (companyName, phone, eMail, endorsement) => {
  const url =
    domain +
    "companies?CompanyName='" +
    companyName.toUpperCase() +
    "'&Phone='" +
    phone +
    "'&EMail='" +
    eMail +
    "'&Endorsement=" +
    endorsement +
    "&IsEntered=true&IsGuest=true";
  const { data } = await axios.post(url);
  return data;
};

export const getCompanyIdByName = async (name) => {
  console.log(name);
  const { data } = await axios.get(
    domain + "companies/name?CompanyName='" + name + "'"
  );

  return data;
};

export const getCompanyNameById = async (id) => {
  const { data } = await axios.get(domain + "companies/id?CompanyId=" + id);
  return data;
};

export const deletePurchase = async (id) => {
  const { data } = await axios.delete(domain + "purchases/" + id);
  return data;
};

export const updateCompany = async (params) => {
  const url =
    domain +
    "companies?CompanyID=" +
    params.companyId +
    "&CompanyName='" +
    params.companyName +
    "'&Phone='" +
    params.phone +
    "'&EMail='" +
    params.eMail +
    "'&IsEntered='" +
    params.isEntered +
    "'&Endorsement=" +
    params.endorsement +
    "&IsGuest";
  console.log(url);
  const { data } = await axios.put(url);
  return data;
};

export const getCompanyById = async (id) => {
  const url = domain + "companies/" + id;
  console.log(url);
  const { data } = await axios.get(url);
  return data;
};

export const getPurchaseById = async (id) => {
  const url = domain + "purchases/" + id;
  console.log(url);
  const { data } = await axios.get(url);
  return data;
};
export const getCompanyName = async () => {
  const url = domain + "company-names";
  const { data } = await axios.get(url);
  return data;
};
export const getCompanies = async () => {
  const url = domain + "companies";
  const { data } = await axios.get(url);
  return data;
};
