import React from 'react'
import { Container } from 'reactstrap'
import NavMenu from '../components/common/NavMenu'
import EventNotifications from '../components/common/EventNotifications'

export default props => (
  <div>
    <NavMenu />
    <EventNotifications />
    <Container>
      {props.children}
    </Container>
  </div>
);
