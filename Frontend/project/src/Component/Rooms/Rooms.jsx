import React from 'react'
import Style from "./Rooms.module.css"
import axios from 'axios'
import {useQuery} from "react-query"

export default function Rooms() {
  function getTempAndHume(livingRoomId) {
    return  axios.post(
      `http://localhost:62863/api/temp_humidity/last/${livingRoomId}`,
    )
     
  }
  
    let roomInfo = useQuery("getTempAndHume" ,()=>getTempAndHume(2471),{
      refetchInterval:30000,
    }) 
  return (
    <div id='cards' className={`${Style.cards}`} >
       <div className=' d-flex  flex-column w-50'>
       <div className={`${Style.card}  ms-3 mb-3 `}>
            <div className={`${Style.cardInner}`}>
              <h2>Indoor Temperature</h2>
              <span className={`${Style.span}`}> <i class="fa-solid fa-house me-3 "></i></span>
            </div>
            <h1>{roomInfo?.data?.data?.humidity} <span className=' fs-2'>%</span></h1>
          </div>
       <div className={`${Style.card}  ms-3 `}>
            <div className={`${Style.cardInner}`}>
              <h2>Humidity</h2>
              <span className={`${Style.span}`}><i className="fa-solid fa-droplet me-3"></i></span>
            </div>
            <h1>{roomInfo?.data?.data?.humidity} <span className=' fs-2'>%</span></h1>
          </div>
       </div>
       <div className='bg-danger w-50 h-100'>
        {/* هنا chart */}
       </div>
    </div>
  )
}
