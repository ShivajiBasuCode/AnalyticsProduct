import React, { useState } from 'react';
import PublicRoute from './public/PublicRoute';
import PrivateRoute from './private/PrivateRoute';

import FileImport from '../Import/FileImport';
import UsersComponent from '../UserManagement/UsersComponent';
import Dashboard from '../Dashboard/Dasboard';


import { Switch, Route, Redirect, withRouter } from "react-router-dom";
import Login from '../Login/Login';

import LocalStorageService from '../../Config/LocalStorageService';
import ManageUserComponent from '../UserManagement/ManageUserComponent';
import UserForm from '../UserManagement/UserForm';
import AccessGroupForm from '../UserManagement/AccessGroupForm'
import AliasName from '../Import/AliasName';
import AccessOperationComponent from '../UserManagement/AccessOperationComponent'
import AccessGroupsComponent from '../UserManagement/AccessGroupsComponent'
import KPIInformationContainer from '../KPI/KPIInformationContainer';

function Layout() {
    const isUserAuthenticated = LocalStorageService.getAccessToken();
    return (
        <>
            <Switch>
                <PublicRoute
                    isUserAuthenticated={isUserAuthenticated}
                    path="/login"
                    exact
                    component={Login}
                />

                <PrivateRoute
                    isUserAuthenticated={isUserAuthenticated}
                    path="/dashboard"
                    exact
                    component={Dashboard}
                />

                <PrivateRoute
                    isUserAuthenticated={isUserAuthenticated}
                    path="/import"
                    exact
                    component={FileImport}
                />
                <PrivateRoute
                    isUserAuthenticated={isUserAuthenticated}
                    path="/data-mapper"
                    exact
                    component={AliasName}
                />
                <PrivateRoute
                    isUserAuthenticated={isUserAuthenticated}
                    path="/usermanagement"
                    exact
                    component={UsersComponent}
                />

                <PrivateRoute
                    isUserAuthenticated={isUserAuthenticated}
                    path="/ManageUser"
                    exact
                    component={ManageUserComponent}
                />


                <PrivateRoute
                    isUserAuthenticated={isUserAuthenticated}
                    path="/user"
                    exact
                    component={UserForm}
                />


                <PrivateRoute
                    isUserAuthenticated={isUserAuthenticated}
                    path="/accessgroup"
                    exact
                    component={AccessGroupsComponent}
                />
                <PrivateRoute
                    isUserAuthenticated={isUserAuthenticated}
                    path="/CreateAccessGroup"
                    exact
                    component={AccessGroupForm}
                />
                <PrivateRoute
                    isUserAuthenticated={isUserAuthenticated}
                    path="/accessoperations"
                    exact
                    component={AccessOperationComponent}
                />

                <PrivateRoute
                    isUserAuthenticated={isUserAuthenticated}
                    path="/KPI-Information"
                    exact
                    component={KPIInformationContainer}
                />

                {!isUserAuthenticated && <Redirect from="/" to="login" />}
                {isUserAuthenticated && <Redirect from="/" to="dashboard" />}

            </Switch>
        </>
    )
}

export default Layout;