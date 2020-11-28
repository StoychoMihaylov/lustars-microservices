import React from 'react'
import { Route, Redirect } from 'react-router-dom'
import { NotificationContainer } from 'react-notifications';
import Layout from './screens/Layout/Layout'
import HomePage from './screens/Home/HomePage'
import ProfileEditPage from './screens/UserProfile/EditMyProfile/ProfileEditPage'
import AccountRegistrationPage from './screens/Account/Registration/AccountRegistrationPage'
import AccountLoginPage from './screens/Account/Login/AccountLoginPage'
import PeopleNearbyPage from './screens/UserProfile/NearBy/PeopleNearbyPage'
import ProfileDetailsPage from './screens/UserProfile/Profile/ProfileDetailsPage'
import WhoMatchedMe from './screens/UserProfile/Matches/WhoMatchedMe'
import WhoLikedMePage from './screens/UserProfile/WhoLikedMe/WhoLikedMePage'
import MyProfileVisitors from './screens/UserProfile/Visitors/MyProfileVisitors'
import ChatPage from './screens/Chat/ChatPage'
import NotificationService from './components/common/NotificationService/NotificationService'

import 'react-notifications/lib/notifications.css'


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
      <NotificationService />
      <Route exact path='/' component={ HomePage } />
      <Route exact path='/home' component={ HomePage } />
      <Route exact path='/account/login' component={ AccountLoginPage } />
      <Route exact path='/account/registration' component={ AccountRegistrationPage } />

      <PrivateRoute exact path='/messages' component={ ChatPage } />
      <PrivateRoute exact path='/my-profile' component={ ProfileEditPage } />
      <PrivateRoute exact path='/people-nearby' component={ PeopleNearbyPage } />
      <PrivateRoute exact path='/profile/:id' component={ ProfileDetailsPage } />
      <PrivateRoute exact path='/who-matched-me' component={ WhoMatchedMe } />
      <PrivateRoute exact path='/who-liked-me' component={ WhoLikedMePage } />
      <PrivateRoute exact path='/who-visited-me' component={ MyProfileVisitors } />
    </Layout>
)
