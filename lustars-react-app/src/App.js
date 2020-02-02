import React from 'react'
import { Route, Redirect } from 'react-router-dom'
import Layout from './views/Layout'
import HomePage from './views/HomePage'
import AccountRegistrationPage from './views/AccountRegistrationPage'
import SomeInternalPage from './views/SomeInternalPage'
import AccountLoginPage from './views/AccountLoginPage'

// Custom private route (if your is not auth via token redirect to login page)
const PrivateRoute = ({ component: Component, ...rest }) => (
  <Route {...rest} render={(props) => (
        localStorage.getItem('lustars_token') !== null
          ? <Component { ...props } />
          : <Redirect to='/account/login' />
      )}
  />
)

export default () => (
  <Layout>
    <Route exact path='/' component={HomePage} />
    <Route exact path='/home' component={HomePage} />
    <Route exact path='/account/login' component={AccountLoginPage} />
    <Route exact path='/account/registration' component={AccountRegistrationPage} />
    <PrivateRoute exact path='/some-internal-page' component={SomeInternalPage} />
  </Layout>
);
