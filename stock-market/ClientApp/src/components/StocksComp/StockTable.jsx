import React from 'react';
import { useState } from 'react';
import axios from 'axios';

import StockGraph from "./StockGraph";

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

//Object to store variables showed. Fetched data;

//Customized to enable expandable row when icon is clicked
const ExpandableRows = ({ children, curStock, ...otherArgs}) => {

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
                            <KeyboardArrowUpIcon sx={{fontSize: 35}} />
                            : <KeyboardArrowDownIcon sx={{fontSize: 35}}/>}
                    </IconButton>
                </TableCell>
            </TableRow>

            {isExpanded && (
                <TableRow>
                    <TableCell padding="checkbox"/>
                    <TableCell colSpan="5">
                        <StockGraph stock={curStock}/>
                    </TableCell>
                    <TableCell padding="checkbox"/>
                </TableRow>
            )}


        </>
    );
}

//Functions to execute the buttons

function buyStock(ticker) {
    const amount = prompt("Please enter desired amount to buy", 1);

    axios.get('transaction/buystock?ticker=' + ticker + '&amount=' + amount)
        .then((response) => {
            if (response) {
                alert('Successfully bought ' + amount + ' shares of ' + ticker);
            }
            else {
                alert('You dont have enough money...;/');
            }
        })
}

function deleteStock(ticker) {
    axios.get('stock/deletestock?ticker=' + ticker)
        .then((response) => {
            if (response) {
                window.location.reload();
            }
        })
}

//Component to list out the table with the data

export default function StockTable({ stockObj }) {

    const customTheme = useTheme();

    return (
        <Box sx={{
            width: 'auto',
            overflowX: 'auto',

        }}>
            <Button variant='contained' color='success'
                    onClick={() => alert('Here you can add another stock')}
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
                        <TableCell padding='checkbox'/>
                        <TableCell align="right">
                            <Typography variant='h6'
                                        color={customTheme.palette.primary.contrastText}> Name </Typography>
                        </TableCell>
                        <TableCell align="right" sx={{textAlign:'center'}}>
                            <Typography variant='h6' color={customTheme.palette.primary.contrastText}>Value</Typography>
                        </TableCell>
                        <TableCell padding='checkbox'/>
                        <TableCell padding='checkbox'/>
                        <TableCell padding="checkbox"/>
                    </TableRow>
                </TableHead>

                <TableBody>
                    {stockObj.map(stock => (

                        <ExpandableRows
                            key={stock.ticker}
                            curStock={stock}
                        >
                            <TableCell sx={{maxWidth: 2}}>
                                <Typography variant='body1' color={customTheme.palette.primary.contrastText}>
                                    {stock.ticker}
                                </Typography>
                            </TableCell>

                            <TableCell align="right">
                                <Typography variant='body1' color={customTheme.palette.primary.contrastText}>
                                    {stock.name}
                                </Typography>
                            </TableCell>

                            <TableCell align="right" sx={{textAlign:'center'}}>
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
