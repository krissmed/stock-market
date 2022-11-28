import React, { useState } from 'react';
import Box from '@mui/material/Box';
import Avatar from '@mui/material/Avatar';
import Menu from '@mui/material/Menu';
import MenuItem from '@mui/material/MenuItem';
import IconButton from '@mui/material/IconButton';
import Typography from '@mui/material/Typography';
import Tooltip from '@mui/material/Tooltip';
import LogoutRoundedIcon from '@mui/icons-material/LogoutRounded';
import PersonAddAltRoundedIcon from '@mui/icons-material/PersonAddAltRounded';
import EditRoundedIcon from '@mui/icons-material/EditRounded';
import { isMobile } from '../pages/Layout'
import { Navigate } from 'react-router-dom';


export default function MyAvatar() {

    const [username, setUsername] = useState(localStorage.getItem('user'));

    const [anchorEl, setAnchorEl] = React.useState(null);
    const open = Boolean(anchorEl);
    const handleClick = (event) => {
        setAnchorEl(event.currentTarget);
    };
    const handleClose = () => {
        setAnchorEl(null);
    };

    const logOut = () => {
        localStorage.setItem('isLoggedIn', false);
        window.location.href = "/login";
    }

        return (
            <>
                <Box sx={{ display: 'flex', alignItems: 'center', textAlign: 'center' }}>
                    {isMobile() ? 
                        <></>
                        :
                        <div>
                        <Typography variant='h6'>
                            {username}
                        </Typography>

                    </div> }

                    <Tooltip title="Profile">
                        <IconButton
                            onClick={handleClick}
                            size="small"
                            sx={{ ml: 2 }}
                            aria-controls={open ? 'dropdown-users' : undefined}
                            aria-haspopup="true"
                            aria-expanded={open ? 'true' : undefined}
                        >
                            <Avatar sx={{ width: 40, height: 40 }}>{username.charAt(0)}</Avatar>
                        </IconButton>
                    </Tooltip>
                </Box>
                <Menu
                    anchorEl={anchorEl}
                    id="dropdown-users"
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
                    <MenuItem
                        onClick={() => <Navigate to="edituser" />}>
                        <EditRoundedIcon/>  Edit user
                    </MenuItem>
                    <MenuItem
                        onClick={() => <Navigate to="/signup" />}>
                        <PersonAddAltRoundedIcon/>  Register new user
                    </MenuItem>
                    <MenuItem
                        onClick={() => logOut()}>
                        <LogoutRoundedIcon/>  Log Out
                    </MenuItem>
                </Menu>
            </>
        );
    }

