import React, { useEffect } from 'react';
import { TopBar } from '../components/TopBar.jsx';
import { isMobile } from './Layout'
import TransactionData from '../fetchingData/FetchTransactions';
import { Navigate } from 'react-router-dom';

import Container from '@mui/material/Container';

export default function Dashboard() {
    const drawerWidth = isMobile() ? 0 : 220;

    return (
        (localStorage.getItem('isLoggedIn') === 'true')
            ?
        <>
            <TopBar title='Transactions' />
            <Container sx={{
                ml: drawerWidth + 'px',
                width: 'auto',
            }}>
                <TransactionData />
            </Container>
            </>

            :

            <Navigate to="/login" />
    );
}



