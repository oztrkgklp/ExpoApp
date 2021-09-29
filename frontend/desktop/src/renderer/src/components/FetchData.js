import axios from "axios";
import { domain } from "../assets/domain";
import regeneratorRuntime from "regenerator-runtime";

export const companies = async ()=>{
    const {data} = await axios.get(domain+"companies")
    return data
}

export const purchases = async ()=>{
    const {data} = await axios.get(domain+"purchases/with-name")
    return data
}

export const enteredCompany = async ()=>{
    const {data} = await axios.get(domain+"companies/entered")
    return data
}

export const notEnteredCompany = async ()=>{
    const {data} = await axios.get(domain+"companies/not-entered")
    return data
}

