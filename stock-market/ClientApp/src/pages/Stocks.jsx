import React, { useEffect } from 'react';

import { Navigate } from 'react-router-dom';

import { TopBar } from '../components/TopBar.jsx';
import StocksData from '../fetchingData/StocksData';
 
import { isMobile } from './Layout'
import Container from '@mui/material/Container';
import { useTheme } from '@mui/material/styles';



export default function Dashboard() {

    const customTheme = useTheme();
    const drawerWidth = isMobile() ? '0px' : '220px';

    return (
        (localStorage.getItem('isLoggedIn') === 'true')
            ?
                <>
                    <TopBar title='All Stocks' />
                    <Container sx={{
                        ml: drawerWidth,
                        width: 'auto',
                    }}>
                        <StocksData />
                    </Container>
                </>
            :

            <Navigate to="/login" />
        
    );
}



