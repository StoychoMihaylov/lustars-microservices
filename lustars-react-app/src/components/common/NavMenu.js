import React from 'react'
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap'
import FormAccountLogout from '../authentication/FormAccountLogout'
import NavbarLinks from './NavbarLinks'
import '../../styles/components/NavMenu.css'


export default class NavMenu extends React.Component {
  constructor (props) {
    super(props);

    this.toggle = this.toggle.bind(this);
    this.state = {
      isOpen: false
    };
  }

  toggle () {
    this.setState({
      isOpen: !this.state.isOpen
    });
  }

  render () {
    return (
      <header>
        <Navbar className="navbar-expand-sm navbar-toggleable-sm border-bottom box-shadow mb-3 navbar"  >
          <Container>
            <NavbarBrand to="/">Lustars logo!</NavbarBrand>
            <NavbarToggler onClick={this.toggle} className="mr-2" />
            <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={this.state.isOpen} navbar>
              <ul className="navbar-nav flex-grow">
                <NavbarLinks />
                <NavItem>
                  <NavLink>|</NavLink>
                </NavItem>
                <FormAccountLogout />
              </ul>
            </Collapse>
          </Container>
        </Navbar>
      </header>
    );
  }
}
