import React from 'react';
import { useTheme } from '@mui/material/styles';

import Box from '@mui/material/Box';
import Typography from '@mui/material/Typography';
import ListItem from '@mui/material/ListItem';


export default function OverViewStocks() {

    const customTheme = useTheme();

    const dummyData = [
        { key: 'Apple', value: 50},
        { key: 'Google', value: 43},
        { key: 'Safari', value: 12},
        { key: 'VS', value: 69},
        { key: 'VsCode', value: 34},
        { key: 'NITO', value: 56},
        { key: 'CapGemini', value: 43},
        { key: 'Intility', value: 74},
    ];

    return (
            <Box sx={{
                    display: 'flex',
                    flexWrap: 'noWrap',
                    justifyContent: 'space-evenly',
                    alignContent: 'center'
            }}>
                {dummyData.map(
                    (r) =>
                        <ListItem
                            key={r.key}
                            sx={{

                            }}
                        >

                            <Typography
                                variant='subtitle2'
                                sx={{
                                    color: r.value > 40 ?
                                        customTheme.palette.success.main
                                        :
                                        customTheme.palette.error.main,
                                    cursor: 'default'
                                }}
                            >
                                {r.key} {r.value}kr
                            </Typography>
                           

                            </ListItem>
            )}
            </Box>
        );
}
