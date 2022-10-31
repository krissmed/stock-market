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


function createData(name, curPrice, tarPrice, amount) {
    return { name, curPrice, tarPrice, amount };
}

const rows = [
    createData('APPL', 159, 160, 10),
    createData('TSLA', 237, 240, 10),
    createData('AMAZ', 262, 270, 10),
    createData('ABCD', 305, 310, 10),
    createData('APPL', 356, 360, 10),
];

export default function DashboardWatchlist() {
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
                        <TableCell sx={{ color: customTheme.palette.primary.contrastText }}><Typography variant='h6'>Name</Typography></TableCell>
                        <TableCell sx={{ color: customTheme.palette.primary.contrastText }}><Typography variant='h6'>Current Price</Typography></TableCell>
                        <TableCell sx={{ color: customTheme.palette.primary.contrastText }}><Typography variant='h6'>Target Price</Typography></TableCell>
                        <TableCell sx={{ color: customTheme.palette.primary.contrastText }}><Typography variant='h6'>Amount</Typography></TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {rows.map((row) => (
                        <TableRow
                            key={row.name}
                        >
                            <TableCell component="th" scope="row" sx={{ color: customTheme.palette.primary.contrastText }}>
                                {row.name}
                            </TableCell>
                            <TableCell sx={{ color: customTheme.palette.primary.contrastText }}>{row.curPrice}</TableCell>
                            <TableCell sx={{ color: customTheme.palette.primary.contrastText }}>{row.tarPrice}</TableCell>
                            <TableCell sx={{ color: customTheme.palette.primary.contrastText }}>{row.amount}</TableCell>
                        </TableRow>
                    ))}
                </TableBody>
            </Table>
        </TableContainer>
    );
}