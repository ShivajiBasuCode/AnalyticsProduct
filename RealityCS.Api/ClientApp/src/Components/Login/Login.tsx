import React, { useContext, useState } from 'react';

import Link from '@material-ui/core/Link';
import Typography from '@material-ui/core/Typography';

import Axios from '../../Config/Settings';

import LocalStorageService from '../../Config/LocalStorageService';


import logo from '../../img/logo.jpg';
import Amazonlogo from '../../img/amazon-logo.png';

function Copyright() {
    return (
        <Typography variant="body2" color="textSecondary" align="center">
            {'Copyright © '}
            <Link color="inherit" href="https://material-ui.com/">
                Your Website
        </Link>{' '}
            {new Date().getFullYear()}
            {'.'}
        </Typography>
    );
}

const Login = () => {
    const submit = async (e) => {
        e.preventDefault();
        let data = { "username": e.target.elements.username.value, "password": e.target.elements.password.value };
        let response = await Axios.post('/api/common/Account/Login', data);
        if (response.status === 200) {

            LocalStorageService.setToken(response.data.Data);
            window.location.href = '/dashboard';
        }
        else {
        }
    };

    return (
        <>
            <div className="col-md-5 p-2 login-forms" style={{ height: "100vh" }}>

                <div className="d-flex">
                    <div className="col-md-6"><img src={logo} height="50" /></div>
                    <div className="col-md-6 text-right"><img src={Amazonlogo} height="50" /></div>
                </div>

                <div className="m-auto mt-5 col-md-12 clienttabs">
                    <ul className="nav nav-pills mb-5 justify-content-evenly" id="ex1" role="tablist">
                        <li className="nav-item text-center" role="presentation">
                            <a className="nav-link m-auto active" id="ex1-tab-1" data-toggle="pill" href="#ex1-pills-1" role="tab"
                                aria-controls="ex1-pills-1" aria-selected="true">
                                <i className="fas fa-tools"></i>
                            </a>
            Configuration
          </li>
                        <li className="nav-item text-center" role="presentation">
                            <a className="nav-link m-auto" id="ex1-tab-2" data-toggle="pill" href="#ex1-pills-2" role="tab"
                                aria-controls="ex1-pills-2" aria-selected="false">
                                <i className="fas fa-tachometer-alt"></i>
                            </a>
            Visualisation
          </li>
                        <li className="nav-item text-center" role="presentation">
                            <a className="nav-link m-auto" id="ex1-tab-3" data-toggle="pill" href="#ex1-pills-3" role="tab"
                                aria-controls="ex1-pills-3" aria-selected="false">
                                <i className="fas fa-chart-line"></i> </a>
            Predicitive
          </li>
                    </ul>


                    <div className="tab-content" id="ex1-content">
                        <div className="col-md-9 m-auto text-center">
                            <h3 className="mt-5">Login</h3>
                        </div>
                        <div className="tab-pane fade show active" id="ex1-pills-1" role="tabpanel" aria-labelledby="ex1-tab-1">
                            <form className="loginforms col-md-10 mt-5 m-auto piyush" onSubmit={submit}>

                                <div className="input-group mb-3">
                                    <div className="input-group-prepend">
                                        <span className="input-group-text" id="basic-addon1"><i className="fas fa-user"></i>
                                        </span>
                                    </div>
                                    <input type="text" className="form-control" placeholder="Username" aria-label="Username"
                                        aria-describedby="basic-addon1" name="username" />
                                </div>
                                <div className="input-group mb-3">
                                    <div className="input-group-prepend">
                                        <span className="input-group-text" id="basic-addon1"><i className="fas fa-lock"></i></span>
                                    </div>
                                    <input type="password" className="form-control" placeholder="Password" aria-label="Password"
                                        aria-describedby="basic-addon1" name="password" />
                                </div>
                                <a href="">Forgot your Password?</a>

                                <div className="text-center p-5 col-md-7 m-auto">
                                    <button type="submit" className="btn btn-primary btn-lg btn-block btn-rounded m-auto" onSubmit={submit}>Login</button>
                                </div>
                            </form>
                        </div>

                        <div className="tab-pane fade" id="ex1-pills-2" role="tabpanel" aria-labelledby="ex1-tab-2">
                            <form className="loginforms col-md-10 mt-5 m-auto" onSubmit={submit}>

                                <div className="input-group mb-3">
                                    <div className="input-group-prepend">
                                        <span className="input-group-text" id="basic-addon1"><i className="fas fa-user"></i>
                                        </span>
                                    </div>
                                    <input type="text" className="form-control" placeholder="Username" aria-label="Username"
                                        aria-describedby="basic-addon1" />
                                </div>
                                <div className="input-group mb-3">
                                    <div className="input-group-prepend">
                                        <span className="input-group-text" id="basic-addon1"><i className="fas fa-lock"></i></span>
                                    </div>
                                    <input type="password" className="form-control" placeholder="Password" aria-label="Password"
                                        aria-describedby="basic-addon1" />
                                </div>
                                <a href="">Forgot your Password?</a>

                                <div className="text-center p-5 col-md-7 m-auto">
                                    <button type="submit" className="btn btn-primary btn-lg btn-block btn-rounded m-auto">Login</button>
                                </div>
                            </form>
                        </div>

                        <div className="tab-pane fade" id="ex1-pills-3" role="tabpanel" aria-labelledby="ex1-tab-3">
                            <form className="loginforms col-md-10 mt-5 m-auto" onSubmit={submit}>

                                <div className="input-group mb-3">
                                    <div className="input-group-prepend">
                                        <span className="input-group-text" id="basic-addon1"><i className="fas fa-user"></i>
                                        </span>
                                    </div>
                                    <input type="text" className="form-control" placeholder="Username" aria-label="Username"
                                        aria-describedby="basic-addon1" />
                                </div>
                                <div className="input-group mb-3">
                                    <div className="input-group-prepend">
                                        <span className="input-group-text" id="basic-addon1"><i className="fas fa-lock"></i></span>
                                    </div>
                                    <input type="password" className="form-control" placeholder="Password" aria-label="Password"
                                        aria-describedby="basic-addon1" />
                                </div>
                                <a href="">Forgot your Password?</a>

                                <div className="text-center p-5 col-md-7 m-auto">
                                    <button type="submit" className="btn btn-primary btn-lg btn-block btn-rounded m-auto">Login</button>
                                </div>
                            </form>
                        </div>

                    </div>
                </div>



                <footer className="p-2 text-center">
                    <p>Copyright © 2020, ATA Ventures, All rights reserved
        </p>
                </footer>
            </div>
        </>
    );
}
export default Login;
