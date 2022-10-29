import React from 'react';
import { Outlet } from "react-router-dom";
import Nav from '../components/Nav';


export function isMobile() {
    const [width, setWindowWidth] = useState(0);
    const [height, setWindowHeight] = useState(0);
    const updateDimensions = () => {
        const width = window.innerWidth
        const height = window.innerHeight
        setWindowWidth(width)
        setWindowHeight(height)

    }
    useEffect(() => {

        updateDimensions();

        window.addEventListener("resize", updateDimensions);
    })

    const isMobile = width < 640;
    return isMobile;
}


export function Layout() {

return (
        <>
            {isMobile() ? <></> : <Nav />}

            <Outlet />
        </>
    );
}
