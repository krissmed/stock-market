import { TopBar } from '../components/TopBar.jsx';
import TextField from '@mui/material/TextField';
import React, { useEffect, useState } from 'react';
import axios from 'axios';




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
                <form onSubmit={handleSubmit }>
                    <TextField value={inputs.first_name}
                        name="first_name"
                        type={"text"}
                        onChange={handleChange }
                        sx={{ margin: 3 }}
                        placeholder="Name"
                        variant="outlined"
                    />

                    <TextField value={inputs.last_name}
                        name="last_name"
                        type={"text"}
                        onChange={handleChange}
                        sx={{ margin: 3 }}
                        placeholder="Name"
                        variant="outlined"
                    />
                    

                    <button type='submit'>submit</button>
                </form>
                </div>
            


            
            </>
        )
    
}