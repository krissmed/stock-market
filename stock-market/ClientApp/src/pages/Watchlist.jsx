import React, { useEffect } from 'react';
import { TopBar } from '../components/TopBar.jsx';
import WatchlistData from '../fetchingData/WatchlistData';
import { isMobile } from './Layout'
import Container from '@mui/material/Container';
import { Navigate } from 'react-router-dom';

export default function Dashboard() {
    const drawerWidth = isMobile() ? 0 : 220;


    return (
        (localStorage.getItem('isLoggedIn') === 'true')
            ?
        <>
            <TopBar title='Watchlist' />

            <Container sx={{
                ml: drawerWidth + 'px',
                width: 'auto',
            }}>
                <WatchlistData />
            </Container>
            </>

            :

            <Navigate to="/login" />
    );
}



