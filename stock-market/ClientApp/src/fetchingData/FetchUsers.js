import React, { useState } from 'react';
import axios from 'axios';

export const AllUsers = () => {
    const [fetchedUsers, setFetchedUsers] = useState([{
        username: ''
    }])

    axios.get('users/getfullname?userid=1')
        .then((res) => {
            setFetchedUsers(res.data);
        })

    return fetchedUsers;
}