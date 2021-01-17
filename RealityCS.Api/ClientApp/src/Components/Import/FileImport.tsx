import React, { useEffect } from 'react';
import Row from './Row';
import SaveButtonProgress from '../shared/SaveButtonProgress';
import Axios from '../../Config/Settings';
import BottomSnackbar from '../shared/BottomSnackBar'

interface IRowData {
    rowId: number;
    dataSource: string;
    dataSourceType: string;
    file: any;
}
interface IImportData {
    dataSourceName: string;
    item: Array<IRowData>
}
export default function FileImport() {
    const [value, setValue] = React.useState(0);
    const [saveData, setSaveData] = React.useState<IImportData>({
        dataSourceName: "",
        item: [
            { rowId: 1, dataSource: "", dataSourceType: "", file: null }
        ]
    });
    const [loading, setLoading] = React.useState(false);
    const [successSnack, setSuccessSnack] = React.useState(false);
    const [errorSnack, setErrorSnack] = React.useState(false);
    const [message, setMessage] = React.useState("");
    let content: any = [];

    const handleChange = (rowId, event, name) => {
        if (rowId == 0) {
            setSaveData({ ...saveData, [name]: event.target.value });
        }
        else {
            if (name === "file") {
                saveData.item.forEach((x) => {
                    if (x.rowId === rowId) {
                        x.file = event;
                    }
                }
                )
            }
            else {
                saveData.item.forEach((x) => {
                    if (x.rowId === rowId) {
                        x[name] = event.target.value;
                    }
                }
                );
            }
            setSaveData({ ...saveData });
            // setValue(newValue);
        }
    };
    const getFormData = () => {
        debugger;
        let form_data = new FormData();
        for (var key in saveData) {
            if (key === "item") {
                var index = 0;
                for (var pair of saveData[key]) {
                    for (var elements in pair) {
                        form_data.append("item[" + index + "]." + elements, pair[elements]);
                    }
                    index++;
                }
            }
            else {
                form_data.append(key, saveData[key]);
            }
        }
        return form_data;
    }

    useEffect(() => {
    }, [])
    const save = async () => {
        setLoading(true);
        console.log("SAVE", saveData);
        let response = await Axios.post('/api/configuration/ConfigurationLegalEntity/ImportData', getFormData());
        if (response.status === 200) {
            setLoading(false);
            setSuccessSnack(true);
            setMessage("Data imported successfully !");

        }
        else {
            setLoading(false);
            setErrorSnack(true);
            setMessage("Some error occured !");
        }
    };


    const handleClose = () => {
        setSuccessSnack(false);
        setErrorSnack(false);
        window.location.reload(true);
    }

    const AddRow = (prevRowId) => {
        debugger;
        saveData.item.push(
            { rowId: prevRowId + 1, dataSource: "", dataSourceType: "", file: null }
        );
        setSaveData({ ...saveData });
    };

    const DeleteRow = (prevRowId) => {
        debugger;
        saveData.item.splice(prevRowId - 1, 1);
        setSaveData({ ...saveData });
    };


    let { item } = saveData;

    content = item.map((data, index) => {
        if (index == item.length - 1 && item.length != 1) {
            return (
                <Row rowId={data.rowId} onChange={handleChange} data={data} onAdd={AddRow} onDelete={DeleteRow} />
            )
        }
        else if (index == item.length - 1) {
            return (
                <Row rowId={data.rowId} onChange={handleChange} data={data} onAdd={AddRow} />
            )
        }
        else {
            return (
                <Row rowId={data.rowId} onChange={handleChange} data={data} onDelete={DeleteRow} />
            )
        }
    });
    return (
        <>
      <div className="content-main h-80 rounded m-3 p-3">

        <h3>Data Import</h3>
        <small className="heading-note"><span className="error">*</span>Required field</small>
        <form className="mt-3">
          <div className="row mb-1">
            <div className="col-md-4 row align-items-end">
            <div className="form col">
              <label className="form-label" htmlFor="formControl">Data Source Name<span className="error">*</span>
              <input type="text" id="formControl" className="form-control" onChange={(e) => handleChange(0, e, "dataSourceName")} />
              </label>

              </div>
            </div>

          </div>


        {content}
        



          <div className="text-center p-5 col-md-12 m-auto text-right">
          {loading ? <SaveButtonProgress />: <button type="button" className="btn btn-rounded float-right"  onClick={save}>Save</button>}
          </div>
          {successSnack && (<BottomSnackbar type="success" handleClose={handleClose} message={message} open={successSnack} />)}
            {errorSnack && (<BottomSnackbar type="error" handleClose={handleClose} message={message} open={errorSnack} />)}

        </form>
        </div>
       </>
    );
}
