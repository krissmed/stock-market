import React, { useEffect } from 'react';
import { useState } from 'react';
import axios from 'axios';

import { CircularProgress } from "@mui/material";

import StockTable from '../components/StocksComp/StockTable';


export default function StockData() {


    const [stocks, setStocks] = useState([{
        id: null,
        ticker: "",
        name: "",
        value: null,
        history: null
    }]);
    const [fetching, setFetching] = useState('true');

    
    //Fetching all stocks with axios get-request;

    fetch('http://localhost:44394/stock/getstocks')
        .then(response => {
            JSON.stringify(response);
            console.log(JSON.stringify(response));
        })
        .then((data) => {
            setStocks(data);
            setFetching('false');
            console.log('fetching');
            console.log(data);
        })
    


    //console.log(stocks);

    return (
        fetching
            ?
            <div
                style={{
                    display: "flex",
                    justifyContent: "center"
                }}>
                <CircularProgress />
            </div>
            :
            <StockTable stockObj={stocks} />
    )
}
