import React from 'react';

import AppBar from '@mui/material/AppBar';
import Typography from '@mui/material/Typography';
import { useTheme } from '@mui/material/styles';
import MyAvatar from './Avatar.jsx';
import { isMobile } from '../pages/Layout'
import RespNav from './RespNav.jsx';

export const topBarHeight = 100;

export const TopBar = ({ title }) => {

    const customTheme = useTheme();
    const drawerWidth = isMobile ? 0 : 220;
    return (
        <AppBar
            position='relative'
            elevation={0} //No shadow
            sx={{
                ml: drawerWidth + 'px',
                padding: 1,
                width: 'auto',
                backgroundColor: isMobile() ? customTheme.palette.primary.main : customTheme.palette.background.default,
                display: 'flex',
                flexDirection: 'row',
                alignItems: 'center',
                justifyContent: 'space-between'
            }}
        >
            {isMobile() ? <RespNav /> : <></>}
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
