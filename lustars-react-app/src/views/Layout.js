import React from 'react'
import { Container } from 'reactstrap'
import NavMenu from '../components/common/NavMenu'
import Footer from '../components/common/Footer'
import ActiitiesMenu from '../components/common/ActivitiesMenu'
import '../styles/views/Layout.css'

export default props => (
  <div className="layout-container">
    <NavMenu />
    <ActiitiesMenu />
      <Container>
        {props.children}
      </Container>
    <Footer />
  </div>
);
