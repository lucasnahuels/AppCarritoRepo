import React, { Component } from 'react';
import './App.css';
import axios from 'axios';
import { Product } from './entities/product';
import 'bootstrap/dist/css/bootstrap.min.css';
import { Button } from '@material-ui/core';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import DialogTitle from '@material-ui/core/DialogTitle';
import SearchAppBar from './toolbar';
import Icon from '@material-ui/core/Icon';
import TextField from '@material-ui/core/TextField';
import DeleteIcon from '@material-ui/icons/Delete';
import UpdateIcon from '@material-ui/icons/Update';
import AddCircleIcon from '@material-ui/icons/AddCircle';

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
    this.setState({buttonName : "Cancel"}); 
    this.setState({isHidden : true});
  }
  
  HideForm(){
    document.getElementById("divAdd")!.style.display = "none"; //The ! means "trust me, this is not a null reference"
    this.setState({buttonName : "Add new product"});
    this.setState({isHidden : false});
  }


  render() {
    return (
      <React.Fragment>
        <SearchAppBar></SearchAppBar>
        <DialogTitle>List of products</DialogTitle>

        <Table size="medium">
          <TableHead>
            <TableRow>
              <TableCell>Name</TableCell>
              <TableCell>Price</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {this.state.listOfProducts.map(product => (
              <TableRow key= {`${product.name}_${product.price}`}>
                <TableCell>{product.name} </TableCell>
                <TableCell>{product.price}</TableCell>
                <TableCell><Button variant="contained" color="default">Update<UpdateIcon/></Button></TableCell>
                <TableCell><Button variant="contained" color="secondary">Delete<DeleteIcon/></Button>  </TableCell>
              </TableRow>
            ))}
            </TableBody>
        </Table>

        <br/>
        <br/>

        {!this.state.isHidden ? (
          <Button variant="contained" color="primary" id="buttonAdd" onClick={() => this.ShowForm()}> {this.state.buttonName} <AddCircleIcon/> </Button> 
          ) : (
          <Button variant="contained" color="primary" id="buttonAdd" onClick={() => this.HideForm()}> {this.state.buttonName} </Button> 
          )
        }

        <br/><br/>
          <div id="divAdd">
              <TextField label="Name" name="inputName" placeholder="input the name" />
              <br/><br/>
              <TextField label="Price" name="inputPrice" placeholder="input the price"/>
              <br/><br/>
              <Button variant="contained" color="default" onClick={this.AddProduct}>Save</Button>
          </div>
      </React.Fragment>
    );
  }
}
export default App;
