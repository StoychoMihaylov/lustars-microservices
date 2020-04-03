import React, { Component } from "react"
import YesNoInputField from '../../components/profile/YesNoInputField'
import NumbersField from '../../components/profile/NumbersField'
import Avatar from '../../components/profile/Avatar'

class ProfileMainSettings extends Component {
    constructor(props) {
        super(props)
    }

    updateIsProfileActive = (newValue) => {
        this.props.updateIsProfileActive(newValue)
    }

    updateIsEmailSubscribed = (newValue) => {
        this.props.updateIsEmailSubscribed(newValue)
    }

    render() {
        return (
            <div>
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

export default ProfileMainSettings