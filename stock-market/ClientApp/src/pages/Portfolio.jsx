import React, { useEffect } from 'react';
import { TopBar } from '../components/TopBar.jsx';
import FetchPortfolio from '../fetchingData/FetchPortfolio';
import { isMobile } from './Layout'
import { Navigate } from 'react-router-dom';

import Container from '@mui/material/Container';

export default function Dashboard() {
    const drawerWidth = isMobile() ? 0 : 220;

    return (
        (localStorage.getItem('isLoggedIn') === 'true')
            ?
        <>
            <TopBar title='Portfolio' />
            <Container sx={{
                ml: drawerWidth + 'px',
                width: 'auto',
            }}>
                <FetchPortfolio />
            </Container>
            </>
            :
            <Navigate to="/login" />
    );
}



