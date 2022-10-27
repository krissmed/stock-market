import React, { useState } from 'react';
import Chart from "react-apexcharts";
import Box from '@mui/material/Box';
import { useTheme } from '@mui/material/styles';

export default function StockGraph({ ticker, seriesHistory }) {

    const customTheme = useTheme();
    const series = [{
        name: ticker,
        data: seriesHistory
    }];

    const options = {
        chart: {
            foreColor: customTheme.palette.primary.contrastText,
            type: 'area',
            zoom: {
                type: 'x',
                enabled: true,
                autoScaleYaxis: true
            },

        },
        stroke: {
            curve: 'smooth',
            width: 1
        },
        title: {
            text: ticker + ' price history',
            align: 'left'
        },
        yaxis: {
            title: {
                text: 'Value (USD)'
            },
            labels: {
                formatter: function (val) {
                    return val.toFixed() + '$';
                }
            }
        },
        xaxis: {
            type: 'datetime',
            labels: {
                datetimeUTC: false
            }
        },
        tooltip: {
            x: {
                format: 'dddd d MMM - HH:mm:ss'
            },
            theme: 'dark'
        },
        grid: {
            show: true,
            borderColor: customTheme.palette.primary.light
        },
        fill: {
            type: "gradient",
            gradient: {
                opacityFrom: 0.8,
                opacityTo: 0,
            },
            colors: customTheme.palette.primary.main
        },
        dataLabels: {
            enabled: false,
        },
    }


    return (

            <Box sx={{
                maxWidth: '100%'
            }}>
                <Chart
                    options={options}
                    series={series}
                    type='area'
                />
            </Box>
       
    );
}