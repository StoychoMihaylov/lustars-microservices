import React from 'react'
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap'
import FormAccountLogout from '../authentication/FormAccountLogout'
import NavbarLinks from './NavbarLinks'
import ActivitiesMenu from '../common/ActivitiesMenu'
import '../../styles/components/common/NavMenu.css'


export default class NavMenu extends React.Component {
  constructor (props) {
    super(props);

    this.toggle = this.toggle.bind(this);
    this.state = {
      isOpen: false,
      isActivitiesActive: false
    };
  }

  toggle () {
    this.setState({
      isOpen: !this.state.isOpen
    });
  }

  openActivitiesMenu() {
    if (this.state.isActivitiesActive === false) {
      this.setState({
        isActivitiesActive: true
      })
    } else {
      this.setState({
        isActivitiesActive: false
      })
    }
  }

  render () {
    return (
      <header>
        <Navbar className="navbar-expand-sm navbar-toggleable-sm navbar">
          <Container>
            <NavbarBrand to="/">Lustars logo!</NavbarBrand>
            <NavbarToggler onClick={this.toggle} className="mr-2" />
            <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={this.state.isOpen} navbar>
              <ul className="navbar-nav flex-grow">
                <NavbarLinks />
                <NavItem>
                  <NavLink>|</NavLink>
                </NavItem>
                <NavLink className="cursor-pointer navbar-link" onClick={ this.openActivitiesMenu.bind(this) }>Activities</NavLink>
                <NavItem>
                  <NavLink>|</NavLink>
                </NavItem>
                <FormAccountLogout />
              </ul>
            </Collapse>
          </Container>
        </Navbar>
        <Container>
          {
            this.state.isActivitiesActive === true
              ? <ActivitiesMenu />
              : null
          }
        </Container>
      </header>
    )
  }
}
