import React from 'react'
import Style from "./Home.module.css"
import { useState } from 'react';
import gas from "../../Assets/WhatsApp Image 2024-03-12 at 4.44.12 PM.jpeg"
import electricity from "../../Assets/WhatsApp Image 2024-03-12 at 4.44.15 PM.jpeg"
import axios from 'axios'
import {useQuery} from "react-query"
import { useEffect } from 'react';
import { Chart as ChartJS, LineElement, PointElement, LinearScale, CategoryScale, Tooltip, Legend } from 'chart.js';
import { Line } from 'react-chartjs-2';

export default function Home() {

  let homeId = localStorage.getItem("homeid");
  homeId = parseInt(homeId);

  const [chartData, setChartData] = useState({
    labels: [],
    datasets: [],
  });

  // Fetch data from API on component mount
  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await fetch(`http://localhost:62863/api/home_overall/all/${homeId}`);
        const data = await response.json();
        const formattedData = formatChartData(data);
        setChartData(formattedData);
      } catch (error) {
        console.error('Error fetching data:', error);
      }
    };

    fetchData();
  }, []);

  // Function to format API data into chart data structure
  const formatChartData = (apiData) => {
    const labels = apiData.map((item) => item.start); // Extract labels (start date)
    const gasData = apiData.filter((item) => item.energyType === 'Gas').map((item) => item.homeConsumption); // Filter and extract Gas data
    const electricityData = apiData.filter((item) => item.energyType === 'Electricity').map((item) => item.homeConsumption); // Filter and extract Electricity data

    return {
      labels,
      datasets: [
        {
          label: 'Gas Consumption',
          data: gasData,
          borderColor: 'rgb(255, 99, 132)', // Red color for Gas line
          backgroundColor: 'rgba(255, 99, 132, 0.5)', // Transparent red for fill
        },
        {
          label: 'Electricity Consumption',
          data: electricityData,
          borderColor: 'rgb(54, 162, 235)', // Blue color for Electricity line
          backgroundColor: 'rgba(54, 162, 235, 0.5)', // Transparent blue for fill
        },
      ],
    };
  };


ChartJS.register(LineElement, PointElement, LinearScale, CategoryScale, Tooltip, Legend);




   function getLivingRoomId() {
      return  axios.request({
        method: 'GET',
        url: 'http://localhost:62863/api/Room/GetRoomByType/',
        headers: {
          
        },
        params: {
          type: "LivingRoom"
        },

      }).then((response)=> response)
      .catch((error)=> error); 
   }
 let data = useQuery("getLivingRoomId" ,getLivingRoomId );
 
// console.log("Living Room Info : ",obj);
 let livingRoomId =data?.data?.data?.id;


 

  function getTempAndHume(livingRoomId) {
  return  axios.post(
    `http://localhost:62863/api/temp_humidity/last/${livingRoomId}`,
  )
   
}

  let roomInfo = useQuery("getTempAndHume" ,()=>getTempAndHume(2471),{
    refetchInterval:30000,
  }) 



  function getConsumptionGas(homeId) {
    
      return axios.post(`http://localhost:62863/api/home_overall/last/${homeId}`,{
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
      return axios.get(`http://localhost:62863/api/home_overall/all/${homeId}`,{
       
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


{
       <div className='w-100 mt-3'>
        <div className={Style.Line} >
        <Line data={chartData} />
        </div>
      </div> 
<<<<<<< HEAD
  }

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
=======
  
          }
          <div className='row w-100 align-items-center justify-content-around  mt-3'>

          <div  id='card' className={`${Style.card} ${Style.C3} col-md-5 align-items-center justify-content-center`}>
                  <div className="inner d-flex ">
                    <div  className={`${Style.info} col-md-4 justify-content-center align-items-center d-flex flex-column me-4 `}>
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
>>>>>>> 0c3f826d09c553d195770e456ca8937cb6a383df
