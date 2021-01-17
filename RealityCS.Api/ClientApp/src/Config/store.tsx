import {createStore,applyMiddleware} from 'redux';
import appReducerObject from './rootReducer';
import createSagaMiddleware from 'redux-saga'
import rootSaga from './rootSaga';

const sagaMiddleware = createSagaMiddleware()

const store = createStore(appReducerObject,applyMiddleware(sagaMiddleware));

sagaMiddleware.run(rootSaga);

export default store;