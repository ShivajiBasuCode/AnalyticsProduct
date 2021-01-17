import ACTIONS from './umActionType';

function fetchUserRequest(params?: any) {
    return {
        type: ACTIONS.FETCH_USER_REQUEST, payload: { params }
    };
}
export { fetchUserRequest };
