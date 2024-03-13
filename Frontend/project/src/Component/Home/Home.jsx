import React from 'react'
import Style from "./Home.module.css"
import { useState } from 'react';
import LineChart from '../../LineChart';
import gas from "../../Assets/WhatsApp Image 2024-03-12 at 4.44.12 PM.jpeg"
import electricity from "../../Assets/WhatsApp Image 2024-03-12 at 4.44.15 PM.jpeg"
import axios from 'axios'
import {useQuery} from "react-query"
import getChartData from "../../Data"
import { response } from 'express';


export default function Home() {
  // const [chartInfo , setChartInfo] = useState({
  //   labels: chartInfo.map((data) => data.energyType) ,
  //   datasets:[{
  //     label: "home Consumption",
  //     data: chartInfo.map((data) => data.homeConsumption),
  //     backgroundColor: [
  //       "#f3ba2f",
  //       "#2a71d0",
  //     ],
  //     borderWidth: 2,
  //   },],
  // })

  // async function chartData() {
  //   let {data} = await getChartData()
  //   setChartInfo(data)
  // }
  // const [chartsData , chartsData] = useState({
  //   labels: UserData.map((data) => data.year) ,
  //   datasets:[{
  //     label: "Users Gained",
  //     data: UserData.map((data) => data.userGain),
  //     backgroundColor: [
  //       "rgba(75,192,192,1)",
  //       "#ecf0f1",
  //       "#50AF95",
  //       "#f3ba2f",
  //       "#2a71d0",
  //     ],
  //     borderWidth: 2,
  //   },],
  // })

   let homeId = localStorage.getItem("homeid")
let roomid =1;
let body1 = {
  type: "living room"
};
   function getLivingRoomId() {
  //   return axios.request({
  //     method: 'GET',
  //     url:' http://localhost:62863/api/Room/GetRoomByType',
   
  //   {headers:{'Content-Type':'application/json'}},
  //   params: {
  //     type: "living room"
  //   }
    
   
  //   ,).then((response)=> response)
  //   .catch((error)=> error);  
  // }
 
 return  axios.request({
    method: 'GET',
    url: 'http://localhost:62863/api/Room/GetRoomByType/',
    headers: {
      
    },
    params: {
      type: "living room"
    },
  
  }).then((response)=> response)
  .catch((error)=> error); 
   }
  let { data} = useQuery("getLivingRoomId" ,getLivingRoomId )
 console.log("data: ",data);
let livingRoomId =1;
// fetch(`http://localhost:62863/api/Room/${livingRoomId}`, {
//    method: 'GET',
//    headers: {
//        'Content-Type': 'application/json',
//    }


//  })
//  .then(function (response) {
//    console.log(response);
//  })
//  .catch(function (error) {
//    console.log(error);
//  });
  //let livingRoomId =data.id;
//console.log("homeid:" ,data);
  // function getTempAndHume(livingRoomId) {
  //   return axios.get(`http://localhost:62863/api/temp_humidity/last/${livingRoomId}`)
  // }

  // let roomInfo = useQuery("getTempAndHume" ,getTempAndHume,{
  //   refetchInterval:30000,
  // } )
  // console.log("data: ",roomInfo);
  // function getConsumptionGas(homeId) {
  //   return axios.get(`http://localhost:62863/api/home_overall/last/${homeId}`,{
  //     energyType: "Gas",
  //   })
  // }

  // let consumptionGas = useQuery("getConsumptionGas" ,getConsumptionGas ,{
  //   refetchInterval:30000,
  // })

  // function getConsumptionElectricity(homeId) {
  //   return axios.get(`http://localhost:62863/api/home_overall/last/${homeId}`,{
  //     energyType: "Electricity",
  //   })
  // }

  // let consumptionElectricity = useQuery("getConsumptionElectricity" ,getConsumptionElectricity,{
  //   refetchInterval:30000,
  // } )




  return <div className=' d-flex flex-row flex-wrap align-items-center'>
    <div id='cards' className={`${Style.cards} d-flex `}>
      <div id='card' className={`${Style.card} ${Style.C1} col-md-6 align-items-center justify-content-center`}>
      <div className="inner d-flex ">
      <div  className={` ${Style.info} col-md-4 justify-content-center align-items-center d-flex flex-column me-5`}>
          
          <i class={`${Style.icon} fa-solid fa-temperature-three-quarters mb-2`}></i>
          <h3 className='ms-2 fw-bold'>Living Room</h3>
        </div>
        <div className='col-md-8 '>
          <h1 className={`${Style.title} h5`}> Indoor Temperature</h1>
          <div className=' align-items-center d-flex flex-column justify-content-center '>
          {/* <span className={`${Style.degree} mb-5 mt-5`}>{roomInfo.data.temperature}<span className=' fs-2'>C</span></span>
          <h5 className={`${Style.detail} h5`}>{roomInfo.data.dataTime}</h5> */}
          </div>
        </div>
      </div>
      </div>

      <div id='card' className={`${Style.card} ${Style.C2} col-md-6 align-items-center justify-content-center`}>
      <div className="inner d-flex ">
      <div  className={`${Style.info} col-md-4 justify-content-center align-items-center d-flex flex-column me-5`}>
          <i className={`${Style.icon} fa-solid fa-droplet mb-3 `}></i>
          <h3 className='ms-2 fw-bold'>Living Room</h3>
        </div>
        <div className='col-md-8 '>
          <h1 className={`${Style.title} h5`}>Humidity</h1>
          <div className=' align-items-center d-flex flex-column justify-content-center '>
          {/* <span className={`${Style.degree} mb-5 mt-5`}>{roomInfo.data.humidity} <span className=' fs-2'>%</span></span>
          <h5 className={`${Style.detail} h5`}>{roomInfo.data.dataTime}</h5> */}
          </div>
        </div>
      </div>
      </div>
      

      
      
    </div>


  
      {/* <div className='w-100 mt-3'>
        <div className={Style.Line} >
          <LineChart chartData={chartInfo} />
        </div>
      </div> */}


<div className='row w-100 align-items-center justify-content-around  mt-3'>

<div  id='card' className={`${Style.card} ${Style.C3} col-md-5 align-items-center justify-content-center`}>
        <div className="inner d-flex ">
          <div  className={`${Style.info} col-md-4 justify-content-center align-items-center d-flex flex-column me-4`}>
            {/* <i className={`${Style.icon} fa-solid fa-wind mb-3 `}></i> */}
            <img src={electricity} alt="" className='w-100' />
            <h3 className='ms-2 fw-bold'></h3>
          </div>
            <div className='col-md-8 '>
              <h1 className={`${Style.title} h5`}>Electricity consumption</h1>
                <div className=' align-items-center d-flex flex-column justify-content-center '>
                  {/* <span className={`${Style.degree} mb-5 mt-5`}>{consumptionElectricity.homeConsumption} <span className=' fs-2'>Kwh</span> </span> */}
                  <h5 className={`${Style.detail} h5`}>At Last Hour</h5>
                </div>
          </div>
        </div>
      </div>


<div  id='card' className={`${Style.card} ${Style.C3} col-md-5 align-items-center justify-content-center`}>
        <div className="inner d-flex ">
          <div  className={`${Style.info} col-md-4 justify-content-center align-items-center d-flex flex-column me-4`}>
          <img src={gas} alt="" className='w-100' />
            <h3 className='ms-2 fw-bold'></h3>
          </div>
            <div className='col-md-8 '>
              <h1 className={`${Style.title} h5`}>Gas consumption</h1>
                <div className=' align-items-center d-flex flex-column justify-content-center '>
                  {/* <span className={`${Style.degree} mb-5 mt-5`}>{consumptionGas.homeConsumption} <span className=' fs-2'>Kwh</span> </span> */}
                  <h5 className={`${Style.detail} h5`}>At Last Hour</h5>
                </div>
          </div>
        </div>
      </div>

</div>
      

 </div>
  
  }
