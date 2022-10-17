import React from 'react';

import AppBar from '@mui/material/AppBar';
import Typography from '@mui/material/Typography';
import { useTheme } from '@mui/material/styles';
import Box from '@mui/material/Box';


import { drawerWidth } from './Nav.jsx';

export const topBarHeight = 100;

export const TopBar = ({ title }) => {

    const customTheme = useTheme();

    return (
        <AppBar
            position='relative'
            elevation={ 0 } //No shadow.
            sx={{
                ml: drawerWidth + 'px',
                height: topBarHeight + 'px',
                width: 'auto',
                backgroundColor: customTheme.palette.background.default,
                justifyContent: 'center',
                textAlign: 'center',
                alignItems: 'center'
            }}

        >
            <Typography variant="h4">
                {title}
            </Typography>

           
        </AppBar>

        );
}
