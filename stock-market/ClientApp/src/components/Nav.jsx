import React from 'react';
//LOGO;
import Logo from '../assets/LOGO.svg';
//Material UI
import AppBar from '@mui/material/AppBar';
import Typography from '@mui/material/Typography';
import Drawer from '@mui/material/Drawer';
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import ListItemButton from '@mui/material/ListItemButton';
import ListItemIcon from '@mui/material/ListItemIcon';
import ListItemText from '@mui/material/ListItemText';
import Toolbar from '@mui/material/Toolbar';
import { useTheme } from '@mui/material/styles';

//Icons from material UI
import DashboardRoundedIcon from '@mui/icons-material/DashboardRounded';
import ContactPageRoundedIcon from '@mui/icons-material/ContactPageRounded';
import VisibilityRoundedIcon from '@mui/icons-material/VisibilityRounded';
import PaidRoundedIcon from '@mui/icons-material/PaidRounded';
import QueryStatsRoundedIcon from '@mui/icons-material/QueryStatsRounded';


export default function Nav() {


    //Colorpalette;
    const customTheme = useTheme();

    return (
        <Drawer variant='permanent'
            anchor='left'
            sx={{
                bgcolor: "#252638"
            }}
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
                                <DashboardRoundedIcon/>
                            </ListItemIcon>
                                <ListItemText primary="Dashboard" />
                        </ListItem>
                    </ListItemButton>


                    <ListItemButton>
                        <ListItem> 
                            <ListItemIcon>
                                <QueryStatsRoundedIcon />
                            </ListItemIcon>
                            <ListItemText primary="Buy Stocks" />
                       </ListItem>
                    </ListItemButton>

                    <ListItemButton>
                        <ListItem> 
                            <ListItemIcon>
                                <ContactPageRoundedIcon />
                            </ListItemIcon>
                            <ListItemText primary="Portfolio" />
                        </ListItem>
                    </ListItemButton>

                    <ListItemButton>
                        <ListItem> 
                            <ListItemIcon>
                                <VisibilityRoundedIcon />
                            </ListItemIcon>
                            <ListItemText primary="Watchlist" />
                        </ListItem>
                    </ListItemButton>

                    <ListItemButton>
                        <ListItem> 
                            <ListItemIcon>
                                <PaidRoundedIcon />
                            </ListItemIcon>
                            <ListItemText primary="Transactions" />
                        </ListItem>
                    </ListItemButton>
                </List>

            </Drawer>
    )
}
