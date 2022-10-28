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

//Icons from material UI
import DashboardRoundedIcon from '@mui/icons-material/DashboardRounded';
import ContactPageRoundedIcon from '@mui/icons-material/ContactPageRounded';
import VisibilityRoundedIcon from '@mui/icons-material/VisibilityRounded';
import PaidRoundedIcon from '@mui/icons-material/PaidRounded';
import QueryStatsRoundedIcon from '@mui/icons-material/QueryStatsRounded';

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

                    <Link to="/" style={{
                        textDecoration: 'none',
                        
                    }}>  
                    <ListItemButton>
                            <ListItem>                        
                            <ListItemIcon>
                                <DashboardRoundedIcon sx={{ color: customTheme.palette.primary.contrastText}} />
                                </ListItemIcon>
                                <ListItemText primary="Dashboard" sx={{
                                    color: customTheme.palette.primary.contrastText,
                                    textDecoration: 'none',
                              
                                }} />
                            </ListItem>
                       </ListItemButton>
                </Link>

                    <Link to="/stocks" style={{
                        textDecoration: 'none',

                    }}>
                    <ListItemButton>
                            <ListItem> 
                                <ListItemIcon>
                                <QueryStatsRoundedIcon sx={{ color: customTheme.palette.primary.contrastText }} />
                                </ListItemIcon>
                                <ListItemText primary="Buy Stocks" sx={{
                                    color: customTheme.palette.primary.contrastText,
                                    textDecoration: 'none',

                                }} />
                           </ListItem>
                        </ListItemButton>
                </Link>

                    <Link to="/portfolio" style={{
                        textDecoration: 'none',

                    }}>
                <ListItemButton>
                        <ListItem> 
                            <ListItemIcon>
                            <ContactPageRoundedIcon sx={{ color: customTheme.palette.primary.contrastText }} />
                            </ListItemIcon>
                                <ListItemText primary="Portfolio" sx={{
                                    color: customTheme.palette.primary.contrastText,
                                    textDecoration: 'none',

                                }} />
                        </ListItem>
                    </ListItemButton>
                </Link>

                    <Link to="/watchlist" style={{
                        textDecoration: 'none',

                    }}>
                <ListItemButton>
                        <ListItem> 
                            <ListItemIcon>
                            <VisibilityRoundedIcon sx={{ color: customTheme.palette.primary.contrastText }} />
                            </ListItemIcon>
                                <ListItemText primary="Watchlist" sx={{
                                    color: customTheme.palette.primary.contrastText,
                                    textDecoration: 'none',

                                }} />
                        </ListItem>
                    </ListItemButton>
                </Link>

                    <Link to="/transactions" style={{
                        textDecoration: 'none',

                    }}>
                <ListItemButton>
                        <ListItem> 
                            <ListItemIcon>
                            <PaidRoundedIcon sx={{ color: customTheme.palette.primary.contrastText }} />
                            </ListItemIcon>
                                <ListItemText primary="Transactions" sx={{
                                    color: customTheme.palette.primary.contrastText,
                                    textDecoration: 'none',

                                }} />
                        </ListItem>
                    </ListItemButton>
                </Link>

                </List>
          
        </Drawer>
        </>
    )
}
