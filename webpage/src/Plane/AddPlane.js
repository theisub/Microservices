import React from 'react';  
import axios from 'axios';  
import './AddPlane.css'  
import { Container, Col, Form, Row, FormGroup, Label, Input, Button } from 'reactstrap';  
class AddPlane extends React.Component{  
constructor(props){  
super(props)  
this.state = {  
  planeCompany:'',  
  inCountry:'',
  outCountry:'',    
  inCity:'',  
  outCity:'',
  Price: 0,
  travelTime: 0,
  Transit:false,
  }  
}   
AddPlane=()=>{
  debugger;  
  axios.post('https://localhost:44331/api/Planes', 
  {planeCompany:this.state.planeCompany,inCountry:this.state.inCountry,outCountry:this.state.outCountry,   
    inCity:this.state.inCity,outCity:this.state.outCity,Price:this.state.Price, travelTime:this.state.travelTime,Transit: this.state.Transit})  
.then(json => {  
if(json.data.Status==='Success'){  
  console.log(json.data.Status);  
  alert("Data Save Successfully");  
this.props.history.push('/Planeslist')  
}  
else{  
alert('Data not Saved');  
debugger;  
this.props.history.push('/Planeslist')  
}  
})  
}  
   
handleChange= (e)=> {  
this.setState({[e.target.name]:e.target.value});  
}

handleNumChange= (e)=> {  
  this.setState({[e.target.name]:Number(e.target.value)});  
}

handleBoolChange= (e)=> {  
  this.setState({
    Transit: !this.state.Transit,
  });
}    

   
render() {  
return (  
   <Container className="App">  
    <h4 className="PageHeading">Enter plane Informations</h4>  
    <Form className="form">  
      <Col>  
        <FormGroup row>  
          <Label for="name" sm={2}>planeCompany</Label>  
          <Col sm={10}>  
            <Input type="text" name="planeCompany" onChange={this.handleChange} value={this.state.planeCompany} placeholder="Enter planeCompany" />  
          </Col>  
        </FormGroup>  
        <FormGroup row>  
          <Label for="address" sm={2}>inCountry</Label>  
          <Col sm={10}>  
            <Input type="text" name="inCountry" onChange={this.handleChange} value={this.state.inCountry} placeholder="Enter inCountry" />  
          </Col>  
        </FormGroup>  
        <FormGroup row>  
          <Label for="Password" sm={2}>outCountry</Label>  
          <Col sm={10}>  
            <Input type="text" name="outCountry" onChange={this.handleChange} value={this.state.outCountry} placeholder="Enter outCountry" />  
          </Col>  
        </FormGroup>  
        <FormGroup row>  
          <Label for="Password" sm={2}>inCity</Label>  
          <Col sm={10}>  
            <Input type="text" name="inCity" onChange={this.handleChange} value={this.state.inCity} placeholder="Enter inCity" />  
          </Col>  
        </FormGroup>  
        <FormGroup row>  
          <Label for="Password" sm={2}>outCity</Label>  
          <Col sm={10}>  
            <Input type="text" name="outCity" onChange={this.handleChange} value={this.state.outCity} placeholder="Enter outCity" />  
          </Col>  
        </FormGroup>
        <FormGroup row>  
          <Label for="Password" sm={2}>Price</Label>  
          <Col sm={10}>  
            <Input type="number" name="Price" onChange={this.handleNumChange} value={Number(this.state.Price)} placeholder="Enter Price" />  
          </Col>  
        </FormGroup>
        <FormGroup row>  
          <Label for="Password" sm={2}>travelTime</Label>  
          <Col sm={10}>  
            <Input type="number" name="travelTime" onChange={this.handleNumChange} value={Number(this.state.travelTime)} placeholder="Enter travelTime" />  
          </Col>  
        </FormGroup>  
        <FormGroup row>
          <Label for="Password" sm={2}>Transit</Label>  
          <Col sm={10}>
            <Input type="checkbox" label="Транзит?" onChange = {this.handleBoolChange} defaultChecked = {this.state.Transit} />
          </Col>  
        </FormGroup>    

      </Col>  
      <Col>  
        <FormGroup row>  
          <Col sm={5}>  
          </Col>  
          <Col sm={1}>  
          <button type="button" onClick={this.AddPlane} className="btn btn-success">Submit</button>  
          </Col>  
          <Col sm={1}>  
            <Button color="danger">Cancel</Button>{' '}  
          </Col>  
          <Col sm={5}>  
          </Col>  
        </FormGroup>  
      </Col>  
    </Form>  
  </Container>  
);  
}  
   
}  
   
export default AddPlane;  