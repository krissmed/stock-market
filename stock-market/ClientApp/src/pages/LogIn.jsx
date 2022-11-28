import React, { useState } from 'react';
import '../css/logIn.css';
import Signup from './Signup.jsx';

import {Box, Typography, Button, TextField} from '@mui/material';
import { useTheme } from '@mui/material/styles';
import Divider from '@mui/material/Divider';

import LoginRoundedIcon from '@mui/icons-material/LoginRounded';
import AccountCircle from '@mui/icons-material/AccountCircle';
import PasswordRoundedIcon from '@mui/icons-material/PasswordRounded';
import InputAdornment from '@mui/material/InputAdornment';


function LogIn() {
    const customTheme = useTheme();

    const [user, setUser] = useState({
        username: null,
        password: null
    });

    const [err, setErr] = useState(false);
    const [errMsg, setErrMsg] = useState("");

    const logIn = () => {
        if(!err) {
            localStorage.setItem('isLoggedIn', true);
            localStorage.setItem('user', user.username);

            window.location.href = "/";
        }
        else {
            setErrMsg("Invalid username or password. Try again");
        }
    }

    const handleChange = (e) => {
        setErrMsg("");
        if (e.target.name == 'username'){
            if (checkUsername(e.target.value)) {
                setUser({
                    ...user,
                    username: e.target.value
                })
                }
        }
        if (e.target.name == 'password') {
            setUser({
                ...user,
                password: e.target.value
                })
        }
    }


    const checkUsername = (username) => {

        if(!/^([a-zA-ZæøåÆØÅ._0-9  ]{4,20})$/.test(username)){
            setErr(true)
            setErrMsg("Username invalid");
            return false;
        }
        else {
            setErr(false)
            setErrMsg("");
            return true;
        }
    }
    return (
        <Box className='body'>
            <Box className='logInContainer'>

                <Box sx={{
                    textAlign: 'center',
                    padding: 3,
                    display: 'flex',
                    flexDirection: 'row',
                }}>

                    <Typography variant='h5'>
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
                                   id='1 outlined-required'
                                   label='Username'
                                   autoComplete='off'
                                   fullWidth
                                   color='info'
                                   helperText={errMsg}
                                   name='username'
                                   InputProps={{
                                       startAdornment: (
                                           <InputAdornment position="start">
                                               <AccountCircle />
                                           </InputAdornment>
                                       ),
                                   }}
                                   sx={{
                                        borderColor: customTheme.palette.primary.contrastText,
                                        color: customTheme.palette.primary.contrastText,
                                   }}
                                   onChange={handleChange}
                        />
                    </Box>

                    <Box>
                        <TextField required
                                   id='2 outlined-required'
                                   type='password'
                                   label='Password'
                                   autoComplete='off'
                                   fullWidth
                                   color='info'
                                   name='password'
                                   InputProps={{
                                       startAdornment: (
                                           <InputAdornment position="start">
                                               <PasswordRoundedIcon />
                                           </InputAdornment>
                                       ),
                                   }}
                                   onChange={handleChange}
                        />
                    </Box>
                    <Box sx={{
                        textAlign: 'center',
                        padding: 3,
                    }}>
                        <Typography variant='body2'>   {errMsg}    </Typography>
                    </Box>
                    <Box sx={{
                        textAlign: 'center',
                    }}>
                        <Button variant='contained' className='logInBtn' color='info' sx={{
                            width: '60%',
                            height: 40
                        }}
                        onClick={logIn}
                        >
                            <LoginRoundedIcon sx={{
                                marginRight: 1,
                            }}/>
                                Log in
                        </Button>
                    </Box>
                </Box>
    <Divider />
                <Box sx={{
                    textAlign: 'center',
                    padding: 3,
                }}>
                    <Typography variant='body2'>Not a registered user? Register <a href='/signup'>here</a></Typography>
                </Box>

            </Box>
        </Box>
    );
}

export default LogIn;