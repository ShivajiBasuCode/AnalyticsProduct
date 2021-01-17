import React from "react";


export default function SaveButtonProgress() {
    return (
        <button type="button" id="btn-one" className="btn btn-rounded float-right">
            <span className="spinner-border spinner-border-sm mr-2" role="status" aria-hidden="true"></span>Saving...
        </button>
    );
}
