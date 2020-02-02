import React from 'react'
import { Container } from 'reactstrap'
import NavMenu from '../components/NavMenu'
import EventNotifications from '../components/EventNotifications'

export default props => (
  <div>
    <NavMenu />
    <EventNotifications />
    <Container>
      {props.children}
    </Container>
  </div>
);
