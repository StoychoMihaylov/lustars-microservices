import React from 'react'
import { Container } from 'reactstrap'
import NavMenu from '../components/common/NavMenu'
import Footer from '../components/common/Footer'
import '../styles/views/Layout.css'

export default props => (
  <div className="layout-container">
    <NavMenu />
    <Container>
      {props.children}
    </Container>
    <Footer />
  </div>
);
