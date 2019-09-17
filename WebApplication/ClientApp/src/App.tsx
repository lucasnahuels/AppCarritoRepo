import React, { Component } from 'react';
import logo from './logo.svg';
import './App.css';
import axios from 'axios';
import { Product } from './entities/product';
import 'bootstrap/dist/css/bootstrap.min.css';
// import { Button } from 'react-bootstrap';
// import Button from 'react-bootstrap/Button';
import { Button } from '@material-ui/core';
import Icon from '@material-ui/core/Icon';

interface IState {
 listOfProducts : Product[];
 buttonName : string;
 isHidden : boolean;
}

interface IProps {
}

class App extends Component<IProps, IState> {

  private productData : Product = new Product; 

  constructor(props : IProps){
    super(props)
    this.state = {
      listOfProducts : [],
      buttonName : "Add new product",
      isHidden : false
    }
  }

  componentDidMount() {
    axios.get('https://localhost:44362/api/v1/product')
      .then(response => {
        this.setState({listOfProducts: response.data})
      });
  }

  AddProduct(){
    //llenar productData
    axios.post('https://localhost:44362/api/v1/product', this.productData)
  }

  ShowForm(){
    document.getElementById("divAdd")!.style.display = "block"; //The ! means "trust me, this is not a null reference"
    this.setState({buttonName : "Hide form"}); 
    this.setState({isHidden : true});
  }
  
  HideForm(){
    document.getElementById("divAdd")!.style.display = "none"; //The ! means "trust me, this is not a null reference"
    this.setState({buttonName : "Add new product"});
    this.setState({isHidden : false});
  }


  render() {
    return (
      <div>
        <h3 className="text-center info">List of products</h3>
          <table className="text-center table table-dark">    
              <thead>    
              <tr>
                {/* <th>Id</th> */}
                <th>Name</th>
                <th>Price</th>
              </tr>  
              </thead> 
              <tbody>
              {
                this.state.listOfProducts.map(product => (
                  <tr key= {`${product.name}_${product.price}`}> 
                {/* <td>{product.productId} </td> */}
                    <td >{product.name} </td>
                    <td> {product.price} </td>
                    <td> <Button variant="contained" color="default"> Update</Button> </td>
                    <td> <Button variant="contained" color="secondary"> Delete </Button></td>
                  </tr>
                ))           
              }
              </tbody>
          </table>

          <br/>
          <br/>

          {!this.state.isHidden ? (
            <Button variant="contained" color="primary" id="buttonAdd" onClick={() => this.ShowForm()}> {this.state.buttonName} </Button> 
            ) : (
            <Button variant="contained" color="primary" id="buttonAdd" onClick={() => this.HideForm()}> {this.state.buttonName} </Button> 
            )
          }

          <br/><br/>
            <div id="divAdd">
                <input type="text" name="inputName" placeholder="input the name" />
                <br/><br/>
                <input type="text" name="inputPrice" placeholder="input the price"/>
                <br/><br/>
                <button id="botonF1" onClick={this.AddProduct}>Save</button>
            </div>
      </div>
    );
  }
}
export default App;

