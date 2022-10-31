import React, { useEffect, useState } from 'react';
import PortfolioTable from '../components/PortfolioComp/PortfolioTable';
import PortfolioGraph from '../components/PortfolioComp/PortfolioGraph';
import CircularProgress from '@mui/material/CircularProgress';
import Grid from '@mui/material/Grid';

import axios from 'axios';


export default function FetchPortfolio() {
    const [isLoading, setIsLoading] = useState(true);

   
    const [portfolio, setPortfolios] = useState([]);
    const [historicalFolio, setHistoricalFolio] = useState([]);

    useEffect(() => {

        const fetchData = async () => {


            const curFolio = await axios.get("portfolio/getcurrentportfolio");
            const histFolio = await axios.get("portfolio/gethistoricalportfolios");

            setPortfolios(curFolio.data);
            setHistoricalFolio(histFolio.data);

            setIsLoading(false)
        }
        fetchData();


    }, []);


    return (
        isLoading == true ?
            <>
                <div
                    style={{
                        display: "flex",
                        justifyContent: "center",
                        alignItems: 'center'
                    }}>
                    <CircularProgress color="success" />
                </div>
            </>
            :
            <Grid container spacing={2}>
                <Grid item xs={12}>
                    <PortfolioTable portfolio={portfolio} />
                </Grid>
                <Grid item xs={12}>
                    <PortfolioGraph portfolio={historicalFolio} />
                </Grid>
            </Grid>
    );
}