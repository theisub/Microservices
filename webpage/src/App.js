import React from 'react';  
import AddBus from './Bus/AddBus';  
import BusList from './Bus/BusList';  
import EditBus from './Bus/EditBus';
import SearchPage from './Search/SearchPage';
import PlanesList from './Plane/PlanesList';
import EditPlane from './Plane/EditPlane';
  
import { BrowserRouter as Router, Switch, Route, Link } from 'react-router-dom';  
import './App.css';  
import FavoritesPage from './Favorites/FavoritesPage';


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
                <Link to={'/Planeslist'} className="nav-link">Planes List</Link>  
              </li>
              <li className="nav-item">  
                <Link to={'/search'} className="nav-link"> Search routes</Link>  
              </li>
              <li className="nav-item">  
                <Link to={'/favorites'} className="nav-link"> Favorites</Link>  
              </li>      
            </ul>  
          </div>  
        </nav> <br />  
        <Switch>  
          <Route exact path='/Addbus' component={AddBus} />  
          <Route path='/Buslist' component={BusList} />
          <Route path='/Planeslist' component={PlanesList} />

          <Route path='/search' component={SearchPage} />
          <Route path='/favorites' component={FavoritesPage} />

          <Route path='/editBus/:id' component={EditBus} />
          <Route path='/editPlane/:id' component={EditPlane} />

    
        </Switch>  
      </div>  
    </Router>  
  );  
}  
  
export default App;  