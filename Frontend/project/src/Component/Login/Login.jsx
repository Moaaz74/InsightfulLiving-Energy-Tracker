import React, { useContext, useState } from 'react'
import "./Login.module.css"
import { useFormik } from 'formik'
import * as yup from 'yup'
import axios from 'axios'
import {Link, useNavigate} from "react-router-dom"
import { ThreeCircles } from  'react-loader-spinner'
import { userContext } from '../../Context/UserContext'

export default function Login(value) {

  let {setUserToken} = useContext(userContext)
  let validationSchema = yup.object({
    username:yup.string().matches(/^[a-zA-Z0-9_]{3,30}$/, "Username must be between 3 and 30 characters").required("Username is required"),
    password: yup.string().matches(/[A-Z][a-zA-Z0-9@_]{5,20}/ , "Password is inValid").required("Password is required"),
  })
  let [Error,setError] = useState("")
  let [isLoading,setisLoadind] = useState(false)
  let navigate = useNavigate();
  async function submitRegister(value) {
    setisLoadind(true)
    let {data} = await axios.post(`http://localhost:62863/api/user/Login` ,value)
    .catch((err)=>{ 
      setisLoadind(false)
      setError(err.response.data.title)
      console.log("error : ", data);
    })
   
    if(data){
      setisLoadind(false)
      localStorage.setItem("userToken" , data.token)
      setUserToken(data.token)
        navigate("/")
    }
    
  }

  let formik= useFormik({
    initialValues:{
      username:'',
      password:'',
    },validationSchema,
    onSubmit: submitRegister
  })

  return <>
  <div className='w-75 mx-auto py-5 '>
    {Error?<div className="alert alert-danger">{Error}</div>:""}
    

    <h1>Login Now</h1>

    <form onSubmit={formik.handleSubmit}>

      <label htmlFor="username" className='mb-2'>UserName :</label>
      <input value={formik.values.username} onChange={formik.handleChange} onBlur={formik.handleBlur} name='username' className='form-control mb-2' id='username' type="username" />
      {formik.errors.username && formik.touched.username ? <div className="alert alert-danger mt-2 mb-3 p-2">{formik.errors.username}</div> : null }


      <label htmlFor="password" className='mb-2'>Password :</label>
      <input value={formik.values.password} onChange={formik.handleChange} onBlur={formik.handleBlur} name='password' className='form-control mb-2' id='password' type="password" />
      {formik.errors.password && formik.touched.password ? <div className="alert alert-danger mt-2 mb-3 p-2">{formik.errors.password}</div> : null }

      {!isLoading?
      <div className='d-flex align-items-center mx-2'>
      <button disabled={!(formik.isValid && formik.dirty)} type='submit' className='btn bg-main me-2 mt-2 text-white'>Login</button>
      </div>
      :
      <button type='button' className='bg-main mt-3 text-white none rounded-3'>
       <ThreeCircles
  height="50"
  width="60"
  color="white"
  wrapperStyle={{}}
  wrapperClass=""
  visible={true}
  ariaLabel="three-circles-rotating"
  outerCircleColor=""
  innerCircleColor=""
  middleCircleColor=""
/>
      </button>
      }
      
    </form>


  </div>
  </>
}
