import * as React from 'react';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';
import { useTheme, styled } from '@mui/material/styles';
import Typography from '@mui/material/Typography'


function createData(date, name, amount, price) {
    return { date, name, amount, price };
}

const rows = [
    createData('18.10.22', 'APPL', 10, 100),
    createData('18.10.22', 'TSLA', 5, 134),
];

export default function DashboardTransactions() {
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
                        <TableCell sx={{ color: customTheme.palette.primary.contrastText }}><Typography variant='h6'>Name</Typography></TableCell>
                        <TableCell sx={{ color: customTheme.palette.primary.contrastText }}><Typography variant='h6'>Amount</Typography></TableCell>
                        <TableCell sx={{ color: customTheme.palette.primary.contrastText }}><Typography variant='h6'>Price</Typography></TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {rows.map((row) => (
                        <TableRow
                            key={row.name}
                        >
                            <TableCell component="th" scope="row" sx={{ color: customTheme.palette.primary.contrastText }}>
                                {row.date}
                            </TableCell>
                            <TableCell sx={{ color: customTheme.palette.primary.contrastText }}>{row.name}</TableCell>
                            <TableCell sx={{ color: customTheme.palette.primary.contrastText }}>{row.amount}</TableCell>
                            <TableCell sx={{ color: customTheme.palette.primary.contrastText }}>{row.price}</TableCell>
                        </TableRow>
                    ))}
                </TableBody>
            </Table>
        </TableContainer>
    );
}