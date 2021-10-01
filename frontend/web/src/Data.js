import axios from 'axios'
import domain from './Domain';

export const createGuest = async (obj) => {
  const url = domain + "companies?CompanyName='"+
                  obj.companyName + "'&Phone='"+
                  obj.phone + "'&EMail='"+
                  obj.eMail + "'&Endorsement=0&IsEntered=true&IsGuest=true"
  const { data } = await axios.post(url);
  return data;
};

export const createPurchase = async (obj) => {
  const url = domain + "purchases?SellerId="+
                  obj.sellerId + "'&PurchaserId="+
                  obj.purchaserId + "'&Amount="+
                  obj.amount + "'&Product='"+
                  obj.product 
  const { data } = await axios.post(url);
  return data;
};

export const getIdByName = async (name) => {
  const url = domain + "companies/name?CompanyName='"+name+"'"
  const { data } = await axios.get(url);
  return data;
};
