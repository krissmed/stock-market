import React from 'react';
import { Link } from "react-router-dom";

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

//Applying custom style to Drawer, because theme cannot be applied
//to this MUI-component..
const Drawer = styled(MuiDrawer)(
    ({theme}) => ({
        '& .MuiDrawer-paper': {
            width: 220,
            backgroundColor: theme.palette.primary.main,
            color: theme.palette.primary.contrastText
        },
       '& .MuiListItemButton-root:hover': {
           backgroundColor: theme.palette.primary.light,
       }
       
    }),
);


export default function Nav() {


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
                        <Link to={item.listLink} style={{
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
          
        </Drawer>
        </>
    )
}
