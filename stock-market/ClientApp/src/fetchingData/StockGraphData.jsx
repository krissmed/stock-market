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


    //get the data from the api asyncronously and then update setIsLoading
    useEffect(() => {
        async function fetchData() {
            const response = await axios.get('historicalstock/gethistoricalprice?ticker=' + ticker);
            setHistoricalData(response.data);
            setIsLoading(false);
        }
        fetchData();
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