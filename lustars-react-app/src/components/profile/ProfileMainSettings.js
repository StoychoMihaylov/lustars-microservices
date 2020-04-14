import React, { Component } from "react"
import { connect } from "react-redux"
import { updateUserProfileBoleanField, updateuserProfileTextField } from '../../store/actions/profileActions'
import YesNoInputField from '../../components/profile/YesNoInputField'
import NumbersField from '../../components/profile/NumbersField'
import Avatar from '../../components/profile/Avatar'

class ProfileMainSettings extends Component {
    constructor(props) {
        super(props)
    }

    updateProfileTextField(field, value) {
        let oldState = this.props.profile
        let newState = Object.assign({}, oldState)

        switch (field) {
            case 'name':
                newState.name = value
                this.props.updateuserProfileTextField(newState)
                return
            case 'lastName':
                newState.lastName = value
                this.props.updateuserProfileTextField(newState)
                return
            default:
                return
        }
    }

    updateIsProfileActive = (field, newValue) => {
        let oldState = this.props.profile
        let newState = Object.assign({}, oldState)

        switch (field) {
            case 'isProfileActive':
                newState.isUserProfileActivated = newValue
                this.props.updateUserProfileBoleanField(newState)
                return
            case 'isEmailSubscribed':
                newState.emailNotificationsSubscribed = newValue
                this.props.updateUserProfileBoleanField(newState)
                return
            default:
                return
        }
    }

    render() {
        return (
            <div>
                <div>
                    <input
                        type="text"
                        defaultValue={ this.props.profile.name }
                        onChange={(e) => this.updateProfileTextField("name", e.target.value)}
                    />
                    <input
                        type="text"
                        defaultValue={ this.props.profile.lastName }
                        onChange={(e) => this.updateProfileTextField("lastName", e.target.value)}
                    />
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
                        switchValue={(newValue) => this.updateIsProfileActive('isProfileActive', newValue)}
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
                        switchValue={(newValue) => this.updateIsProfileActive("isEmailSubscribed", newValue)}
                    />
                </span>
            </div>
        )
    }
}

const mapDispatchToProps = dispatch => {
    return {
        updateUserProfileBoleanField: (newValue) => dispatch(updateUserProfileBoleanField(newValue)),
        updateuserProfileTextField: (newValue) => dispatch(updateuserProfileTextField(newValue))
    }
}

export default connect(null, mapDispatchToProps)(ProfileMainSettings)