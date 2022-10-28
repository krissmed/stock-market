import * as React from 'react';
import Box from '@mui/material/Box';
import Avatar from '@mui/material/Avatar';
import Menu from '@mui/material/Menu';
import MenuItem from '@mui/material/MenuItem';
import IconButton from '@mui/material/IconButton';
import Typography from '@mui/material/Typography';
import Tooltip from '@mui/material/Tooltip';
import { AllUsers } from '../fetchingData/FetchUsers';


export default function MyAvatar({ users }) {

    const [anchorEl, setAnchorEl] = React.useState(null);
    const open = Boolean(anchorEl);
    const handleClick = (event) => {
        setAnchorEl(event.currentTarget);
    };
    const handleClose = () => {
        setAnchorEl(null);
    };

    const [user, setUser] = React.useState(users[0]);

    const handleUserChange = (user) => {
        setUser(user);
    }
        return (
            <>
                <Box sx={{ display: 'flex', alignItems: 'center', textAlign: 'center' }}>
                    <div>
                        <Typography variant="subtitle1">
                            {user.first_name + ' ' + user.last_name}
                        </Typography>

                        <Typography variant="subtitle2">
                            Balance: <b>{user.curr_balance}$</b>
                        </Typography>

                    </div>
                    <Tooltip title="Profile">
                        <IconButton
                            onClick={handleClick}
                            size="small"
                            sx={{ ml: 2 }}
                            aria-controls={open ? 'dropdown-users' : undefined}
                            aria-haspopup="true"
                            aria-expanded={open ? 'true' : undefined}
                        >
                            <Avatar sx={{ width: 40, height: 40 }}>{user.first_name.charAt(0)}</Avatar>
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
                    {users.map( (aUser) => ( 
                        <MenuItem
                            key={aUser.id}
                            onClick={() => handleUserChange(aUser)}>
                            <Avatar /> {aUser.first_name + ' ' + aUser.last_name}
                        </MenuItem>
                    ))}
                </Menu>
            </>
        );
    }

