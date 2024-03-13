import axios from "axios";
export default function getChartData() {
        
return axios.get(`http://localhost:62863/api/home_overall/all`)
.then((response)=> response)
.catch((error)=> error);   

}