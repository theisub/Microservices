import React, { Component } from 'react';  
import axios from 'axios';  
import { Link } from 'react-router-dom';  
class Table extends Component {  
  constructor(props) {  
    super(props);  
    }  
      
    
     DeleteBus= async () =>{
    debugger;
    var token = localStorage.getItem('accessToken');

     await axios.delete('https://localhost:44375/api/busesGateway/'+this.props.obj.id,{headers:{'Authorization':"Bearer " + token}})  
    .then(resp => { 
      debugger; 
    if(resp.status===202){  
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
    
  render() {  
    return (  
        <tr>  
          <td>  
            {this.props.obj.busCompany}  
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
          <Link to={"/editBus/"+this.props.obj.id} className="btn btn-success">Edit</Link>  
          </td>  
          <td>  
            <button type="button" onClick={this.DeleteBus} className="btn btn-danger">Delete</button>  
          </td>  
        </tr>  
    );  
  }  
}  
  
export default Table;  