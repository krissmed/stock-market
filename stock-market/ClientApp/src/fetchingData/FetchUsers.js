import React, { useEffect, useState } from 'react';
import axios from 'axios';
import MyAvatar from '../components/Avatar';


export const AllUsers = () => {

    //useRef to make the component not infinite-loop on render

    const [users, setUsers] = useState([{
        id: 0,
        first_name: 'init',
        last_name: 'name',
        curr_balance: 10
    }])

    useEffect(() => {
    axios.get('user/getall')
        .then((res) => {
            setUsers(res.data);
        })
    }, [])

    console.log(users);
    //To easy access from different components. Global variabel

    window.$id = users.id;

    return (
            
            <MyAvatar users={users} />
        
            
        );
}