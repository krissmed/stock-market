import React from 'react';
import {useState} from 'react';

import StockGraph from "./StockGraph";
import isMobile from '../../pages/Layout'

import {useTheme} from '@mui/material/styles';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Box from '@mui/material/Box';
import Button from '@mui/material/Button';
import Typography from '@mui/material/Typography';

import IconButton from '@mui/material/IconButton';
import KeyboardArrowDownIcon from '@mui/icons-material/KeyboardArrowDown';
import KeyboardArrowUpIcon from '@mui/icons-material/KeyboardArrowUp';
import AddShoppingCartOutlinedIcon from '@mui/icons-material/AddShoppingCartOutlined';
import DeleteOutlineOutlinedIcon from '@mui/icons-material/DeleteOutlineOutlined';


//Object to store variables showed
export const stockObj = [
    {
        ticker: 'AAPL',
        name: 'Apple',
        value: 143.39,
        history: [140, 145, 148, 169, 170, 120, 140, 142, 141, 141, 145]

    },
    {
        ticker: 'TSLA',
        name: 'Tesla',
        value: 143.39,
        history: [140, 145, 148, 169, 170, 120, 140, 142, 141, 141, 145]

    },
    {
        ticker: 'MCR',
        name: 'Microsoft',
        value: 143.39,
        history: [140, 145, 148, 169, 170, 120, 140, 142, 141, 141, 145]
    },
    {
        ticker: 'NTFX',
        name: 'Netflix',
        value: 143.39,
        history: [140, 145, 148, 169, 170, 120, 140, 142, 141, 141, 145]
    },
    {
        ticker: 'AMZN',
        name: 'Amazon',
        value: 143.39,
        history: [140, 145, 148, 169, 170, 120, 140, 142, 141, 141, 145]
    },
    {
        ticker: 'ADB',
        name: 'Adobe',
        value: 143.39,
        history: [140, 145, 148, 169, 170, 120, 140, 142, 141, 141, 145]
    },
    {
        ticker: 'TSC',
        name: 'Tesco',
        value: 143.39,
        history: [140, 145, 148, 169, 170, 120, 140, 142, 141, 141, 145]
    },
    {
        ticker: 'KP',
        name: 'Kiloprice',
        value: 143.39,
        history: [140, 145, 148, 169, 170, 120, 140, 142, 141, 141, 145]
    },
    {
        ticker: 'VDF',
        name: 'Vodafone',
        value: 143.39,
        history: [140, 145, 148, 169, 170, 120, 140, 142, 141, 141, 145]
    },
];

//Customized to enable expandable row when icon is clicked
const ExpandableRows = ({children, curStock, ...otherArgs}) => {

    //Hook to determine if expanded and set it to expanded or not
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
                            <KeyboardArrowUpIcon sx={{fontSize: 35}} />
                            : <KeyboardArrowDownIcon sx={{fontSize: 35}}/>}
                    </IconButton>
                </TableCell>
            </TableRow>

            {isExpanded && (
                <TableRow>
                    <TableCell colSpan="5">
                        <StockGraph stock={curStock}/>
                    </TableCell>
                </TableRow>
            )}


        </>
    );
}

//Functions to execute the buttons
function buyStock(ticker) {
    alert('You bought ' + ticker);
}

function updateStock(ticker) {
    alert('You updated ' + ticker);
}

function deleteStock(ticker) {
    alert('You deleted ' + ticker);
}

//Component to list out the table with the data
export default function StockTable() {

    const customTheme = useTheme();

    return (
        <Box sx={{
            width: 'auto',
            overflowX: 'auto',

        }}>
            <Button variant='contained' color='success'
                    onClick={() => alert('Here you can add another stock')}
                    sx={{
                        float: 'right',
                        mt: 1,
                        mx: 3
                    }}>
                <Typography variant='subtitle1'>
                    + Add Stock
                </Typography>
            </Button>
            <Table aria-label='Table with all Stocks'>
                <TableHead>
                    <TableRow>
                        <TableCell>
                            <Typography variant='h6'
                                color={customTheme.palette.primary.contrastText}> Name </Typography>
                        </TableCell>
                        <TableCell>
                            <Typography variant='h6'
                                        color={customTheme.palette.primary.contrastText}> Full Name </Typography>
                        </TableCell>
                        <TableCell>
                            <Typography variant='h6' color={customTheme.palette.primary.contrastText}>Value</Typography>
                        </TableCell>
                        <TableCell padding='checkbox'/>
                        <TableCell padding='checkbox'/>
                        <TableCell padding='checkbox'/>
                    </TableRow>
                </TableHead>

                <TableBody>
                    {stockObj.map(stock => (
                        <ExpandableRows
                            key={stock.ticker}
                            curStock={stock}
                        >
                            <TableCell >
                                <Typography variant='body1' color={customTheme.palette.primary.contrastText}>
                                    {stock.ticker}
                                </Typography>
                            </TableCell>

                            <TableCell>
                                <Typography variant='body1' color={customTheme.palette.primary.contrastText}>
                                    {stock.name}
                                </Typography>
                            </TableCell>

                            <TableCell>
                                <Typography variant='body1' color={customTheme.palette.primary.contrastText}>
                                    {stock.value}$
                                </Typography>
                            </TableCell>

                            <TableCell>
                                {isMobile() ?
                                    <Button variant='outlined'
                                        color='success'
                                        onClick={() => buyStock(stock.ticker)}>
                                        <AddShoppingCartOutlinedIcon />
                                    </Button>
                                    :
                                    <Button variant='outlined'
                                        color='success'
                                        endIcon={<AddShoppingCartOutlinedIcon />}
                                        onClick={() => buyStock(stock.ticker)}>
                                        Buy
                                    </Button>
                                }
                            </TableCell>
                            <TableCell align="right">
                            {isMobile() ? 
                                <Button
                                    variant='outlined'
                                    color='error'
                                    onClick={() => deleteStock(stock.ticker)}>
                                    <DeleteOutlineOutlinedIcon />
                                 </Button>
                                :
                                <Button 
                                    variant='outlined'
                                    color='error'
                                    endIcon={<DeleteOutlineOutlinedIcon />}
                                    onClick={() => deleteStock(stock.ticker)}>
                                    Delete
                                 </Button>
                                }
                            </TableCell>

                        </ExpandableRows>
                    ))}

                </TableBody>
            </Table>
        </Box>
    );
}
