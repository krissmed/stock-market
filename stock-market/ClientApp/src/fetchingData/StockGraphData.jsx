import React, { useState, useEffect } from 'react';
import axios from 'axios';
import CircularProgress from '@mui/material/CircularProgress';

import StockGraph from '../components/StocksComp/StockGraph';

//Formatting to valid JS dateformat
function formatDate(inputDate) {
    const [dateValues, timeValues] = inputDate.split('T');
    console.log(dateValues); // only date
    console.log(timeValues); //only time

    const [year, month, day] = dateValues.split('-');
    const [hours, minutes, seconds] = timeValues.split(':');

    const date = new Date(+year, +month - 1, +day, +hours, +minutes, +seconds);

    return date;
}

export default function StockGraphData({ ticker }) {

    const [isLoading, setIsLoading] = useState(true);

    //Reach hook obj to store incoming data

    const [historicalData, setHistoricalData] = useState([{
        baseStock: {
            id: null,
            ticker: '',
            name: null,
            current_price: null
        },
        timestamp: {
            id: null,
            time: '',
            unix: 0
        },
        id: null,
        price: 0
    }])


    //Fetching the data

    useEffect(() => {
        axios.get('historicalstock/gethistoricalprice?ticker=' + ticker)
            .then((response) => {
                setHistoricalData(response.data);
            })
        setIsLoading(false);
    }, [])


    //Formatting correct into the arrays
    //Starting at 1 since init value is null
    //Converting timestamp.unix to epoch which apexchart thinks is valid 

    let seriesHistory = [];
    historicalData.map((item) => {
        if (item.price != null && item.timestamp != null) {

            //If both valid add them to array
            seriesHistory.push([item.timestamp.time, item.price])
        }
                
    })

    //So its shows from earliest to newest price.

    seriesHistory.reverse();

    //Sorting. There was a bug in the API that the dates were not ordered so the graph was weird

    seriesHistory.sort((a, b) => {
        return (a[0] < b[0]) ? -1 : 1;
    })

    console.log(seriesHistory);


    return (
        isLoading === true
            ?

            <div
                style={{
                    display: "flex",
                    justifyContent: "center",
                    alignItems: 'center'
                }}>
                <CircularProgress color="success" />
            </div>

            :

            <StockGraph
                ticker={ticker}
                seriesHistory={seriesHistory} />
        );
}