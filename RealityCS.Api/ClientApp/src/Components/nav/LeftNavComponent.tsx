import React, { useEffect, useState } from 'react';
import navigation from './navigationList';


export default function LeftNavComponent({activeTopMenuId,leftMenuId}) {
  const [activeId,setActiveId]=useState(0);
  let item:any=[];
  navigation.map((data,index)=>{
    if(index===activeTopMenuId)
    {
      item=data.Child;
    }
  });
  useEffect(()=>{

    setActiveId(leftMenuId);
  },[activeTopMenuId]);
  return <>
   <nav id="sidebar">
        <ul className="list-unstyled">
          {
            item && item.map((data,index)=>{
              if(index===activeId)
              {
                return <li>
                <a href={data.Link} data-toggle="collapse" aria-expanded="false" className="active">
                  <i className={data.class}></i>
                  <span>{data.Name}</span>
                </a>
              </li>
              }
              else
              {
              return <li>
                <a href={data.Link} data-toggle="collapse" aria-expanded="false">
                  <i className={data.class}></i>
                  <span>{data.Name}</span>
                </a>
              </li>
              }
            })
          }
         
        </ul>

      </nav>
  </>
}