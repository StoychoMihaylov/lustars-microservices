import React, { Component } from "react"
import { connect } from 'react-redux'
import { NavItem, NavLink } from 'reactstrap';
import { push, goBack } from "connected-react-router"
import { logoutAccount } from '../../store/actions/accountActions'
import { NotificationManager} from 'react-notifications'
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
                  NotificationManager.success('Loged out!', "", 3000);
                  window.location.reload(false)
              } else {
                  localStorage.clear()
                  NotificationManager.error('Connection problem! Please try again', "", 3000);
                  window.location.reload(false)
              }
          })
    }

    render() {
        return (
            <div>
                {
                    localStorage.getItem('lustars_token') !== null
                    ?
                    <div className="navbar-nav-attribute">
                        <button className="cursor-pointer navbar-link" onClick={ () => this.props.push("/my-profile") }>Hello { localStorage.getItem("lustars_user_name") }!</button>
                        <button className="logout-btn cursor-pointer navbar-link rightmost-link" onClick={ this.logoutUser.bind(this) }>LogOut</button>
                    </div>
                    :
                    <div className="navbar-nav-attribute">
                        <button className='cursor-pointer navbar-link' onClick={ () => this.props.push("/account/login") }>login</button>/
                        <button className='cursor-pointer navbar-link' onClick={ () => this.props.push("/account/registration") }>Register</button>
                    </div>
                }
            </div>
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