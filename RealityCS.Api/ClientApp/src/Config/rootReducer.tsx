import { combineReducers } from 'redux';
import manageUserReducer from '../Components/UserManagement/duck/umReducer';
import masterReducer from '../Components/shared/duck/mReducer';
const appReducerObject = combineReducers(
    {
        manageUser: manageUserReducer,
        master: masterReducer
    }
);
export default appReducerObject;