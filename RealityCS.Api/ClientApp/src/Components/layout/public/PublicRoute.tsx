import React from "react";
import { Route, Redirect } from "react-router-dom";
import PublicLayout from "./PublicLayout";

export default function PublicRoute({
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
                    <Redirect to={{ pathname: "/", state: props.location }} />
                ) : (
                        <PublicLayout>
                            <Component {...innerProps} />
                        </PublicLayout>
                    );
            }}
        />
    );
}
