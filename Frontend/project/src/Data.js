import axios from "axios";
import { useQuery } from "react-query";

// Define getAllHomes function outside

// Create a separate functional component for fetching data
export default function ChartDataFetcher(homeId ) {
    function getAllHomes(homeId) {
        return axios.get(`http://localhost:62863/api/home_overall/all/${homeId}`)
          .then((response) => response) // Assuming you want to return response data
          .catch((error) => { throw error }); // Rethrow error to be caught by React Query
      }
    
  const { data, isLoading, isError } = useQuery("getAllHomes", () => getAllHomes(268), {
    refetchInterval: 30000,
  })
 return data?.data ;
};
