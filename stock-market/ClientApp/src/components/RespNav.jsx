/// <reference path="nav.jsx" />
import React, { useState } from 'react';
import Box from '@mui/material/Box';
import { Link } from "react-router-dom";
import Drawer from '@mui/material/Drawer';
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import ListItemButton from '@mui/material/ListItemButton';
import ListItemIcon from '@mui/material/ListItemIcon';
import ListItemText from '@mui/material/ListItemText';
import MenuIcon from '@mui/icons-material/Menu';
import Logo from '../assets/LOGO.svg';
import { useTheme, styled } from '@mui/material/styles';
import { menuItems } from './Models/Models'



export default function RespNav() {
    const customTheme = useTheme();
    const [drawer, setDrawer] = useState(false)

    const toggleDrawer = (open) => (event) => {
        if (event.type === 'keydown' && (event.key === 'Tab' || event.key === 'Shift')) {
            return;
        }

        setDrawer(open);
    };

    const list = () => (
        <Box
            sx={{
                backgroundColor: customTheme.palette.primary.main,
                height: '100%'
            }}
            role="presentation"
            onClick={toggleDrawer(false)}
            onKeyDown={toggleDrawer(false)}
        >
                    <Link to="/"> 
                <img
                    src={Logo}
                    alt="LOGO"

                />
                    </Link>
                
            <List>
                {menuItems.map(item => (
                    <ListItem key={item.listName} disablePadding>
                        <ListItemButton>
                            <ListItemIcon sx={{ color: 'white' }}>
                                    {item.listIcon}
                                </ListItemIcon>
                            <ListItemText primary={item.listName} sx={{ color: 'white'} } />
                        </ListItemButton>
                    </ListItem>
                ))}
            </List>
        </Box>
    );

    return (
        <div>
            <React.Fragment>
                <MenuIcon onClick={toggleDrawer(true)} sx={{
                    color: 'white',
                    marginLeft: '20px',
                    transform: 'scale(2)'
                }} />
                <Drawer

                        open={drawer}
                        onClose={toggleDrawer(false)}
                    >
                        {list()}
                    </Drawer>
                </React.Fragment>
        </div>
    );
}
