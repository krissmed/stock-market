import React from 'react';

import AppBar from '@mui/material/AppBar';
import Typography from '@mui/material/Typography';
import { useTheme } from '@mui/material/styles';
import MyAvatar from './Avatar.jsx';

import { drawerWidth } from './Nav.jsx';

export const topBarHeight = 100;

export const TopBar = ({ title }) => {

    const customTheme = useTheme();

    return (
        <AppBar
            position='relative'
            elevation={ 0 } //No shadow
            sx={{
                ml: drawerWidth + 'px',
                mr: 2,
                padding: 1,
                width: 'auto',
                backgroundColor: customTheme.palette.background.default,
                display: 'flex',
                flexDirection: 'row',
                alignItems: 'center',
                justifyContent: 'space-between'
            }}
        >

                <Typography variant="h4" sx={{
                    display: 'inline-block',
                    flexGrow: 1,
                    textAlign: 'center'
                }}>
                    {title}
                </Typography>

                <MyAvatar />
         </AppBar>
        );
}
