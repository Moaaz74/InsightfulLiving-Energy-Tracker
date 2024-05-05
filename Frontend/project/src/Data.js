
// (async function() {

//   const data = consumptionElectricityall
//   new Chart(
//     document.getElementById('acquisitions'),
//     {
//       type: 'bar',
//       data: {
//         labels: data.map(row => row.year),
//         datasets: [
//           {
//             label: 'Acquisitions by year',
//             data: data.map(row => row.count)
//           }
//         ]
//       }
//     }
//   );
  
// })();

<<<<<<< HEAD
=======
// Create a separate functional component for fetching data
export default function ChartDataFetcher(homeId ) {
   
        return axios.get(`http://localhost:62863/api/home_overall/all/${homeId}`,{
         
        })
    
};
let consumptionElectricityall = useQuery("getConsumptionElectricityall" ,()=>getConsumptionElectricityall(homeId))
//ChartDataFetcher(268);
>>>>>>> 0c3f826d09c553d195770e456ca8937cb6a383df
