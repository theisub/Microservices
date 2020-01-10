import React, { Component,useState  } from 'react';  
import {InputGroup, InputGroupAddon, InputGroupText, Input } from 'reactstrap';
import { Button, ButtonGroup } from 'reactstrap';
import axios from 'axios';  

 


export default class SearchPage extends Component {  
  
  constructor(props) {  
      super(props);
      this.state = {
        business: [], 
        isBus:false, 
        isPlane:false,
        inCity:'',  
        outCity:''
        };
      
    }

    handleUserInput= (e)=> {
      const name = e.target.name;
      const value = e.target.value;
      this.setState({[name]: value});
    }

    AddFavorite=()=>{
      //debugger;  
      axios.post('https://localhost:44375/api/favoritesgateway/addfavorite?incity='+this.state.inCity+'&outCity='+this.state.outCity)  
    .then(json => {  
    if(json.status===200){  
      console.log(json.data.Status);  
      alert("Data Save Successfully");  
    this.props.history.push('/Buslist')  
    }  
    else{  
    alert('Data saved!');  
    //debugger;
    console.log(json.data.Status);  
      
    this.props.history.push('/Buslist')  
    }  
    })  
    }  
    
/*
    busHandleChange= (e)=> {  
      this.state.isBus = !this.state.isBus;
      //this.setState({[e.target.name]:!e.target.state });
      //debugger;
      if (e.target.style.backgroundColor == 'green')
        e.target.style.backgroundColor = '#007BFF'
      else
        e.target.style.backgroundColor = 'green'; 

    }

    planeHandleChange= (e)=> {  
      this.state.isPlane = !this.state.isPlane;
      //this.setState({[e.target.name]:!e.target.state });
      //debugger;

      if (e.target.style.backgroundColor == 'green')
        e.target.style.backgroundColor = '#007BFF'
      else
        e.target.style.backgroundColor = 'green'; 
    }

    
    */
      
  
    render() {  
      return (
        <div>
        <InputGroup>
          <InputGroupAddon addonType="prepend">
            <InputGroupText>Откуда</InputGroupText>
          </InputGroupAddon>
          <Input type="text" name="inCity" onChange={(event) => this.handleUserInput(event)} value={this.state.inCity} placeholder="Enter inCity" />  
        </InputGroup>
        <br />
        <InputGroup>
          <InputGroupAddon addonType="prepend">
            <InputGroupText>Куда</InputGroupText>
          </InputGroupAddon>
          <Input type="text" name="outCity" onChange={(event) => this.handleUserInput(event)} value={this.state.outCity} placeholder="Enter outCity" />  
        </InputGroup>
        
        <br/>
        <InputGroup>
        </InputGroup>
        <br/>
        <ButtonGroup>
          <Button className="searchBtn" onClick={this.AddFavorite} color="primary">Search</Button>
        </ButtonGroup> 

      </div> 
     
        

      );  
    }  
  }  