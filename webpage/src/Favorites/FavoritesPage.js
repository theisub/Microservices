import React, {Component} from 'react';
import axios from 'axios';
import FavoritesTable from './FavoritesTable';

export default class FavoritesPage extends Component{

    constructor(props) {  
        super(props);  
        this.state = {business: [], current:1, dataCount:1,pageSize:5};  
      }  
      componentDidMount(){  
        axios.get('https://localhost:44357/api/favorites')  
          .then(response => {  
            this.setState({ business: response.data, dataCount: response.data.length});  
            //debugger;  
          })  
          .catch(function (error) {  
            console.log(error);  
          })  
          debugger;
      }
      
      tabRow(){  
        return this.state.business.map(function(object, i){  
            return <FavoritesTable obj={object} key={i}  />;  
        });  
        
      }  
    
      render() {  
        return (  
          <div>  
            <h4 align="center">Favorites List</h4>  
            <table className="table table-striped" style={{ marginTop: 10 }}>  
              <thead className="thead-dark">  
                <tr>              
                  <th>inCountry</th>  
                  <th>outCountry</th>  
                  <th>inCity</th>  
                  <th>outCity</th>
                  <th width="10%">Routes</th>
                  <th colSpan="5">Action</th>  
                </tr>  
              </thead>  
              <tbody>  
               { this.tabRow() }   
              </tbody>  
            </table>
          </div>
         
            
        );  
      }  
    }  