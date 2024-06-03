import React from 'react'
import Style from "./Devices.module.css"
import axios from 'axios'
import {useQuery} from "react-query"

export default function Devices() {
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
                  <h2>Device Type</h2>
                  <span className={`${Style.span}`}> <i class="fa-solid fa-house-laptop me-3"></i></span>
                </div>
                <h1>{roomInfo?.data?.data?.humidity} TV</h1>
              </div>
           <div className={`${Style.card}  ms-3 `}>
                <div className={`${Style.cardInner}`}>
                  <h2>Energy Type</h2>
                  <span className={`${Style.span}`}><i class="fa-solid fa-lightbulb me-3"></i></span>
                </div>
                <h1>{roomInfo?.data?.data?.humidity} Gas</h1>
              </div>
           </div>
           <div className='bg-danger w-50 h-100'>
            {/* هنا chart */}
           </div>
        </div>
      )
}
