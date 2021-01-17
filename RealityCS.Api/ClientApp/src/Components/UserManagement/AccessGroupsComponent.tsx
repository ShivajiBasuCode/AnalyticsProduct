import React, { useEffect } from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Paper from '@material-ui/core/Paper';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableContainer from '@material-ui/core/TableContainer';
import TableHead from '@material-ui/core/TableHead';
import TablePagination from '@material-ui/core/TablePagination';
import TableRow from '@material-ui/core/TableRow';
import { Box, Button, CircularProgress } from '@material-ui/core';
import LocalStorageService from '../../Config/LocalStorageService';
import EditIcon from '@material-ui/icons/Edit';
import ActionIcon from '../shared/ActionIcon';
import AccessGroupForm from './AccessGroupForm';
import Axios from '../../Config/Settings';

interface Column {
    id: 'Id' | 'Name' | 'Description' | 'IsActive' | 'Action' | 'Edit';
    icons?: any;
    label: string;
    minWidth?: number;
    align?: 'left';
    format?: (value: number) => string;

}

const columns: Column[] = [
    { id: 'Id', label: 'Order' },
    { id: 'Name', label: 'Group Name' },
    { id: 'Description', label: 'Description' },
    { id: 'IsActive', label: 'Status' },
    { id: 'Action', label: 'Action' },
];

const actions: Column[] = [
    { id: 'Edit', icons: EditIcon, label: 'Edit' }
];

interface Data {
    Id: number;
    name: string;
    description: string;
    IsActive: boolean;
    Status: string;
}




const useStyles = makeStyles({
    root: {
        width: '100%',
    },
    container: {
        minWidth: 650,
    },
});


export default function AccessGroupsComponent() {
    const classes = useStyles();
    const [page, setPage] = React.useState(0);
    const [rowsPerPage, setRowsPerPage] = React.useState(10);
    const [data, setData] = React.useState({ Data: [] });
    const [loading, setLoading] = React.useState(false);
    const [view, setView] = React.useState<{ data: any }>({ data: {} });
    let content: any;
    useEffect(() => {
        setLoading(true);

        const fetchData = async () => {
            const result = await Axios.get(`/api/configuration/ConfigurationUserManagement/AccessGroups`)
            setData(result.data);

            setLoading(false);
        };


        fetchData();



    }, []);

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
    const CreateAccessGroup = () => {
        window.location.href = "\CreateAccessGroup";
    }
    let { data: viewData } = view;
    if (!viewData.type) {

        content = (
            <>
                <div className="content-main  rounded m-3 p-3">
                    <div className="row align-items-end">
                        <div className="col">
                            <h3><span>User Access Management :</span> Access Group</h3>
                        </div>
                        <div className="col text-right mr-2">
                            <button className="btn btn-link p-0 " onClick={CreateAccessGroup}>Add New<i
                                className="fas fa-plus ml-2"></i></button></div>
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
                            {data.Data.slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage).map((row, index) => {
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
                        </tbody>

                    </table>
                    <TablePagination
                        rowsPerPageOptions={[10, 25, 50]}
                        component="div"
                        count={data.Data.length}
                        rowsPerPage={rowsPerPage}
                        page={page}
                        onChangePage={handleChangePage}
                        onChangeRowsPerPage={handleChangeRowsPerPage}
                    />
                </div>
            </>
        );
    }
    else if (viewData.type === 'Edit') {
        content = (<AccessGroupForm id={viewData.id} />);
    }
    // return (
    //     <div>
    //     </div>
    // );
    return (
        <>
            {content}
        </>
    )
}
