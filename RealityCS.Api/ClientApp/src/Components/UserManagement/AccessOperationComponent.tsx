import React, { useEffect } from 'react';
import { makeStyles } from '@material-ui/core/styles';
import TablePagination from '@material-ui/core/TablePagination';
import Axios from '../../Config/Settings';

interface Column {
    id: string;
    label: string;
    minWidth?: number;
    align?: 'left';
    format?: (value: number) => string;
}

const columns: Column[] = [
    { id: 'No', label: 'No.' },
    { id: 'DomainName', label: 'Domain Name' },
    { id: 'EntityGroupName', label: 'Entity Group Name' },
    { id: 'EntityName', label: 'Entity Name' },
    { id: 'Name', label: 'Access Operation Name' },
    { id: 'Description', label: 'Description' },
    { id: 'IsActive', label: 'Status' },
];

interface Data {
    Id: number;
    Name: string;
    Description: string;
    IsActive: string;
    DomainName: string;
    EntityGroupName: string;
    EntityName: string;
}




const useStyles = makeStyles({
    root: {
        width: '100%',
    },
    container: {
        minWidth: 650,
    },
});


export default function AccessOperationComponent() {
    const classes = useStyles();
    const [page, setPage] = React.useState(0);
    const [rowsPerPage, setRowsPerPage] = React.useState(10);
    const [data, setData] = React.useState({ Data: [] });
    const [loading, setLoading] = React.useState(false);

    useEffect(() => {
        setLoading(true);

        const fetchData = async () => {
            const result = await Axios.get(`/api/configuration/ConfigurationUserManagement/AccessOperation`)
            setData(result.data);
            setLoading(false);
        };
        fetchData();
    }, []);

    const handleChangePage = (event: unknown, newPage: number) => {
        setPage(newPage);
    };

    const handleChangeRowsPerPage = (event: React.ChangeEvent<HTMLInputElement>) => {
        setRowsPerPage(+event.target.value);
        setPage(0);
    };

    return (<>
        <div className="content-main  rounded m-3 p-3">

            <div className="row align-items-end">
                <div className="col">
                    <h3><span>User Access Management :</span> Access Operation</h3>
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
                    {data.Data.slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage).map((row, index) => {
                        debugger;
                        return (
                            <tr>
                                {columns.map((column) => {
                                    const value = column.id === 'No' ? ++index : row[column.id];
                                    return (
                                        <td key={column.id} align={column.align}>
                                            {column.format && typeof value === 'number' ? column.format(value) : value}
                                        </td>
                                    );
                                })}
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
