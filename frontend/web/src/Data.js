import axios from "axios";
import domain from "./Domain";

export const createGuest = async (companyName, phone, eMail) => {
  const url =
    domain +
    "companies?CompanyName='" +
    companyName +
    "'&Phone='" +
    phone +
    "'&EMail='" +
    eMail +
    "'&Endorsement=0&IsEntered=true&IsGuest=true";
  const { data } = await axios.post(url);
  return data;
};

export const createPurchase = async (
  sellerId,
  purchaserId,
  amount,
  product
) => {
  const url =
    domain +
    "purchases?SellerId=" +
    sellerId +
    "&PurchaserId=" +
    purchaserId +
    "&Amount=" +
    amount +
    "&Product='" +
    product +
    "'";
  const { data } = await axios.post(url);
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

export const getIdByName = async (name) => {
  const url = domain + "companies/name?CompanyName='" + name + "'";
  const { data } = await axios.get(url);
  return data;
};

export const getCompanyById = async (id) => {
  const url = domain + "companies/" + id;
  const { data } = await axios.get(url);
  return data;
};

export const getGuests = async () => {
  const url = domain + "guests";
  const { data } = await axios.get(url);
  return data;
};
export const getCompanies = async () => {
  const url = domain + "companies";
  const { data } = await axios.get(url);
  return data;
};