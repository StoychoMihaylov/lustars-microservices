import React, { Component } from "react"
import { connect } from "react-redux"
import axios from 'axios'
import { updateUserProfileBoleanField, updateUserProfileTextField } from '../../store/actions/profileActions'
import YesNoInputField from '../../components/profile/YesNoInputField'
import NumbersField from '../../components/profile/NumbersField'
import Avatar from '../../components/profile/Avatar'
import '../../styles/components/profile/ProfileMainSettings.css'

class ProfileMainSettings extends Component {
    constructor(props) {
        super(props)

        this.state = {
            location: "",

            showNameInput: false,
            showLastNameInput: false
        }
    }

    setGeolocation() {
        if(navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(position => {
                this.getUserGeolocation(position)
            })

        } else {
            //TO DO: show error message
            console.log("geolocation is not allowed...")
        }
    }

    getUserGeolocation(position) {
        axios.get('https://maps.googleapis.com/maps/api/geocode/json?latlng=' +
            position.coords.latitude +
            ',' +
            position.coords.longitude +
            '&sensor=fase&key=AIzaSyCReeeqP32sURxShaQ2XHxtirN6AWGDkAY'
        )
        .then(response => {
            this.setState({
                location: response.data.results[0].formatted_address
            })
        })
        .catch({
            // TO DO: Show connection error
        })
    }

    updateProfileTextField(field, value) {
        let oldState = this.props.profile
        let newState = Object.assign({}, oldState)

        switch (field) {
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
        return (
            <div className="profile-main-settings">
                <div className="settings">
                    <div>
                        <Avatar
                            imageUrl={ this.props.profile.avatarImage }
                        />
                    </div>
                    <table className="profile-main-settings-table">
                    <tbody>
                            <tr>
                                <th></th>
                                <th></th>
                            </tr>
                            <tr>
                                <td><hr/></td>
                                <td><hr/></td>
                            </tr>
                            <tr>
                                <td><label htmlFor="location">Location:</label></td>
                                <td>
                                    {
                                        this.state.location !== ""
                                            ?  this.state.location
                                            :  <button className="location-btn" onClick={ this.setGeolocation.bind(this) }>Get Location</button>
                                    }
                                </td>
                            </tr>
                            <tr>
                                <td><label htmlFor="from-city">From City:</label></td>
                                <td>
                                    <input
                                        id="from-city"
                                        type="text"
                                        placeholder="City"
                                        className="text-input-profile-about"
                                        defaultValue={ this.props.profile.city }
                                        onChange={(e) => this.updateProfileTextField("city", e.target.value)}
                                    />
                                </td>
                            </tr>
                            <tr>
                                <td><label htmlFor="from-country">From Country:</label></td>
                                <td>
                                    <input
                                        id="from-country"
                                        type="text"
                                        placeholder="Country"
                                        className="text-input-profile-about"
                                        defaultValue={ this.props.profile.country }
                                        onChange={(e) => this.updateProfileTextField("country", e.target.value)}
                                    />
                                </td>
                            </tr>
                            <tr>
                                <td><hr/></td>
                                <td><hr/></td>
                            </tr>
                            <tr>
                                <td><label htmlFor="profile-activated">Profile Activated:</label></td>
                                <td>
                                    <YesNoInputField
                                        id="profile-activated"
                                        value={ this.props.profile.isUserProfileActivated }
                                        switchValue={(newValue) => this.updateProfileBooleanField('isProfileActive', newValue)}
                                    />
                                </td>
                            </tr>
                            <tr>
                                <td><label htmlFor="credits">Credits:</label></td>
                                <td>
                                    <NumbersField
                                        id="credits"
                                        value={ this.props.profile.credits }
                                    />
                                </td>
                            </tr>
                            <tr>
                                <td><label htmlFor="super-likes">Super-likes:</label></td>
                                <td>
                                    <NumbersField
                                        id="super-likes"
                                        value={ this.props.profile.superlikes }
                                    />
                                </td>
                            </tr>
                            <tr>
                                <td><label htmlFor="email-subscribed">Email subscribed:</label></td>
                                <td>
                                    <YesNoInputField
                                        id="email-subscribed"
                                        value={ this.props.profile.emailNotificationsSubscribed }
                                        switchValue={(newValue) => this.updateProfileBooleanField("isEmailSubscribed", newValue)}
                                    />
                                </td>
                            </tr>
                        </tbody>
                    </table>
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