import React from 'react';

import { TopBar } from '../components/TopBar.jsx';
import { drawerWidth } from '../components/Nav.jsx';
import DashboardGraph from '../components/DashboardGraph';

import Container from '@mui/material/Container';
import Grid from '@mui/material/Grid';



export default function Dashboard() {

    return (
        <>
            <TopBar title='My Dashboard' />


            <Container>
                <Grid container spacing={1} sx={{
                    ml: drawerWidth + 'px',
                    width: 'auto'
                }}>

                    <Grid item xs={12} sx={{
                        minHeight: '120px',
                        textAlign: 'center',
                        alignContent: 'center'
                    }}>
                        <h5>TopBar to display the popular stocks</h5>
                        </Grid>

                    <Grid item xs={12} md={8} sx={{
                            minHeight: '350px'
                        }}>
                            <DashboardGraph />
                        </Grid>

                    <Grid item xs={12} md={4} sx={{
                            minHeight: '350px'
                    }}>
                        <h5>Stocks</h5>
                        </Grid>

                    <Grid item xs={12} md={6} sx={{
                            minHeight: '150px'
                        }}>
                        <h5>Watchlist</h5>
                        </Grid>

                    <Grid item xs={12} md={6} sx={{
                            minHeight: '150px'
                        }}>
                        <h5>Transactions</h5>
                        </Grid>

                </Grid>
            </Container>

         </>
        );
}


