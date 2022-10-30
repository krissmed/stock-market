import React from 'react';

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
import DeleteOutlineOutlinedIcon from '@mui/icons-material/DeleteOutlineOutlined';

import { useTheme } from '@mui/material/styles';

const StyledTableRow = styled(TableRow)(({ theme }) => ({
    //Change color on hover
    '&:hover': {
        backgroundColor: theme.palette.primary.light,
    },
    // hide last border.
    '&:last-child td, &:last-child th': {
        border: 0,
    },
    textAlign: {

    }
}));

export default function PortfolioTable({ data }) {
    const customTheme = useTheme();

    if (data.length == 0) {
        return (
            <Box sx={{ textAlign: 'center', mt: 2, height: 500 + 'px' }}>
                <Typography variant='h6' color={customTheme.palette.primary.contrastText}>Your portfolio is empty... </Typography>
            </Box>
        );
    }

    //Inputtig into different array to display seperate
    const buy = [];
    const sell = []

    data.map((item) => {

        //Converting to readable datetime

        item.timestampid = new Date(item.timestampid).toDateString();
        if (item.type == 'BUY') {
            buy.push(item)
        }
        else {
            sell.push(item)
        }
    });

    return (
        <Table sx={{ minWidth: 400 }}
            sx={{
                color: customTheme.palette.primary.contrastText
            }}
        >
            <TableHead>
                <Typography variant='subtitle1' color={customTheme.palette.primary.contrastText}>Your Stocks:</Typography>
            </TableHead>
            <TableHead>
                <TableRow>
                    <TableCell>
                        <Typography variant='subtitle2' color={customTheme.palette.primary.contrastText}>
                            Ticker
                        </Typography>
                    </TableCell>
                    <TableCell>
                        <Typography variant='subtitle2' color={customTheme.palette.primary.contrastText}>
                            Quantity
                        </Typography>
                    </TableCell>
                    <TableCell>
                        <Typography variant='subtitle2' color={customTheme.palette.primary.contrastText}>
                            Price
                        </Typography>
                    </TableCell>
                    <TableCell>
                        <Typography variant='subtitle2' color={customTheme.palette.primary.contrastText}>
                            Sell
                        </Typography>
                    </TableCell>
                </TableRow>
            </TableHead>
            <TableBody>
                {buy.map((row) => (
                    <StyledTableRow
                        key={row.Id}
                    >
                        <TableCell align="right">
                            <Typography variant='body2' color={customTheme.palette.primary.contrastText} align='left'>
                                {row.ticker}
                            </Typography>
                        </TableCell>
                        <TableCell align="right">
                            <Typography variant='body2' color={customTheme.palette.primary.contrastText} align='left'>
                                {row.quantity}
                            </Typography>
                        </TableCell>
                        <TableCell align="right">
                            <Typography variant='body2' color={customTheme.palette.primary.contrastText} align='left'>
                                {row.price} $
                            </Typography>
                        </TableCell>
                        <TableCell align="right">
                            <Typography variant='body2' color={customTheme.palette.primary.contrastText} align='left'>
                                <Button variant='outlined'
                                    color='error'
                                    
                                    endIcon={<DeleteOutlineOutlinedIcon />}
                                    >
                                    Sell
                                </Button>
                            </Typography>
                        </TableCell>
                    </StyledTableRow>
                ))}
            </TableBody>
        </Table>
    )
}
