import React, { Component } from "react"
import { connect } from "react-redux"
import { changeIsUserActive, changeUserEmailSubsribed } from '../../store/actions/profileActions'
import YesNoInputField from '../../components/profile/YesNoInputField'
import NumbersField from '../../components/profile/NumbersField'
import Avatar from '../../components/profile/Avatar'

class ProfileMainSettings extends Component {
    constructor(props) {
        super(props)
    }

    updateIsProfileActive = (newValue) => {
        let oldState = this.props.profile
        let newState = Object.assign({}, oldState)
        newState.isUserProfileActivated = newValue

        this.props.changeIsUserActive(newState)
    }

    updateIsEmailSubscribed = (newValue) => {
        let oldState = this.props.profile
        let newState = Object.assign({}, oldState)
        newState.emailNotificationsSubscribed = newValue

        this.props.changeUserEmailSubsribed(newState)
    }

    render() {
        return (
            <div>
                <div>
                    <input type="text" defaultValue={ this.props.profile.name } />
                    <input type="text" defaultValue={ this.props.profile.lastName } />
                </div>
                <div>
                    <Avatar
                        imageUrl={ this.props.profile.avatarImage }
                    />
                </div>
                <br/>
                <span>
                    <YesNoInputField
                        label="Profile Active"
                        value={ this.props.profile.isUserProfileActivated }
                        switchValue={this.updateIsProfileActive.bind(this)}
                    />
                </span>
                <br/>
                <span>
                    <NumbersField
                        label="Credits"
                        value={ this.props.profile.credits }
                    />
                </span>
                <br/>
                <span>
                    <NumbersField
                        label="Super likes"
                        value={ this.props.profile.superlikes }
                    />
                </span>
                <br/>
                <span>
                    <YesNoInputField
                        label="Email subscribed"
                        value={ this.props.profile.emailNotificationsSubscribed }
                        switchValue={this.updateIsEmailSubscribed.bind(this)}
                    />
                </span>
            </div>
        )
    }
}

const mapDispatchToProps = dispatch => {
    return {
        changeIsUserActive: (newValue) => dispatch(changeIsUserActive(newValue)),
        changeUserEmailSubsribed: (newValue) => dispatch(changeUserEmailSubsribed(newValue)),
    }
}

export default connect(null, mapDispatchToProps)(ProfileMainSettings)