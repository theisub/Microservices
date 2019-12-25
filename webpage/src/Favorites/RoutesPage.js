import React, {Component} from 'react';
import axios from 'axios';
import FavoritesTable from './FavoritesTable';
import RoutesTable from './RoutesTable';

export default class RoutesPage extends Component{

    constructor(props) {  
        super(props);
        debugger;
        console.log(this.props.props.obj.busesRoute);
        debugger;
      }  
    
      
      tabRowBuses(){
          return this.props.props.obj.busesRoute.map(function(object,i){
              return <RoutesTable obj={object} key={i} />;
          });
      }
      tabRowPlanes(){
          return this.props.props.obj.planesRoute.map(function(object,i){
              return <RoutesTable obj={object} key={i} />;
          });
      }
        
        
    
      render() {  
        return (  
          <div>  
            <h4 align="center">Bus List</h4>  
            <table className="table table-striped" style={{ marginTop: 10 }}>  
              <thead className="thead-dark">  
                <tr>              
                  <th>CompanyName</th>  
                  <th>Price</th>  
                  <th>traveltime</th>  
                  <th>transit</th>
                </tr>  
              </thead>  
              <tbody>  
               { this.tabRowBuses() }   
              </tbody>  
            </table>
            
            <h4 align="center">Plane List</h4>  
            <table className="table table-striped" style={{ marginTop: 10 }}>  
              <thead className="thead-dark">  
                <tr>              
                  <th>CompanyName</th>  
                  <th>Price</th>  
                  <th>traveltime</th>  
                  <th>transit</th>
                </tr>  
              </thead>  
              <tbody>  
               { this.tabRowPlanes() }   
              </tbody>  
            </table>
          </div>
         
            
        );  
      }  
    }  