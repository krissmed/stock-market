import React, { useState, useEffect } from 'react';
import { Outlet } from "react-router-dom";
import Nav from '../components/Nav';
import RespNav from '../components/RespNav'

export function Layout() {
    const [width, setWindowWidth] = useState(0);
    const [height, setWindowHeight] = useState(0);
    const updateDimensions = () => {
        const width = window.innerWidth
        setWindowWidth(width)

    }
    useEffect(() => {

        updateDimensions();

        window.addEventListener("resize", updateDimensions);
    })

    const isMobile = width < 640;


    return (
        <>
            {isMobile ? <RespNav /> : <Nav />}

            <Outlet />
        </>
    );
}
