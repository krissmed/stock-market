import React, { Component } from 'react';
import Avatar from '@mui/material/Avatar';

function MyAvatar() {
    return (
        <div style={{ display: 'flex', marginTop: '16px', height: '86px', right: '0px', justifyContent: 'flex-end', marginRight: '43px' }}>
            <div style={{ marginRight: 15 }}>
                <h1 style={{ fontWeight: 400, fontSize: '32px', lineHeight: '39px', color: '#FEFEFE' }}>Kristian Smedsrød</h1>
                <h4 style={{ fontWeight: 400, fontSize: '24px', lineHeight: '29px', color: '#FEFEFE' }}>Balance: 34$</h4>
                </div >
            <Avatar alt="Kristian Smedsrød" src="../assets/test_avatar.jpg" />
            </div>
        );
    }

export default MyAvatar