import React, { Component } from 'react';  
import axios from 'axios';  
import { Link } from 'react-router-dom';  
import RoutesTable from './RoutesTable';
import RoutesPage from './RoutesPage';
class FavoritesTable extends Component {  
  constructor(props) {  
    super(props); 
    console.log(this.props.obj);

    debugger;
    }

    DeleteFavorite= async () =>{
        debugger;  
         await axios.delete('https://localhost:44375/api/favoritesgateway/'+this.props.obj.id)  
        .then(resp => {  
        if(resp.status===200){  
        alert('Record deleted successfully!!');
          window.location.reload()
        }  
        })  
        }
      
  render() {  
    return (  
        <tr>    
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

          <RoutesPage props={this.props}/>

          <td>  
            <button type="button" onClick={this.DeleteFavorite} className="btn btn-danger">Delete</button>  
          </td>  
        </tr>  
    );  
  }  
}  
  
export default FavoritesTable;  