import React from 'react';  
import axios from 'axios';  
import './AddPlane.css'  
import { Container, Col, Form, Row, FormGroup, Label, Input, Button } from 'reactstrap';  
import FormErrors from '../FormErrors';
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
  token:'',

  


  formErrors: 
  {planeCompany: '', inCountry: '',outCountry:'',inCity:'',outCity:''},
    planeCompanyValid: false,
    inCountryValid: false,
    outCountryValid: false,
    inCityValid:false,
    outCityValid:false
  }  
}
   
AddPlane=()=>{
  //debugger;
  var token = localStorage.getItem('accessToken');  
  axios.post('https://localhost:44375/api/planesgateway', 
  {planeCompany:this.state.planeCompany,inCountry:this.state.inCountry,outCountry:this.state.outCountry,   
    inCity:this.state.inCity,outCity:this.state.outCity,Price:this.state.Price, travelTime:this.state.travelTime,Transit: this.state.Transit},{headers:{'Authorization':"Bearer " + token}})  
.then(json => {  
if(json.status===201){  
  console.log(json.status);  
  debugger;
  alert("Data Save Successfully." + json.statusText + ". Status code: " + json.status);  
  this.props.history.push('/Planeslist')  
}  
else{  
alert('Data is not saved!' + json.statusText + "Status code: " + json.status);

debugger;
console.log(json.data.Status);  
this.props.history.push('/Planeslist')  
}  
})
.catch(function (error) { 
  alert(error + " Response code: " + error.response.status);
  if (error.response.status == 401)
  {
        localStorage.removeItem("accessToken");
        window.location.reload();
    debugger;
  };
  debugger;
  console.log(error);  
}
)    
}



AddPlaneAndFavorite=()=>{
  var token = localStorage.getItem('accessToken');
  debugger;  
  axios.post('https://localhost:44375/api/favoritesgateway/AddPlaneAndFavorite/', 
  {planeCompany:this.state.planeCompany,inCountry:this.state.inCountry,outCountry:this.state.outCountry,   
    inCity:this.state.inCity,outCity:this.state.outCity,Price:this.state.Price, travelTime:this.state.travelTime,Transit: this.state.Transit},{headers:{'Authorization':"Bearer " + token}})  
.then(json => {  
if(json.status===200){  
  console.log(json.data.Status);  
  alert("Data Save Successfully");  
this.props.history.push('/Planeslist')  
}  
else{  
alert('Data saved!');  
debugger;  
this.props.history.push('/Planeslist')  
}  
}).catch(function (error) { 
  alert(error + " Response code: " + error.response.status);
  if (error.response.status == 401)
  {
    localStorage.removeItem("accessToken");
    window.location.reload();
        debugger;
  };
  debugger;
  console.log(error);  
})
}  

handleUserInput= (e)=> {
  const name = e.target.name;
  const value = e.target.value;
  this.setState({[name]: value}, 
    () => { this.validateField(name, value); });
}

validateField=(fieldName, value)=> {
  let fieldValidationErrors = this.state.formErrors;
  let planeCompanyValid = this.state.planeCompanyValid;
  let inCountryValid = this.state.inCountryValid;
  let outCountryValid = this.state.outCountryValid;
  let inCityValid = this.state.inCityValid;
  let outCityValid = this.state.outCityValid;
  //debugger;
  switch(fieldName) {
    case 'planeCompany':
      planeCompanyValid = value.length >= 2;
      fieldValidationErrors.planeCompany = planeCompanyValid ? '': ' слишком короткое название, должно быть больше 2';
      break;
    case 'inCountry':
        inCountryValid = value.length >= 2;
        fieldValidationErrors.inCountryValid = inCountryValid ? '': ' слишком короткое название, должно быть больше 2';
        break;
    case 'outCountry':
        outCountryValid = value.length >= 2;
        fieldValidationErrors.outCountryValid = outCountryValid ? '': ' слишком короткое название, должно быть больше 2';
        break;
    case 'inCity':
          inCityValid = value.length >= 2;
          fieldValidationErrors.inCityValid = inCityValid ? '': ' слишком короткое название, должно быть больше 2';
          break;
    case 'outCity':
          outCityValid = value.length >= 2;
          fieldValidationErrors.outCityValid = outCityValid ? '': ' слишком короткое название, должно быть больше 2';
          break;


    default:
      break;
  }
  this.setState({formErrors: fieldValidationErrors,
                  planeCompanyValid: planeCompanyValid,
                  inCountryValid: inCountryValid,
                  outCountryValid: outCountryValid,
                  inCityValid: inCityValid,
                  outCityValid: outCityValid

                }, this.validateForm);
}

validateForm=()=> {
  this.setState({formValid: this.state.planeCompanyValid && this.state.inCountryValid && this.state.outCountryValid && this.state.inCityValid  && this.state.outCityValid });
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
    <FormErrors formErrors={this.state.formErrors} />
  
      <Col>  
        <FormGroup row>  
          <Label for="name" sm={2}>planeCompany</Label>  
          <Col sm={10}>  
            <Input type="text" name="planeCompany" onChange={(event) => this.handleUserInput(event)} value={this.state.planeCompany} placeholder="Enter planeCompany" />  
          </Col>  
        </FormGroup>  
        <FormGroup row>  
          <Label for="address" sm={2}>inCountry</Label>  
          <Col sm={10}>  
            <Input type="text" name="inCountry" onChange={(event) => this.handleUserInput(event)} value={this.state.inCountry} placeholder="Enter inCountry" />  
          </Col>  
        </FormGroup>  
        <FormGroup row>  
          <Label for="Password" sm={2}>outCountry</Label>  
          <Col sm={10}>  
            <Input type="text" name="outCountry" onChange={(event) => this.handleUserInput(event)} value={this.state.outCountry} placeholder="Enter outCountry" />  
          </Col>  
        </FormGroup>  
        <FormGroup row>  
          <Label for="Password" sm={2}>inCity</Label>  
          <Col sm={10}>  
            <Input type="text" name="inCity" onChange={(event) => this.handleUserInput(event)} value={this.state.inCity} placeholder="Enter inCity" />  
          </Col>  
        </FormGroup>  
        <FormGroup row>  
          <Label for="Password" sm={2}>outCity</Label>  
          <Col sm={10}>  
            <Input type="text" name="outCity" onChange={(event) => this.handleUserInput(event)} value={this.state.outCity} placeholder="Enter outCity" />  
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
          <button type="button" onClick={this.AddPlane} className="btn btn-success" disabled={!this.state.formValid}> Submit</button>  
          </Col>
          
        
          <Col sm={1}>  
          <button type="button" onClick={this.AddPlaneAndFavorite} className="btn btn-success" disabled={!this.state.formValid}> Submit + Favorite</button>  
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