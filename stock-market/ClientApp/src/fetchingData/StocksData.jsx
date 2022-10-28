﻿import React, { useEffect } from 'react';
import { useState } from 'react';
import axios from 'axios';

import CircularProgress from '@mui/material/CircularProgress';

import StockTable from '../components/StocksComp/StockTable';


export default function StockData() {
    const [historicHook, setHistoricHook] = useState([{
        price: null,
        time: null
    }])

    const [dependency, setDependency] = useState(false);

    const [stocks, setStocks] = useState([{
        id: null,
        ticker: "",
        name: "",
        current_price: null,
        history: historicHook
    }]);

    const [fetching, setFetching] = useState(false);

    console.log(fetching + " init")

    //Fetching all stocks with axios get-request; Effect to update once on mount;

    useEffect(() => {
        setFetching(true);

        axios.get('stock/getstocks')
            .then((res) => {
                setStocks(res.data);
                
            })

        setFetching('false');

    }, [dependency]);

    return (
        fetching === true
            ?
            <div
            style = {{
                display: "flex",
                    justifyContent: "center",
                    alignItems: 'center'
                }}>
                <CircularProgress color="success" />
            </div>
            :
           <StockTable stockObj = { stocks } />
    )
}