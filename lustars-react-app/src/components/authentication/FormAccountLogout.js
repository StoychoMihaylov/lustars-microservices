import React, { Component } from "react"
import { connect } from 'react-redux'
import { NavItem, NavLink } from 'reactstrap';
import { push, goBack } from "connected-react-router"
import { logoutAccount } from '../../store/actions/accountActions'
import '../../styles/components/authentication/FormAccountLogout.css'

class FormAccountLogout extends Component {

    logoutUser() {
        let userToken = {
          userId: localStorage.getItem("lustars_user_id"),
          token: localStorage.getItem("lustars_token")
        }
        this.props.logoutAccount(userToken)
          .then(response => {
              if (response.status === 200) {
                  localStorage.clear()
                  this.props.successfulNotification("Loged out!")
                  window.location.reload(false)
              } else {
                  localStorage.clear()
                  this.props.errorNotification("Connection problem! Please try again")
                  window.location.reload(false)
              }
          })
    }

    render() {
        return (
            <NavItem>
                {
                    localStorage.getItem('lustars_token') !== null
                    ?
                    <ul className="navbar-nav flex-grow">
                        <NavLink className="cursor-pointer navbar-link" onClick={ () => this.props.push("/profile") }>Hello { localStorage.getItem("lustars_user_name") }!</NavLink>
                        <NavLink className="logout-btn cursor-pointer navbar-link" onClick={ this.logoutUser.bind(this) }>LogOut</NavLink>
                    </ul>
                    :
                    <ul className="navbar-nav flex-grow">
                        <NavLink className='cursor-pointer navbar-link' onClick={ () => this.props.push("/account/login") }>login</NavLink>
                        <NavLink>/</NavLink>
                        <NavLink className='cursor-pointer navbar-link' onClick={ () => this.props.push("/account/registration") }>Register</NavLink>
                    </ul>
                }
            </NavItem>
        )
    }
}

const mapDispatchToProps = dispatch => {
    return {
        logoutAccount: (userToken) => dispatch(logoutAccount(userToken)),

        // Navigation
        goBack: () => dispatch(goBack()),
        push: (url) => dispatch(push(url))
    }
  }

export default connect(null, mapDispatchToProps)(FormAccountLogout)