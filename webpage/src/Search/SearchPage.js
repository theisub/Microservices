import React, { Component,useState  } from 'react';  
import {InputGroup, InputGroupAddon, InputGroupText, Input } from 'reactstrap';
import { Button, ButtonGroup } from 'reactstrap';

 


export default class SearchPage extends Component {  
  
  constructor(props) {  
      super(props);
      this.state = {
        business: [], 
        isBus:false, 
        isPlane:false};
      
    }

    

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

    
    
      
  
    render() {  
      return (
        <div>
        <InputGroup>
          <InputGroupAddon addonType="prepend">
            <InputGroupText>Откуда</InputGroupText>
          </InputGroupAddon>
          <Input placeholder="inCity" />
        </InputGroup>
        <br />
        <InputGroup>
          <InputGroupAddon addonType="prepend">
            <InputGroupText>Куда</InputGroupText>
          </InputGroupAddon>
          <Input placeholder="outCity" />
        </InputGroup>
        <br />
        <InputGroup>
          <InputGroupAddon addonType="prepend">$</InputGroupAddon>
          <Input placeholder="Amount" min={0} max={100} type="number" step="1" />
        </InputGroup>
        <br/>
        <InputGroup>
        </InputGroup>
        <ButtonGroup>
          <Button className="busBtn" color="primary" onClick={this.busHandleChange} value={this.state.isBus} >Bus</Button>
          <Button className="planeBtn" color="primary" onClick={this.planeHandleChange} value={this.state.isPlane} >Plane</Button>
        </ButtonGroup> 
        <br/>
        <ButtonGroup>
          <Button className="searchBtn" color="primary">Search</Button>
        </ButtonGroup> 

      </div> 
     
        

      );  
    }  
  }  