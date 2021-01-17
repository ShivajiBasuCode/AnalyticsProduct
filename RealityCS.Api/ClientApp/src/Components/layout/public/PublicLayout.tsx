import React from 'react';

import PublicHeader from './PublicHeader';
import MainImage from '../../../img/main-image.png';
import logo from '../../../img/logo.jpg';
import Amazonlogo from '../../../img/amazon-logo.png';



export default function PublicLayout(props) {
  const [open, setOpen] = React.useState(true);
  const handleDrawerOpen = () => {
    setOpen(true);
  };
  const handleDrawerClose = () => {
    setOpen(false);
  };
    return (
        <>
          <div className="container-fluid p-0">
    <div className="d-lg-flex align-items-center">
      <div className="col-md-7 login pt-5">
        <div className="col-md-12 pt-4"></div>
        <div className="align-items-center">
          <div className="loginimage text-center d-flex align-items-center justify-content-center">
            <img src={MainImage} width="430" />
          </div>
        </div>

        <div className="flex-column text-center">
          <h2>Welcome to the world of Realitycs</h2>
          <p>Get meaningful results with essential tools for goal setting and resulting</p>
        </div>

      </div>

      {props.children}


    </div>
  </div>
         
        </>
    )
}