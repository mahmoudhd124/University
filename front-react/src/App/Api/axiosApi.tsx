import axios from "axios";

export const BASE_URL = 'https://localhost:7035/api/'
export const axiosApi = axios.create({
    baseURL: BASE_URL.slice(0,-1),
    headers: {
        "Content-Type": "application/json",
    },
    withCredentials:true
});
