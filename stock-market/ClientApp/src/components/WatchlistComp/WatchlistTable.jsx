import React, { useState } from 'react';
import axios from 'axios';

import { useTheme } from '@mui/material/styles';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Typography from '@mui/material/Typography';
import Button from '@mui/material/Button';
import { styled } from '@mui/material/styles';
import Modal from '@mui/material/Modal';
import TextField from '@mui/material/TextField';
import Box from '@mui/material/Box';

import DeleteOutlineOutlinedIcon from '@mui/icons-material/DeleteOutlineOutlined';
import ModeEditOutlineOutlinedIcon from '@mui/icons-material/ModeEditOutlineOutlined';
import CircularProgress from '@mui/material/CircularProgress';


//Styling the rows

const StyledTableRow = styled(TableRow)(({ theme }) => ({
    //Change color on hover
    '&:hover': {
        backgroundColor: theme.palette.primary.light,
    },
    // hide last border.
    '&:last-child td, &:last-child th': {
        border: 0,
    },
}));


export default function WatchlistTable({ data }) {
    const customTheme = useTheme();

    //Functions to take action to serverSide

    const [isLoading, setIsLoading] = useState(false);

    function deleteStock() {
        setDeleteOpen(false);
        setIsLoading(true);
        axios.get('watchlist/deletestock?id=' + rowId)
            .then((response) => {
                if (response) {
                    window.location.reload();
                    setIsLoading(false);
                }
                else {
                    alert("Something wrong. Ask Erling");
                }
            }).catch(err => {
                if (err.status === 401) {
                    window.location.href = "/login";
                    localStorage.setItem('isLoggedIn', false);
                }
                else {
                    alert("Server error. Sorry try again");
                }
            })
    }

    function addStock() {

        setIsLoading(true);
        axios.get('watchlist/addstock?ticker=' + chosenTicker + "&amount=" + chosenAmount + "&target_price=" + targetPrice)
            .then((response) => {
                if (response) {
                    window.location.reload();
                    setIsLoading(false);
                }
                else {
                    alert("Something wrong. Either stock doesnt exists or you should ask Erling");
                }
            }).catch(err => {
                if (err.status === 401) {
                    window.location.href = "/login";
                    localStorage.setItem('isLoggedIn', false);
                }
                else {
                    setAddOpen(true);
                    setErrTicker("Ticker doesnt exists in database. Try again");
                }
                setIsLoading(false);
            })

    }

    function editStock() {

        setIsLoading(true);
        axios.get('watchlist/updatestock?id=' + rowId + "&amount=" + chosenAmount + "&target_price=" + targetPrice)
            .then((response) => {
                if (response) {
                    window.location.reload();
                    setIsLoading(false);
                }
                else {
                    alert("Something wrong. Ask Erling");
                }
            }).catch(err => {
                if (err.status === 401) {
                    window.location.href = "/login";
                    localStorage.setItem('isLoggedIn', false);
                }
                else {
                    setEditOpen(true);
                    setErrMsgInputNumber("Server error. Try again please");
                }

            })

    }

    //For modals
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

    const [chosenTicker, setChosenTicker] = useState("");
    const [chosenAmount, setChosenAmount] = useState();
    const [targetPrice, setTargetPrice] = useState();

    //Add ticker Modal;
    const [addOpen, setAddOpen] = useState(false);
    const [errTicker, setErrTicker] = useState("");
    const [errMsgInputNumber, setErrMsgInputNumber] = useState("");
    const [errPriceInput, setErrPriceInput] = useState("");
    const [addError, setAddError] = useState("");
    

    const inputTickerChange = (e) => {
        setErrTicker("");
        const ticker = e.target.value.toUpperCase();
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

    const inputPriceChange = (e) => {
        setErrPriceInput("");
        const targetPrice = e.target.value;
        if (checkPrice(targetPrice)) {
            setTargetPrice(targetPrice);
        }
        else {
            setErrPriceInput("Target price must be between 1-999$");
        }
    }
    const checkPrice = (price) => {
        if (/^([1-9]|[1-9][0-9]|[1-9][0-9][0-9])$/.test(price)) {
            return true;
        }
        else {
            return false;
        }
    }

    const confirmAdd = () => {
        
        if (checkTicker(chosenTicker)
            && check0to1000(chosenAmount)
            && checkPrice(targetPrice)
        ) {
            setAddOpen(false);
            addStock();
        }
        else {
            setAddError("Invalid input. Try again");
        }
    }


    //Modal edit
    const [editOpen, setEditOpen] = useState(false);
    const [rowId, setRowId] = useState();

    const confirmEdit = () => {

        if (checkPrice(targetPrice) && check0to1000(chosenAmount)) {
            setEditOpen(false);
            editStock();
        }
        else {
            setErrMsgInputNumber("Invalid input. Try again");
        }
    }

    //Modal Delete
    const [deleteOpen, setDeleteOpen] = useState(false);

    
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
         <Button variant='contained' color='success'
                    onClick={() => setAddOpen(true)}
            sx={{
                float: 'right',
                mt: 1,
                mx: 3
            }}>
            <Typography variant='subtitle1'>
                + Add A Stock to Watchlist
                </Typography>

        </Button>


        <Table sx={{ minWidth: 400 }}
            sx={{
                color: customTheme.palette.primary.contrastText
            }}
        >
            <TableHead>
                <TableRow>
                    <TableCell>
                        <Typography variant='subtitle2' color={customTheme.palette.primary.contrastText}>
                            Ticker
                        </Typography>
                    </TableCell>
                    <TableCell>
                        <Typography variant='subtitle2' color={customTheme.palette.primary.contrastText}>
                            Current Price
                        </Typography>
                    </TableCell>
                    <TableCell>
                        <Typography variant='subtitle2' color={customTheme.palette.primary.contrastText}>
                            Target Price
                        </Typography>
                    </TableCell>
                    <TableCell>
                        <Typography variant='subtitle2' color={customTheme.palette.primary.contrastText}>
                            Amount of Shares
                     </Typography>
                    </TableCell>
                            <TableCell padding='checkbox' />
                            <TableCell padding='checkbox' />
                </TableRow>
            </TableHead>

            <TableBody>
                {data.stocks.map((row) => (
                    <StyledTableRow
                        key={row.id}
                    >
                        <TableCell component="th" scope="row">
                            <Typography variant='body2' color={customTheme.palette.primary.contrastText}>
                                {row.stock.ticker}
                            </Typography>
                        </TableCell>
                        <TableCell align="right">
                            <Typography variant='body2' color={customTheme.palette.primary.contrastText} align='left'>
                                {row.stock.current_price}$
                            </Typography>
                        </TableCell>
                        <TableCell align="right">
                            <Typography variant='body2' color={customTheme.palette.primary.contrastText} align='left'>
                                {row.target_price}$
                            </Typography>
                        </TableCell>
                        <TableCell align="right">
                            <Typography variant='body2' color={customTheme.palette.primary.contrastText} align='left'>
                                {row.amount}
                                </Typography>
                        </TableCell>

                        <TableCell align="right">
                            <Button variant='outlined'
                                color='info'
                                endIcon={<ModeEditOutlineOutlinedIcon />}
                                onClick={() => { setEditOpen(true); setRowId(row.id) }}>
                                Edit
                           </Button>
                        </TableCell>

                        <TableCell align="right">
                            <Button variant='outlined'
                                color='error'
                                endIcon={<DeleteOutlineOutlinedIcon />}
                                onClick={() => { setDeleteOpen(true); setRowId(row.id) }}>
                                Delete
                           </Button>
                        </TableCell>
                    </StyledTableRow>
                ))}
            </TableBody>
                </Table>


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
                            Has to be a valid ticker from <a href='https://iextrading.com/trading/eligible-symbols/'>ticker overview</a>. I.e: GOOGL
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

                        <Box sx={{ display: 'flex', alignItems: 'center' }}>
                            <Typography>Desired amount is </Typography>
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
                                    mx: 1,
                                }}
                            />
                            <Typography> share(s) </Typography>
                        </Box>

                        <Box sx={{ display: 'flex', alignItems: 'center' }}>
                            <Typography>Targetprice($) is </Typography>
                            <TextField
                                id="outlined-number"
                                label="Number"
                                type="number"
                                onChange={inputPriceChange}
                                InputLabelProps={{
                                    shrink: true,
                                }}
                                helperText={errPriceInput}
                                sx={{
                                    width: 100,
                                    mx: 1,
                                }}
                            />
                        </Box>
                        <Typography sx={{color: customTheme.palette.error.main}}>
                            {addError}
                        </Typography>
                        <Button variant="contained" color='success' sx={{
                            width: '55%',
                            bgColor: customTheme.palette.success.main,
                        }}
                            onClick={confirmAdd}
                        >Add the stock</Button>
                    </Box>

                </Modal>

                <Modal
                    open={editOpen}
                    onClose={() => setEditOpen(false)}
                    aria-labelledby="modal-modal-title"
                    aria-describedby="modal-modal-description"
                >
                    <Box sx={modalStyle}
                    >


                        <Typography id="modal-modal-title" variant="h6" component="h2">
                            Edit a watchlist-stock
                        </Typography>
                        <Typography id="modal-modal-title" variant="subtitle1" component="h2">
                            Update your details on watchlist.
                        </Typography>

                        <Box sx={{ display: 'flex', alignItems: 'center' }}>
                            <Typography>New desired amount is </Typography>
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
                                    mx: 1,
                                }}
                            />
                            <Typography> share(s) </Typography>
                        </Box>

                        <Box sx={{ display: 'flex', alignItems: 'center' }}>
                            <Typography>New targetprice($) is </Typography>
                            <TextField
                                id="outlined-number"
                                label="Number"
                                type="number"
                                onChange={inputPriceChange}
                                InputLabelProps={{
                                    shrink: true,
                                }}
                                helperText={errPriceInput}
                                sx={{
                                    width: 100,
                                    mx: 1,
                                }}
                            />
                        </Box>

                        <Button variant="contained" color='success' sx={{
                            width: '55%',
                            bgColor: customTheme.palette.success.main,
                        }}
                            onClick={confirmEdit}
                        >Edit the item</Button>
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
                            Do you want to delete this row?
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
            </>
    )
}
