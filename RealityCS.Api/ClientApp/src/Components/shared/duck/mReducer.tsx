import { combineReducers } from "redux";
import ACTIONS from "./mActionType";

interface IinitialData {
    data: any;
    loading: boolean;
    error: any;
}
const initialData = { data: null, loading: false, error: null };


const manageDepartmentReducer = (
    state = initialData,
    action: { type: String; payload: any }
): IinitialData => {
    switch (action.type) {
        case ACTIONS.FETCH_DEPARTMENT_REQUEST:
            return { ...state, loading: true, error: null };
        case ACTIONS.FETCH_DEPARTMENT_SUCCESS:
            return { ...state, data: action.payload, loading: false, error: null };
        case ACTIONS.FETCH_DEPARTMENT_FAILURE:
            return {
                ...state,
                loading: false,
                error: action.payload
            };
        default:
            return state;
    }
};

const manageDivisionReducer = (
    state = initialData,
    action: { type: String; payload: any }
): IinitialData => {
    switch (action.type) {
        case ACTIONS.FETCH_DIVISIONS_REQUEST:
            return { ...state, loading: true, error: null };
        case ACTIONS.FETCH_DIVISIONS_SUCCESS:
            return { ...state, data: action.payload, loading: false, error: null };
        case ACTIONS.FETCH_DIVISIONS_FAILURE:
            return {
                ...state,
                loading: false,
                error: action.payload
            };
        default:
            return state;
    }
};


const manageIndustryReducer = (
    state = initialData,
    action: { type: String; payload: any }
): IinitialData => {
    switch (action.type) {
        case ACTIONS.FETCH_INDUSTRY_REQUEST:
            return { ...state, loading: true, error: null };
        case ACTIONS.FETCH_INDUSTRY_SUCCESS:
            return { ...state, data: action.payload, loading: false, error: null };
        case ACTIONS.FETCH_DEPARTMENT_FAILURE:
            return {
                ...state,
                loading: false,
                error: action.payload
            };
        default:
            return state;
    }
};

const manageCountryReducer = (
    state = initialData,
    action: { type: String; payload: any }
): IinitialData => {
    switch (action.type) {
        case ACTIONS.FETCH_COUNTRY_REQUEST:
            return { ...state, loading: true, error: null };
        case ACTIONS.FETCH_COUNTRY_SUCCESS:
            return { ...state, data: action.payload, loading: false, error: null };
        case ACTIONS.FETCH_COUNTRY_FAILURE:
            return {
                ...state,
                loading: false,
                error: action.payload
            };
        default:
            return state;
    }
};

const manageStateReducer = (
    state = initialData,
    action: { type: String; payload: any }
): IinitialData => {
    switch (action.type) {
        case ACTIONS.FETCH_STATE_REQUEST:
            return { ...state, loading: true, error: null };
        case ACTIONS.FETCH_STATE_SUCCESS:
            return { ...state, data: action.payload, loading: false, error: null };
        case ACTIONS.FETCH_STATE_FAILURE:
            return {
                ...state,
                loading: false,
                error: action.payload
            };
        default:
            return state;
    }
};

const manageCityReducer = (
    state = initialData,
    action: { type: String; payload: any }
): IinitialData => {
    switch (action.type) {
        case ACTIONS.FETCH_CITY_REQUEST:
            return { ...state, loading: true, error: null };
        case ACTIONS.FETCH_CITY_SUCCESS:
            return { ...state, data: action.payload, loading: false, error: null };
        case ACTIONS.FETCH_CITY_FAILURE:
            return {
                ...state,
                loading: false,
                error: action.payload
            };
        default:
            return state;
    }
};

export default combineReducers({
    manageDepartment: manageDepartmentReducer,
    manageDivision: manageDivisionReducer,
    manageIndustry: manageIndustryReducer,
    manageCountry: manageCountryReducer,
    manageSate: manageStateReducer,
    manageCity: manageStateReducer
});
