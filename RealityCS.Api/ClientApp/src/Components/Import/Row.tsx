import React from 'react';

import FileInput from './FileInput';

export default function Row({ rowId, onChange, data, onAdd, onDelete }:{rowId,onChange,data,onAdd?,onDelete?}) {
    const handleFile = (file) => {
        onChange(rowId, file, "file");
    };
    let {
        dataSource,
        file
    } = data;
    return (
          <div className="row mb-1" style={{width:"100% !important"}}>
              <div className="col-md-1">
              <div className="form" style={{paddingTop:"25px",paddingLeft:"30px"}}>
                {rowId}

              </div>
                            </div>
            <div className="col-md-2">
              <div className="form">
              <label className="form-label" htmlFor="formControl">Data Source <span className="error">*</span>
              <input type="text" id="formControl" className="form-control" value={dataSource} onChange={(e) => onChange(rowId, e, "dataSource")} />
              </label>

              </div>
            </div>
            <div className="col-md-4">
              <div className="form">
              <label className="form-label" htmlFor={rowId}>
                Select File(.csv)
              <FileInput id={rowId} handleFile={handleFile} file={file} />
              </label>
              </div>
            </div>
            <div className="col-md-3">
            {onAdd && <a className="btn btn-outline-dark btn-floating" style={{marginTop:"25px"}} onClick={() => onAdd(rowId)} role="button">
                <i className="fas fa-plus"></i>
                </a>
                        }
            {onDelete && <a className="btn btn-outline-dark btn-floating" style={{marginTop:"25px"}} onClick={() => onDelete(rowId)} role="button">
                <i className="fas fa-minus"></i>
                </a>
                          
            }
            </div>

          </div>
    )
}
