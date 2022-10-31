import React, { useEffect, useState } from 'react';
import TransactionTable from '../components/TransactionComp/TransactionTable';
import CircularProgress from '@mui/material/CircularProgress';

import axios from 'axios';


export default function FetchTransactions() {
    const [isLoading, setIsLoading] = useState(true);

    const dummyData = [
        {
            Id: 0,
            ticker: 'AAPL',
            price: 254.32,
            type: 'BUY',
            quantity: 10,
            timestampid: '2022-10-15T00:01:00Z',
            Userid: 0,
        },
        {
            Id: 1,
            ticker: 'AAPL',
            price: 254.32,
            type: 'BUY',
            quantity: 10,
            timestampid: '2022-10-15T00:01:00Z',
            Userid: 0,
        },
        {
            Id: 2,
            ticker: 'AAPL',
            price: 254.32,
            type: 'BUY',
            quantity: 10,
            timestampid: '2022-10-15T00:01:00Z',
            Userid: 0,
        },
        {
            Id: 3,
            ticker: 'AAPL',
            price: 254.32,
            type: 'BUY',
            quantity: 10,
            timestampid: '2022-10-15T00:01:00Z',
            Userid: 0,
        },
        {
            Id: 4,
            ticker: 'AAPL',
            price: 254.32,
            type: 'SELL',
            quantity: 10,
            timestampid: '2022-10-15T00:01:00Z',
            Userid: 0,
        },
        {
            Id: 5,
            ticker: 'AAPL',
            price: 254.32,
            type: 'SELL',
            quantity: 10,
            timestampid: '2022-10-15T00:01:00Z',
            Userid: 0,
        },
    ];

    const [transactions, setTransactions] = useState([]);

    useEffect(() => {
        axios.get("transaction/listall")
            .then(res => {
                setTransactions(res.data);
                console.log(res);
                
                setIsLoading(false);
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
                <TransactionTable data={dummyData} />
            </>
        );
}