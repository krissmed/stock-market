import React from 'react';
import NavBar from '../components/Nav.jsx';
import { TopBar } from '../components/TopBar.jsx';

import Container from '@mui/material/Container';


export default function Dashboard() {

    return (
        <>
            <NavBar />
            <TopBar title='All Stocks' />

            <Container>

            </Container>
        </>
    );
}



