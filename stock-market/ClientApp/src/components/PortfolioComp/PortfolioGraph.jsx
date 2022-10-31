import React, { useState } from 'react';
import Chart from "react-apexcharts";


import Box from '@mui/material/Box';
import CircularProgress from '@mui/material/CircularProgress';
import { useTheme } from '@mui/material/styles';
import Typography from '@mui/material/Typography';

export default function PortfolioGraph({ portfolio }) {
    const customTheme = useTheme();

    //Separating the 3 values into different arrays.

    let liquidValue = [];
    let stockValue = [];
    let totalValue = [];

    let date = [];

    portfolio.map(item => {
        const lVal = item.liquid_value;
        const sVal = item.stock_value;
        const tVal = item.total_value;
        const theDate = item.time;



        liquidValue.push([theDate, lVal]);
        stockValue.push([theDate, sVal]);
        totalValue.push([theDate, tVal]);
    });

    //Datapoints
    const series = [
        {
            name: "Liquid Value",
            data: liquidValue
        },
        {
            name: "Stock Value",
            data: stockValue
        },
        {
            name: "Total Value",
            data: totalValue
        },
    ];

    //Options for chart
    const options = {
        chart: {
            foreColor: customTheme.palette.primary.contrastText,
            height: 'auto',
            type: 'line',
            toolbar: {
                show: false
            },
            zoom: {
                enabled: false,
            },
            animation: {
                enabled: false
                },
        },
        markers: {
            size: 0
        },
        stroke: {
            curve: 'smooth',
            width: 1,
        },
        xaxis: {
            type: 'datetime',
            labels: {
                datetimeUTC: false
            },
        },
        yaxis: {
            title: {
                text: 'Value (USD)'
            },
            
        },
        grid: {
            show: true,
            borderColor: customTheme.palette.primary.light
        },
        legend: {
            position: 'top',
            horizontalAlign: 'right'
        },
        tooltip: {
            theme: 'dark',
        },

    };

    return (
            <>
                <Chart
                    options={options}
                    series={series}
                    type="line"
                    height='auto'
                />
            </>
        );
}
