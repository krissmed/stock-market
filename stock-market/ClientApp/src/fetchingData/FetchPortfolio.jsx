import React, { useEffect, useState, setState } from 'react';
import PortfolioTable from '../components/PortfolioComp/PortfolioTable';
import CircularProgress from '@mui/material/CircularProgress';

import axios from 'axios';


export default function FetchTransactions() {
    const [isLoading, setIsLoading] = useState(true);

   
    const [portfolio, setPortfolios] = useState([]);

    useEffect(() => {
        axios.get("portfolio/getcurrentportfolio")
            .then(res => {
                console.log(res)
                setPortfolios(res.data)
                

                setIsLoading(false);
            })
    }, [])


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
                <PortfolioTable data={portfolio} />
            </>
    );
}