import React, { useEffect } from 'react';
import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import { useLocation } from 'react-router-dom';

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

        return (
            <Routes>
                
                    <Route path="/" element={<Layout />}>
                            <Route index element={<Dashboard />} />
                            <Route path="stocks" element={<Stocks />} />
                            <Route path="portfolio" element={<Portfolio />} />
                            <Route path="watchlist" element={<Watchlist />} />
                            <Route path="transactions" element={<Transactions />} />
                            <Route path="edituser" element={<Edituser />} />
                    </Route>

                <Route path="/login" element={<LogIn />} />
                <Route path="/signup" element={<SignUp />} />

            </Routes>
        );
    
}

export default App;