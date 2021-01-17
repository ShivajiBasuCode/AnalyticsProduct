import React, { useEffect, useState } from 'react';
import { makeStyles, Theme } from '@material-ui/core/styles';
import Paper from '@material-ui/core/Paper';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableContainer from '@material-ui/core/TableContainer';
import TableHead from '@material-ui/core/TableHead';
import TablePagination from '@material-ui/core/TablePagination';
import TableRow from '@material-ui/core/TableRow';

import LocalStorageService from '../../Config/LocalStorageService';
import { Box, Button, CircularProgress, Grid, IconButton, Link } from '@material-ui/core';
import EditIcon from '@material-ui/icons/Edit';
import ActionIcon from '../shared/ActionIcon';
import UserForm from './UserForm';
import Axios from '../../Config/Settings';
import { useSelector } from 'react-redux';

interface Column {
    id: 'Id' | 'UserName' | 'AccessGroupName' | 'IsActive' | 'Action' | 'Edit';
    label: string;
    minWidth?: number;
    align?: 'left';
    icons?: any;
    format?: (value: number) => string;
}

const columns: Column[] = [
    { id: 'Id', label: 'Order' },
    { id: 'UserName', label: 'User' },
    { id: 'AccessGroupName', label: 'Access Group Name' },
    { id: 'IsActive', label: 'Status' },
    { id: 'Action', label: 'Action' },
];

const actions: Column[] = [
    { id: 'Edit', icons: EditIcon, label: 'Edit' }
];

interface IUser {
    Id: number;
    UserName: string;
    EmailId: string;
    AccessGroupName: string;
    IsActive: boolean;
    Status: string;

}


const useStyles = makeStyles((theme: Theme) => ({
    root: {
        width: '100%',
    },
    container: {
        minWidth: 650,
    },
    progressroot: {
        display: 'flex',
        '& > * + *': {
            marginLeft: theme.spacing(2),
        },
    }
}));



export default function UsersComponent() {
    const classes = useStyles();

    const manageUserData = useSelector(({ manageUser }: any) => manageUser);
    const [page, setPage] = React.useState(0);
    const [rowsPerPage, setRowsPerPage] = React.useState(10);
    // const [data, setData] = React.useState({ Data: [] });
    // const [loading, setLoading] = React.useState(false);
    const [view, setView] = React.useState<{ data: any }>({ data: {} })

    console.log("manageUserData", manageUserData);

    let { data, loading, error } = manageUserData;




    useEffect(() => {
        // setLoading(true);

        // const fetchData = async () => {
        //     const result = await Axios.get(`/api/configuration/ConfigurationUserManagement/Users`)
        //     setData(result.data.Data);
        //     setLoading(false);
        // };

        // fetchData();

    }, []);

    if (loading) {
        return <CircularProgress />;
    }
    if (!data) {
        return null;
    }
    const handleAction = (type, id) => {

        debugger;
        switch (type) {
            case 'Edit':
                setView({ data: { type, id } });
                break;

            default:
                break;
        }
    };

    const handleChangePage = (event: unknown, newPage: number) => {
        setPage(newPage);
    };

    const handleChangeRowsPerPage = (event: React.ChangeEvent<HTMLInputElement>) => {
        setRowsPerPage(+event.target.value);
        setPage(0);
    };
    let { data: viewData } = view;

    if (viewData.type === 'Edit') {
        return (<UserForm id={viewData.id} />);
    }
    const CreateUser = () => {
        window.location.href = "/user";
    }

    return (
        <> 
            <div className="content-main whiteBg rounded m-4 p-4">
                <div className="row align-items-end">
                    <div className="col">
                        <h3><span>User Access Management :</span> Users</h3>
                    </div>
                    <div className="col text-right mr-2">
                        <button className="btn btn-link p-0 " onClick={CreateUser}>Add New<i
                            className="fas fa-plus ml-2"></i>
                        </button>
                    </div>
                </div>

                <table id="dtVerticalScrollExample" className="table table-striped table-sm mt-3" cellSpacing="0" width="100%">
                    <thead>
                        <tr>
                            {columns.map((column) => (
                                <th className="th-sm">{column.label}
                                </th>
                            ))}
                        </tr>
                    </thead>
                    <tbody>
                        {data.slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage).map((row, index) => {
                            return (<tr>
                                {columns.map((column) => {
                                    if (column.id === 'Action') {
                                        const { Id } = row;
                                        const icons = actions.map(({ icons, label, id: type }, index) => {

                                            return <ActionIcon
                                                key={index}
                                                component={icons}
                                                label={label}
                                                onClick={() => handleAction(type, Id)}
                                            />
                                        });
                                        return (
                                            <td key={column.id} align={column.align}>
                                                {icons}
                                            </td>
                                        );
                                    }
                                    else {
                                        const value = row[column.id];
                                        return (
                                            <td key={column.id} align={column.align}>
                                                {column.format && typeof value === 'number' ? column.format(value) : typeof value === 'boolean' ? (value === true ? "Active" : "Deactive") : value}
                                            </td>
                                        );
                                    }
                                })};
                            </tr>
                            );

                        })}
                        {loading && <Box m={2}><CircularProgress size={25} /> </Box>}
                    </tbody>

                </table>
                <TablePagination
                    rowsPerPageOptions={[10, 25, 50]}
                    component="div"
                    count={data.length}
                    rowsPerPage={rowsPerPage}
                    page={page}
                    onChangePage={handleChangePage}
                    onChangeRowsPerPage={handleChangeRowsPerPage}
                />
            </div>

        </>
    );
}
