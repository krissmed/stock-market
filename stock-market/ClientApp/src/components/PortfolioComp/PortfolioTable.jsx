import React, { useState } from 'react';

import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Typography from '@mui/material/Typography';
import Box from '@mui/material/Box';
import { styled } from '@mui/material/styles';
import Button from '@mui/material/Button';
import IconButton from '@mui/material/IconButton';
import KeyboardArrowDownIcon from '@mui/icons-material/KeyboardArrowDown';
import KeyboardArrowUpIcon from '@mui/icons-material/KeyboardArrowUp';
import { useTheme } from '@mui/material/styles';
import axios from 'axios';



const StyledTableRow = styled(TableRow)(({ theme }) => ({
    //Change color on hover
    '&:hover': {
        backgroundColor: theme.palette.primary.light,
    },
    // hide last border.
    '&:last-child td, &:last-child th': {
        border: 0,
    }
}));

function sellStock(ticker, amount) {
    axios.get("transaction/sellstock?ticker=" + ticker + "&amount=" + amount)
        .then(res => {
            alert("Sold " + amount + " of " + ticker);
            window.location.reload();
        }).catch(err => {
            if (err.status === 401) {
                window.location.href = "/login";
                localStorage.setItem('isLoggedIn', false);
            }
        })
}

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
                
                curStock.map(stock => (
                    <>
                        <Box sx={{
                            color: customTheme.palette.primary.contrastText,
                            textAlign: 'center',
                            width: '100',
                            mt: 2
                        }}
                            key={stock.id}
                        >
                            <Typography variant='h6'
                                color={customTheme.palette.primary.contrastText}>
                                Your Stocks
                             </Typography>
                        </Box>
                        <Table key={stock.id}>
                            <TableHead>
                                <TableRow>
                                    <TableCell>
                                        <Typography variant='subtitle1'
                                        color={customTheme.palette.primary.contrastText}>
                                            Ticker
                                            </Typography>
                                    </TableCell>
                                    <TableCell>
                                        <Typography variant='subtitle1'
                                        color={customTheme.palette.primary.contrastText}>
                                            Current Price
                                            </Typography>
                                    </TableCell>
                                    <TableCell>
                                        <Typography variant='subtitle1'
                                        color={customTheme.palette.primary.contrastText}>
                                            Amount of Shares
                                         </Typography>
                                    </TableCell>
                                    <TableCell></TableCell>
                                </TableRow>
                            </TableHead>

                            <TableBody>
                                <TableRow>
                                    <TableCell>
                                        <Typography variant='body1'
                                        color={customTheme.palette.primary.contrastText}>
                                            { stock.historical.baseStock.ticker }
                                            </Typography>
                                    </TableCell>
                                    <TableCell>
                                        <Typography variant='body1'
                                        color={customTheme.palette.primary.contrastText}>
                                            { stock.historical.price }$
                                            </Typography>
                                    </TableCell>
                                    <TableCell>
                                        <Typography variant='body1'
                                        color={customTheme.palette.primary.contrastText}>
                                            { stock.count}
                                            </Typography>
                                    </TableCell>
                                    <TableCell align='right' sx={{ textAlign: 'center' }}>
                                        <Button variant='outlined'
                                            color='success'
                                            onClick={() => sellStock(stock.historical.baseStock.ticker, stock.count)}
                                        >
                                            Sell
                                        </Button>
                                    </TableCell>
                                </TableRow>
                            </TableBody>
                        </Table>
                        </>
                    ))
            )}


        </>
    );
}


export default function PortfolioTable({ portfolio }) {
    const customTheme = useTheme();

    

    return (
        <Box sx={{
            width: 'auto',
            overflowX: 'auto',
        }}>
            <Table aria-label='PortfolioTable' >
                <TableHead>
                    <TableRow>
                        <TableCell>
                            <Typography variant='h6'
                                color={customTheme.palette.primary.contrastText}> Your Total Value </Typography>

                        </TableCell>

                        <TableCell align="right" sx={{ textAlign: 'center' }}>
                            <Typography variant='h6' color={customTheme.palette.primary.contrastText}>
                                In Stock Value
                            </Typography>
                        </TableCell>
                        <TableCell align="right" sx={{ textAlign: 'center' }}>
                            <Typography variant='h6' color={customTheme.palette.primary.contrastText}>
                                In Liquid Value
                            </Typography>
                        </TableCell>
                        <TableCell padding='checkbox' />
                    </TableRow>
                </TableHead>

                <TableBody>

                    <ExpandableRows
                        key={portfolio.id}
                            curStock={portfolio.stock_counter}
                        >
                            <TableCell sx={{ maxWidth: 2 }}>
                            <Typography variant='body1' color={customTheme.palette.primary.contrastText}>
                                {portfolio.total_value.toFixed(2)}$
                                </Typography>
                            </TableCell>

                        <TableCell align="right" sx={{ textAlign: 'center' }}>
                            <Typography variant='body1' color={customTheme.palette.primary.contrastText}>
                                {portfolio.stock_value.toFixed(2)}$
                                </Typography>
                            </TableCell>

                            <TableCell align="right" sx={{ textAlign: 'center' }}>
                            <Typography variant='body1' color={customTheme.palette.primary.contrastText}>
                                {portfolio.liquid_value.toFixed(2)}$

                                    </Typography>
                        </TableCell>

                        </ExpandableRows>
                </TableBody>
            </Table>
        </Box>
    )
}
