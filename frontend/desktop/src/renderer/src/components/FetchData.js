import axios from "axios";
import { domain } from "../assets/domain";
import regeneratorRuntime from "regenerator-runtime";

export const companies = async () => {
  const { data } = await axios.get(domain + "companies");
  return data;
};

export const purchases = async () => {
  const { data } = await axios.get(domain + "purchases/with-name");
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
export const createCompany = async (params) => {
  const url =
    domain +
    "companies?CompanyName='" +
    params.companyName +
    "'&Phone='" +
    params.phone +
    "'&EMail='" +
    params.eMail +
    "'&IsEntered='" +
    params.isEntered +
    "'&Endorsement=" +
    params.endorsement;
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
      params.amount
  );
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
    params.endorsement;
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
