import React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import HomePage from './views/HomePage'

export default () => (
  <Layout>
    <Route exact path='/' component={HomePage} />
    <Route exact path='/home' component={HomePage} />
  </Layout>
);
