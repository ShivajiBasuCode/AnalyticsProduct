import React from 'react';
import logo from './logo.svg';
import './App.css';

import {Provider} from 'react-redux';
import store from './Config/store';
import { BrowserRouter, Link, Switch, Route } from 'react-router-dom';
import ReactDOM from 'react-dom'
import Layout from '../src/Components/layout/Layout';
function App() {
    return (
        <Provider store={store}>
            <BrowserRouter>
                <div className="App">
                    <Layout />
                </div>
            </BrowserRouter>
        </Provider>
    );
}



export default App;
