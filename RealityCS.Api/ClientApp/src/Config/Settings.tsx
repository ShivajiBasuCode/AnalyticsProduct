import Axios from 'axios';
import { Switch, Route, Redirect, withRouter } from "react-router-dom";

//import router from "./router/router";
import LocalStorageService from './LocalStorageService';

// LocalstorageService

let axios =Axios.create();
// Add a request interceptor
axios.interceptors.request.use(
   config => {       
       const token = LocalStorageService.getAccessToken();
       if (token) {
           config.headers['Authorization'] = 'Bearer ' + token;
       }
       // config.headers['Content-Type'] = 'application/json';
       return config;
   },
   error => {
       Promise.reject(error)
   });



//Add a response interceptor

axios.interceptors.response.use(r=>r,(error)=>{
   const originalRequest = error.config;
   if(error.response.status===401)
   {
       LocalStorageService.clearToken();
       window.location.href = '/login';
   }
//    debugger;
//    if (error.response.status === 401 && originalRequest.url === 'http://13.232.130.60:8081/v1/auth/token') {
//        return Promise.reject(error);
//    }
//    if (error.response.status === 401 && !originalRequest._retry) {

//        originalRequest._retry = true;
//        const refreshToken = LocalStorageService.getRefreshToken();
//        return axios.post('/auth/token',
//            {
//                "refresh_token": refreshToken
//            })
//            .then(res => {
//                if (res.status === 201) {
//                 LocalStorageService.setToken(res.data);
//                    axios.defaults.headers.common['Authorization'] = 'Bearer ' + LocalStorageService.getAccessToken();
//                    return axios(originalRequest);
//                }
//            })
//    }
   return Promise.reject(error);
});
 export default axios;