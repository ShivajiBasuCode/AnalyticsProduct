import React from 'react';
import clsx from 'clsx';
import IconDashBoard from '../../assets/ImageDashBoard.png'


export default function Dashboard() {

  const [open, setOpen] = React.useState(true);
  
  return (
    <div className="content-main rounded m-3 p-3">
    <h1>Welcome !</h1>
          <img src={IconDashBoard} width={"100%"} height={"660px"} />
    </div>
  );
}