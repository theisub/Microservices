import React, { Component } from 'react';  
import axios from 'axios';  
import Table from './Table';
import Pagination from 'rc-pagination';
import 'rc-pagination/assets/index.css';


  
export default class BusList extends Component {  
  
  constructor(props) {  
      super(props);  
      this.state = {business: [], current:1, dataCount:1,pageSize:5};  
    }  
    componentDidMount(){  
      axios.get('https://localhost:44331/api/buses/companies/FrenchTour?pageSize='+this.state.pageSize+'&pageNum='+this.state.current)  
        .then(response => {  
          this.setState({ business: response.data, dataCount: response.data.length});  
          //debugger;  
        })  
        .catch(function (error) {  
          console.log(error);  
        })  
    }
    
    
    onChange = async (page) => {
      console.log(page);
      this.state.current = page;
      debugger;
      await axios.get('https://localhost:44331/api/buses/companies/FrenchTour?pageSize='+this.state.pageSize+'&pageNum='+this.state.current)  
        .then(response => {  
          this.setState({ business: response.data, dataCount: response.data.length});  
          //debugger;  
        })  
        .catch(function (error) {  
          console.log(error);  
        })  
    }  
      
    tabRow(){  
      return this.state.business.map(function(object, i){  
          return <Table obj={object} key={i}  />;  
      });  
    }  
  
    render() {  
      return (  
        <div>  
          <h4 align="center">Bus List</h4>  
          <table className="table table-striped" style={{ marginTop: 10 }}>  
            <thead className="thead-dark">  
              <tr>  
                <th>busCompany</th>  
                <th>inCountry</th>  
                <th>outCountry</th>  
                <th>inCity</th>  
                <th>outCity</th>
                <th>Price</th>
                <th>travelTime</th>
                <th>Transit</th>  
                <th colSpan="5">Action</th>  
              </tr>  
            </thead>  
            <tbody>  
             { this.tabRow() }   
            </tbody>  
          </table>
          <Pagination onChange={this.onChange} current={this.state.current} total={50} />;
        </div>
       
          
      );  
    }  
  }  