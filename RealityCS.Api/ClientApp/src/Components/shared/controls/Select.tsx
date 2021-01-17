import React from 'react'
export default function Select(props) {
    const { name, label, value, error = null, onChange, options, className } = props;
    let optionsItem: any = [];
    if (options) {
        optionsItem.push(<option value="">None</option>);
        options.map(
            item => {
                optionsItem.push(
                    <option key={item.Id} value={item.Id}>{item.Name}</option>
                )
            }
        )
    }

    return (
        <>
            <div className={className}>
                <label className="form-label" htmlFor="formControl">{label}
                    <span className="error">*</span></label>

                <select className="select form-control"
                    data-filter="true"
                    aria-label={label}
                    name={name}
                    value={value}
                    onChange={onChange}
                >
                    {
                        optionsItem
                    }
                </select>
                {error &&
                    <label className="form-label" htmlFor="formControl">
                        {error}
                    </label>
                }
            </div>
        </>
    )
}
