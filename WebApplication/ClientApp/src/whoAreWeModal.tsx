import React from 'react';
import Modal from '@material-ui/core/Modal';
import { makeStyles, Theme, createStyles } from '@material-ui/core/styles';

export interface ConfirmModalProps {
    show: boolean,
    hideModal: Function,
}
/*export default*/
const WhoAreWeModal = ({ show, hideModal }: ConfirmModalProps) => {

    const Button = require('@material-ui/core/Button').default;

    const [modalStyle] = React.useState(getModalStyle);

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
        }
      })
    );

    const classes = useStyles();

    const handleClose = () => {
        hideModal();
    };

    return(
        <div>
            <Modal
                aria-labelledby='simple-modal-title'
                aria-describedby='simple-modal-description'
                open={show}
                onClose={handleClose}
            >
                <div style={modalStyle} className={classes.paper}>
                    <h4 id='simple-modal-title'>Who are we?</h4>
                    <div id='simple-modal-description'>
                        {/* tslint:disable-next-line: no-multi-spaces*/}
                        <p> We are a supermarket chain that blah blah blah</p>  <br/>
                        <Button onClick={handleClose}> Close </Button>
                    </div>
                </div>
            </Modal>
        </div>
    )
}

export default WhoAreWeModal;
