import React from 'react';
import Chart from "react-apexcharts";
import Box from '@mui/material/Box';

import { useTheme } from '@mui/material/styles';
import Typography from '@mui/material/Typography';


export default function DashboardGraph() {

    const customTheme = useTheme();

    const cSeries = [{
        name: 'Liquid',
        data: [23, 24, 25, 15, 25, 28, 38, 46],
    },
    {
        name: 'Stock Worth',
        data: [20, 29, 57, 36, 44, 45, 50, 58],
    },
    {
            name: 'Total Value',
            data: [43, 29+24, 57+25, 36+15, 44+25, 45+28, 50+38, 58+46],
    },
    ];
    const cOptions = {
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
        },
        stroke: {
            curve: 'smooth',
            width: 1,
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
        <Box sx={{
            backgroundColor: customTheme.palette.primary.main,
            color: customTheme.palette.primary.contrastText,
            padding: 1,

        }}>
            <Typography 
                color='customTheme.palette.primary.contrastText'
                variant='h6'
                sx={{
                    padding: 1
                }}            >
                    Your Balance
            </Typography>


                <Chart
                    options={cOptions}
                    series = {cSeries}
                    type="line"
                    height='250'
                />
            </Box>
        );
}
