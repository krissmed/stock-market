import React, { useEffect, useState } from 'react';
import PortfolioGraph from '../PortfolioComp/PortfolioGraph';
import CircularProgress from '@mui/material/CircularProgress';
import axios from 'axios';

export default function DashboardGraph() {
    const [isLoading, setIsLoading] = useState(true);

    const [historicalFolio, setHistoricalFolio] = useState([]);

    useEffect(() => {

        const fetchData = async () => {


            const histFolio = await axios.get("portfolio/gethistoricalportfolios");

            if (histFolio.status === 401) {
                    window.location.href = "/login";
                    localStorage.setItem('isLoggedIn', false);
            }

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
            <>
                <PortfolioGraph portfolio={historicalFolio} />
            </>
        );
}
