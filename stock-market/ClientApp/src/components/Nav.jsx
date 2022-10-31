import React from 'react';
import { Link } from "react-router-dom";
import Box from '@mui/material/Box'
import Typography from '@mui/material/Typography';

//LOGO;
import Logo from '../assets/LOGO.svg';
//Material UI;
import MuiDrawer from '@mui/material/Drawer';
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import ListItemButton from '@mui/material/ListItemButton';
import ListItemIcon from '@mui/material/ListItemIcon';
import ListItemText from '@mui/material/ListItemText';
import Toolbar from '@mui/material/Toolbar';
import { useTheme, styled } from '@mui/material/styles';
import { menuItems } from './Models/Models'
//Icons from material UI


//Width of drawer
export const drawerWidth = 220;
//Applying custom style to Drawer, because theme cannot be applied
//to this MUI-component..

const Drawer = styled(MuiDrawer)(
    ({theme}) => ({
        '& .MuiDrawer-paper': {
            width: drawerWidth,
            backgroundColor: theme.palette.primary.main,
            color: theme.palette.primary.contrastText
        },
       '& .MuiListItemButton-root:hover': {
           backgroundColor: theme.palette.primary.light,
       }
       
    }),
);


export default function Nav() {
    const dummyBalance = [
        { liquid: 100, stocks: 200}
    ]

    //Colorpalette;
    const customTheme = useTheme();

    return (
        <>
        <Drawer variant='permanent'
            anchor='left'
        >
           
                <Toolbar sx={{
                    my: 3,
                    mx: 'auto',
                }}>
                    <Link to="/"> 
                        <img src={Logo} alt="LOGO" />
                    </Link>
                </Toolbar>
            
            <List>


                    {menuItems.map(item => (
                        <Link to={item.listLink} key={item.listName} style={{
                            textDecoration: 'none',

                        }}>
                            <ListItemButton key={item.listName}>
                                <ListItem>
                                    <ListItemIcon sx={{ color: customTheme.palette.primary.contrastText }} >
                                        {item.listIcon}
                                    </ListItemIcon>
                                    <ListItemText primary={item.listName} sx={{
                                        color: customTheme.palette.primary.contrastText,
                                        textDecoration: 'none'

                                    }} />
                                </ListItem>
                            </ListItemButton>
                        </Link>
                    ))}

                </List>
                <Box
                    sx={{
                        marginTop: '100%',
                        marginLeft: '25px'
                    } }
                >
                    <Typography variant='h4'>
                    Balance:
                    </Typography>
                    {dummyBalance.map(items => (
                        <>
                            <Typography variant='h6'>Liquid: {items.liquid}$</Typography>
                            <Typography variant='h6'>Stocks: {items.stocks}$</Typography>
                            <Typography variant='h6'>Total: {items.stocks+items.liquid}$    </Typography>
                        </>
))}
                </Box>
        </Drawer>
        </>
    )
}
