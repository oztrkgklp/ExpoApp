import axios from "axios";
import { domain } from "../assets/domain";
import regeneratorRuntime from "regenerator-runtime";

export const fetchWeather = async ()=>{
    const {data} = await axios.get(domain+"purchases/with-name")
    console.log(data)
    return data
}

export const purchases = async ()=>{
    const {data} = await axios.get(domain+"purchases/with-name")
    console.log(data)
    return data
}

