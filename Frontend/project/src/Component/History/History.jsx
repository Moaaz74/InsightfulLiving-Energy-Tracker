import React from 'react'
import Style from "./History.module.css"
import axios from 'axios'
import { useQuery } from 'react-query';

export default function History() {
    let homeId = localStorage.getItem("homeid");
    homeId = parseInt(homeId);

    function getConsumptionElectricity(homeId) {
        return axios.post(`http://localhost:62863/api/home_overall/last/${homeId}`,{
          energyType: "Electricity",
        })
      }
     // console.log("res: ", getConsumptionElectricity(homeId));
      let consumptionElectricity = useQuery("getConsumptionElectricity" ,()=>getConsumptionElectricity(homeId),{
      refetchInterval:30000,
      } )
  return <div className='  d-flex flex-column ms-5 py-5 '>
    <h3 className='h1 mb-5'>Show Your History Consumption</h3>

    <form className='  d-flex flex-column' >
    <label htmlFor="StartDate" className='h4 fw-bolder'>Start date:</label>

<input type="date" id="StartDate" className= "form-control w-75 mb-5 p-3" name="trip-start" value="2018-07-22" min="2022-01-01" max="2025-12-31" />


<label htmlFor="EndDate" className='h4 fw-bolder'>End date:</label>

<input type="date" id="EndDate" name="trip-start"  className= "form-control w-75 p-3 mb-5" value="2018-07-22" min="2022-01-02" max="2025-12-31" />
    
<label htmlFor="date" className='h4 fw-bolder'>Select time:</label>

<select name="date" id="date" className= "form-control w-75 p-3 mb-5">
  <option value="Day">Day</option>
  <option value="Month">Month</option>
  <option value="Year">Year</option>
</select>

<button className='btn btn-success w-25 p-2 fw-bolder'>Submit</button>
    </form>

    

  </div>
}
