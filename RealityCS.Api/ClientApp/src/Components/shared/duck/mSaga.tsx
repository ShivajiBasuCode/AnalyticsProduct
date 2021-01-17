import { put, takeEvery, call, all, select, takeLatest } from 'redux-saga/effects'

import ACTIONS from './mActionType';
import Axios from '../../../Config/Settings';
import API from '../../../Config/apiUrls';

function fetchDepartments() {
    return Axios.get(API.GET_DEPARTMENT);
}
function fetchDivisions() {
    return Axios.get(API.GET_DIVISIONS);
}
function fetchIndustries() {
    return Axios.get(API.GET_INDUSTRIES);
}
function fetchCountries() {
    return Axios.get(API.GET_COUNTRY);
}
function fetchStates() {
    return Axios.get(API.GET_STATE);
}
function fetchCities() {
    return Axios.get(API.GET_CITY);
}


function* handleDepartments() {
    debugger;
    try {
       // yield put({type: ACTIONS.FETCH_USER_REQUEST});
      
        let resp = yield call(fetchDepartments);

        yield put({
            type: ACTIONS.FETCH_DEPARTMENT_SUCCESS,
            payload: resp.data.Data,
        });
    } catch (err) {
        yield put({type: ACTIONS.FETCH_DEPARTMENT_FAILURE, payload: err});
    }
}


function* handleIndustries() {
    debugger;
    try {
       // yield put({type: ACTIONS.FETCH_USER_REQUEST});
      
        let resp = yield call(fetchIndustries);

        yield put({
            type: ACTIONS.FETCH_INDUSTRY_SUCCESS,
            payload: resp.data.Data,
        });
    } catch (err) {
        yield put({type: ACTIONS.FETCH_INDUSTRY_FAILURE, payload: err});
    }
}


function* handleDivisions() {
    debugger;
    try {
       // yield put({type: ACTIONS.FETCH_USER_REQUEST});
      
        let resp = yield call(fetchDivisions);

        yield put({
            type: ACTIONS.FETCH_DIVISIONS_SUCCESS,
            payload: resp.data.Data,
        });
    } catch (err) {
        yield put({type: ACTIONS.FETCH_DIVISIONS_FAILURE, payload: err});
    }
}

function* handleCountries() {
    debugger;
    try {
        // yield put({type: ACTIONS.FETCH_USER_REQUEST});

        let resp = yield call(fetchCountries);

        yield put({
            type: ACTIONS.FETCH_COUNTRY_SUCCESS,
            payload: resp.data.Data,
        });
    } catch (err) {
        yield put({ type: ACTIONS.FETCH_COUNTRY_FAILURE, payload: err });
    }
}

function* handleStates() {
    debugger;
    try {
        // yield put({type: ACTIONS.FETCH_USER_REQUEST});

        let resp = yield call(fetchCountries);

        yield put({
            type: ACTIONS.FETCH_STATE_SUCCESS,
            payload: resp.data.Data,
        });
    } catch (err) {
        yield put({ type: ACTIONS.FETCH_STATE_FAILURE, payload: err });
    }
}

function* handleCities() {
    debugger;
    try {
        // yield put({type: ACTIONS.FETCH_USER_REQUEST});

        let resp = yield call(fetchCountries);

        yield put({
            type: ACTIONS.FETCH_CITY_SUCCESS,
            payload: resp.data.Data,
        });
    } catch (err) {
        yield put({ type: ACTIONS.FETCH_CITY_FAILURE, payload: err });
    }
}


export default function* masterDataWatcher() {
    yield takeLatest(ACTIONS.FETCH_DIVISIONS_REQUEST, handleDivisions);
    yield takeLatest(ACTIONS.FETCH_INDUSTRY_REQUEST, handleIndustries);
    yield takeLatest(ACTIONS.FETCH_DEPARTMENT_REQUEST, handleDepartments);
    yield takeLatest(ACTIONS.FETCH_COUNTRY_REQUEST, handleCountries);
    yield takeLatest(ACTIONS.FETCH_STATE_REQUEST, handleStates);
    yield takeLatest(ACTIONS.FETCH_CITY_REQUEST, handleCities);
}
