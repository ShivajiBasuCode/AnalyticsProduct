import React, { useEffect, forwardRef } from "react";
//import useStyles from "./style";
// import { Access } from "../../config/permissions/Access";
import {
    Box,
    Dialog,
    DialogActions,
    DialogContent,
    DialogContentText,
    DialogTitle,
    Button,
    CircularProgress,
    IconButton
} from "@material-ui/core";
import { DialogProps } from "@material-ui/core/Dialog";
import CloseIcon from "@material-ui/icons/Close";

import SaveButtonProgress from './SaveButtonProgress';
interface IFormDialog {
    open: boolean;
    fullScreen?;
    close?;
    dialogContent?: string;
    isLoading?: any;
    title?: any;
    savetext?: string;
    saveaction?: any;
    closetext?: any;
    closeaction?: any;
    actionComponent?: React.ReactNode;
    children: React.ReactNode;
    setWidth?: any;
    scroll?: React.UIEventHandler;
}
const FormDialog = forwardRef(
    (
        {
            open,
            dialogContent,
            title,
            children,
            savetext,
            saveaction,
            close,
            isLoading,
            closeaction,
            closetext,
            fullScreen,
            actionComponent,
            setWidth,
            scroll,
        }: IFormDialog,
        ref
    ) => {
    
        const [fullWidth, setFullWidth] = React.useState(true);
        // const [maxWidth, setMaxWidth] = setWidth ? React.useState<DialogProps["maxWidth"]>(setWidth) : React.useState<DialogProps["maxWidth"]>("md");
        const [maxWidth, setMaxWidth] = React.useState<DialogProps["maxWidth"]>(
            setWidth ? setWidth : "md"
        );
        let btnContent: any = savetext ? savetext : "Save"
        btnContent = isLoading ? <CircularProgress size="1.5rem" style={{ color: "#fff" }} /> : btnContent

        return ( <>
                <Dialog
                className="dialogBox"
                fullScreen={!!fullScreen}
                open={open}
                aria-labelledby={`dialog-${title}`}
                fullWidth={fullWidth}
                maxWidth={maxWidth}
                >
                    {title ? (
                        <DialogTitle id={`dialog-${title}`}  className="dialogBoxHeader">
                            <Box
                                display="flex"
                                alignItems="center"
                                justifyContent="space-between"
                            >
                                {title}
                                <IconButton onClick={close} >
                                    <CloseIcon />
                                </IconButton>
                            </Box>
                        </DialogTitle>
                    ) : null}
                <DialogContent dividers ref={ref} onScroll={scroll} className="dialogBoxBody">
                        {dialogContent ? (
                            <DialogContentText className="dialogBoxWidth">{dialogContent}</DialogContentText>
                        ) : null}
                        {children}
                    </DialogContent>
                    {saveaction || closeaction ? (
                        <DialogActions>
                        {closetext && (
                                <Button
                                    onClick={e => closeaction(e)}
                                    variant="contained"
                                    disabled={isLoading}
                                    className="cancelBtn"
                                // color="primary"
                                >
                                    {closetext ? closetext : "cancel"}
                                </Button>
                            )}
                        {savetext && isLoading && <SaveButtonProgress />}
                        {savetext && !isLoading && (
                                <Button
                                    variant="contained"
                                    color="primary"
                                    //disabled={isLoading}
                                    style={{ pointerEvents: isLoading ? "none" : "initial"}}
                                    onClick={e => saveaction(e)}
                                    className="okBtn"
                                >
                                    {btnContent}
                                </Button>
                            )}
                        </DialogActions>
                    ) : null}
                    {actionComponent && <DialogActions>{actionComponent}</DialogActions>}
                </Dialog>

{/*     
            <div
  className="modal fade"
  id="exampleModal"
  tabIndex={-1}
  aria-labelledby="exampleModalLabel"
  aria-hidden="true"
>
  <div className="modal-dialog">
    <div className="modal-content">
      <div className="modal-header">
        <h5 className="modal-title" id="exampleModalLabel">Modal title</h5>
        <button
          type="button"
          className="btn-close"
          data-mdb-dismiss="modal"
          aria-label="Close"
        ></button>
      </div>
      <div className="modal-body">...</div>
      <div className="modal-footer">
        <button type="button" className="btn btn-secondary" data-mdb-dismiss="modal">
          Close
        </button>
        <button type="button" className="btn btn-primary">Save changes</button>
      </div>
    </div>
  </div>
</div>
            <div className="modal" tabIndex={-1}
                aria-labelledby={`dialog-${title}`}
            >
                <div className="modal-dialog">
                    <div className="modal-content">
                        <div className="modal-header">
                            {title ? (
                                <div>
                                    <h5 className="modal-title" id={`dialog-${title}`}>{title}</h5>
                                    <button
                                        type="button"
                                        className="btn-close"
                                        aria-label="Close"
                                        onClick={close}
                                    ></button>
                                </div>
                            ) : null
                            }


                        </div>
                        <div className="modal-body" onScroll={scroll}>
                            {dialogContent ? (
                                <p>{dialogContent}</p>
                            ) : null}
                            {children}
                        </div>
                        {saveaction || closeaction ? (
                            <div className="modal-footer">

                                {closetext && (
                                    <button
                                        type="submit"
                                        onClick={e => closeaction(e)}
                                        style={{ pointerEvents: isLoading ? "none" : "initial" }}
                                        className="btn btn-rounded float-right"
                                        disabled={isLoading}
                                    >
                                        {closetext ? closetext : "cancel"}
                                    </button>
                                )
                                }
                                {savetext && isLoading && <SaveButtonProgress />}
                                {savetext && !isLoading && (
                                    <button
                                        type="submit"
                                        style={{ pointerEvents: isLoading ? "none" : "initial" }}
                                        className="btn btn-rounded float-right"
                                    >
                                        {btnContent}
                                    </button>
                                )}
                            </div>
                        ) : null}
                    </div>
                </div>
            </div> */}
            </>
        );
    }
);
export default FormDialog;
