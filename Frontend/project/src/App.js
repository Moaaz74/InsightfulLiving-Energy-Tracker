import './App.css';
import { createBrowserRouter , RouterProvider } from 'react-router-dom'
import Home from "./Component/Home/Home"
import Login from './Component/Login/Login';
import Layout from './Component/Layout/Layout';
import { useContext, useEffect } from 'react';
import  { userContext } from './Context/UserContext';
import ProtectedRoute from './Component/ProtectedRoute/ProtectedRoute';
import History from './Component/History/History';
import Rooms from './Component/Rooms/Rooms';
import Devices from './Component/Devices/Devices';

function App() {


  let {setUserToken} = useContext(userContext)


  useEffect(()=>{
    if(localStorage.getItem("userToken") !== null){
      setUserToken(localStorage.getItem("userToken"))
    }
  } , []);

  let routers = createBrowserRouter([
    {path:"/", element:  <Layout/>  ,children:[
      {index:true , element:<ProtectedRoute><Home /> </ProtectedRoute>  },
      {path:"history" , element: <History/>},
      {path:"rooms" , element: <Rooms/>},
      {path:"devices" , element: <Devices/>},
      
      
    ]},
    {path:"login" , element: <Login/>},
      
  ])

  return <RouterProvider router={routers}></RouterProvider>
}

export default App;
