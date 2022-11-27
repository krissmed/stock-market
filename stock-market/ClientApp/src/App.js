import React, { useEffect } from 'react';
import { BrowserRouter, Routes, Route } from 'react-router-dom';

import { Layout } from './pages/Layout';

import Dashboard from './pages/Dashboard';
import Portfolio from './pages/Portfolio';
import Stocks from './pages/Stocks';
import Transactions from './pages/Transactions';
import Watchlist from './pages/Watchlist';
import Edituser from './pages/Edituser';
import LogIn from './pages/LogIn';
import SignUp from './pages/Signup';

function App() {
    var isLoggedIn = false;

    useEffect(() => {
        isLoggedIn = localStorage.getItem('isLoggedIn');
    }, [])



        return (
            <BrowserRouter>
                <Routes>
                        <Route path="/" element={<Layout/>}>
                        <Route index path="login" element={<LogIn />} />
                        <Route path="dashboard" element={<Dashboard/>}/>
                        <Route path="stocks" element={<Stocks/>}/>
                        <Route path="portfolio" element={<Portfolio/>}/>
                        <Route path="watchlist" element={<Watchlist/>}/>
                        <Route path="transactions" element={<Transactions/>}/>
                        <Route path="edituser" element={<Edituser/>}/>
                        <Route path="signup" element={<SignUp/>}/>
                    </Route>
                </Routes>

            </BrowserRouter>
        );
}

export default App;