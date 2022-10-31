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
import axios from 'axios';
import { useState } from 'react';



export default function DashboardWatchlist() {
    const [watchlist, setWatchlist] = useState([]);

    React.useEffect(() => {
        const fetchData = async () => {
            const wList = await axios.get("watchlist/getfullwatchlist");

            setWatchlist(wList.data);
                
        }
        fetchData();
    }, [])

    const customTheme = useTheme();
    return (
        <>
        <TableContainer
            component={Paper}
            sx={{
                backgroundColor: customTheme.palette.primary.main
            }}
        >
            <Table sx={{ minWidth: 100 }} aria-label="simple table">
                <TableHead>
                    <TableRow>
                        <TableCell sx={{ color: customTheme.palette.primary.contrastText }}><Typography variant='h6'>Ticker</Typography></TableCell>
                        <TableCell sx={{ color: customTheme.palette.primary.contrastText }}><Typography variant='h6'>Current Price</Typography></TableCell>
                        <TableCell sx={{ color: customTheme.palette.primary.contrastText }}><Typography variant='h6'>Target Price</Typography></TableCell>
                    </TableRow>
                </TableHead>
                    <TableBody>
                        {watchlist.stocks.length == 0
                            ?
                            <TableRow><Typography variant='subtitle1'>No stock added to watchlist</Typography></TableRow>
                            :
                        <>
                            {watchlist.stock.map(row => (
                                    <TableRow
                                        key={row.ticker}
                                    >
                                        <TableCell component="th" scope="row" sx={{ color: customTheme.palette.primary.contrastText }}>
                                            {row.ticker}
                                        </TableCell>
                                        <TableCell sx={{ color: customTheme.palette.primary.contrastText }}>{row.current_price}</TableCell>
                                        <TableCell sx={{ color: customTheme.palette.primary.contrastText }}>{row.target_price}</TableCell>
                                    </TableRow>
                                ))
                            }
                        </>
                    }
                </TableBody>
            </Table>
            </TableContainer>
            </>
    );
}