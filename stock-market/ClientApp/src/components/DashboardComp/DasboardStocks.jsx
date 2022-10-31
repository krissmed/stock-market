import * as React from 'react';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';
import { useTheme, styled } from '@mui/material/styles';
import Typography from '@mui/material/Typography';
import { useEffect, useState } from 'react';
import CircularProgress from '@mui/material/CircularProgress';
import axios from 'axios';


export default function DashboardStocks() {

    const [isLoading, setIsLoading] = useState(true);

    const [stocks, setStocks] = useState([]);

    useEffect(() => {

        const fetchData = async () => {


            const stocks = await axios.get("stock/getstocks");

            setStocks(stocks.data);

            setIsLoading(false)
        }
        fetchData();


    }, []);

    const customTheme = useTheme();
    return (
        isLoading == true ?
            <>
                <div
                    style={{
                        display: "flex",
                        justifyContent: "center",
                        alignItems: 'center'
                    }}>
                    <CircularProgress color="success" />
                </div>
            </>
            :
            <TableContainer
                sx={{
                    backgroundColor: customTheme.palette.primary.main
                } }
            >
                <Table sx={{ minWidth: 100 }} aria-label="simple table">
                    <TableHead>
                        <TableRow>
                            <TableCell sx={{ color: customTheme.palette.primary.contrastText }}><Typography variant='h6'>Ticker</Typography></TableCell>
                            <TableCell sx={{ color: customTheme.palette.primary.contrastText }}><Typography variant='h6'>Current Price</Typography></TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {stocks.map((row) => (
                            <TableRow
                                key={row.id}
                                sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
                            >
                                <TableCell component="th" scope="row" sx={{ color: customTheme.palette.primary.contrastText }}>
                                    {row.ticker}
                                </TableCell>
                                <TableCell sx={{ color: customTheme.palette.primary.contrastText, textAlign: 'center' }}>{row.current_price}</TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>
        );
    }