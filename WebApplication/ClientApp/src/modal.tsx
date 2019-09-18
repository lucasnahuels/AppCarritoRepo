import React, { ChangeEvent } from 'react';
import { makeStyles, Theme, createStyles } from '@material-ui/core/styles';
import Modal from '@material-ui/core/Modal';
import { Button } from '@material-ui/core';
import TextField from '@material-ui/core/TextField';
import axios from 'axios';
import { Product } from './entities/product';

export interface ConfirmModalProps {
    show: boolean,
}

function getModalStyle() {
  const top = 28;
  const left = 35;

  return {
    top: `${top}%`,
    left: `${left}%`,
    transform: `translate(-${top}%, -${left}%)`,
  };
}

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    paper: {
      position: 'absolute',
      margin:100,
      width: 400,
      backgroundColor: theme.palette.background.paper,
      border: '1px solid #000',
      boxShadow: theme.shadows[5],
      padding: theme.spacing(2, 4, 3),
    },
  }),
);

const SimpleModal =  ({ show }: ConfirmModalProps) => {

    let productData : Product = new Product; 
    let nameFieldValue : string = "hola"; 
    
    const AddProduct = () => {
        alert(state.nameFieldValue)
        //llenar productData
        // let name = document.getElementById("inputName")!;
        // let price = document.getElementById("inputPrice")!.id;
        // productData.name = name; 
        // productData.price = parseInt(price);
        axios.post('https://localhost:44362/api/v1/product', productData);
    }
    
    const classes = useStyles();
    // getModalStyle is not a pure function, we roll the style only on the first render
    const [modalStyle] = React.useState(getModalStyle);
    const [open, setOpen] = React.useState(false);
    const [state, setState] = React.useState({
        nameFieldValue : ''
    });

    const handleInputChange = (e: ChangeEvent<HTMLInputElement>) => {
        setState({...state, nameFieldValue : e.target.value})
    }

    const handleClose = () => {
        setOpen(false);
    };

    return (
        <div>
        <Modal
            aria-labelledby="simple-modal-title"
            aria-describedby="simple-modal-description"
            open={show}
            onClose={handleClose}
        >
            <div style={modalStyle} className={classes.paper}>
                <h2 id="simple-modal-title">Add modal</h2>
                    <div id="simple-modal-description">
                        <TextField label="Name" id="inputName" name="inputName" placeholder="input the name" value={state.nameFieldValue} onChange={handleInputChange} />
                        <br/><br/>
                        <TextField label="Price" id="inputPrice" name="inputPrice" placeholder="input the price"/>
                        <br/><br/>
                        <Button variant="contained" color="default" onClick={AddProduct} >Save</Button>
                    </div>
            </div>
        </Modal>
        </div>
    );
    }

    export default SimpleModal;