import React, { useState, useEffect } from 'react';
import Controls from '../shared/controls/Controls';
import { UseForm, Form } from '../shared/UseForm';
import Paper from '@material-ui/core/Paper';
import Axios from '../../Config/Settings';
import LocalStorageService from '../../Config/LocalStorageService';

import BottomSnackbar from '../shared/BottomSnackBar'
import CircularProgress from '@material-ui/core/CircularProgress';
import SaveButtonProgress from '../shared/SaveButtonProgress';



const initialValues = {
    id: 0,
    UserName: '',
    EmailId: '',
    Password: '',
    AccessGroupId: null,
}


export default function UserForm({ id }: { id?: any }) {
    const [accessGroup, setAccessGroup] = useState([]);
    const [successSnack, setSuccessSnack] = React.useState(false);
    const [errorSnack, setErrorSnack] = React.useState(false);
    const [message, setMessage] = React.useState("");
    const [loading, setLoading] = React.useState(false);
    const [data, setData] = React.useState(initialValues);

    const validate = (fieldValues = values) => {
        debugger;
        let temp = { ...errors }
        if ('UserName' in fieldValues)
            temp.UserName = fieldValues.UserName ? "" : "User name is required."
        if ('EmailId' in fieldValues)
            temp.EmailId = fieldValues.EmailId ? (temp.EmailId = (/$^|.+@.+..+/).test(fieldValues.EmailId) ? "" : "Email is not valid!") : "Email is required!"
        if ('Password' in fieldValues && !id)
            temp.Password = fieldValues.Password ? (temp.Password = fieldValues.Password.length >= 8 ? "" : "Password must be 8 characters long!") : "Password is required!"
        if ('AccessGroupId' in fieldValues)
            temp.AccessGroupId = fieldValues.AccessGroupId >= 0 ? "" : "Access Group is required."

        setErrors({
            ...temp
        })

        if (fieldValues == values)
            return Object.values(temp).every(x => x == "")
    }

    const {
        values,
        setValues,
        errors,
        setErrors,
        handleInputChange,
        resetForm
    } = UseForm(data, true, validate);


    const fetchData = async () => {

        const result = await Axios.get(`/api/configuration/ConfigurationUserManagement/AccessGroups`)
        if (result.data.StatusCode === 200) {
            setAccessGroup(result.data.Data);

        }
        else {
            setErrorSnack(true);
            setMessage("Some error occured !");
        }
    };

    const userData = async (id) => {

        const result = await Axios.get(`/api/configuration/ConfigurationUserManagement/GetUserDetails?id=` + id)
        if (result.data.StatusCode === 200) {
            let { UserName, EmailId, AccessGroupId } = result.data.Data;
            setData(
                {
                    ...data,
                    id: id,
                    UserName: UserName,
                    AccessGroupId: AccessGroupId,
                    EmailId: EmailId
                });
            setValues({
                ...values,
                id: id,
                UserName: UserName,
                AccessGroupId: AccessGroupId,
                EmailId: EmailId
            });
        }
        else {
            setErrorSnack(true);
            setMessage("Some error occured !");
        }

    };
    useEffect(() => {
        // setLoading(true);
        fetchData();
        debugger;
        if (id) {
            userData(id);
        }
    }, []);




    const handleSubmit = async e => {

        e.preventDefault()
        if (validate()) {
            setLoading(true);

            const response = await Axios.post(`/api/configuration/ConfigurationUserManagement/User`, values);

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
            //resetForm()
        }
    }
    const handleUpdate = async e => {

        e.preventDefault()
        if (validate()) {
            setLoading(true);
            const response = await Axios.post(`/api/configuration/ConfigurationUserManagement/UpdateUser`, values);

            if (response.status === 200) {
                setLoading(false);
                setSuccessSnack(true);
                setMessage("User Updated successfully !");

                window.location.href = '/usermanagement';
            }
            else {
                setLoading(false);
                setErrorSnack(true);
                setMessage("Some error occured !");
            }
            //resetForm()
        }
    }

    console.log("Values", values);
    const handleClose = () => {
        setSuccessSnack(false);
        setErrorSnack(false);
        window.location.reload(true);
    }
    let label = 'Create User';
    if (id) {
        label = 'Edit User';
    }

    return (
        <div className="content-main rounded m-3 p-3">
            <h3>{label}</h3>
            <small className="heading-note"><span className="error">*</span>Required field</small>

            <Form className="mt-3" noValidate autoComplete="off" onSubmit={id ? handleUpdate : handleSubmit}>
                <div className="row mb-1">
                    <Controls.Input
                        name="UserName"
                        label="Full Name"
                        value={values.UserName}
                        onChange={handleInputChange}
                        error={errors.UserName}
                        className="form col-md-6"
                    />

                    <Controls.Select
                        name="AccessGroupId"
                        label="Access Group"
                        value={values.AccessGroupId}
                        onChange={handleInputChange}
                        options={accessGroup}
                        error={errors.AccessGroupId}
                        className="form col-md-6"
                    />
                </div>
                <div className="row mb-1">
                    <Controls.Input
                        label="Email"
                        name="EmailId"
                        value={values.EmailId}
                        onChange={handleInputChange}
                        error={errors.EmailId}
                        className="form col-md-6"
                    />
                </div>
                <div className="row mb-1">

                    {!id &&
                        <Controls.Input
                            label="Password"
                            name="Password"
                            value={values.Password}
                            onChange={handleInputChange}
                            error={errors.Password}
                            className="form col-md-6"
                        />
                    }
                </div>
                <div className="text-center p-5 col-md-12 m-auto text-right">
                    {loading && <SaveButtonProgress />}
                    {data.id === 0 && !loading &&
                        <button type="submit" className="btn btn-rounded float-right"
                        >Save</button>}


                    {data.id !== 0 && !loading &&
                        <button type="submit"
                            className="btn btn-rounded float-right"
                        >Update</button>
                    }
                </div>



            </Form>
            {successSnack && (<BottomSnackbar type="success" handleClose={handleClose} message={message} open={successSnack} />)}
            {errorSnack && (<BottomSnackbar type="error" handleClose={handleClose} message={message} open={errorSnack} />)}

        </div>
    )
}
