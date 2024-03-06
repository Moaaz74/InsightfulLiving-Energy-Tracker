import React from 'react'
import {Line} from "react-chartjs-2"
import {Chart as ChartJS} from "chart.js/auto"

function LineChart({chartData}) {
  return <>
  <div>
    <Line data={chartData} width={1380} height={340} />
  </div>
  </>
}

export default LineChart