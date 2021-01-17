import { all, select } from "redux-saga/effects";
import userManagementWatcher from '../Components/UserManagement/duck/umSaga';
import masterWatcher from '../Components/shared/duck/mSaga';

const rootSaga=function* () {
    yield all([
        userManagementWatcher(),
        masterWatcher()
    ]);    
}
export default rootSaga;