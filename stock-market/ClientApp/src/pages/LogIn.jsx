import React, { useState } from 'react';
import axios from 'axios';
import '../css/logIn.css';
import Signup from './Signup.jsx';

import {Box, Typography, Button, TextField} from '@mui/material';
import { useTheme } from '@mui/material/styles';
import Divider from '@mui/material/Divider';

import LoginRoundedIcon from '@mui/icons-material/LoginRounded';
import AccountCircle from '@mui/icons-material/AccountCircle';
import PasswordRoundedIcon from '@mui/icons-material/PasswordRounded';
import InputAdornment from '@mui/material/InputAdornment';

const loggingInUser = (user) => {
    console.log(user);

    axios.post('user/login', user)
        .then(res => {
            console.log(res);
            if (res.status === 200) {
                console.log("logged in");
                return true;
            }
            return false;

        }).catch(err => {
            return false;
        })

}

function LogIn() {
    const customTheme = useTheme();

    const [user, setUser] = useState({
        username: null,
        password: null
    });

    const [err, setErr] = useState(false);
    const [errMsg, setErrMsg] = useState("");
    const [errUser, setErrUser] = useState("");
    const [errPass, setErrPass] = useState("");

    const logIn = () => {
        if (!err) {

            if (loggingInUser(user)) {
                alert("valid, logged in");
            }
            //localStorage.setItem('isLoggedIn', true);
            //localStorage.setItem('user', user.username);

            //window.location.href = "/";

            else {
                setErrMsg("Invalid username or password. Try again");
            }

        }
        else {
            setErrMsg("Invalid username or password. Try again");
        }
    }

    const handleChange = (e) => {
        setErrMsg("");
        if (e.target.name == 'username') {
            setErrUser("");
            if (checkUsername(e.target.value)) {
                setUser({
                    ...user,
                    username: e.target.value
                })
                }
        }
        if (e.target.name == 'password') {
            setErrPass("");

            if (checkPassword(e.target.value)) {
                setUser({
                    ...user,
                    password: e.target.value
                })
            }
        }
    }


    const checkUsername = (username) => {

        if (!/^([a-zA-ZæøåÆØÅ. \-]{2,20})$/.test(username)){
            setErr(true)
            setErrUser("Username invalid");
            return false;
        }
        else {
            setErr(false)
            setErrUser("");
            return true;
        }
    }

    const checkPassword = (password) => {

        if (!/^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,16})$/.test(password)) {
            setErr(true);
            setErrPass("Max 16chars, minimum 8chars: 1 uppercase, 1 lowercase, 1 digit and 1 special char");
            return false;
        }
        else {
            setErr(false)
            setErrPass("");
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
                                   helperText={errUser}
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
                                    helperText={errPass}
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
                        <Typography variant='subtitle2' sx={{
                            color: customTheme.palette.error.main
                        }}>   {errMsg}    </Typography>
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