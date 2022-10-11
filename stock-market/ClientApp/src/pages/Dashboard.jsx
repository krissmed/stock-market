import React from 'react';

import NavBar from '../components/Nav.jsx';
import { TopBar, topBarHeight } from '../components/TopBar.jsx';

import { useTheme } from '@mui/material/styles';
import Container from '@mui/material/Container';
import Grid from '@mui/material/Grid';

import { drawerWidth } from '../components/Nav.jsx';

export default function Dashboard() {

    const customTheme = useTheme();

    return (
        <>
            <TopBar title='My Dashboard' />


            <Container>
                <Grid container rowSpacing={3} sx={{
                    ml: drawerWidth + 'px',
                    width: 'auto',
                    height: 'auto',
                    justifyContent: 'space-between',
                }}>

                    <Grid item xs={12} sx={{
                            backgroundColor: customTheme.palette.success.main,
                            minHeight: '120px'
                    }}>
                        TopBar to displaye stocks
                        </Grid>

                    <Grid item xs={12} md={7} sx={{
                            backgroundColor: customTheme.palette.primary.main,
                            minHeight: '350px'
                        }}>
                            GRAF
                        </Grid>

                    <Grid item xs={12} md={4} sx={{
                            backgroundColor: customTheme.palette.primary.dark,
                            minHeight: '350px'
                    }}>
                            Stocks
                        </Grid>

                    <Grid item xs={12} md={6} sx={{
                            backgroundColor: customTheme.palette.error.light,
                            minHeight: '150px'
                        }}>
                            Watchlist
                        </Grid>

                    <Grid item xs={12} md={6} sx={{
                            backgroundColor: customTheme.palette.error.dark,
                            minHeight: '150px'
                        }}>
                            Transactions
                        </Grid>

                </Grid>
            </Container>

         </>
        );
}


