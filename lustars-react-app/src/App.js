import React from 'react';
import { Route } from 'react-router-dom';
import Layout from './views/Layout';
import HomePage from './views/HomePage'
import AccountRegistration from './views/AccountRegistration';

export default () => (
  <Layout>
    <Route exact path='/' component={HomePage} />
    <Route exact path='/home' component={HomePage} />
    <Route exact path='/account/registration' component={AccountRegistration} />
  </Layout>
);
