import React from 'react'
import Style from "./Home.module.css"
import { useState } from 'react';
import LineChart from '../../LineChart';
import gas from "../../Assets/WhatsApp Image 2024-03-12 at 4.44.12 PM.jpeg"
import electricity from "../../Assets/WhatsApp Image 2024-03-12 at 4.44.15 PM.jpeg"
import axios from 'axios'
import {useQuery} from "react-query"
import ChartDataFetcher from "../../Data"





export default function Home() {
  // async function chartData() {
  //   let {data} = await ChartDataFetcher(268)
  //   setChartInfo(data)
  // }
  // let data1 =  ChartDataFetcher(268)
  //
  let data1;
  async function updateItem() {
    data1 = await ChartDataFetcher(268)
    console.log("data1:", data1)
  }
updateItem();
  // const [chartInfo , setChartInfo] = useState({
  //   labels: data1.map((data1) => data1?.energyType) ,
  //   datasets:[{
  //     label: "home Consumption",
  //     data: data1.map((data1) => data1?.homeConsumption),
  //     backgroundColor: [
  //       "#f3ba2f",
  //       "#2a71d0",
  //     ],
  //     borderWidth: 2,
  //   },],
  // })


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

  let homeId = localStorage.getItem("homeid");
  homeId = parseInt(homeId);

   function getLivingRoomId() {
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
 let data = useQuery("getLivingRoomId" ,getLivingRoomId );
 
// console.log("Living Room Info : ",obj);
 let livingRoomId =data?.data?.data?.id;
console.log("Room Id : " , livingRoomId);



// function getChartData(homeId) {
        
//   return axios.get(`http://localhost:62863/api/home_overall/all/${homeId}`)
//   .then((response)=> response)
//   .catch((error)=> error);   
  
//   }

 

  function getTempAndHume(livingRoomId) {
  return  axios.post(
    `http://localhost:62863/api/temp_humidity/last/2471`,
  )
   
}

  let roomInfo = useQuery("getTempAndHume" ,()=>getTempAndHume(livingRoomId),{
    refetchInterval:30000,
  }) 
   console.log("Temp &  Humidity of Living Room : ",roomInfo);



  function getConsumptionGas(homeId) {
    
      return axios.post(`http://localhost:62863/api/home_overall/last/268`,{
        energyType: "Gas",
      })
    
    }

  let consumptionGas = useQuery("getConsumptionGas" ,()=>getConsumptionGas(homeId) ,{
    refetchInterval:30000,
  })
   console.log("Gas Consumption : " , consumptionGas);


  function getConsumptionElectricity(homeId) {
    return axios.post(`http://localhost:62863/api/home_overall/last/${homeId}`,{
      energyType: "Electricity",
    })
  }
 // console.log("res: ", getConsumptionElectricity(homeId));
  let consumptionElectricity = useQuery("getConsumptionElectricity" ,()=>getConsumptionElectricity(homeId),{
  refetchInterval:30000,
  } )
    console.log("Electricity Consumption : " , consumptionElectricity);

    function getConsumptionElectricityall(homeId) {
      return axios.get(`http://localhost:62863/api/home_overall/all/268`,{
       
      })
    }
   // console.log("res: ", getConsumptionElectricity(homeId));
    let consumptionElectricityall = useQuery("getConsumptionElectricityall" ,()=>getConsumptionElectricityall(homeId),{
    refetchInterval:30000,
    } )
      console.log("all Consumption : " , consumptionElectricityall);
  


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
           <span className={`${Style.degree} mb-5 mt-5`}>{roomInfo?.data?.data?.temperature}<span className=' fs-2'>C</span></span>
          <h5 className={`${Style.detail} h5`}>{roomInfo?.data?.data?.dataTime}</h5> 
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
          <span className={`${Style.degree} mb-5 mt-5`}>{roomInfo?.data?.data?.humidity} <span className=' fs-2'>%</span></span>
          <h5 className={`${Style.detail} h5`}>{roomInfo?.data?.data?.dataTime}</h5> 
          </div>
        </div>
      </div>
      </div>
      

      
      
    </div>


{/*   
       <div className='w-100 mt-3'>
        <div className={Style.Line} >
          <LineChart chartData={chartInfo} />
        </div>
      </div> 
  */}

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
                   <span className={`${Style.degree} mb-5 mt-5`}>{consumptionElectricity?.data?.data?.homeConsumption} <span className=' fs-2'>Kwh</span> </span> 
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
                   <span className={`${Style.degree} mb-5 mt-5`}>{consumptionGas?.data?.data?.homeConsumption} <span className=' fs-2'>Kwh</span> </span> 
                  <h5 className={`${Style.detail} h5`}>At Last Hour</h5>
                </div>
          </div>
        </div>
      </div>

</div>
      

 </div>
  
  }
