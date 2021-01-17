import ACTIONS from './mActionType';

function fetchDepartmentRequest(params?: any) {
    return {
        type: ACTIONS.FETCH_DEPARTMENT_REQUEST, payload: { params }
    };
}

function fetchDivisionsRequest(params?: any) {
    return {
        type: ACTIONS.FETCH_DIVISIONS_REQUEST, payload: { params }
    };
}


function fetchIndustryRequest(params?: any) {
    return {
        type: ACTIONS.FETCH_INDUSTRY_REQUEST, payload: { params }
    };
}

function fetchCountryRequest(params?: any) {
    return {
        type: ACTIONS.FETCH_COUNTRY_REQUEST, payload: { params }
    };
}

function fetchStateRequest(params?: any) {
    return {
        type: ACTIONS.FETCH_STATE_REQUEST, payload: { params }
    };
}

function fetchCityRequest(params?: any) {
    return {
        type: ACTIONS.FETCH_CITY_REQUEST, payload: { params }
    };
}

export { fetchDepartmentRequest, fetchDivisionsRequest, fetchIndustryRequest, fetchCountryRequest, fetchStateRequest, fetchCityRequest};
