import * as React from 'react';
import Box from '@mui/material/Box';
import Avatar from '@mui/material/Avatar';
import Menu from '@mui/material/Menu';
import MenuItem from '@mui/material/MenuItem';
import IconButton from '@mui/material/IconButton';
import Typography from '@mui/material/Typography';
import Tooltip from '@mui/material/Tooltip';

import { allUsers } from './Models/Models';

export default function MyAvatar() {

    const [anchorEl, setAnchorEl] = React.useState(null);
    const open = Boolean(anchorEl);
    const handleClick = (event) => {
        setAnchorEl(event.currentTarget);
    };
    const handleClose = () => {
        setAnchorEl(null);
    };

    //Getting the global variable declared in index.js
    const [username, setUsername] = React.useState(window.$name);

    //Finding the whole user whom maches the is
    const currentUser = allUsers.find(user => {
        return user.username === username;
    })

    //Init the balance of said user
    const [balance, setBalance] = React.useState(currentUser.balance);

    //Handles the userchange. Sets window name to keep the global variable uptodate
    const handleUserChange = (user) => {
        setUsername(user.username);
        window.$name = user.username;
        setBalance(user.balance);
    };


    return (
        <React.Fragment>
            <Box sx={{ display: 'flex', alignItems: 'center', textAlign: 'center' }}>
                <div>
                    <Typography variant="subtitle1">
                        {username}
                    </Typography>

                    <Typography variant="subtitle2">
                        Balance: <b>{balance}$</b>
                    </Typography>

                </div>
                <Tooltip title="Profile">
                    <IconButton
                        onClick={handleClick}
                        size="small"
                        sx={{ paddingLeft: 2 }}
                        aria-controls={open ? 'account-menu' : undefined}
                        aria-haspopup="true"
                        aria-expanded={open ? 'true' : undefined}
                    >
                        <Avatar sx={{ width: 40, height: 40 }}>{username.charAt(0)}</Avatar>
                    </IconButton>
                </Tooltip>
            </Box>
            <Menu
                anchorEl={anchorEl}
                id="account-menu"
                open={open}
                onClose={handleClose}
                onClick={handleClose}
                PaperProps={{
                    elevation: 0,
                    sx: {
                        overflow: 'visible',
                        filter: 'drop-shadow(0px 2px 8px rgba(0,0,0,0.32))',
                        mt: 1.5,
                        '& .MuiAvatar-root': {
                            width: 32,
                            height: 32,
                            ml: -0.5,
                            mr: 1,
                        },
                        '&:before': {
                            content: '""',
                            display: 'block',
                            position: 'absolute',
                            top: 0,
                            right: 14,
                            width: 10,
                            height: 10,
                            bgcolor: 'background.paper',
                            transform: 'translateY(-50%) rotate(45deg)',
                            zIndex: 0,
                        },
                    },
                }}
                transformOrigin={{ horizontal: 'right', vertical: 'top' }}
                anchorOrigin={{ horizontal: 'right', vertical: 'bottom' }}
            >
                {allUsers.map(user => (
                        <MenuItem
                            key={user.username}
                            onClick={() => handleUserChange(user)}>
                            <Avatar /> { user.username }
                        </MenuItem>
                    )
                )}

            </Menu>
        </React.Fragment>
    );
}
