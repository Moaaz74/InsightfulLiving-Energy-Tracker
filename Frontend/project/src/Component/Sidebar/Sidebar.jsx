import {React ,useContext } from 'react'
import Style from "./Sidebar.module.css"
import { Link } from 'react-router-dom'
import { useNavigate} from "react-router-dom"
import { userContext } from '../../Context/UserContext'


export default function Sidebar() {

  let {userToken , setUserToken} = useContext(userContext)
let navigate = useNavigate()

function logOut() {
  localStorage.removeItem('userToken')
  setUserToken(null)
  navigate("/login")
}

  return <div className= {`${Style.Sidebar} `} >
  <div className={`${Style.icon} text-center pt-4 border-bottom pb-4 border-3 mb-4`}>
  <i class="fa-solid fa-house-signal fa-xl text-white"></i>
  </div>

  {userToken !== null?
  <>
  <Link to={"/"} className=' text-white fs-4 d-flex justify-content-center align-items-center mb-4'>
        <i class="fa-solid fa-house me-3 "></i>Home</Link>
  <Link to={"/rooms"} className=' text-white fs-4 d-flex justify-content-center align-items-center mb-4'>
  <i class="fa-solid fa-person-shelter me-3"></i>Rooms</Link>
  <Link to={"/"} className=' text-white fs-4 d-flex justify-content-center align-items-center mb-4'>
  <i class="fa-solid fa-display me-3"></i>Devices</Link>
  <Link to={"/history"} className=' text-white fs-4 d-flex justify-content-center align-items-center mb-4'>
  <i class="fa-regular fa-calendar-days me-3"></i>History</Link>
  </>
  :''
}
       
{userToken !== null ?<>
          <li className="nav-item">
          <span className=' text-white fs-4 d-flex justify-content-center align-items-center mb-4' onClick={()=> logOut()}>LogOut</span>
        </li>
        </>:
        <>
        
        <li className="nav-item">
          <Link className=' text-white fs-4 d-flex justify-content-center align-items-center mb-4' to={"/login"}>LogIn</Link>
        </li>
        <li className="nav-item">
          <Link className=' text-white fs-4 d-flex justify-content-center align-items-center mb-4' to={"/register"}>Register</Link>
        </li>
        
        </>}


  </div>
        
}
