import React, { useState } from 'react';
import axios from 'axios';
import '../css/logIn.css';
import LogIn from './LogIn';

import { Box, Typography, Button, TextField } from '@mui/material';
import { useTheme } from '@mui/material/styles';
import Divider from '@mui/material/Divider';

import LoginRoundedIcon from '@mui/icons-material/LoginRounded';
import AccountCircle from '@mui/icons-material/AccountCircle';
import PasswordRoundedIcon from '@mui/icons-material/PasswordRounded';
import InputAdornment from '@mui/material/InputAdornment';



function Signup() {

    const registerUser = (user) => {
        axios.post('user/register', user)
            .then(res => {
                if (res.status === 200) {
                    window.location.href = "/login";
                }

            }).catch(err => {
               
                    if (err.response.status === 401) {
                        axios.get('/user/logout')
                            .then(res => {
                                localStorage.setItem('isLoggedIn', false);
                                window.location.href = "/login";
                            })
                    }

                setErrMsg(err.response.data);
            })
    }

    const customTheme = useTheme();

    const [user, setUser] = useState({
        username: null,
        firstname: null,
        lastname: null,
        password: null
    });

    const [err, setErr] = useState(false);
    const [errMsg, setErrMsg] = useState("");
    const [errUser, setErrUser] = useState("");
    const [errName, setErrName] = useState("");
    const [errPass, setErrPass] = useState("");

    const register = () => {
        if (!err) {

            registerUser(user);
        }
        else {
            setErrMsg("Invalid input. Try again");
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
        else if (e.target.name == 'firstname' || e.target.name == 'lastname') {
            setErrName("");
            const { name, value } = e.target;

            if (checkName(value)) {
                setUser(({
                    ...user,
                    [name]: value
                }))
            }


        }
        else if (e.target.name == 'password') {
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

        if (!/^([a-zA-ZæøåÆØÅ. \-]{2,20})$/.test(username)) {
            setErr(true)
            setErrUser("Username invalid. Only letters, might be seperated with '.'");
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
                setErr(true)
                setErrPass("Max 16chars, minimum 8chars: 1 uppercase, 1 lowercase, 1 digit and 1 special char");
                return false;
            }
            else {
                setErr(false)
                setErrPass("");
                return true;
            }
        }

    const checkName = (name) => {
        if (!/^([a-zA-ZæøåÆØÅ \-]{2,50})$/.test(name)) {
            setErr(true)
            setErrName("Names has to consist of 2 or more letters");
            return false;
        }
        else {
            setErr(false)
            setErrName("");
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
                        Register at KEVO Stocks!
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
                            id='firstname outlined-required'
                            label='Firstname'
                            autoComplete='off'
                            fullWidth
                            color='info'
                            helperText={errName}
                            name='firstname'
                            sx={{
                                borderColor: customTheme.palette.primary.contrastText,
                                color: customTheme.palette.primary.contrastText,
                            }}
                            onChange={handleChange}
                        />
                    </Box>

                    <Box>
                        <TextField required
                            id='lastname outlined-required'
                            label='Lastname'
                            autoComplete='off'
                            fullWidth
                            color='info'
                            helperText={errName}
                            name='lastname'
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

                    <Box>
                        <Typography variant='subtitle2' sx={{
                            color: customTheme.palette.error.main
                        }}>
                            {errMsg}
                        </Typography>
                    </Box>

                    <Box sx={{
                        textAlign: 'center',
                    }}>
                        <Button variant='contained' className='logInBtn' color='info' sx={{
                            width: '60%',
                            height: 40
                        }}
                            onClick={register}
                        >
                            <LoginRoundedIcon sx={{
                                marginRight: 1,
                            }} />
                                Sign up
                        </Button>
                    </Box>
                </Box>

            </Box>
        </Box>
    );
}

export default Signup;