import React from 'react'

export default function AliasRow({index,data,onChange})
{
    let {Name,AliasName,Id}=data;
    return <tr>
              <td>{index+1}</td>
              <td>{Name}</td>
              <td>              
                  <input type="text" id="formControl" className="form-control"
                   defaultValue={AliasName} style={{width:"50%"}}
                onBlur={(e) => onChange(index, e, "AliasName")} />
              </td>
            </tr>
    
    ;
}