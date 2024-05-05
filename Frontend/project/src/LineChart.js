import React from 'react'
import {Line} from "react-chartjs-2"
// import {Chart as ChartJS} from "chart.js/auto"

function LineChart({final}) {
  return <>
  <div>
    <Line data={final} width={1380} height={300} />
  </div>
  </>
}

export default LineChart