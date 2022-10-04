import Nav from './components/Nav';
import React, { Component } from 'react';


export default class App extends Component {
  static displayName = App.name;

  render () {
      return (
        <>
              <Nav />
        </>
              );
  }
}
