import React from 'react'
import { Container } from 'reactstrap'
import NavMenu from '../../components/common/Navbar/Menu/NavMenu'
import Footer from '../../components/common/Footer/Footer'
import './Layout.css'

export default props => (
  <div className="layout-container">
    <NavMenu />
      <Container>
        {props.children}
      </Container>
    <Footer />
  </div>
);
