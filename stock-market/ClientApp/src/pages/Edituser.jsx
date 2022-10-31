import { TopBar } from '../components/TopBar.jsx';
import TextField from '@mui/material/TextField';
import React, { useEffect, useState } from 'react';
import axios from 'axios';
import Button from '@mui/material/Button';
import { useTheme, styled } from '@mui/material/styles';




export default function edituser() {

    const [inputs, setInputs] = useState({
        id: "",
        first_name: "",
        last_name: "",
        

    });

    const handleChange = (e) => {
        setInputs((prevState) => ({
            ...prevState,
            [e.target.name]: e.target.value,
            id: 1
        }))

    }
    
    const handleSubmit = (e) => {
        e.preventDefault();
        console.log(inputs)
        try {
            const resp = axios.post('user/EditUser', { inputs });
            console.log(resp.data);
        } catch (error) {
            console.log(error);
        }
    }
    return (
        
        <>
            <TopBar title="test" />
            
                <div style={{
                    display: 'flex',
                    alignItems: 'center',
                    justifyContent: 'center',
                
                }}>
                <form onSubmit={handleSubmit} sx={{ display: 'flex'} }>
                    <TextField value={inputs.first_name}
                        name="first_name"
                        type='filled'
                        label='First Name'
                        onChange={handleChange }
                        sx={{
                            margin: 3,
                            color: customTheme.palette.primary.contrastText

                        }}
                        variant="filled"
                    />

                    <TextField value={inputs.last_name}
                        name="last_name"
                        type='filled'
                        label='Last Name'
                        onChange={handleChange}
                        sx={{ margin: 3 }}
                        variant="filled"
                    />
                    

                </form>
                <Button onclickcolor='success' variant='contained'>submit</Button>
                </div>
            


            
            </>
        )
    
}