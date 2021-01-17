import React, { useState } from 'react';
import Avatar from '@material-ui/core/Avatar';
import Button from '@material-ui/core/Button';
import CssBaseline from '@material-ui/core/CssBaseline';
import TextField from '@material-ui/core/TextField';
import FormControlLabel from '@material-ui/core/FormControlLabel';
import Checkbox from '@material-ui/core/Checkbox';
import Link from '@material-ui/core/Link';
import Grid from '@material-ui/core/Grid';
import Box from '@material-ui/core/Box';
import LockOutlinedIcon from '@material-ui/icons/LockOutlined';
import Typography from '@material-ui/core/Typography';
import { makeStyles } from '@material-ui/core/styles';
import Container from '@material-ui/core/Container';
import { AppBar, Tab } from '@material-ui/core';
import Axios from '../../Config/Settings';
import LocalStorageService from '../../Config/LocalStorageService';
import BottomSnackbar from '../shared/BottomSnackBar'

const validEmailRegex = RegExp(/^(([^<>()\[\]\.,;:\s@\"]+(\.[^<>()\[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/i);
const validateForm = (errors) => {
    let valid = true;
    Object.values(errors).forEach(
        (val: any) => val.length > 0 && (valid = false)
    );
    return valid;
}

const useStyles = makeStyles((theme) => ({
    paper: {
        marginTop: theme.spacing(2),
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'center',
    },
    avatar: {
        margin: theme.spacing(1),
        backgroundColor: theme.palette.secondary.main,
    },
    form: {
        width: '100%', // Fix IE 11 issue.
        marginTop: theme.spacing(3),
    },
    submit: {
        margin: theme.spacing(3, 0, 2),
    },
}));

export interface IManageUser {
    userName: string;
    emailId: string;
    password: string;
    accessGroupId: number;
}

const InitialState = {
    username: null,
    email: null,
    password: null,
    errors: {
        username: '',
        email: '',
        password: '',
        errorusernameflag: false,
        erroremailflag: false,
        errorpasswordflag: false
    }
};
export default function ManageUserComponent() {
    const classes = useStyles();

    const [loading, setLoading] = React.useState(false);
    const [successSnack,setSuccessSnack]=React.useState(false);
    const [errorSnack,setErrorSnack]=React.useState(false);
    const[message,setMessage]=React.useState("");
    const [state, setState] = useState(InitialState);

    const handleChange = (event) => {
        event.preventDefault();
        const { name, value } = event.target;
        console.log("init  ", state)
        let updtedState = state;

        switch (name) {
            case 'username':
                updtedState.username =  value;
                updtedState.errors.username =
                    value.length < 5
                        ? 'User Name must be 5 characters long!'
                        : '';
                updtedState.errors.errorusernameflag = !updtedState.errors.username ? false : true;
                break;
            case 'email':
                updtedState.email =  value;
                updtedState.errors.email =
                    value.length == 0 ? 'Email is required!' : (

                        validEmailRegex.test(value)
                            ? ''
                            : 'Email is not valid!');
                updtedState.errors.erroremailflag = !updtedState.errors.email ? false : true;
                break;
            case 'password':
                updtedState.password =  value;
                updtedState.errors.password =
                    value.length < 8
                        ? 'Password must be 8 characters long!'
                        : '';
                updtedState.errors.errorpasswordflag = !updtedState.errors.password ? false : true;
                break;
            default:
                break;
        }

        setState(updtedState);
        console.log("State", state);
    }
    const getFormData = (e) => {
       // let data:IManageUser;
        let data ={"userName":e.target.elements.username.value,
        "password":e.target.elements.password.value,"EmailId":e.target.elements.email.value};
        return data;
    }
    const handleSubmit = async (e) => {

        e.preventDefault();
        if (validateForm(state.errors)) {
            const response = await Axios.post(`/api/configuration/ConfigurationUserManagement/User`,getFormData(e));

            if (response.status === 200) {
                setLoading(false);
                setSuccessSnack(true);
                setMessage("User added successfully !");
              
                window.location.href = '/usermanagement';
            }
            else {
                setLoading(false);
            setErrorSnack(true);
            setMessage("Some error occured !");
            }
        } else {
            console.error('Invalid Form')
        }
    }
    const handleClose=()=>{
        setSuccessSnack(false);
        setErrorSnack(false);
        window.location.reload(true);
    }
    return (
        <Container component="main" maxWidth="md">
            <AppBar position="static">
                <Tab label="User" />
            </AppBar>
            <div className={classes.paper} >
                <form className={classes.form} noValidate autoComplete="off" onSubmit={handleSubmit}>
                    <Grid container spacing={6}>
                        <Grid item xs={12}>
                            <TextField

                                error={state.errors.errorusernameflag}
                                helperText={state.errors.username}
                                autoComplete="off"
                                name="username"
                                variant="outlined"
                                required
                                fullWidth
                                id="username"
                                label="User Name"

                                onChange={handleChange}

                            />
                        </Grid>


                        <Grid item xs={12}>
                            <TextField

                                error={state.errors.erroremailflag}
                                helperText={state.errors.email}
                                variant="outlined"
                                required
                                fullWidth
                                id="email"
                                label="Email Address"
                                name="email"
                                autoComplete="off"
                                onChange={handleChange}
                            />
                        </Grid>
                        <Grid item xs={12}>
                            <TextField

                                error={state.errors.errorpasswordflag}
                                helperText={state.errors.password}
                                variant="outlined"
                                required
                                fullWidth
                                name="password"
                                label="Password"
                                type="password"
                                id="password"
                                autoComplete="off"
                                onChange={handleChange}

                            />
                        </Grid>

                    </Grid>
                    <Button
                        type="submit"
                        variant="contained"
                        color="primary"
                        className={classes.submit}
                    >
                        Save
          </Button>

                </form>
                {successSnack && (<BottomSnackbar type="success" handleClose={handleClose} message={message} open={successSnack} />)}
            {errorSnack && (<BottomSnackbar type="error" handleClose={handleClose} message={message} open={errorSnack} />)}

            </div>

        </Container>
    );
}
