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