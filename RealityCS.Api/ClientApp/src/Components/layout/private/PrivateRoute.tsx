import React from "react";
import { Route, Redirect } from "react-router-dom";
import PrivateLayout from "./PrivateLayout";

export default function PrivateRoute({
    component: Component,
    isUserAuthenticated ,
    ...props
}) {
    // const { from } = props.location.state || { from: { pathname: "/dashboard" } };
    return (
        <Route
            {...props}
            render={innerProps => {
                // there was /dashboard. Removed it, now which page will be open by default will be decided in Root.tsx file.
                return isUserAuthenticated ? (
                    <PrivateLayout>
                        <Component {...innerProps} />
                    </PrivateLayout>
                ): (
                        <Redirect to={{ pathname: "/login", state: props.location }} />
                    );
            }}
        />
    );
}
