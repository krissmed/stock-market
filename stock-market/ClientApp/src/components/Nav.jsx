import React from 'react';
//LOGO;
import Logo from '../assets/LOGO.svg';
//Material UI
import AppBar from '@mui/material/AppBar';
import Typography from '@mui/material/Typography';
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
//const drawerWidth = 25;

//Applying custom style to Drawer, because theme is not applied.
const Drawer = styled(MuiDrawer)(
    ({theme}) => ({
        '& .MuiDrawer-paper': {
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
        <Drawer variant='permanent'
            anchor='left'
            >
                <Toolbar sx={{
                    my: 3,
                    mx: 'auto',
                }}>
                    <img src={Logo} alt="LOGO" />
                </Toolbar>
                <List>
                    <ListItemButton>
                        <ListItem>                        
                        <ListItemIcon>
                            <DashboardRoundedIcon sx={{ color: customTheme.palette.primary.contrastText}} />
                            </ListItemIcon>
                                <ListItemText primary="Dashboard" />
                        </ListItem>
                    </ListItemButton>


                    <ListItemButton>
                        <ListItem> 
                            <ListItemIcon>
                            <QueryStatsRoundedIcon sx={{ color: customTheme.palette.primary.contrastText }} />
                            </ListItemIcon>
                            <ListItemText primary="Buy Stocks" />
                       </ListItem>
                    </ListItemButton>

                    <ListItemButton>
                        <ListItem> 
                            <ListItemIcon>
                            <ContactPageRoundedIcon sx={{ color: customTheme.palette.primary.contrastText }} />
                            </ListItemIcon>
                            <ListItemText primary="Portfolio" />
                        </ListItem>
                    </ListItemButton>

                    <ListItemButton>
                        <ListItem> 
                            <ListItemIcon>
                            <VisibilityRoundedIcon sx={{ color: customTheme.palette.primary.contrastText }} />
                            </ListItemIcon>
                            <ListItemText primary="Watchlist" />
                        </ListItem>
                    </ListItemButton>

                    <ListItemButton>
                        <ListItem> 
                            <ListItemIcon>
                            <PaidRoundedIcon sx={{ color: customTheme.palette.primary.contrastText }} />
                            </ListItemIcon>
                            <ListItemText primary="Transactions" />
                        </ListItem>
                    </ListItemButton>
                </List>

            </Drawer>
    )
}
