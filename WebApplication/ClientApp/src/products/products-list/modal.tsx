import React, { ChangeEvent, useEffect } from 'react';
import { makeStyles, Theme, createStyles } from '@material-ui/core/styles';
import Modal from '@material-ui/core/Modal';
import { Button, Container, List, ListItemText, ListItem, FormControl, InputLabel, Select, MenuItem } from '@material-ui/core';
import TextField from '@material-ui/core/TextField';
import axios from 'axios';
import { IProduct } from '../product'; //.. para ir para atras
import { ToastsStore, ToastsContainer, ToastsContainerPosition } from 'react-toasts';
import { Category } from '../../categories/category';

export interface ConfirmModalProps {
    show: boolean,
    hideModal: Function,
    getAllProducts: Function,
    productId?: number | undefined
}

export interface ICategoryList {
    categoryList: Category[]
}

function getModalStyle () {
  const top = 28;
  const left = 35;

  return {
    top: `${top}%`,
    left: `${left}%`,
    transform: `translate(-${top}%, -${left}%)`
  };
}

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    paper: {
      position: 'absolute',
      margin: 100,
      width: 400,
      backgroundColor: theme.palette.background.paper,
      border: '1px solid #000',
      boxShadow: theme.shadows[5],
      padding: theme.spacing(2, 4, 3)
    },
    root: {
        display: 'flex',
        flexWrap: 'wrap'
    },
    formControl: {
        margin: theme.spacing(1),
        minWidth: 120
    },
    selectEmpty: {
        marginTop: theme.spacing(2)
    }
  })
);

const SimpleModal = ({ show, hideModal, getAllProducts, productId }: ConfirmModalProps) => {

    const classes = useStyles();
    // getModalStyle is not a pure function, we roll the style only on the first render
    const [modalStyle] = React.useState(getModalStyle);
    const [state, setState] = React.useState({
        nameFieldValue : '',
        priceFieldValue : 0
    });
    const [categoryNames, setCategoryNames] = React.useState<ICategoryList>();
    const [values, setValues] = React.useState({
        categoryId : 0,
        categoryName: ''
    });
    const [isUpdate, setIsUpdate] = React.useState(false)

    const fetchCategories = async () => {
        const response = await axios.get(`https://localhost:44362/api/v1/category`);

        setCategoryNames({ ...categoryNames, categoryList: response.data });
    };

    // tslint:disable-next-line: no-floating-promises
    useEffect(() => { fetchCategories() }, []); // el segundo parametro es el then

    const AddProduct = () => {
        let productData: IProduct = { name: state.nameFieldValue, price: state.priceFieldValue };
            axios.post('https://localhost:44362/api/v1/product', productData).then(() => {
            handleClose();
            ToastsStore.success('The product was saved');
            getAllProducts();
        }).catch(() => {
            ToastsStore.error('The product was not saved');
        })
    }

    const UpdateProduct = async (productId: number) => {
        let productData: IProduct = { name: state.nameFieldValue, price: state.priceFieldValue };
        let productToUpdate: IProduct
        // await productToUpdate = axios.get('https://localhost:44362/api/v1/product/' + productId.toString()); //esto esta mal.. al ser porque es una promise
        const response = await axios.get('https://localhost:44362/api/v1/product/' + productId.toString());

        await axios.put('https://localhost:44362/api/v1/product/' + productId.toString(), productData);
    }

    const handleInputNameChange = (e: ChangeEvent<HTMLInputElement>) => {
        setState({ ...state, nameFieldValue : e.target.value }); // : asignacion
    }
    const handleInputPriceChange = (e: ChangeEvent<HTMLInputElement>) => {
        // tslint:disable-next-line: radix
        setState({ ...state, priceFieldValue : parseInt(e.target.value) }); // : asignacion // POR QUE LAS LLAVES?
    }

    const handleClose = () => {
        hideModal();
    };

    const handleChangeSelect = (event: React.ChangeEvent<{ name?: string; value: unknown }>) => {
        setValues((oldValues: any) => ({
            ...oldValues,
            [event.target.name as string]: event.target.value
        }));
    };

    return (
        <div>
            <ToastsContainer position={ToastsContainerPosition.TOP_RIGHT} store={ToastsStore}/>
        <Modal
            aria-labelledby='simple-modal-title'
            aria-describedby='simple-modal-description'
            open={show}
            onClose={handleClose}
        >
            <div style={modalStyle} className={classes.paper}>
                <h2 id='simple-modal-title'>Add product</h2>
                    <div id='simple-modal-description'>
                        <TextField label='Name' id='inputName' name='inputName' placeholder='input the name' value={state.nameFieldValue} onChange={handleInputNameChange} />
                        <br/><br/>
                        <TextField label='Price' id='inputPrice' name='inputPrice' placeholder='input the price' value={state.priceFieldValue} onChange={handleInputPriceChange}/>
                        <br/><br/>
                        <form className={classes.root} autoComplete='off'>
                            <FormControl className={classes.formControl}>
                                <InputLabel htmlFor='category-select'>Category</InputLabel>
                                <Select
                                    value={values.categoryId}
                                    onChange={handleChangeSelect}
                                    inputProps={{
                                        name: 'categoryId', //le pasa el id al que tiene el nombre de name
                                        id: 'category-select'
                                    }}
                                >

                                    {categoryNames !== undefined && categoryNames.categoryList !== undefined ? categoryNames.categoryList.map(category => (
                                        <MenuItem value={category.categoryId} key={category.categoryId}
                                        >{category.name}
                                        </MenuItem>
                                    ))
                                    : null}
                                </Select>
                            </FormControl>
                        </form>
                        <br /><br />
                        <Button variant='contained' color='default' onClick={handleClose} >Cancel</Button> {/*en el momento del click y manda el elemento como parametro por defecto.. Si fuera handleClose(), el onClick estaria esperando lo que le retorna esa funcion (x ej. una llamada a aotra funcion)*/ }

                        {!isUpdate ? (
                            <Button variant='contained' color='default' onClick={() => AddProduct()} >Save</Button>
                        ) : (
                            <Button variant='contained' color='default' onClick={() => UpdateProduct(productId!)} >Save</Button>
                            )
                        }
                    </div>
            </div>
        </Modal>
        </div>
    );
    }

export default SimpleModal
