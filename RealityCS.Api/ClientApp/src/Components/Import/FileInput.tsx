import React, { useState } from 'react';
export default function FileInput({id,handleFile,file})
{
    const [fileName,setFileName]=useState("");
    const fileChange=(e)=>
    {
        setFileName(e.target.files[0].name);
        handleFile(e.target.files[0]);
    };
    let inputId="file "+id;
    return (
    <label className="file form-control">
                  <input type="file" accept=".csv" id={inputId} aria-label="File browser example" onChange={fileChange} />
                  <span className="file-custom"></span>
                </label>
    );
}