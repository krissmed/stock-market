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
        current_price: null,
    }]);
    const [fetching, setFetching] = useState('true');

    
    //Fetching all stocks with axios get-request; Effect to update once on new render;

    useEffect(() => {

        axios.get('stock/getstocks')
            .then((res) => {
                setStocks(res.data);
            })

        setFetching('false');

    }, []);


    return (
        fetching
            ?
           <StockTable stockObj = { stocks } />
            :
         
        < div
            style = {{
                display: "flex",
                    justifyContent: "center",
                    alignItems: 'center'
                }}>
                <CircularProgress />
            </div>
    )
}
