import React from 'react';
import NavBar from '../components/Nav.jsx';
import { TopBar } from '../components/TopBar.jsx';
import WatchlistData from '../fetchingData/WatchlistData';
import { drawerWidth } from '../components/Nav';


import Container from '@mui/material/Container';

export default function Dashboard() {

    return (
        <>
            <NavBar />
            <TopBar title='Watchlist' />

            <Container sx={{
                ml: drawerWidth + 'px',
                width: 'auto',
            }}>
                <WatchlistData />
            </Container>
        </>
    );
}



