import React, { useState } from 'react'
import "./Register.module.css"
import { useFormik } from 'formik'
import * as yup from 'yup'
import axios from 'axios'
import {useNavigate} from "react-router-dom"
import { ThreeCircles } from  'react-loader-spinner'

export default function Register(value) {

  const phoneRegExp = /^((\\+[1-9]{1,4}[ \\-]*)|(\\([0-9]{2,3}\\)[ \\-]*)|([0-9]{2,4})[ \\-]*)*?[0-9]{3,4}?[ \\-]*[0-9]{3,4}?$/


  let validationSchema = yup.object({
    name: yup.string().min(3 , "Name minLength is 3").max(10 , "Name maxLength is 10").required("name is required"),
    email: yup.string().email("E-mail is inValid").required("E-mail is required"),
    phone: yup.string().matches(phoneRegExp, "Phone is inValid").required("Phone is required"),
    password: yup.string().matches(/[A-Z][a-z0-9]{5,10}/ , "Password is inValid").required("Password is required"),
    rePassword: yup.string().oneOf([yup.ref("password")], "Password and rePassword don't match").required("rePassword is required")
  })
  let [Error,setError] = useState("")
  let [isLoading,setisLoadind] = useState(false)
  let navigate = useNavigate();
  async function submitRegister(value) {
    setisLoadind(true)
    let {data} = await axios.post(`https://ecommerce.routemisr.com/api/v1/auth/signup` ,value)
    .catch((err)=>{ 
      setisLoadind(false)
      setError(err.response.data.message)
    })
    if(data.message === "success" ){
      setisLoadind(false)
        navigate("/login")
    }
  }

  let formik= useFormik({
    initialValues:{
      name: '',
      email:'',
      phone:'',
      password:'',
      rePassword:'',
    },validationSchema,
    onSubmit: submitRegister
  })

  return <>
  <div className='w-75 mx-auto py-4 '>
    {Error?<div className="alert alert-danger">{Error}</div>:""}
    

    <h1>Register Now</h1>

    <form onSubmit={formik.handleSubmit}>

      <label htmlFor="name" className='mb-2'>Name :</label>
      <input value={formik.values.name} onChange={formik.handleChange} onBlur={formik.handleBlur} name='name' className='form-control mb-2' id='name' type="text" />
      {formik.errors.name && formik.touched.name ? <div className="alert alert-danger mt-2 mb-3 p-2">{formik.errors.name}</div> : null }
      

      <label htmlFor="email" className='mb-2'>Email :</label>
      <input value={formik.values.email} onChange={formik.handleChange} onBlur={formik.handleBlur} name='email' className='form-control mb-2' id='email' type="email" />
      {formik.errors.email && formik.touched.email ? <div className="alert alert-danger mt-2 mb-3 p-2">{formik.errors.email}</div> : null }


      <label htmlFor="phone" className='mb-2'>Phone :</label>
      <input value={formik.values.phone} onChange={formik.handleChange} onBlur={formik.handleBlur} name='phone' className='form-control mb-2' id='phone' type="tel" />
      {formik.errors.phone && formik.touched.phone ? <div className="alert alert-danger mt-2 mb-3 p-2">{formik.errors.phone}</div> : null }


      <label htmlFor="password" className='mb-2'>Password :</label>
      <input value={formik.values.password} onChange={formik.handleChange} onBlur={formik.handleBlur} name='password' className='form-control mb-2' id='password' type="password" />
      {formik.errors.password && formik.touched.password ? <div className="alert alert-danger mt-2 mb-3 p-2">{formik.errors.password}</div> : null }


      <label htmlFor="rePassword " className='mb-2'>rePassword :</label>
      <input value={formik.values.rePassword} onChange={formik.handleChange} onBlur={formik.handleBlur} name='rePassword' className='form-control ' id='rePassword' type="password" />
      {formik.errors.rePassword && formik.touched.rePassword ? <div className="alert alert-danger mt-2 mb-3 p-2">{formik.errors.rePassword}</div> : null }

      {!isLoading?<button disabled={!(formik.isValid && formik.dirty)} type='submit' className='btn bg-main mt-2 text-white'>Register</button>:
      <button type='button' className='bg-main mt-3 text-white none rounded-3'>
       <ThreeCircles
  height="50"
  width="45"
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
