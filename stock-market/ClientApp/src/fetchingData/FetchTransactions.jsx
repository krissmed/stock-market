import React, { useEffect, useState } from 'react';
import TransactionTable from '../components/TransactionComp/TransactionTable';
import CircularProgress from '@mui/material/CircularProgress';

import axios from 'axios';


export default function FetchTransactions() {
    const [isLoading, setIsLoading] = useState(true);


    const [transactions, setTransactions] = useState([
        {
            Id:0,
            ticker: '',
            price: null,
            type: '',
            quantity: null,
            timestamp: '',
        }
    ]);

    useEffect(() => {
        axios.get("transaction/listall")
            .then(res => {

                setTransactions(res.data);                
                setIsLoading(false);
            }).catch(err => {
                if (err.status === 401) {
                    localStorage.setItem('isLoggedIn', false);
                    window.location.href = "/login";
                }
            })
    }, [])


    return (
        isLoading == true ?
            <>
                <div
                    style={{
                        display: "flex",
                        justifyContent: "center",
                        alignItems: 'center'
                    }}>
                    <CircularProgress color="success" />
                </div>
            </>
            :
            <>
                <TransactionTable data={transactions} />
            </>
        );
}