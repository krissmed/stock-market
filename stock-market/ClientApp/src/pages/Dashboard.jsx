﻿import React from 'react';

import { TopBar } from '../components/TopBar.jsx';
import DashboardGraph from '../components/DashboardComp/DashboardGraph';
import DashboardStocks from '../components/DashboardComp/DasboardStocks'
import { isMobile } from './Layout'
import Container from '@mui/material/Container';
import Grid from '@mui/material/Grid';
import { useTheme, styled } from '@mui/material/styles';
import { Typography } from '@mui/material';
import { Link } from "react-router-dom";
import Box from '@mui/material/Box';
import DashboardWatchlist from '../components/DashboardComp/DashboardWatchlist'
import DashboardTransactions from '../components/DashboardComp/DashboardTransactions'





export default function Dashboard() {
    const drawerWidth = isMobile() ? 0 : 220;
    const customTheme = useTheme();
    return (
        <>
            <TopBar title='My Dashboard' />

            <Box
                sx={{
                    marginLeft: drawerWidth + 'px',
                    marginTop: '25px'
                }}
            >
            <Container>
                <Grid
                    container spacing={5}

                >

                        <Grid item xs={12} md={8} zeroMinWidth sx={{
                            minHeight: '150px',
                            backgroundClip: 'content-box',
                            backgroundColor: customTheme.palette.primary.main

                        }}>
                            <DashboardGraph noWrap />
                        </Grid>

                        <Grid item xs={12} md={4} sx={{
                            minHeight: '150px',
                            backgroundClip: 'content-box',
                            backgroundColor: customTheme.palette.primary.main

                        }}>
                            <Typography
                                variant='h5'
                                sx={{
                                    paddingTop: '8px',
                                    paddingLeft: '8px',
                                    color: 'white',
                                    backgroundColor: customTheme.palette.primary.main
                                }}
                                >
                                Stocks
                            </Typography>
                            <DashboardStocks />
                            <Link to='/stocks'>
                            <Typography
                                variant='subtitle2'
                                align='right'
                                sx={{
                                    paddingTop: '8px',
                                    paddingRight: '8px',
                                    paddingBottom: '8px',
                                    color: customTheme.palette.primary.contrastText

                                }}>
                                View more➔
                                </Typography>
                            </Link>
                        </Grid>

                    <Grid item xs={12} md={7.4} sx={{
                            minHeight: '150px',
                            backgroundClip: 'content-box',
                            backgroundColor: customTheme.palette.primary.main

                        }}>
                            <Typography
                                variant='h5'
                                sx={{
                                    paddingTop: '8px',
                                    paddingLeft: '8px',
                                    color: 'white',
                                }}
                            >
                                Watchlist
                            </Typography>
                            <DashboardWatchlist />
                            <Link to='/watchlist'>
                            <Typography
                                variant='subtitle2'
                                align='right'
                                sx={{
                                    paddingTop: '8px',
                                    paddingRight: '8px',
                                    paddingBottom: '8px',
                                    color: customTheme.palette.primary.contrastText

                                }}>
                                View more➔
                                </Typography>
                            </Link>
                        </Grid>

                        <Grid item xs={12} md={7.25} sx={{
                            minHeight: '150px',
                            backgroundClip: 'content-box',
                            backgroundColor: customTheme.palette.primary.main
                        }}>
                            <Typography
                                variant='h5'
                                sx={{
                                    paddingTop: '8px',
                                    paddingLeft: '8px',
                                    color: customTheme.palette.primary.contrastText
                                }}>
                                Transactions
                            </Typography>
                            <DashboardTransactions />
                            <Link to='/transactions'>
                            <Typography
                                variant='subtitle2'
                                align='right'
                                sx={{
                                    paddingTop: '8px',
                                    paddingRight: '8px',
                                    paddingBottom: '8px',
                                    color: customTheme.palette.primary.contrastText

                                }}>
                                View more➔
                                </Typography>
                            </Link>
                        </Grid>

                </Grid>
                </Container>
            </Box>
         </>
        );
}


