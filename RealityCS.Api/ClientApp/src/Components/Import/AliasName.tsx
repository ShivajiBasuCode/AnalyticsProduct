import React, { useEffect } from 'react';

import AliasRow from './AliasRow';
import CircularProgress from '@material-ui/core/CircularProgress';
import Axios from '../../Config/Settings';
import BottomSnackbar from '../shared/BottomSnackBar';
import API from '../../Config/apiUrls';
import SaveButtonProgress from '../shared/SaveButtonProgress';
function SelectDropDown({selectData,onChange,title,value,name})
{
    let { data,loading,error } = selectData;
    let optionsItem:any=[];
    if(data)
    {
        optionsItem.push(<option value="">Select</option>)
        data.map((item)=>
            { optionsItem.push(<option key={item.Id} value={item.Id}>{item.Value}</option>)}
        )

    }
    if(loading)
    {
        return <CircularProgress />;
    }
    return <>
    <label className="form-label" htmlFor="formControl">{title}
    <span className="error">*</span></label>
               
    <select className="select form-control" data-filter="true" 
         aria-label={title}
            name={name}
            value={value}
            onChange={(e)=>onChange(e,name)}>
            {
                optionsItem
            }
    </select>
    </>
}

const IntialData={
    data:null as any,
    loading:false,
    error:null
}
export default function AliasName() {
    const [dataSourceNameValue, setDataSourceNameValue] = React.useState();
    const [dataSourceValue, setDataSourceValue] = React.useState();
   
    const [loading, setLoading] = React.useState(false);
    const [successSnack, setSuccessSnack] = React.useState(false);
    const [errorSnack, setErrorSnack] = React.useState(false);
    const [message, setMessage] = React.useState("");

    const [dataSourceName,setDataSourceName]=React.useState(IntialData);
    const [dataSource,setDataSource]=React.useState(IntialData);
    const [dataSourceElements,setDataSourceElements]=React.useState(IntialData);

    let content: any = [];

    const fetchDataSourceName = async () => {
        setDataSourceName({...dataSourceName,loading:true});
        const result = await Axios.get(API.FETCH_DATASOURCE_NAME)
        let {StatusCode,Error,Data}=result.data;
        if(StatusCode===200)
        {
            setDataSourceName({loading:false,data:Data,error:null});
        }
        else
        {
            setDataSourceName({loading:false,data:null,error:Error});
        }
    };
    const fetchDataSource = async (ParentId) => {
        setDataSource({...dataSource,loading:true});
        let params={id:ParentId};
        const result = await Axios.get(API.FETCH_DATASOURCE,{params})
        let {StatusCode,Error,Data}=result.data;
        if(StatusCode===200)
        {
            setDataSource({loading:false,data:Data,error:null});
        }
        else
        {
            setDataSource({loading:false,data:null,error:Error});
        }
    };

    const fetchDataSourceElements = async (Id) => {
        setDataSourceElements({...dataSourceElements,loading:true});
        let params={id:Id};
        const result = await Axios.get(API.FETCH_DATASOURCE_ELEMENTS,{params})
        let {StatusCode,Error,Data}=result.data;
        if(StatusCode===200)
        {
            setDataSourceElements({loading:false,data:Data,error:null});
        }
        else
        {
            setDataSourceElements({loading:false,data:null,error:Error});
        }
    };


    const handleChange = (rowId, event, name) => {
        let newValue=event.target.value;
        dataSourceElements.data.forEach((x,index) => {
            if (index === rowId) {
                x.AliasName = newValue;
            }
        })
        setDataSourceElements({
            ...dataSourceElements
        }
        );
    };

    useEffect(() => {
        fetchDataSourceName();
    }, [])

    const getSaveData=()=>{
        return {
            Id:dataSourceValue,
            Elements:dataSourceElements.data
        } 
    }
    const save = async () => {
        setLoading(true);
        let response = await Axios.post(API.SAVE_DATASOURCE_ELEMENTS_ALIASNAME,getSaveData());
        let {StatusCode,Data}=response.data
        if (StatusCode === 200) {
            setLoading(false);
            setSuccessSnack(true);
            setMessage(Data.Message);

        }
        else {
            setLoading(false);
            setErrorSnack(true);
            setMessage("Some error occured !");
        }
    };

    const onChange=(e,type)=>{
        debugger;
        if(type==="DataSourceName")
        {
            setDataSourceNameValue(e.target.value);
            setDataSource(IntialData);
            setDataSourceElements(IntialData);
            fetchDataSource(e.target.value);
        }
        else if(type==="DataSource")
        {
            setDataSourceValue(e.target.value);
            setDataSourceElements(IntialData);
            fetchDataSourceElements(e.target.value);
        }
    };
   
    const handleClose = () => {
        setSuccessSnack(false);
        setErrorSnack(false);
    }

    let { data:eData,loading:eLoading,error:eError } = dataSourceElements;

    console.log("Edata",eData);

    if(eData)
    {
        content = eData.map((data, index) => {
                return (
                    <AliasRow index={index} onChange={handleChange} data={data}  />
                )
        });
    }
    else
    {
        content=<tr><td></td><td>Please select Data Source</td><td></td></tr>;
    }

    return (
        <>
        <div className="content-main rounded m-3 p-3">
        <h3>Customer Data Mapper</h3>
        <small className="heading-note"><span className="error">*</span>Required field</small>

        <form className="mt-3">
          <div className="row mb-1">
            <div className="col-md-4 row align-items-end">
              <div className="form col">
              <SelectDropDown name="DataSourceName" onChange={onChange} 
                    title="Data Source Name"
                    selectData={dataSourceName}
                    value={dataSourceNameValue}
                    />
              </div>
            </div>


            <div className="col-md-4 align-items-end">
              <div className="form col">
              <SelectDropDown name="DataSource" onChange={onChange} 
                    title="Data Source"
                    selectData={dataSource}
                    value={dataSourceValue}
                    />
              </div>
            </div>
          </div>
        </form>

      </div>


      <div className="content-main  rounded m-3 p-3">
        <h3><span>Customer Data :</span> Sales</h3>
        <table id="dtVerticalScrollExample" className="table table-striped table-sm mt-3" cellSpacing="0" width="100%">
          <thead>
            <tr>
              <th className="th-sm">SL.No
              </th>
              <th className="th-sm">Names
              </th>
              <th className="th-sm">Alias Names
              </th>
            </tr>
          </thead>
          <tbody>
           {content}
          </tbody>

        </table>

        <div className="text-center p-5 col-md-12 m-auto text-right">
        {loading ? <SaveButtonProgress />: 
        <button type="button" className="btn btn-rounded float-right"  onClick={save}>Save</button>}
        </div>

      </div>
    {successSnack && (<BottomSnackbar type="success" handleClose={handleClose} message={message} open={successSnack} />)}
     {errorSnack && (<BottomSnackbar type="error" handleClose={handleClose} message={message} open={errorSnack} />)}
</>
    
    );
}
