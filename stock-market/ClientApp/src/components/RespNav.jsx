<reference path="nav.jsx" />
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
import Typography from '@mui/material/Typography';



export default function RespNav() {
    const dummyBalance = [
        { liquid: 100, stocks: 200 }
    ]
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
            role="presentation"
            onClick={toggleDrawer(false)}
            onKeyDown={toggleDrawer(false)}
            sx={{
                height: '100%',
                backgroundColor: customTheme.palette.primary.main
            }}
        >
            <Box
                sx={{
                    paddingLeft: '23px',
                    paddingTop: '10px'
                }}            >
            <img
                src={Logo}
                alt="LOGO"

                />
            </Box>
            <List >
                {menuItems.map(item => (
                    <Link to={item.listLink} key={item.listName}>
                    <ListItem key={item.listName} disablePadding>
                        <ListItemButton>
                                <ListItemIcon
                                    sx={{
                                        color: customTheme.palette.primary.contrastText
                                    }}
                                >
                                    {item.listIcon}
                                </ListItemIcon>
                                <ListItemText
                                    primary={item.listName}
                                    sx={{
                                        color: customTheme.palette.primary.contrastText,
                                        textDecoration: 'none'
                                    }}
                                />
                        </ListItemButton>
                        </ListItem>
                    </Link>
                ))}
            </List>
            <Box
                sx={{
                    marginTop: '200%',
                    marginLeft: '25px'
                }}
            >
                <Typography
                    variant='h4'
                    sx={{
                        color: customTheme.palette.primary.contrastText
                    }}
                >
                    Balance:
                </Typography>
                {dummyBalance.map(items => (
                    <>
                                        
                        <Typography variant='h6' sx={{ color: customTheme.palette.primary.contrastText}}>Liquid: {items.liquid}$</Typography>
                        <Typography variant='h6' sx={{ color: customTheme.palette.primary.contrastText}}>Stocks: {items.stocks}$</Typography>
                        <Typography variant='h6' sx={{ color: customTheme.palette.primary.contrastText}}>Total: {items.stocks + items.liquid}$    </Typography>
                    </>
                ))}
            </Box>
        </Box>
    );

    return (
        <div>
            <React.Fragment>
                <MenuIcon
                    onClick={toggleDrawer(true)}
                    sx={{
                        color: 'white',
                        transform: 'scale(1.5)',
                        marginLeft: '15px'
                    }}
                />
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
