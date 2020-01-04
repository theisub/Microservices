import React, { Component } from 'react';  
import axios from 'axios';  
import { Link } from 'react-router-dom';  
class PlaneTable extends Component {  
  constructor(props) {  
    super(props);  
    }  
      
    
     DeletePlane= async () =>{
     await axios.delete('https://localhost:44375/api/planesgateway/'+this.props.obj.id)  
    .then(resp => { 
    console.log(resp.status); 
    if(resp.status==202){  
    alert('Record deleted successfully!!');
      window.location.reload()
    }  
    else
    {
      console.log(resp.status);  
      debugger;
      alert("Record didn't delete." + resp.statusText + ". Status code: " + resp.status);  
    }  
    }).catch(function (error) { 
      alert(error + " Response code: " + error.response.status);
      debugger;
      console.log(error);  
    })    
    }
    
  render() {  
    return (  
        <tr>  
          <td>  
            {this.props.obj.planeCompany}  
          </td>  
          <td>  
            {this.props.obj.inCountry}  
          </td>  
          <td>  
            {this.props.obj.outCountry}  
          </td>  
          <td>  
            {this.props.obj.inCity}  
          </td>
          <td>  
            {this.props.obj.outCity}  
          </td>
          <td>  
            {this.props.obj.price}  
          </td>  
          <td>  
            {this.props.obj.travelTime}  
          </td>  
          <td>  
            {Number(this.props.obj.transit)}  
          </td>    
          <td>  
          <Link to={"/editPlane/"+this.props.obj.id} className="btn btn-success">Edit</Link>  
          </td>  
          <td>  
            <button type="button" onClick={this.DeletePlane} className="btn btn-danger">Delete</button>  
          </td>  
        </tr>  
    );  
  }  
}  
  
export default PlaneTable;  