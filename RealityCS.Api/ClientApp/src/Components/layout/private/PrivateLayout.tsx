import React, { useEffect, useState } from 'react';
import { useDispatch, useSelector } from "react-redux";
import PrivateHeader from './PrivateHeader';
import LeftNavComponent from '../../nav/LeftNavComponent';
import { fetchUserRequest } from '../../UserManagement/duck/umActionCreator';
import { fetchDepartmentRequest, fetchDivisionsRequest, fetchIndustryRequest, fetchCountryRequest } from '../../shared/duck/mActionCreator';

import navigation from '../../nav/navigationList';

const drawerWidth = 240;

export default function PrivateLayout(props) {
    const dispatch = useDispatch();
    const [topMenuId, setTopMenuId] = useState(0);
    const [leftMenuId, setLeftMenuId] = useState(0);

    useEffect(() => {
        dispatch(fetchUserRequest());
        dispatch(fetchDepartmentRequest());
        dispatch(fetchDivisionsRequest());
        dispatch(fetchIndustryRequest());
        dispatch(fetchCountryRequest());

    }, [topMenuId]);

    let sideMenuList: any = [];
    const navbarTopOnChange = (id) => {
        let leftMenuId = 0;
        if (id !== 0)
            leftMenuId = 1;
        navigation.forEach((data, index) => {
            data.Child.forEach((childItem, cIndex) => {
                if (cIndex === leftMenuId && index === id) {
                    window.location.href = childItem.Link;
                }
            })
        })
    };

    useEffect(() => {
        navigation.forEach((data, index) => {
            data.Child.forEach((childItem, cIndex) => {
                if (childItem.Link === window.location.pathname && window.location.pathname !== "/dashboard") {
                    setTopMenuId(index);
                    setLeftMenuId(cIndex);
                }
            })
        })
    }, []);

    return (
        <>
            <div className="container-fluid d-flex p-0">
                <div className="sidenav col-md-1" data-mode="side" data-content="#content">
                    <LeftNavComponent activeTopMenuId={topMenuId} leftMenuId={leftMenuId} />
                </div>

                <div id="content" className="col-md-11">

                    <PrivateHeader navbarTopOnChange={navbarTopOnChange} />


                    {props.children}


                    <footer className="footer pl-3">
                        <p>Copyright © 2020, ATA Ventures, All rights reserved</p>
                    </footer>


                </div>

            </div>
            {/* <div className={classes.root}>
            <PrivateHeader />
            <main className={classes.content}>
             <div className={classes.appBarSpacer} />
              <Container maxWidth="lg" className={classes.container}>
         
                {props.children}
                 </Container>
              </main>  
          </div>       */}
        </>
    )
}