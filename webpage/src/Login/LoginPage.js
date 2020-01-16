import React, { useState, Component } from "react";
import { Button, FormGroup, FormControl, ControlLabel, FormLabel } from "react-bootstrap";
import axios from 'axios';  
import "./Login.css";





export default function Login(props) {
  
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const redirectUri = "/";
  const clientId="LoginPage";
  var isAuthorized = false;

  function validateForm() {
    return email.length > 0 && password.length > 0;
  }
  function sleep (time) {
    return new Promise((resolve) => setTimeout(resolve, time));
  }

  function handleSubmit(event) {
    var authCode= '';

    if (isAuthorized)
    {
    axios.post('https://localhost:5051/api/accounts/oauth/',{Username:email, Password:password },{mode:'no-cors'}).then(response =>
    { 
      if (response.status == 200)
      {
        authCode = response.data.authCode;
        debugger;
        props.history.push("/"+authCode);         
        debugger;
      }
      
      debugger;  
    
    } )   
    
    
    sleep(1000).then(() => {
    axios.post('https://localhost:5051/api/accounts/', 
    {Username:authCode},{mode:'no-cors'}).then(response =>
      { 
        
        if (response.status == 200)
        {
          localStorage.clear();
          localStorage.setItem('accessToken',response.data.token);
          localStorage.setItem('refToken',response.data.refToken);
          debugger;
          props.history.push("/");         
          debugger;
        }
        
        debugger; 
      
      } )  

    })
    }

    
    else
    {
      alert("Error with authorization server, please try again later");
    }
    event.preventDefault();
  }

  axios.post('https://localhost:5051/api/accounts/auth/',{Username:clientId, Password: redirectUri },{mode:'no-cors'}).then(response =>
    { 
      if (response.status == 200)
      {
        isAuthorized = true;
      }
      
    })   


  return (
    <div className="Login">
      <form onSubmit={handleSubmit}>
        <FormGroup controlId="usernam" bsSize="large">
          <FormLabel>Username</FormLabel>
          <FormControl
            autoFocus
            type="username"
            value={email}
            onChange={e => setEmail(e.target.value)}
          />
        </FormGroup>
        <FormGroup controlId="password" bsSize="large">
          <FormLabel>Password</FormLabel>
          <FormControl
            value={password}
            onChange={e => setPassword(e.target.value)}
            type="password"
          />
        </FormGroup>
        <Button block bsSize="large" disabled={!validateForm()} type="submit">
          Login
        </Button>
      </form>
    </div>
  );
  
  
}