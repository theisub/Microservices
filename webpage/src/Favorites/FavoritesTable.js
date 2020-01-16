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
         var token = localStorage.getItem('accessToken');
  
         await axios.delete('https://localhost:44375/api/favoritesgateway/'+this.props.obj.id,{headers:{'Authorization':"Bearer " + token}})  
        .then(resp => {  
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