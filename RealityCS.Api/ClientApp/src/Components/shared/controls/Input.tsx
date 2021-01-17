import React from 'react'

export default function Input(props) {

    const { name, label, value, error = null, onChange, inputProps, className } = props;

    return (
        <div className={className}>
            <label className="form-label" htmlFor="formControl" style={{ width: "100%" }}>{label} <span className="error">*</span>
                <input type="text" id="formControl" className="form-control"
                    value={value}
                    name={name}
                    onChange={onChange}
                    required
                    {...inputProps}
                    {...(error && { error: true, helperText: error })}
                />
            </label>

        </div>


    )
}
