import React from 'react';
// tslint:disable-next-line: no-duplicate-imports
import { useEffect } from 'react';
import axios from 'axios';
import { IProduct } from '../product';
import 'bootstrap/dist/css/bootstrap.min.css';
import { Button } from '@material-ui/core';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import DialogTitle from '@material-ui/core/DialogTitle';
import DeleteIcon from '@material-ui/icons/Delete';
import AddCircleIcon from '@material-ui/icons/AddCircle';
import EditIcon from '@material-ui/icons/Edit';
import { makeStyles, Theme, createStyles } from '@material-ui/core/styles';
import SimpleModal from './modal';

export interface IProductList {
  listOfProduct: IProduct[]
}

const ProductsList = () => {
  const useStyles = makeStyles((theme: Theme) =>
        createStyles({
          dialogTitle: {
            float: 'left'
          },
          buttonAdd: {
            float: 'right',
            margin: '15px',
            marginRight: '25px'
          },
          paper: {
            position: 'absolute',
            margin: 100,
            width: 400,
            backgroundColor: theme.palette.background.paper,
            border: '1px solid #000',
            boxShadow: theme.shadows[5],
            padding: theme.spacing(2, 4, 3)
          }
        })
    );

  const classes = useStyles();
  const[stateProduct, setProduct] = React.useState<IProductList>();
  const[buttonName, setButtonName] = React.useState('Add new product');
  const[showModal, setShowModal] = React.useState(false)

  const fetchProducts = async () => {
    const response = await axios.get(`https://localhost:44362/api/v1/product`);
    setProduct({ ...stateProduct, listOfProduct: response.data });
  };

     // tslint:disable-next-line: no-floating-promises
  useEffect(() => { fetchProducts() }, []);

  const ShowForm = (isEdit: boolean, productId?: number | undefined) => {
    setButtonName('Cancel');
    setShowModal(true);
    if (isEdit) {
      <SimpleModal show={showModal} hideModal={HideForm} getAllProducts={fetchProducts} productId={productId}/>
    }
  }

  const HideForm = () => {
    setButtonName('Add new product');
    setShowModal(false);
  }

    return(
        <div>
            <div>
                <DialogTitle className={classes.dialogTitle}> List of products</DialogTitle>
                <Button variant='contained' color='primary' className={classes.buttonAdd} onClick={() => ShowForm(false)}> {buttonName} <AddCircleIcon/> </Button>
            </div>

            <Table size='medium'>
                <TableHead>
                <TableRow>
                    <TableCell size='medium'>Name</TableCell>
                    <TableCell size='medium'>Price</TableCell>
                    <TableCell size='medium'>Actions</TableCell>
                </TableRow>
                </TableHead>
                <TableBody>
                    {stateProduct !== undefined && stateProduct.listOfProduct !== undefined ? stateProduct.listOfProduct.map(product => (
                        <TableRow key= {`${product.productId}`}>
                            <TableCell>{product.name} </TableCell>
                            <TableCell>{product.price}</TableCell>
                            <TableCell>
                                <Button variant='contained' color='default' onClick={() => ShowForm(true, product.productId)}><EditIcon/></Button> {/*! means trust me, is not undefined */}
                                <Button variant='contained' color='secondary'><DeleteIcon/></Button>
                            </TableCell>
                        </TableRow>
                    )) : null}
                </TableBody>
            </Table>

            <SimpleModal show={showModal} hideModal={HideForm} getAllProducts={fetchProducts}/>
        </div>
    )
}

export default ProductsList
