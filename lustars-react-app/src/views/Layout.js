import React from 'react'
import { Container } from 'reactstrap'
import NavMenu from '../components/common/NavMenu'
import Footer from '../components/common/Footer'
import EventNotifications from '../components/common/EventNotifications'
import '../styles/views/Layout.css'

export default props => (
  <div className="layout-container">
    <NavMenu />
    <EventNotifications />
    <Container>
      {props.children}
    </Container>
    <Footer />
  </div>
);
