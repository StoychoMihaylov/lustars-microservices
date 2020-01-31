import React from 'react';
import { Container } from 'reactstrap';
import NavMenu from '../components/NavMenu';

export default props => (
  <div>
    <NavMenu />
    <Container>
      {props.children}
    </Container>
  </div>
);
