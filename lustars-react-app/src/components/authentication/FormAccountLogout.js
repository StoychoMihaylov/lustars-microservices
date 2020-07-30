import React, { Component } from "react"
import { connect } from 'react-redux'
import { push, goBack } from "connected-react-router"
import { api } from '../../constants/endpoints'
import { logoutAccount } from '../../store/actions/accountActions'
import { getCurrentUserAvatarImageURL } from '../../store/actions/myProfileActions'
import { NotificationManager} from 'react-notifications'
import '../../styles/components/authentication/FormAccountLogout.css'

class FormAccountLogout extends Component {

    componentWillMount() {
        this.props.getCurrentUserAvatarImageURL()
    }

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
        let avatarImg =  this.props.currentUserAvatarImgURL !== null && this.props.currentUserAvatarImgURL !== undefined
            ?   <span><img className="avatar-image-navbar-menu" src={ api.imageAPI + this.props.currentUserAvatarImgURL } alt="" /></span>
            :   null
        return (
            <div>
                {
                    localStorage.getItem('lustars_token') !== null
                    ?
                    <div className="navbar-nav-attribute">
                        { avatarImg }
                        <button
                            className="cursor-pointer navbar-link"
                            onClick={ () => this.props.push("/my-profile") }>Hello { localStorage.getItem("lustars_user_name") }!
                        </button>
                        <button className="logout-btn cursor-pointer navbar-link rightmost-link" onClick={ this.logoutUser.bind(this) }>LogOut</button>
                    </div>
                    :
                    <div className="navbar-nav-attribute">
                        <button
                            className='cursor-pointer navbar-link'
                            onClick={ () => this.props.push("/account/login") }>login
                        </button>/
                        <button
                            className='cursor-pointer navbar-link'
                            onClick={ () => this.props.push("/account/registration") }>Register
                        </button>
                    </div>
                }
            </div>
        )
    }
}

const mapStateToProps = state => {
    return {
        currentUserAvatarImgURL: state.myProfile.currentUserAvatarImgURL,
    }
}

const mapDispatchToProps = dispatch => {
    return {
        logoutAccount: (userToken) => dispatch(logoutAccount(userToken)),
        getCurrentUserAvatarImageURL: () => dispatch(getCurrentUserAvatarImageURL()),

        // Navigation
        goBack: () => dispatch(goBack()),
        push: (url) => dispatch(push(url))
    }
  }

export default connect(mapStateToProps, mapDispatchToProps)(FormAccountLogout)