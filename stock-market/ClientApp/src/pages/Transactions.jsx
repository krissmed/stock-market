import React from 'react';
import { TopBar } from '../components/TopBar.jsx';
import { isMobile } from './Layout'
import TransactionData from '../fetchingData/FetchTransactions';

import Container from '@mui/material/Container';

export default function Dashboard() {
    const drawerWidth = isMobile() ? 0 : 220;

    return (
        <>
            <TopBar title='Transactions' />
            <Container sx={{
                ml: drawerWidth + 'px',
                width: 'auto',
            }}>
                <TransactionData />
            </Container>
        </>
    );
}



