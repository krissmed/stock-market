import * as React from 'react';
import { useState } from 'react';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';
import { useTheme, styled } from '@mui/material/styles';
import Typography from '@mui/material/Typography'
import axios from 'axios';


export default function DashboardStocks() {
    const [stocks, setStocks] = useState([]);

    React.useEffect(() => {
        axios.get("stock/getstocks")
            .then(res => {
                setStocks(res.data)
            })
    }, [])

    console.log()

    const customTheme = useTheme();
        return (
            <TableContainer
                sx={{
                    backgroundColor: customTheme.palette.primary.main
                } }
            >
                <Table sx={{ minWidth: 100 }} aria-label="simple table">
                    <TableHead>
                        <TableRow>
                            <TableCell sx={{ color: customTheme.palette.primary.contrastText }}><Typography variant='h6'>Name</Typography></TableCell>
                            <TableCell sx={{ color: customTheme.palette.primary.contrastText }}><Typography variant='h6'>Value</Typography></TableCell>
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
                                <TableCell sx={{ color: customTheme.palette.primary.contrastText }}>{row.current_price}</TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>
        );
    }