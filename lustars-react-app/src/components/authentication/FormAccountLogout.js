import React, { Component } from "react"
import { connect } from 'react-redux'
import { NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import { logoutAccount } from '../../store/actions/accountActions'
import {
    infoNotification,
    successfulNotification,
    errorNotification
} from '../../store/actions/eventNotifications'
import '../../styles/components/FormAccountLogout.css'

class FormAccountLogout extends Component {

    logoutUser() {
        let userToken = {
          userId: localStorage.getItem("lustars_user_id"),
          token: localStorage.getItem("lustars_token")
        }
        this.props.logoutAccount(userToken)
          .then(response => {
            console.log(response)
              if (response.status === 200) {
                  localStorage.clear()
                  this.props.successfulNotification("Loged out!")
                  window.location.reload(false);
              } else {
                  this.props.errorNotification("Connection problem! Please try again")
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
                        <NavItem className="cursorPointer">
                        <NavLink className="text-dark">Hello {localStorage.getItem("lustars_user_name")}!</NavLink>
                        </NavItem>
                        <NavItem>
                            <NavLink className="logoutBtn" onClick={this.logoutUser.bind(this)}>LogOut</NavLink>
                        </NavItem>
                    </ul>
                    :
                    <ul className="navbar-nav flex-grow">
                        <NavItem>
                            <NavLink tag={Link} className="text-dark" to="/account/login" >login</NavLink>
                        </NavItem>
                        <NavItem>
                            <NavLink className='cursorPointer'>/</NavLink>
                        </NavItem>
                        <NavItem>
                            <NavLink tag={Link} className="text-dark" to="/account/registration" >Register</NavLink>
                        </NavItem>
                    </ul>
                }
            </NavItem>
        )
    }
}

const mapDispatchToProps = dispatch => {
    return {
        logoutAccount: (userToken) => dispatch(logoutAccount(userToken)),

        // Notifications
        infoNotification: (message) => dispatch(infoNotification(message)),
        successfulNotification: (message) => dispatch(successfulNotification(message)),
        errorNotification: (message) => dispatch(errorNotification(message))
    }
  }

export default connect(null, mapDispatchToProps)(FormAccountLogout)