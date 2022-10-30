import React, { useEffect, useState } from 'react';
import axios from 'axios';
import CircularProgress from '@mui/material/CircularProgress';
import WatchlistTable from '../components/WatchlistComp/WatchlistTable';

export default function WatchlistData() {
    const [isLoading, setIsLoading] = useState(true);

    const [watchlist, setWatchlist] = useState();

    useEffect(() => {
        async function fetchData() {
            const response = await axios.get('watchlist/getfullwatchlist');
            setWatchlist(response.data);
            setIsLoading(false);
        }
        fetchData();
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
                <WatchlistTable data={watchlist} />
            </>
        );
}
