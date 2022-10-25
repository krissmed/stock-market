import SubTitle from './components/EksempelKomp';
import React, { Component } from 'react';


export default class App extends Component {
  static displayName = App.name;

  render () {
      return (
        <>
              <h1>Her skal Stock Market appen utvikles. Her legges alle komponentene inn</h1>
              <SubTitle />
        </>
              );
  }
}
