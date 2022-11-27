import React from 'react';
import '../css/logIn.css';

import {Box, Typography, Button, TextField} from '@mui/material';
import { useTheme } from '@mui/material/styles';

import LoginRoundedIcon from '@mui/icons-material/LoginRounded';
import AccountCircle from '@mui/icons-material/AccountCircle';
import PasswordRoundedIcon from '@mui/icons-material/PasswordRounded';
import InputAdornment from '@mui/material/InputAdornment';


function LogIn() {

    const customTheme = useTheme();

    return (
        <Box className='logInContainer' sx={{
            boxShadow: 3,

        }}>

            <Box sx={{
                textAlign: 'center',
                padding: 3,
                display: 'flex',
                flexDirection: 'row',
            }}>

                <Typography variant='h5' sx={{color: customTheme.palette.primary.contrastText}}>
                    Welcome to KEVO Stocks!
                </Typography>
            </Box>

            <Box sx={{
                padding: 3,
                flexGrow: 1,
                display: 'flex',
                flexDirection: 'column',
                justifyContent: 'space-between',
            }}>

                <Box>
                    <TextField required
                               id='outlined-required'
                               label='Username'
                               autoComplete='false'
                               fullWidth
                               color='info'
                               InputProps={{
                                   startAdornment: (
                                       <InputAdornment position="start">
                                           <AccountCircle />
                                       </InputAdornment>
                                   ),
                               }}
                               sx={{

                               }}
                    />
                </Box>

                <Box>
                    <TextField required
                               id='outlined-required'
                               type='password'
                               label='Password'
                               autoComplete='false'
                               fullWidth
                               color='info'
                               InputProps={{
                                   startAdornment: (
                                       <InputAdornment position="start">
                                           <PasswordRoundedIcon />
                                       </InputAdornment>
                                   ),
                               }}
                               sx={{
                                   color: customTheme.palette.primary.light
                               }}
                    />
                </Box>
                <Button color='info' >
                    <LoginRoundedIcon sx={{
                        marginRight: 1,
                    }}/>
                        Log in
                </Button>
            </Box>

            <Box sx={{
                textAlign: 'center',
                padding: 3,
                backgroundColor: '#ff4500'
            }}>
                <Typography variant={"h5"}>Signup</Typography>
            </Box>

        </Box>
    );
}

export default LogIn;