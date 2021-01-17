import { put, takeEvery, call, all, select, takeLatest } from 'redux-saga/effects'

import ACTIONS from './umActionType';
import Axios from '../../../Config/Settings';

function fetchUsers() {
    return Axios.get(`/api/configuration/ConfigurationUserManagement/Users`);
}

function* handleUserManagement() {
    debugger;
    try {
       // yield put({type: ACTIONS.FETCH_USER_REQUEST});
      
        let resp = yield call(fetchUsers);

        yield put({
            type: ACTIONS.FETCH_USER_SUCCESS,
            payload: resp.data.Data,
        });
    } catch (err) {
        yield put({type: ACTIONS.FETCH_USER_FAILURE, payload: err});
    }
}

export default function* userManagementWatcher() {
    yield takeLatest(ACTIONS.FETCH_USER_REQUEST, handleUserManagement);
}
