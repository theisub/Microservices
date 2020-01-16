import React, { useState, Component } from "react";
import { Button, FormGroup, FormControl, ControlLabel, FormLabel } from "react-bootstrap";
import axios from 'axios';  
import "./Login.css";





export default class Login extends Component {
  
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  validateForm() {
    return email.length > 0 && password.length > 0;
  }

  handleSubmit(event) {
    var authCode= '';

    debugger;
    axios.post('https://localhost:5051/api/auth/',{Username:email, Password:password },{mode:'no-cors'}).then(response =>
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
    
    
    
    axios.post('https://localhost:5051/api/accounts/', 
    {Username:email, Password:password },{mode:'no-cors'}).then(response =>
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
    event.preventDefault();
  }

  render() {  
  return (
    <div className="Login">
      <form onSubmit={handleSubmit}>
        <FormGroup controlId="usernam" bsSize="large">
          <FormLabel>Email</FormLabel>
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
}