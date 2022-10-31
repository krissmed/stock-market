import React from 'react';
import NavBar from '../components/Nav.jsx';
import { TopBar } from '../components/TopBar.jsx';
import { drawerWidth } from '../components/Nav';
import FetchPortfolio from '../fetchingData/FetchPortfolio';

import Container from '@mui/material/Container';

export default function Dashboard() {


    return (
        <>
            <NavBar />
            <TopBar title='Portfolio' />
            <Container sx={{
                ml: drawerWidth + 'px',
                width: 'auto',
            }}>
                <FetchPortfolio />
            </Container>
        </>
    );
}



