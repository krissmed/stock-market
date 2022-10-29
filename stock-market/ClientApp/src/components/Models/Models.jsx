import React from 'react';
import DashboardRoundedIcon from '@mui/icons-material/DashboardRounded';
import ContactPageRoundedIcon from '@mui/icons-material/ContactPageRounded';
import VisibilityRoundedIcon from '@mui/icons-material/VisibilityRounded';
import PaidRoundedIcon from '@mui/icons-material/PaidRounded';
import QueryStatsRoundedIcon from '@mui/icons-material/QueryStatsRounded';

export const allUsers = [
    {username: 'Ole Jørgen Knoph', balance: '69'},
    {username: 'Kristian Smedsrød', balance: '21'},
    {username: 'Vetle Endrerud', balance: '43'},
    {username: 'Erling Mathias Staff', balance: '-3'},
];

export const menuItems = [
    { listName: 'Dashboard', listIcon: <DashboardRoundedIcon /> },
    { listName: 'Buy Stocks', listIcon: <QueryStatsRoundedIcon /> },
    { listName: 'Portfolio', listIcon: <ContactPageRoundedIcon /> },
    { listName: 'Watchlist', listIcon: <VisibilityRoundedIcon /> },
    { listName: 'Transactions', listIcon: <PaidRoundedIcon /> }
];