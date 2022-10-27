import React from 'react';  
import { useState } from 'react';
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
import KeyboardArrowDownIcon from '@mui/icons-material/KeyboardArrowDown';
import KeyboardArrowUpIcon from '@mui/icons-material/KeyboardArrowUp';
import AddShoppingCartOutlinedIcon from '@mui/icons-material/AddShoppingCartOutlined';
import DeleteOutlineOutlinedIcon from '@mui/icons-material/DeleteOutlineOutlined';
import CircularProgress from '@mui/material/CircularProgress';



//Object to store variables showed. Fetched data;


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
                    <TableCell padding="checkbox" />

                    <TableCell colSpan="5">
                        <StockGraphData ticker={curStock.ticker} />
                    </TableCell>
                    <TableCell padding="checkbox" />

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

    function buyStock(ticker) {
        const amount = prompt("Please enter desired amount to buy", 1);

        setIsLoading(true);
        axios.get('transaction/buystock?ticker=' + ticker + '&amount=' + amount)
            .then((response) => {
                if (response) {
                    alert('Successfully bought ' + amount + ' shares of ' + ticker);
                }
                else {
                    alert('You dont have enough money...;/');
                }
                setIsLoading(false);
            })
    }

    function deleteStock(ticker) {
        setIsLoading(true);
        axios.get('stock/deletestock?ticker=' + ticker)
            .then((response) => {
                if (response) {
                    window.location.reload();
                    setIsLoading(false);  
                }
                else {
                    alert("Something wrong. Ask Erling");
                }
            })
    }

    function addStock() {
        const chosenTicker = prompt("Enter the ticker of the stock you want to add", 'GOOGL').toUpperCase();

        setIsLoading(true);
        axios.get('stock/addstock?ticker=' + chosenTicker)
            .then((response) => {
                if (response) {
                    window.location.reload();
                    setIsLoading(false);
                }
                else {
                    alert("Something wrong. Ask Erling");
                }
            })
    }

    const customTheme = useTheme();

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

    <Box sx={{
        width: 'auto',
        overflowX: 'auto',

    }}>
        <Button variant='contained' color='success'
            onClick={() => addStock()}
            sx={{
                float: 'right',
                mt: 1,
                mx: 3
            }}>
            <Typography variant='subtitle1'>
                + Add Stock
                </Typography>

        </Button>
        <Table aria-label='Table with all Stocks'>
            <TableHead>
                <TableRow>
                    <TableCell padding='checkbox' />
                    <TableCell align="right">
                        <Typography variant='h6'
                            color={customTheme.palette.primary.contrastText}> Name </Typography>
                    </TableCell>
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

                        <TableCell align="right">
                            <Button variant='outlined'
                                color='success'
                                endIcon={<AddShoppingCartOutlinedIcon />}
                                onClick={() => buyStock(stock.ticker)}>
                                Buy

                                </Button>
                        </TableCell>

                        <TableCell align="right">
                            <Button variant='outlined'
                                color='error'
                                endIcon={<DeleteOutlineOutlinedIcon />}
                                onClick={() => deleteStock(stock.ticker)}>
                                Delete
                                </Button>
                        </TableCell>

                    </ExpandableRows>
                ))}

            </TableBody>
        </Table>
    </Box>

    );
}
