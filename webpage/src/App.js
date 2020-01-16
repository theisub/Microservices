import React from 'react';  
import AddBus from './Bus/AddBus';  
import BusList from './Bus/BusList';  
import EditBus from './Bus/EditBus';
import SearchPage from './Search/SearchPage';
import PlanesList from './Plane/PlanesList';
import EditPlane from './Plane/EditPlane';
import Login from "./Login/LoginPage";

  
import { BrowserRouter as Router, Switch, Route, Link } from 'react-router-dom';  
import './App.css';  
import FavoritesPage from './Favorites/FavoritesPage';
import AddPlane from './Plane/AddPlane';
import { func } from 'prop-types';


function App() { 
 
  
  var thing = false;

  var hours = 0.5;
  var now = new Date().getTime();
  var setupTime = localStorage.getItem('setupTime');
  if(setupTime == null){
    localStorage.setItem('setupTime', now)
    debugger;
  }
  else
  {
    debugger;
    localStorage.setItem('setupTime',now);

    if (now-setupTime > hours *60*60*1000)
    {
      localStorage.clear();
      localStorage.setItem('setupTime',now);
      debugger;
    }
  }

  debugger;
  var isAuth = localStorage.getItem("accessToken");
  if (isAuth!= null)
  {
    thing = true;

  }

  debugger;
  function clearAll()
  {
    localStorage.clear();
    window.location.reload();
  }
  if (isAuth)
  { 
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
              <li className={"btn-group pull-right " + (thing ? 'show' : 'hidden')}>  
              <button onClick={clearAll} > Logout</button>  
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
  else
  {
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
                <li className={"btn-group pull-right " + (thing ? 'show' : 'hidden')}>  
                  <Link to={'/login'} className="nav-link">Login</Link>  
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
            
            <Route path="/login" exact component={Login} />

      
          </Switch>  
        </div>  
      </Router>  
    );


  }  
}  
  
export default App;  