import React, { useEffect, useState } from 'react';
import axios from 'axios';
import MyAvatar from '../components/Avatar';


export const AllUsers = () => {

    //useRef to make the component not infinite-loop on render

    const [isLoading, setIsLoading] = useState(true);

    const [users, setUsers] = useState([{
        id: 1,
        first_name: 'John',
        last_name: 'Doe',
        curr_balance: 100000
    }])

    //Balance harcoded. Has to be changed to oblig2
    useEffect(() => {
    axios.get('user/getall')
        .then((res) => {
            users[0].curr_balance = res.data[0].curr_balance;

            setIsLoading(false);
        })
        
    }, [])

    //To easy get access from different components. Global variabel

    window.$id = users.id;

    return (
        isLoading
            ?
            <></>
            :
            <MyAvatar users={users} />
        );
}