import React from 'react'
import { Outlet } from 'react-router-dom'
import Sidebar from './../Sidebar/Sidebar';
import Style from "./Layout.module.css"

export default function Layout() {
  return <>
 <div className='  d-flex min-vh-100'>
    <div className={`${Style.sidebar} position-fixed `} >
      <Sidebar />
    </div>
    <div className={`${Style.content} `}>
      <Outlet></Outlet>
    </div>

  </div>

</>
}