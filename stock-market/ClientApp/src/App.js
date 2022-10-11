import Nav from './components/Nav';
import React, { Component } from 'react';
import { createTheme, ThemeProvider } from '@mui/material/styles';
import CssBaseline from '@mui/material/CssBaseline';
import MyAvatar from "./components/Avatar"

const customTheme = createTheme({
    palette: {
        primary: {
            main: '#252638',
            contrastText: '#FEFEFE'
        },
        background: {
            default: '#27262B'
        },
        error: {
            main: '#CB6508'
        },
        success: {
            main: '#65CC14'
        },
    }
});

export default class App extends Component {
    static displayName = App.name;



  render () {
      return (
          <ThemeProvider theme={customTheme}>
              <CssBaseline />
              <MyAvatar />
              <Nav />
          </ThemeProvider>
              );
  }
}
