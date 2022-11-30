import React, { useState } from 'react';  
import axios from 'axios';

import StockGraphData from "../../fetchingData/StockGraphData";

import {useTheme} from '@mui/material/styles';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Box from '@mui/material/Box';
import Button from '@mui/material/Button';
import Typography from '@mui/material/Typography';
import IconButton from '@mui/material/IconButton';
import Modal from '@mui/material/Modal';
import TextField from '@mui/material/TextField';

import KeyboardArrowDownIcon from '@mui/icons-material/KeyboardArrowDown';
import KeyboardArrowUpIcon from '@mui/icons-material/KeyboardArrowUp';
import AddShoppingCartOutlinedIcon from '@mui/icons-material/AddShoppingCartOutlined';
import DeleteOutlineOutlinedIcon from '@mui/icons-material/DeleteOutlineOutlined';
import CircularProgress from '@mui/material/CircularProgress';




//Customized to enable expandable row when icon is clicked

const ExpandableRows = ({ children, curStock, ...otherArgs }) => {

    //Hook to determine if expanded and set it to expanded or not;
    const [isExpanded, setIsExpanded] = useState(false);
    const customTheme = useTheme();

    return (
        <>


            <TableRow {...otherArgs}>

                {children}

                <TableCell padding='checkbox'>
                    <IconButton sx={{
                        color: customTheme.palette.primary.contrastText,
                        padding: 3,
                        fontSize: 10
                    }}
                        onClick={() => setIsExpanded(!isExpanded)}>
                        {isExpanded ?
                            <KeyboardArrowUpIcon sx={{ fontSize: 35 }} />
                            : <KeyboardArrowDownIcon sx={{ fontSize: 35 }} />}
                    </IconButton>
                </TableCell>
            </TableRow>

            {isExpanded && (
                <TableRow>
                    <TableCell colSpan="5">
                        <StockGraphData ticker={curStock.ticker} />
                    </TableCell>

                </TableRow>
            )}


        </>
    );
}



//Component to list out the table with the data

export default function StockTable({ stockObj }) {
    //To check if loading
    const [isLoading, setIsLoading] = useState(false);

    //Functions to execute the buttons

    function buyStock() {
        setIsLoading(true);
        axios.get('transaction/buystock?ticker=' + chosenTicker + '&amount=' + chosenAmount)
            .then((response) => {
                if (response) {
                    window.location.reload();
                }
                
                setIsLoading(false);
            }).catch(err => {
                if (err.response.status === 401) {
                    window.location.href = "/login";
                    localStorage.setItem('isLoggedIn', false);
                }
                //If not enough money

                else {
                    alert('You dont have enough money...;/');
                }
                setIsLoading(false);
            })

        
    }

    function deleteStock() {
        setDeleteOpen(false);
        setIsLoading(true);
        axios.get('stock/deletestock?ticker=' + chosenTicker)
            .then((response) => {
                if (response) {
                    window.location.reload();
                    setIsLoading(false);  
                }
                else {
                    alert("Something wrong. Ask Erling");
                }
            }).catch(err => {
                if (err.response.status === 401) {
                    window.location.href = "/login";
                    localStorage.setItem('isLoggedIn', false);
                }
                setIsLoading(false);
            })
        
    }

    function addStock() {

        setIsLoading(true);
        axios.get('stock/addstock?ticker=' + chosenTicker)
            .then((response) => {
                if (response) {
                    window.location.reload();
                    setIsLoading(false);
                }
            }).catch(err => {
                if (err.status === 401) {
                    window.location.href = "/login";
                    localStorage.setItem('isLoggedIn', false);
                }
                else {
                    setAddOpen(true);
                    setErrTicker("The ticker didnt exists. Check link")
                }
                setIsLoading(false);
            })
    }

    const customTheme = useTheme();
    const modalStyle = {
        position: 'absolute',
            top: '50%',
                left: '50%',
                    transform: 'translate(-50%, -50%)',
                        maxWidth: 400,
                            minHeight: 200,
                                bgcolor: '#FEFEFE',
                                    color: '#252525',
                                        border: '2px solid #252525',
                                            borderRadius: '5%',
                                                boxShadow: 24,
                                                    p: 4,
                                                        display: 'flex',
                                                            flexDirection: 'column',
                                                                justifyContent: 'center',
                                                                    gap: 2,

                    }
    
    ///For modal buy;
    const [open, setOpen] = useState(false);
    const [chosenTicker, setChosenTicker] = useState("");
    const [chosenAmount, setChosenAmount] = useState();
    const [errMsgInputNumber, setErrMsgInputNumber] = useState("");

    //For modal delete
    const [deleteOpen, setDeleteOpen] = useState(false);

    //For modal add
    const [addOpen, setAddOpen] = useState(false);
    const [errTicker, setErrTicker] = useState("");

    const handleClose = () => {
        setOpen(false);
    }

    const inputChange = (e) => {
        setErrMsgInputNumber("");
        const value = e.target.value;
        if (check0to1000(value)) {
            setChosenAmount(e.target.value);
        }
        else {
            setErrMsgInputNumber("Has to be larger than 0 and less than 1000");
        }
    }

    const check0to1000 = (value) => {
        if (value > 0 && value < 1000) {
            return true
        }
        else {
            return false
        }
    }

    const confirmBuy = () => {
        setOpen(false);
        if (check0to1000(chosenAmount)) {
            buyStock();
        }
        else {
            setErrMsgInputNumber("Invalid input. Try again");
        }
    }

    const inputTickerChange = (e) => {
        const ticker = e.target.value.toUpperCase();
        console.log(ticker);
        if (checkTicker(ticker)) {
            setChosenTicker(ticker);
        }
        else {
            setErrTicker("Only between 1 and 10 letters allowed");
        }
    }
    const checkTicker = (ticker) => {
        if (/^([A-Z]{1,10})$/.test(ticker)) {
            return true;
        }
        else {
            return false;
        }
    }
    const confirmAdd = () => {
        if (checkTicker(chosenTicker)) {
            addStock();
        }
        else {
            setErrTicker("Invalid input. Try again");
        }
    }

    return (
        
            isLoading
            ?

            <div
                style={{
                    display: "flex",
                    justifyContent: "center",
                    alignItems: 'center'
                }}>
                <CircularProgress color="success" />
            </div>

                :
            <>
    <Box sx={{
        width: 'auto',
        overflowX: 'auto',
    }}>
                    <Button variant='contained' color='success'
                        onClick={() => setAddOpen(true)}
            sx={{
                float: 'right',
                mt: 1,
                mx: 3
            }}>
            <Typography variant='subtitle1'>
                + Add Stock
                </Typography>

        </Button>
                <Table aria-label='Table with all Stocks' >
            <TableHead>
                <TableRow>
                    <TableCell>
                        <Typography variant='h6'
                                    color={customTheme.palette.primary.contrastText}> Name </Typography>

                            </TableCell>
                            <TableCell padding='checkbox' />

                    <TableCell align="right" sx={{ textAlign: 'center' }}>
                        <Typography variant='h6' color={customTheme.palette.primary.contrastText}>Value</Typography>
                    </TableCell>
                    <TableCell padding='checkbox' />
                    <TableCell padding='checkbox' />
                    <TableCell padding="checkbox" />
                </TableRow>
            </TableHead>

            <TableBody>
                        {stockObj.map(stock => (

                            <ExpandableRows
                                key={stock.ticker}
                                curStock={stock}
                            >
                                <TableCell sx={{ maxWidth: 2 }}>
                                    <Typography variant='body1' color={customTheme.palette.primary.contrastText}>
                                        {stock.ticker}
                                    </Typography>
                                </TableCell>

                                <TableCell align="right">
                                    <Typography variant='body1' color={customTheme.palette.primary.contrastText}>
                                        {stock.name}
                                    </Typography>
                                </TableCell>

                                <TableCell align="right" sx={{ textAlign: 'center' }}>
                                    <Typography variant='body1' color={customTheme.palette.primary.contrastText}>
                                        {stock.current_price} $

                                    </Typography>
                                </TableCell>

                                <TableCell>
                                    <Button variant='outlined'
                                        color='success'
                                        endIcon={<AddShoppingCartOutlinedIcon />}
                                        onClick={() => { setOpen(true)
                                                        setChosenTicker(stock.ticker)}}>
                                        Buy
                                    </Button>
                                </TableCell>
                                
                                <TableCell align="right">
                                    <Button variant='outlined'
                                        color='error'
                                        endIcon={<DeleteOutlineOutlinedIcon />}
                                        onClick={() => {
                                            setDeleteOpen(true)
                                            setChosenTicker(stock.ticker)
                                        }}>
                                        Delete
                                    </Button>
                                </TableCell>

                            </ExpandableRows>
                        ))}
                    </TableBody>
        </Table>
    </Box>


                <Modal
                    open={open}
                    onClose={handleClose}
                    aria-labelledby="modal-modal-title"
                    aria-describedby="modal-modal-description"
                >
                    <Box sx={modalStyle}
                    >


                        <Typography id="modal-modal-title" variant="h6" component="h2">
                            Choose amount of shares
                        </Typography>
                        <Box sx={{display: 'flex', alignItems: 'center'}}>
                        <TextField
                            id="outlined-number"
                            label="Number"
                            type="number"
                                onChange={inputChange}
                            InputLabelProps={{
                                shrink: true,
                            }}
                                helperText={errMsgInputNumber}
                            sx={{
                                width: 100,
                                mr: 1,
                            }}
                            />
                            <Typography>share(s) of {chosenTicker}</Typography>
                        </Box>
                        <Button variant="contained" sx={{
                            width: '60%',
                        }}
                            onClick={confirmBuy}
                        >Buy {chosenTicker}</Button>
                    </Box>

                </Modal>

                <Modal
                    open={deleteOpen}
                    onClose={() => setDeleteOpen(false)}
                    aria-labelledby="modal-modal-title"
                    aria-describedby="modal-modal-description"
                >
                    <Box sx={modalStyle}
                    >


                        <Typography id="modal-modal-title" variant="h6" component="h2">
                            Do you want to delete {chosenTicker}?
                        </Typography>
                        <Box sx={{ display: 'flex', alignItems: 'center', justifycontent: 'space-between' }}>
                            <Button variant="contained" sx={{
                                width: '20%',
                                mr: 'auto',
                            }}
                                color='error'
                                onClick={() => setDeleteOpen(false)}
                            >No</Button>
                            <Button variant="contained" color='success' sx={{
                                width: '20%',
                                bgColor: customTheme.palette.success.main,
                            }}
                                onClick={deleteStock}
                            >Yes</Button>
                        </Box>
                        
                    </Box>

                </Modal>

                <Modal
                    open={addOpen}
                    onClose={() => setAddOpen(false)}
                    aria-labelledby="modal-modal-title"
                    aria-describedby="modal-modal-description"
                >
                    <Box sx={modalStyle}
                    >


                        <Typography id="modal-modal-title" variant="h6" component="h2">
                            Add a stock
                        </Typography>
                        <Typography id="modal-modal-title" variant="subtitle1" component="h2">
                            Has to be a valid ticker from <a href='https://iextrading.com/trading/eligible-symbols/'>ticker overview</a>. I.e: GOOGL, MSFT
                        </Typography>
                        <TextField required
                            id='outlined-required'
                            label='Ticker'
                            autoComplete='off'
                            color='info'
                            helperText={errTicker}
                            name='ticker'
                            onChange={inputTickerChange}
                        />

                        <Button variant="contained" color='success' sx={{
                            width: '55%',
                            bgColor: customTheme.palette.success.main,
                        }}
                            onClick={addStock}
                        >Add the stock</Button>
                    </Box>

                </Modal>

            </>
    );
}
