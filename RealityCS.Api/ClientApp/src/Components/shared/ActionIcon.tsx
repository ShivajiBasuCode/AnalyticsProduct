import { makeStyles } from "@material-ui/core";
import React from "react";
import Tooltip from '@material-ui/core/Tooltip';    

const useStyles= makeStyles(()=>({
    alignActionIcon: {
        margin: ".4rem",
        cursor:"pointer"
      },
}));

interface IActionIcon {
  component: any;
  disabled?;
  onClick: any;
    Id?: any;
    props?:any;
    label?:any;
}

export default function ActionIcon({
  component: Component,
  disabled,
  onClick,
    Id,
    label,
    ...props
}: IActionIcon) {
  const classes = useStyles();
  // cursor: disabled ? "pointer" : "none"
  return (
    <span className={classes.alignActionIcon}>
        <Tooltip title={label}>    
          <Component onClick={onClick} disabled={disabled} {...props} />
        </Tooltip>    
    </span>
  );
}
