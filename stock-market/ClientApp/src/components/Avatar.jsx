import React, { Component } from 'react';
import Avatar from '@mui/material/Avatar';
import Typography from '@mui/material/Typography';




function MyAvatar() {

    const balance = 34;

    return (
        <div style={{
            display: 'flex', width: 'auto', height: '60px', alignContent: 'center', justifyContent: 'center',
            marginRight: 3,
        }}>
            <div style={{
                marginRight: 15, display: 'flex', flexDirection: 'column', justifyContent: 'center'
            }}>
                <Typography variant="subtitle1">Kristian Smedsrød</Typography>
                <Typography variant="subtitle2">Balance: {balance}$</Typography>
            </div >
            <div style={{
                display: 'flex', flexDirection: 'column', justifyContent: 'flex-start'
            }}>
                <Avatar alt="Kristian Smedsrød" src="../assets/test_avatar.jpg"
                    sx={{mt: 1}}                />
            </div>
        </div>
        );
    }

export default MyAvatar