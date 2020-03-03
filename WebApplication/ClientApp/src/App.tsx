import React, { Component } from 'react';
import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import SearchAppBar from './toolbar';
import ProductsList from './products/products-list';

class App extends Component {

  render() {
    return (
      <React.Fragment>
        
        <SearchAppBar />
        <ProductsList />
        
      </React.Fragment>
    );
  }
}
export default App;
