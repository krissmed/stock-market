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


    function createData(name, price) {
        return { name, price};
    }

    const rows = [
        createData('APPL', 159),
        createData('TSLA', 237),
        createData('AMAZ', 262),
        createData('ABCD', 305),
        createData('APPL', 356),
    ];

export default function DashboardStocks() {
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
                        {rows.map((row) => (
                            <TableRow
                                key={row.name}
                                sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
                            >
                                <TableCell component="th" scope="row" sx={{ color: customTheme.palette.primary.contrastText }}>
                                    {row.name}
                                </TableCell>
                                <TableCell sx={{ color: customTheme.palette.primary.contrastText }}>{row.price}</TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>
        );
    }