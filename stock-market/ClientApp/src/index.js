import 'bootstrap/dist/css/bootstrap.css';
import React from 'react';
import ReactDOM from 'react-dom';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import registerServiceWorker from './registerServiceWorker';

import Dashboard from './pages/Dashboard';
import Portfolio from './pages/Portfolio';
import Stocks from './pages/Stocks';
import Transactions from './pages/Transactions';
import Watchlist from './pages/Watchlist';
import Edituser from './pages/Edituser';

import { Layout } from './pages/Layout';
import { createTheme, ThemeProvider } from '@mui/material/styles';
import CssBaseline from '@mui/material/CssBaseline';


const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href');
const rootElement = document.getElementById('root');



const customTheme = createTheme({
    palette: {
        primary: {
            main: '#252638',
            contrastText: '#FEFEFE'
        },
        background: {
            default: '#27262B',
        },
        error: {
            main: '#f5374a'
        },
        success: {
            main: '#4fa579'
        },
    },
});

ReactDOM.render(
    <ThemeProvider theme={customTheme}>
        <CssBaseline />

    <BrowserRouter basename={baseUrl}>

        <Routes>
            <Route path="/" element={<Layout />}>
                <Route index element={<Dashboard />} />
                <Route path="stocks" element={<Stocks />} />
                <Route path="portfolio" element={<Portfolio />} />
                <Route path="watchlist" element={<Watchlist />} />
                <Route path="transactions" element={<Transactions />} />
                <Route path="edituser" element={<Edituser />} />
            </Route>
        </Routes>

        </BrowserRouter>
    </ThemeProvider>,
  rootElement);

registerServiceWorker();

