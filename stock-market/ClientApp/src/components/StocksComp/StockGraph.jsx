import React from 'react';
import Chart from "react-apexcharts";
import Box from '@mui/material/Box';
import {useTheme} from '@mui/material/styles';


export default function StockGraph({stock}) {
    const customTheme = useTheme();
    const series = [{
        name: stock.name,
        data: stock.history
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
            text: stock.name + ' value history',
            align: 'left'
        },
        yaxis: {
            title: {
                text: 'Value (NOK)'
            },
            labels: {
                formatter: function (val) {
                    return val.toFixed(2) + 'kr';
                }
            }
        },
        xaxis: {
            type: 'datetime',
            categories: ["2018-09-19T00:00:00.000Z", "2018-09-19T01:30:00.000Z", "2018-09-19T02:30:00.000Z", "2018-09-19T03:30:00.000Z", "2018-09-19T04:30:00.000Z", "2018-09-19T05:30:00.000Z", "2018-09-19T06:30:00.000Z", "2018-09-19T07:30:00.000Z", "2018-09-19T08:30:00.000Z", "2018-09-19T09:30:00.000Z", "2018-09-19T10:30:00.000Z"]
        },
        tooltip: {
            x: {
                format: 'dd/MM/yy HH:mm'
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
        <>
            <Box sx={{
                maxWidth: '600px'
            }}>
                <Chart
                    options={options}
                    series={series}
                    type='area'
                />
            </Box>
        </>
    );
}