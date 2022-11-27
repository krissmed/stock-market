import React, { useEffect, useState } from 'react';
import axios from 'axios';
import MyAvatar from '../components/Avatar';


export const AllUsers = () => {

    const [isLoading, setIsLoading] = useState(true);

    const [users, setUsers] = useState([{
        id: 1,
        first_name: 'John',
        last_name: 'Doe',
        curr_balance: 100000
    }])

    return (
        isLoading
            ?
            <></>
            :
            <MyAvatar users={users} />
        );
}