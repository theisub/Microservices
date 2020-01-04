import React from 'react';   
import { Container, Col, Form, Row, FormGroup, Label, Input, Button } from 'reactstrap';  
import axios from 'axios'  
import './AddPlane.css'  
class EditPlane extends React.Component {  
    constructor(props) {  
        super(props)  
     
    this.onChangeName = this.onChangeName.bind(this);  
    this.onChangeInCountry = this.onChangeInCountry.bind(this);  
    this.onChangeOutCountry = this.onChangeOutCountry.bind(this);  
    this.onChangeInCity = this.onChangeInCity.bind(this);  
    this.onChangeOutCity = this.onChangeOutCity.bind(this);
    this.onChangePrice= this.onChangePrice.bind(this);
    this.onChangetravelTime = this.onChangetravelTime.bind(this);
    this.onChangeTransit = this.onChangeTransit.bind(this);

    this.onSubmit = this.onSubmit.bind(this);  
  
         this.state = {  
            Name: '',  
            InCountry: '',  
            OutCountry: '',  
            inCity: '',
            outCity: '',
            price: 0,
            travelTime: 0,
            transit:false
  
        }  
    }  
  
  componentDidMount() { 
      console.log("et propsi",this.props) 
      axios.get('https://localhost:44361/api/planes/'+this.props.match.params.id)  
          .then(response => { 
              this.setState({   
                Name: response.data.planeCompany,   
                InCountry: response.data.inCountry,  
                OutCountry: response.data.outCountry,  
                inCity: response.data.inCity,
                outCity: response.data.outCity,
                price: response.data.price,
                travelTime: response.data.travelTime,
                transit:response.data.transit
                });  
  
          })  
          .catch(function (error) {  
              console.log(error);  
          })
          
          debugger;
    }  
  
  onChangeName(e) {  
    this.setState({  
        Name: e.target.value  
    });  
  }  
  onChangeInCountry(e) {  
    this.setState({  
        InCountry: e.target.value  
    });    
  }  
  onChangeOutCountry(e) {  
    this.setState({  
        OutCountry: e.target.value  
    });  
}  
    onChangeInCity(e) {  
        this.setState({  
            inCity: e.target.value  
        });  
  }
   onChangeOutCity(e) {  
        this.setState({  
            outCity: e.target.value  
        });  
  }
  onChangePrice(e) {  
    this.setState({  
        price: Number(e.target.value)
    });  
  }

  onChangetravelTime(e) {  
    this.setState({  
        travelTime: Number(e.target.value)
    });  
  }

  onChangeTransit(e) {  
    this.setState({  
        transit: Boolean(e.target.value)
    });  
  }              
  
  async onSubmit(e) {  
    debugger;  
    e.preventDefault();  
    const plane = {  
      planeCompany: this.state.Name,  
      InCountry: this.state.InCountry,  
      OutCountry: this.state.OutCountry,  
      inCity: this.state.inCity,
      outCity: this.state.outCity,
      price: this.state.price,
      travelTime :this.state.travelTime ,
      transit: this.state.transit
  
    };
     
    await axios.put('https://localhost:44375/api/planesgateway/'+this.props.match.params.id, plane)  
        .then(res => console.log(res.data)).catch(function (error) { 
            alert(error + " Response code: " + error.response.status);
            debugger;
            console.log(error);  
          });  
        debugger;  
        this.props.history.push('/Planeslist')  
  }
  
  
  onCancel(e)
  {
      window.history.go(-1);

  }
    render() {  
        return (  
            <Container className="App">  
  
             <h4 className="PageHeading">Update plane Informations</h4>  
                <Form className="form" onSubmit={this.onSubmit}>  
                    <Col>  
                        <FormGroup row>  
                            <Label for="name" sm={2}>Name</Label>  
                            <Col sm={10}>  
                                <Input type="text" name="Name" value={this.state.Name} onChange={this.onChangeName}  
                                placeholder="Enter Name" />  
                            </Col>  
                        </FormGroup>  
                        <FormGroup row>  
                            <Label for="text" sm={2}>InCountry</Label>  
                            <Col sm={10}>  
                                <Input type="text" name="InCountry" value={this.state.InCountry} onChange={this.onChangeInCountry} placeholder="Enter InCountry" />  
                            </Col>  
                        </FormGroup>  
                         <FormGroup row>  
                            <Label for="text" sm={2}>OutCountry</Label>  
                            <Col sm={10}>  
                                <Input type="text" name="OutCountry" value={this.state.OutCountry} onChange={this.onChangeOutCountry} placeholder="Enter OutCountry" />  
                            </Col>  
                        </FormGroup>  
                         <FormGroup row>  
                            <Label for="text" sm={2}>inCity</Label>  
                            <Col sm={10}>  
                                <Input type="text" name="inCity"value={this.state.inCity} onChange={this.onChangeInCity} placeholder="Enter inCity" />  
                            </Col>  
                        </FormGroup>
                        <FormGroup row>  
                            <Label for="text" sm={2}>outCity</Label>  
                            <Col sm={10}>  
                                <Input type="text" name="outCity"value={this.state.outCity} onChange={this.onChangeOutCity} placeholder="Enter outcity" />  
                            </Col>  
                        </FormGroup>
                        <FormGroup row>  
                            <Label for="text" sm={2}>Price</Label>  
                            <Col sm={10}>  
                                <Input type="number" name="Price"value={this.state.price} onChange={this.onChangePrice} placeholder="Enter price" />  
                            </Col>  
                        </FormGroup>
                        <FormGroup row>  
                            <Label for="text" sm={2}>travelTime</Label>  
                            <Col sm={10}>  
                                <Input type="number" name="travelTime"value={this.state.travelTime} onChange={this.onChangetravelTime} placeholder="Enter travelTime" />  
                            </Col>  
                        </FormGroup>
                        <FormGroup row>  
                            <Label for="text" sm={2}>Transit</Label>  
                            <Col sm={10}>  
                                <Input type="number" min={0} max={1} name="Transit"value={Number(this.state.transit)} onChange={this.onChangeTransit} placeholder="Enter Transit" />  
                            </Col>  
                        </FormGroup>              
                    </Col>  
                    <Col>  
                        <FormGroup row>  
                            <Col sm={5}>  
                            </Col>  
                            <Col sm={1}>  
                          <Button type="submit" color="success">Submit</Button>{' '}  
                            </Col>  
                            <Col sm={1}>  
                                <Button type="button" color="danger" onClick={this.onCancel}>Cancel</Button>{' '}  
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
  
export default EditPlane;  