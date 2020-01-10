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
import AddPlane from './Plane/AddPlane';


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
                <Link to={'/AddPlane'} className="nav-link">AddPlane</Link>  
              </li>   
              <li className="nav-item">  
                <Link to={'/Buslist'} className="nav-link">Bus List</Link>  
              </li>
              <li className="nav-item">  
                <Link to={'/Planeslist'} className="nav-link">Planes List</Link>  
              </li>
              <li className="nav-item">  
                <Link to={'/Searchpage'} className="nav-link">Search</Link>  
              </li>
              <li className="nav-item">  
                <Link to={'/favorites'} className="nav-link"> Favorites</Link>  
              </li>      
            </ul>  
          </div>  
        </nav> <br />  
        <Switch>  
          <Route exact path='/Addbus' component={AddBus} />  
          <Route exact path='/AddPlane' component={AddPlane}/>
          <Route path='/Buslist' component={BusList} />
          <Route path='/Planeslist' component={PlanesList} />

          <Route path='/Searchpage' component={SearchPage} />
          <Route path='/favorites' component={FavoritesPage} />

          <Route path='/editBus/:id' component={EditBus} />
          <Route path='/editPlane/:id' component={EditPlane} />

    
        </Switch>  
      </div>  
    </Router>  
  );  
}  
  
export default App;  