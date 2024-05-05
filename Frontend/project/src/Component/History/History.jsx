import React from 'react'
import Style from "./History.module.css"

export default function History() {
  return <div className='  d-flex flex-column ms-5 py-5 '>
    <h3 className='h1 mb-5'>Show Your History Consumption</h3>

    <form className='  d-flex flex-column' >
    <label htmlFor="StartDate" className='h4 fw-bolder'>Start date:</label>

<input type="date" id="StartDate" className= "form-control w-75 mb-5 p-3" name="trip-start" value="2018-07-22" min="2022-01-01" max="2025-12-31" />


<label htmlFor="EndDate" className='h4 fw-bolder'>End date:</label>

<input type="date" id="EndDate" name="trip-start"  className= "form-control w-75 p-3" value="2018-07-22" min="2022-01-02" max="2025-12-31" />
    </form>

    

  </div>
}
