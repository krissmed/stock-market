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

    function deleteStock(id) {
        setIsLoading(true);
        axios.get('watchlist/deletestock?id=' + id)
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
        const chosenTicker = prompt("Enter the ticker of the stock you want to add. Has to be a legit ticker from https://iextrading.com/trading/eligible-symbols/", 'AAPL').toUpperCase();
        const desiredAmount = prompt("What is your desired amount of " + chosenTicker + "?", 10);
        const targetValue = prompt("At what stock value do you want an alert?", 85);

        setIsLoading(true);
        axios.get('watchlist/addstock?ticker=' + chosenTicker + "&amount=" + desiredAmount + "&target_price=" + targetValue)
            .then((response) => {
                if (response) {
                    window.location.reload();
                    setIsLoading(false);
                }
                else {
                    alert("Something wrong. Either stock doesnt exists or you should ask Erling");
                }
            })

    }

    function editStock(id) {
        const desiredAmount = prompt("What is your new desired amount of?", 20);
        const targetValue = prompt("At what stock value do you want an alert?", 55);

        setIsLoading(true);
        axios.get('watchlist/updatestock?id=' + id + "&amount=" + desiredAmount + "&target_price=" + targetValue)
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
            onClick={() => addStock()}
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
                                onClick={() => editStock(row.id)}>
                                Edit
                           </Button>
                        </TableCell>

                        <TableCell align="right">
                            <Button variant='outlined'
                                color='error'
                                endIcon={<DeleteOutlineOutlinedIcon />}
                                onClick={() => deleteStock(row.id)}>
                                Delete
                           </Button>
                        </TableCell>
                    </StyledTableRow>
                ))}
            </TableBody>
            </Table>
            </>
    )
}
