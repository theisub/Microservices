import React from 'react';  
import AddBus from './Bus/AddBus';  
import BusList from './Bus/BusList';  
import EditBus from './Bus/EditBus';
import SearchPage from './Search/SearchPage';
  
import { BrowserRouter as Router, Switch, Route, Link } from 'react-router-dom';  
import './App.css';  
function App() {  
  return (  
    <Router>  
      <div className="container">  
        <nav className="navbar navbar-expand-lg navheader">  
          <div className="collapse navbar-collapse" >  
            <ul className="navbar-nav mr-auto">  
              <li className="nav-item">  
                <Link to={'/AddBus'} className="nav-link">AddBus</Link>  
              </li>  
              <li className="nav-item">  
                <Link to={'/Buslist'} className="nav-link">Bus List</Link>  
              </li>
              <li className="nav-item">  
                <Link to={'/search'} className="nav-link"> Search routes</Link>  
              </li>   
            </ul>  
          </div>  
        </nav> <br />  
        <Switch>  
          <Route exact path='/Addbus' component={AddBus} />  
          <Route path='/Buslist' component={BusList} />
          <Route path='/search' component={SearchPage} />
          <Route path='/:id' component={EditBus} />    
        </Switch>  
      </div>  
    </Router>  
  );  
}  
  
export default App;  