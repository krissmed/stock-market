import React, { useEffect } from 'react';
import { useState } from 'react';
import axios from 'axios';

import CircularProgress from '@mui/material/CircularProgress';

import StockTable from '../components/StocksComp/StockTable';


export default function StockData() {
    const [historicHook, setHistoricHook] = useState([{
        price: null,
        time: null
    }])

    //Just dummyVariable to not create infinite rendering

    const [dependency, setDependency] = useState(false);

    const [stocks, setStocks] = useState([{
        id: null,
        ticker: "",
        name: "",
        current_price: null,
        history: historicHook
    }]);

    const [fetching, setFetching] = useState(false);

    //Fetching all stocks with axios get-request; Effect to update once on mount;

    useEffect(() => {
        setFetching(true);

        axios.get('stock/getstocks')
            .then((res) => {
                setStocks(res.data);
                
            }).catch(err => {
                if (err.status === 401) {
                    localStorage.setItem('isLoggedIn', false);
                    window.location.href = "/login";
                }
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

//make an async function that fetches the data and then set the state with the data

//then call the function in the useEffect

