import React, { Component } from 'react';  
import axios from 'axios';  
import { Link } from 'react-router-dom';  
class RoutesTable extends Component {  
  constructor(props) {  
    super(props);
    debugger;
    console.log(this.props.obj);
    debugger; 
    }  
      
  render() {  
    return (  
        <tr>    
          <td>  
            {this.props.obj.companyName}  
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
    
        </tr>  
    );  
  }  
}  
  
export default RoutesTable;  