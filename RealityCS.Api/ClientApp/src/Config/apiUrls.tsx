export const version = "v1.0.0";

let SERVER_URL = "/api/";

let COMMON_SERVER_URL = SERVER_URL + 'common/';
const API = {
    LOGIN_API: SERVER_URL + 'dfvdg',
    FETCH_DATASOURCE_NAME: SERVER_URL + 'configuration/ConfigurationLegalEntity/GetDataSourceName',
    FETCH_DATASOURCE: SERVER_URL + 'configuration/ConfigurationLegalEntity/GetDataSource',
    FETCH_DATASOURCE_ELEMENTS: SERVER_URL + 'configuration/ConfigurationLegalEntity/GetDataSourceElements',
    SAVE_DATASOURCE_ELEMENTS_ALIASNAME: SERVER_URL + 'configuration/ConfigurationLegalEntity/PostDataSourceElementsAliasName',
    ADD_KPI: SERVER_URL + 'configuration/ConfigurationKPIElements/AddKpi',
    GET_INDUSTRIES: COMMON_SERVER_URL + 'Master/Industries',
    GET_DEPARTMENT: COMMON_SERVER_URL + 'Master/Departments',
    GET_DIVISIONS: COMMON_SERVER_URL + 'Master/Divisions',
    GET_COUNTRY: COMMON_SERVER_URL + 'CountryCityState/Countries',
    GET_STATE: COMMON_SERVER_URL + 'CountryCityState/States',
    GET_CITY: COMMON_SERVER_URL + 'CountryCityState/Cities'
}
export default API;