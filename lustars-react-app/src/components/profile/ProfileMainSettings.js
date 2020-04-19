import React, { Component } from "react"
import { connect } from "react-redux"
import { updateUserProfileBoleanField, updateUserProfileTextField } from '../../store/actions/profileActions'
import YesNoInputField from '../../components/profile/YesNoInputField'
import NumbersField from '../../components/profile/NumbersField'
import Avatar from '../../components/profile/Avatar'
import '../../styles/components/profile/ProfileMainSettings.css'

class ProfileMainSettings extends Component {
    constructor(props) {
        super(props)

        this.state = {
            showNameInput: false,
            showLastNameInput: false
        }
    }

    showHideNameInput() {
        let currentState = this.state.showNameInput

        if (!currentState) {
            this.setState({
                showNameInput: true
            })
        } else {
            this.setState({
                showNameInput: false
            })
        }
    }

    showHideLastNameInput() {
        let currentState = this.state.showLastNameInput

        if (!currentState) {
            this.setState({
                showLastNameInput: true
            })
        } else {
            this.setState({
                showLastNameInput: false
            })
        }
    }

    hideNameInput(event) {
        if (event.target.value.length > 0) {
            this.showHideNameInput()
        }
    }

    hideLastNameInput(event) {
        if (event.target.value.length > 0) {
            this.showHideLastNameInput()
        }
    }

    updateProfileTextField(field, value) {
        let oldState = this.props.profile
        let newState = Object.assign({}, oldState)

        switch (field) {
            case 'name':
                newState.name = value
                this.props.updateUserProfileTextField(newState)
                return
            case 'lastName':
                newState.lastName = value
                this.props.updateUserProfileTextField(newState)
                return
            case 'city':
                newState.city = value
                this.props.updateUserProfileTextField(newState)
                return
            case 'country':
                newState.country = value
                this.props.updateUserProfileTextField(newState)
                return
            default:
                return
        }
    }

    updateProfileBooleanField = (field, newValue) => {
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
        let namePxLength = this.props.profile.name !== undefined ? this.props.profile.name.length : 0
        let lastNamePxLenght = this.props.profile.lastName !== undefined ? this.props.profile.lastName.length : 0
        return (
            <div className="profileMainSettings">
                <div className="profileFullName">
                    {
                        this.state.showNameInput !== true
                        ?   <span onClick={ this.showHideNameInput.bind(this) }>{ this.props.profile.name}&nbsp;</span>
                        :   <input
                                autoFocus
                                type="text"
                                placeholder="First Name"
                                className="profileFullNameInput"
                                style={{ maxWidth: namePxLength < 3 ? 120 : namePxLength * 14 }}
                                defaultValue={ this.props.profile.name }
                                onBlur={ this.hideNameInput.bind(this) }
                                onChange={(e) => this.updateProfileTextField("name", e.target.value)}
                            />
                    }
                    {
                        this.state.showLastNameInput !== true
                        ?   <span onClick={ this.showHideLastNameInput.bind(this) }>{ this.props.profile.lastName }</span>
                        :   <input
                                autoFocus
                                type="text"
                                placeholder="Last Name"
                                className="profileFullNameInput"
                                style={{ maxWidth: lastNamePxLenght < 3 ? 120 : lastNamePxLenght * 14 }}
                                defaultValue={ this.props.profile.lastName }
                                onBlur={ this.hideLastNameInput.bind(this) }
                                onChange={(e) => this.updateProfileTextField("lastName", e.target.value)}
                            />
                    }
                </div>
                <div className="settings">
                    <div>
                        <Avatar
                            imageUrl={ this.props.profile.avatarImage }
                        />
                    </div>
                    <div>
                        <label>
                            <span className="inputLabelMainSettings">Location:</span>
                            <input
                                type="text"
                                placeholder="Location"
                                className="textInput"
                                readOnly defaultValue="Stara Zagora, Bulgaria"
                            />
                        </label>
                        <label>
                            <span className="inputLabelMainSettings">From City:</span>
                            <input
                                type="text"
                                placeholder="City"
                                className="textInput"
                                defaultValue={ this.props.profile.city }
                                onChange={(e) => this.updateProfileTextField("city", e.target.value)}
                            />
                        </label>
                        <label>
                            <span className="inputLabelMainSettings">From Country:</span>
                            <input
                                type="text"
                                placeholder="Country"
                                className="textInput"
                                defaultValue={ this.props.profile.country }
                                onChange={(e) => this.updateProfileTextField("country", e.target.value)}
                            />
                        </label>
                    </div>
                    <br/>
                    <span>
                        <YesNoInputField
                            label="Profile Active"
                            value={ this.props.profile.isUserProfileActivated }
                            switchValue={(newValue) => this.updateProfileBooleanField('isProfileActive', newValue)}
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
                            switchValue={(newValue) => this.updateProfileBooleanField("isEmailSubscribed", newValue)}
                        />
                    </span>
                </div>
            </div>
        )
    }
}

const mapDispatchToProps = dispatch => {
    return {
        updateUserProfileBoleanField: (newValue) => dispatch(updateUserProfileBoleanField(newValue)),
        updateUserProfileTextField: (newValue) => dispatch(updateUserProfileTextField(newValue))
    }
}

export default connect(null, mapDispatchToProps)(ProfileMainSettings)