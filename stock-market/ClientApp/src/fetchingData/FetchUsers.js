import React from 'react';
import axios from 'axios';

function AllUsers () {
    let allTheUsers = [{
        id: 0,
        name: 'Please Choose A User',
        last_name: '',
        curr_balance: 0,
        curr_balance_liquid: null,
        curr_balance_stock: null
    }];

    //Fetching all users and adding to the array
    axios.get('user/getall')
        .then((res) => {

            //Easier to format the numbers

            let balance = res.data[0].curr_balance;
            let balance_liquid = res.data[0].curr_balance_liquid;
            let balance_stock = res.data[0].curr_balance_stock;


            users.push({
                id: res.data[0].id,
                name: res.data[0].first_name + ' ' + res.data[0].last_name,
                curr_balance: new Intl.NumberFormat().format(balance),
                curr_balance_liquid: new Intl.NumberFormat().format(balance_liquid),
                curr_balance_stock: new Intl.NumberFormat().format(balance_stock),
            });
        })

    console.log(allTheUsers);

    return allTheUsers;
}

export const users = AllUsers();
