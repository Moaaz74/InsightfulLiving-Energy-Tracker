import axios from "axios";
import { useQuery } from "react-query";

// Define getAllHomes function outside

// Create a separate functional component for fetching data
export default function ChartDataFetcher(homeId ) {
   
        return axios.get(`http://localhost:62863/api/home_overall/all/${homeId}`,{
         
        })
    
};
let consumptionElectricityall = useQuery("getConsumptionElectricityall" ,()=>getConsumptionElectricityall(homeId))
//ChartDataFetcher(268);