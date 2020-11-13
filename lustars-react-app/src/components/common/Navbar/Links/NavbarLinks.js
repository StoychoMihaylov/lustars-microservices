import React, { Component } from "react"
import { connect } from 'react-redux'
import { NavItem, NavLink } from 'reactstrap';
import { push, goBack } from "connected-react-router"

class NavbarLinks extends Component {
    render() {
        return (
            <NavItem>
                 <ul className="navbar-nav flex-grow">
                    <NavLink className="cursor-pointer navbar-link" onClick={() => this.props.push("/home")} >Home</NavLink>
                    <NavLink className="cursor-pointer navbar-link">About us</NavLink>
                </ul>
            </NavItem>
        )
    }
}

const mapDispatchToProps = dispatch => {
    return {
        // Navigation
        goBack: () => dispatch(goBack()),
        push: (url) => dispatch(push(url)),
    }
  }

export default connect(null, mapDispatchToProps)(NavbarLinks)