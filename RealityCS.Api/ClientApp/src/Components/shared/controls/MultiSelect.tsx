import React from 'react'
import { FormHelperText } from '@material-ui/core';

export default function MultiSelect(props) {
    const { name, label, value, error = null, menuProps,
        onChange, inputProps, items, required = false, className } = props;
    const errors = (Array.isArray(props.error) && props.error.length == 0) ? null : error;

    return (
        <>
            <div className={className}>

                <label className="form-label" htmlFor="formControl" style={{ width: "100%" }}>{label}
                    <span className="error">*</span></label>
                <select className="select form-control" data-filter="true"
                    name={name}
                    value={value}
                    // multiple
                    onChange={onChange}
                    required={required}
                    {...inputProps}
                >

                    {/* <Select
                name={name}
                label={label}
                multiple
                value={value}
                onChange={onChange}                                      
                renderValue={(selected:any) => (selected as string[]).join(', ')}
                required = {required}
                inputProps = {inputProps}
                MenuProps = {menuProps}
               
            > */}

                    {items.map((item) => (
                        <option key={item.Id} value={item.Name}>
                            {/* <Checkbox checked={value.indexOf(item.Name) > -1} /> */}
                            {item.Name}
                        </option>

                    ))}
                </select>
                {(errors) && <FormHelperText>{error}</FormHelperText>}
            </div>
        </>
    )
}
