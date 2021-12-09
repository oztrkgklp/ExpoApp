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
  const { data } = await axios.delete(domain + "accommodations/"+id);
  return data;
};

export const enteredCompany = async () => {
  const { data } = await axios.get(domain + "companies/entered");
  return data;
};

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
export const createCompany = async (companyName,phone,eMail,isEntered,endorsement,isGuest) => {
  console.log(typeof(isEntered))
  console.log(typeof(isGuest))
  console.log(isGuest)
  console.log(isEntered)
  const url =
    domain +
    "companies?CompanyName=\'" +
    companyName +
    "'&Phone='" +
    phone +
    "'&EMail='" +
    eMail +
    "'&IsEntered=" +
    (isEntered === true ?"true":"false") +
    "&Endorsement=" +
    endorsement +
    "&IsGuest="+
    (isGuest === true ? "true":"false");
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
      params.amount+
      "&Product='"+
      params.product+
      "'"
  );
  return data;
};

export const createAccommodation = async (params) => {
    var i = 1
    if(params.secondGuest.length>1)
      i++
    if(params.thirdGuest.length>1)
      i++
    
  const { data } = await axios.post(
    domain +
      "accommodations?CompanyName='" +
      params.companyName + "'"+
      "&Hotel='" +
      params.hotel + "'" +
      "&CheckIn=" +
      params.checkInDate+ " 00:00:00" + 
      "&FirstGuest='"+
      params.firstGuest+ "'"+
      "&SecondGuest='"+
      params.secondGuest+ "'"+
      "&ThirdGuest='"+
      params.thirdGuest+ "'"+
      "&NumberOfGuests="+
      i+
      "&GuestCompanyName='"+
      params.guestCompanyName+ "'"+
      "&Phone='"+
      params.phone+ "'"+
      "&SNG='"+
      params.SNG+ "'"+
      "&DBL='"+
      params.DBL+ "'"+
      "&TRPL='"+
      params.TRPL+ "'"+
      "&QUAT='"+
      params.QUAT+ "'"+
      "&SNGCHD='"+
      params.SNGCHD+ "'"+
      "&DBLCHD='"+
      params.DBLCHD+ "'"+
      "&TRPLCHD='"+
      params.TRPLCHD+ "'"+
      "&CheckOut=" +
      params.checkOutDate+ " 00:00:00" +
      "&SNG_='"+
      params._SNG+ "'"+
      "&DBL_='"+
      params._DBL+ "'"+
      "&TRPL_='"+
      params._TRPL+ "'"+
      "&QUAT_='"+
      params._QUAT+ "'"+
      "&SNG_CHD='"+
      params._SNGCHD+ "'"+
      "&DBL_CHD='"+
      params._DBLCHD+ "'"+
      "&TRPL_CHD='"+
      params._TRPLCHD+ "'"+
      "&Description='" +
      params.description+"'"

      
  );
  return data;
};
export const updateAcc = async (params) => {
  var [key, value] = Object.entries(params)[0];
  console.log(key)
  console.log(value)

  var i = 1
  if(params.secondGuest)
    i++
  if(params.thirdGuest)
    i++
  
const { data } = await axios.put(
  domain +
    "accommodations/"+ key + "?CompanyName='" +
    value.companyName.value + "'"+
    "&Hotel='" +
    value.hotel.value + "'" +
    "&CheckIn=" +
    value.checkInDate.value+ " 00:00:00" + 
    "&FirstGuest='"+
    value.firstGuest.value+ "'"+
    "&SecondGuest='"+
    value.secondGuest.value+ "'"+
    "&ThirdGuest='"+
    value.thirdGuest.value+ "'"+
    "&NumberOfGuests="+
    i+
    "&GuestCompanyName='"+
    value.guestCompanyName.value+ "'"+
    "&Phone='"+
    value.phone.value+ "'"+
    "&SNG='"+
    value.SNG.value+ "'"+
    "&DBL='"+
    value.DBL.value+ "'"+
    "&TRPL='"+
    value.TRPL.value+ "'"+
    "&QUAT='"+
    value.QUAT.value+ "'"+
    "&SNGCHD='"+
    value.SNGCHD.value+ "'"+
    "&DBLCHD='"+
    value.DBLCHD.value+ "'"+
    "&TRPLCHD='"+
    value.TRPLCHD.value+ "'"+
    "&CheckOut=" +
    value.checkOutDate.value+ " 00:00:00" +
    "&SNG_='"+
    value._SNG.value+ "'"+
    "&DBL_='"+
    value._DBL.value+ "'"+
    "&TRPL_='"+
    value._TRPL.value+ "'"+
    "&QUAT_='"+
    value._QUAT.value+ "'"+
    "&SNG_CHD='"+
    value._SNGCHD.value+ "'"+
    "&DBL_CHD='"+
    value._DBLCHD.value+ "'"+
    "&TRPL_CHD='"+
    value._TRPLCHD.value+ "'"+
    "&Description='" +
    value.description.value+"'"

    
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
    console.log(url)
  const { data } = await axios.put(url);
  return data;
};

export const createGuest = async (companyName, phone, eMail,endorsement) => {
  
  const url =
    domain +
    "companies?CompanyName='" +
    companyName.toUpperCase() +
    "'&Phone='" +
    phone +
    "'&EMail='" +
    eMail +
    "'&Endorsement=" + endorsement+"&IsEntered=true&IsGuest=true";
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
    "&IsGuest"
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