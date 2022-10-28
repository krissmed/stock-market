import React from 'react';

import { TopBar } from '../components/TopBar.jsx';
import StockTable from '../components/StocksComp/StockTable';
import isMobile from '../components/RespNav'
import Container from '@mui/material/Container';
import { useTheme } from '@mui/material/styles';



export default function Dashboard() {
    const drawerWidth = isMobile() ? 0 : 220;
    const customTheme = useTheme();

    return (
        <>
            <TopBar title='All Stocks' />

            <Container sx={{
                ml: drawerWidth+'px',
                width: 'auto',
            }}>
                <StockTable />
            </Container>
        </>
    );
}



