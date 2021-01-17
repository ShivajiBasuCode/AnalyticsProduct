import React, { useState, useEffect } from 'react';
import { Box, AppBar, Button, Container, Grid, makeStyles, Tab } from '@material-ui/core';
import Controls from '../shared/controls/Controls';
import { UseForm, Form } from '../shared/UseForm';
import Paper from '@material-ui/core/Paper';
import Axios from '../../Config/Settings';

import BottomSnackbar from '../shared/BottomSnackBar'
import CircularProgress from '@material-ui/core/CircularProgress';
import SaveButtonProgress from '../shared/SaveButtonProgress';



const ITEM_HEIGHT = 48;
const ITEM_PADDING_TOP = 8;
const MenuProps = {
    PaperProps: {
        style: {
            maxHeight: ITEM_HEIGHT * 4.5 + ITEM_PADDING_TOP,
            width: 250,
        },
    },
};

const initialValues = {
    id: 0,
    Name: '',
    Description: '',
    Operations: []
}


export default function AccessGroupForm({ id }: { id?: any }) {
    const [opetrations, setOpetrations] = useState([]);
    const [successSnack, setSuccessSnack] = React.useState(false);
    const [errorSnack, setErrorSnack] = React.useState(false);
    const [message, setMessage] = React.useState("");
    const [loading, setLoading] = React.useState(false);
    const [data, setData] = React.useState(initialValues);

    const [operationName, setOperationName] = React.useState<string[]>([]);


    const multiSelectHandleChange = (event) => {
        const { name, value } = event.target

        if (name === 'Operations') {
            setValues({
                ...values,
                Operations: opetrations.reduce((ids, operation) => {
                    if (value.includes(operation['Name'])) {
                        ids.push(operation['Id']);
                    }
                    return ids;
                }, [])
            });

            setOperationName(event.target.value as string[]);
        }
        else
            setValues({
                ...values,
                [name]: value
            })

    };

    const validate = (fieldValues = values) => {

        let temp = { ...errors }
        if ('Name' in fieldValues)
            temp.Name = fieldValues.Name ? "" : "Name is required!";
        if ('Description' in fieldValues)
            temp.Description = fieldValues.Description ? "" : "Description is required!";
        if ('Operations' in fieldValues)
            temp.Operations = fieldValues.Operations.length > 0 ? "" : "Operation is required!";



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
    } = UseForm(data, false, validate);


    const fetchData = async () => {

        const result = await Axios.get(`/api/configuration/ConfigurationUserManagement/AccessOperation`)

        if (result.data.StatusCode === 200) {
            setOpetrations(result.data.Data);

        }
        else {
            setErrorSnack(true);
            setMessage("Some error occured !");
        }
    };

    const accessGroupData = async (id) => {

        const result = await Axios.get(`/api/configuration/ConfigurationUserManagement/GetAccessGroup?id=` + id)

        if (result.data.StatusCode === 200) {
            let { Name, Description, AccessGroupId, operations } = result.data.Data;
            setData(
                {
                    ...data,
                    id: id,
                    Name: Name,
                    Description: Description,
                    Operations: operations.map(op => op.Id)
                });
            setValues({
                ...values,
                id: id,
                Name: Name,
                Description: Description,
                Operations: operations.map(op => op.Id)
            });
            setOperationName(operations.map(op => op.Name));

        }
        else {
            setErrorSnack(true);
            setMessage("Some error occured !");
        }

    };
    useEffect(() => {
        // setLoading(true);
        fetchData();
        if (id) {
            accessGroupData(id);
        }

    }, []);


    const handleUpdateSubmit = async e => {
        debugger
        e.preventDefault()
        if (validate()) {
            setLoading(true);


            const response = await Axios.post(`/api/configuration/ConfigurationUserManagement/UpdateAccessGroup`, values);

            if (response.status === 200) {
                setLoading(false);
                setSuccessSnack(true);
                setMessage("Access Group added successfully !");

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

    const handleSubmit = async e => {
        debugger;
        e.preventDefault()
        if (validate()) {
            setLoading(true);


            const response = await Axios.post(`/api/configuration/ConfigurationUserManagement/AccessGroup`, values);

            if (response.status === 200) {
                setLoading(false);
                setSuccessSnack(true);
                setMessage("Access Group added successfully !");

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

    const handleClose = () => {
        setSuccessSnack(false);
        setErrorSnack(false);
        window.location.reload(true);
    }
    let label = 'Create Access Group';
    if (id) {
        label = 'Edit Access Group';
    }

    return (
        <div className="content-main rounded m-3 p-3">
            <h3>{label}</h3>
            <small className="heading-note"><span className="error">*</span>Required field</small>

            <Form className="mt-3" noValidate autoComplete="off" onSubmit={id ? handleUpdateSubmit : handleSubmit}>

                <div className="row mb-1">
                    <Controls.Input
                        name="Name"
                        label="Access Group"
                        value={values.Name}
                        onChange={handleInputChange}
                        error={errors.Name}
                        className="form col-md-6"
                    />
                    <Controls.MultiSelect
                        label="Operations"
                        name="Operations"
                        value={operationName}
                        onChange={multiSelectHandleChange}
                        error={errors.Operations}
                        inputProps={{ maxLength: 300 }}
                        required={true}
                        items={opetrations}
                        className="form col-md-6"
                    />
                </div>
                <div className="row mb-1">
                    <Controls.Input
                        label="Description"
                        name="Description"
                        value={values.Description}
                        onChange={handleInputChange}
                        error={errors.Description}
                        inputProps={{ maxLength: 300 }}
                        MenuProps={MenuProps}
                        className="form col-md-12"

                    />
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
