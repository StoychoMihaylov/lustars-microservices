import React from 'react'
import { Route, Redirect } from 'react-router-dom'
import { NotificationContainer } from 'react-notifications';
import Layout from './views/Layout'
import HomePage from './views/HomePage'
import ProfileEditPage from './views/ProfileEditPage'
import AccountRegistrationPage from './views/Account/AccountRegistrationPage'
import AccountLoginPage from './views/Account/AccountLoginPage'
import PeopleNearbyPage from './views/PeopleNearbyPage'
import 'react-notifications/lib/notifications.css';

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
      <NotificationContainer/>
      <Route exact path='/' component={ HomePage } />
      <Route exact path='/home' component={ HomePage } />
      <Route exact path='/account/login' component={ AccountLoginPage } />
      <Route exact path='/account/registration' component={ AccountRegistrationPage } />

      <PrivateRoute exact path='/profile' component={ ProfileEditPage } />
      <PrivateRoute exact path='/people-nearby' component={ PeopleNearbyPage } />
    </Layout>
)
