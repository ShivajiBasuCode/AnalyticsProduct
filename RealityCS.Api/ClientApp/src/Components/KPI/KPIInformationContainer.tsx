import React, { useState } from 'react';
import FormDialog from '../shared/FormDialog';
import API from '../../Config/apiUrls';
import CircularProgress from '@material-ui/core/CircularProgress';
import { useSelector } from 'react-redux';

const intialDialogData = {
    open: false,
    closeText: "",
    saveText: "",
    closeAction: ()=>{},
    saveAction: ()=>{},
    title: ""
}

function SelectDropDown({ selectData, onChange, title, value, name, onClickDialog }) {
    console.log("Sop", selectData);
    let { data, loading, error } = selectData;
    // console.log("Sop",selectData);
    let optionsItem: any = [];
    if (data) {
        optionsItem.push(<option value="">Select</option>)
        data.map((item) => { optionsItem.push(<option key={item.Id} value={item.Id}>{item.Name}</option>) }
        )

    }
    if (loading) {
        return <CircularProgress />;
    }
    return <>
        <label className="form-label" htmlFor="formControl">{title}
            <span className="error">*</span></label>
        <div className="plusDiv">
            <select className="select form-control" data-filter="true"
                aria-label={title}
                name={name}
                value={value}
                onChange={(e) => onChange(e, name)}>
                {
                    optionsItem
                }
            </select>
        </div>
        <button type="button" className="plusIcon" onClick={() => onClickDialog("Item")}><i className="flaticon-more-1"></i></button>
    </>
}


export default function KPIInformationContainer() {
    const [isCreate, setIsCreate] = useState(false);
    const [dialogData, setDialogData] = useState(intialDialogData);
    const masterData = useSelector(({ master }: any) => master);
    let { manageDepartment, manageDivision, manageIndustry } = masterData;
    console.log("Master", masterData);

    const onClickDialog = (type) => {
        setDialogData({
           // ...dialogData,
            open: true,
            saveText: "Save",
            closeText: "close",
            title: "Industry",
            saveAction: toggleCreate,
            closeAction: toggleCreate
        });
    }

    const toggleCreate = () => {
        setIsCreate(!isCreate);
    }
    let { open, saveAction, closeAction, closeText, saveText, title } = dialogData;

    const onChange = () => { };
    console.log("Open", open);

    return <div className="content-main whiteBg rounded m-4 p-4 h-80">
        <h3>KPI Elements - Information</h3>
        <small className="heading-note"><span className="error">*</span>Required field</small>

        <form className="mt-3">
            <div className="row">
                <div className="col-md-4">
                    <label className="form-label" htmlFor="formControl">Name<span className="error">*</span></label>
                    {isCreate ?
                        <input type="text" id="formControl" className="form-control" placeholder="Enter KPI Name" />
                        :
                        <select className="select form-control" data-filter="true">
                            <option>Sajal De</option>
                            <option value="1">ATA year 2019-2020 data</option>
                            <option value="2">ATA year 2018-2019 data</option>
                            <option value="3">ATA year 2017-2018 data</option>
                        </select>
                    }
                </div>

                <div className="col-md-1 pl-0 mt-5">
                    <button type="button" className="plusIcon mt-4 ml-1" onClick={toggleCreate}><i className="flaticon-more-1"></i></button>
                </div>
            </div>

            <div className="row">
                <div className="col-md-4">
                    <label className="form-label" htmlFor="formControl">Description</label>
                    <input type="text" id="formControl" className="form-control" placeholder="Enter Description" />
                </div>
                <div className="col-md-4">
                    <label className="form-label" htmlFor="formControl">Objective  <span className="error">*</span></label>
                    <input type="text" id="formControl" className="form-control" placeholder="Enter KPI Objective " />
                </div>
                <div className="col-md-4">
                    <SelectDropDown name="Industry" selectData={manageIndustry}
                        title="Industry" value="0" onChange={onChange}
                        onClickDialog={onClickDialog} />
                </div>
            </div>

            <div className="row">
                <div className="col-md-4">
                    <SelectDropDown name="Department"
                        selectData={manageDepartment} title="Department" value="0"
                        onChange={onChange}
                        onClickDialog={onClickDialog} />


                </div>

                <div className="col-md-4">
                    <SelectDropDown name="Division" selectData={manageDivision}
                        title="Division" value="0" onChange={onChange}
                        onClickDialog={onClickDialog} />

                </div>

                <div className="col-md-4">
                    <label className="form-label" htmlFor="formControl">Value Stream  <span className="error">*</span></label>
                    <div className="plusDiv">
                        <select className="select form-control" data-filter="true">
                            <option>- - - Select Value Stream - - -</option>
                            <option value="1">ATA year 2019-2020 data</option>
                            <option value="2">ATA year 2018-2019 data</option>
                            <option value="3">ATA year 2017-2018 data</option>
                        </select>
                    </div>
                    <button type="button" className="plusIcon" onClick={() => onClickDialog("ValueStream")}><i className="flaticon-more-1"></i></button>
                </div>
            </div>


            <div className="text-center p-5 col-md-12 m-auto text-right">
                <button type="button" className="btn btn-rounded float-right">Save</button>
            </div>
        </form>
        <FormDialog
            open={open}
            closetext={closeText}
            savetext={saveText}
            closeaction={closeAction}
            close={closeAction}
            saveaction={saveAction}
            title={title}
        >
        </FormDialog>
    </div>
}
