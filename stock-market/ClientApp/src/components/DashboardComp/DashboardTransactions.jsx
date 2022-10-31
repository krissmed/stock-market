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
import axios from 'axios';
import { useState } from 'react';

export default function DashboardTransactions() {
    const [transactions, setTransactions] = useState([]);

    React.useEffect(() => {
        axios.get("transaction/listall")
            .then(res => {
                setTransactions(res.data);
                }
        )
    }, [])

    const customTheme = useTheme();
    return (
        <TableContainer
            component={Paper}
            sx={{
                backgroundColor: customTheme.palette.primary.main
            }}
        >
            <Table sx={{ minWidth: 100 }} aria-label="simple table">
                <TableHead>
                    <TableRow>
                        <TableCell sx={{ color: customTheme.palette.primary.contrastText }}><Typography variant='h6'>Date</Typography></TableCell>
                        <TableCell sx={{ color: customTheme.palette.primary.contrastText }}><Typography variant='h6'>Ticker</Typography></TableCell>
                        <TableCell sx={{ color: customTheme.palette.primary.contrastText }}><Typography variant='h6'>Amount</Typography></TableCell>
                        <TableCell sx={{ color: customTheme.palette.primary.contrastText }}><Typography variant='h6'>Price</Typography></TableCell>
                        <TableCell sx={{ color: customTheme.palette.primary.contrastText }}><Typography variant='h6'>Type</Typography></TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {transactions.map((row) => (
                        <TableRow
                            key={row.id}
                        >
                            <TableCell component="th" scope="row" sx={{ color: customTheme.palette.primary.contrastText }}>
                                {row.timestamp.time}
                            </TableCell>
                            <TableCell sx={{ color: customTheme.palette.primary.contrastText }}>{row.ticker}</TableCell>
                            <TableCell sx={{ color: customTheme.palette.primary.contrastText }}>{row.quantity}</TableCell>
                            <TableCell sx={{ color: customTheme.palette.primary.contrastText }}>{row.price}</TableCell>
                            <TableCell sx={{ color: customTheme.palette.primary.contrastText }}>{row.type}</TableCell>
                        </TableRow>
                    ))}
                </TableBody>
            </Table>
        </TableContainer>
    );
}