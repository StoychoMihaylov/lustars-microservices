import React, { Component } from "react"
import { connect } from "react-redux"
import { NotificationManager } from 'react-notifications'
import {
    updateUserProfileBoleanField,
    updateUserProfileTextField,
    editMyUserProfileDetails,
    updateUserProfileGeaolocation,
    getUserLocationFromGOOGLE
} from '../../../store/actions/myProfileActions'
import YesNoInputField from '../../common/YesNoInput/YesNoInputField'
import NumbersField from '../../common/NumbersField/NumbersField'
import Avatar from '../Avatar/Avatar'
import { city_states } from '../../../constants/countriesAndCities'
import './MyProfileMainSettings.css'

class MyProfileMainSettings extends Component {
    constructor(props) {
        super(props)

        this.state = {
            location: "",
            isFromCountrySelected: false,

            // Show/hide fields
            editCountry: false,
            editCity: false
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
        let latitude = position.coords.latitude
        let longitude = position.coords.longitude

        getUserLocationFromGOOGLE(latitude, longitude)
        .then(response => {
            let location = response.data.results[0].formatted_address
            let arrLocation = location.split(',')
            let postCodeAndCity = arrLocation[1].trim().split(' ')

            let geoLocations = {
                street: arrLocation[0],
                postCode: postCodeAndCity[0],
                city: postCodeAndCity[1],
                country: arrLocation[2],
                latitude: latitude,
                longitude: longitude,
                createdOn: new Date(),
                IsActive: true
            }

            let updateResponse = this.props.updateUserProfileGeaolocation(geoLocations) // Update User Profile Geo location as background proccess
            console.log(updateResponse)

            this.setState({
                location: arrLocation[1] + ", " + arrLocation[2]
            })

            NotificationManager.success('Location updated!', '', 3000)
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
                newState.fromCity = value
                this.props.updateUserProfileTextField(newState)
                return
            case 'country':
                newState.fromCountry = value
                this.props.updateUserProfileTextField(newState)
                this.setState({
                    isFromCountrySelected: true
                })
                return
            default:
                return
        }
    }

    updateProfileBooleanField = (field, newValue) => {
        let oldState = this.props.profile
        let newState = Object.assign({}, oldState)

        switch (field) {
            case 'isEmailSubscribed':
                newState.emailNotificationsSubscribed = newValue
                this.props.updateUserProfileBoleanField(newState)
                return
            default:
                return
        }
    }

    closeFieldForModification(field) {
        switch (field) {
            case 'country':
                this.setState({ editCountry: false })
                return
            case 'city':
                this.setState({ editCity: false })
                return

            default:
                return
        }
    }

    updateUserProfile(field) {
        this.props.editMyUserProfileDetails(this.props.profile)
            .then(response => {
                if (response.status === 202) {
                    NotificationManager.info('Updating you profile...', 'Processing data...', 3000)
                } else {
                    NotificationManager.error('Something went wrong! Please check your connection!', 'Error!', 5000, () => {
                        alert('There is some problem! Please try again or check your network!');
                      });
                }

                this.closeFieldForModification(field)
            })
    }

    render() {
        let getLocationBtn = this.state.location !== ""
            ?  <span>{ this.state.location }<span onClick={ this.setGeolocation.bind(this) } className="location-refresh-btn">&#8634;</span></span>
            :  <button className="location-btn" onClick={ this.setGeolocation.bind(this) }>Get Location</button>

        let selectCountries = Object.keys(city_states)
        let countrySelectOptions = selectCountries.map((country, index) => {
            return (
                <option key={ index } value={ country } >{ country }</option>
            )
        })

        let selectCities = this.props.profile.fromCountry !== undefined && this.props.profile.fromCountry
            ?   city_states[this.props.profile.fromCountry]
            :   city_states["Select Country"]

        let citySelectOptions = selectCities !== null
            ?   selectCities.map((city, index) => {
                    return (
                        <option key={ index } value={ city }>{ city }</option>
                    )
                })
            : null

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
                                        this.props.profile.geoLocation !== null && this.props.profile.geoLocation !== undefined
                                            ?   <div>
                                                    <span>{ this.props.profile.geoLocation.city },
                                                    { this.props.profile.geoLocation.country }
                                                </span>
                                                    <span onClick={ this.setGeolocation.bind(this) } className="location-refresh-btn"> &#8634;</span>
                                                </div>
                                            :   getLocationBtn
                                    }
                                </td>
                            </tr>
                            <tr>
                                <td><label htmlFor="from-country">From Country:</label></td>
                                <td>
                                    <span
                                        style={{ display:!this.state.editCountry ? "block" : "none" }}
                                        onClick={ () => this.setState({ editCountry: true })}>{ this.props.profile.fromCountry }
                                    </span>
                                    <select
                                        id="from-country"
                                        style={{ display:this.state.editCountry ? "block" : "none" }}
                                        className="text-input-profile-about"
                                        value={ this.props.profile.fromCountry }
                                        onChange={(e) => this.updateProfileTextField("country", e.target.value)}
                                        onBlur={ () => this.updateUserProfile("country") }>
                                        {
                                            this.props.profile.fromCountry === null || this.props.profile.fromCountry === undefined
                                                ? <option selected="selected">Select Country</option>
                                                : null
                                        }
                                        { countrySelectOptions }
                                    </select>
                                </td>
                            </tr>
                            <tr>
                            <td><label htmlFor="from-city">From City:</label></td>
                                <td>
                                    <span
                                        style={{ display:!this.state.editCity ? "block" : "none" }}
                                        onClick={ () => this.setState({ editCity: true })}>{ this.props.profile.fromCity }
                                    </span>
                                    <select
                                        id="from-city"
                                        style={{ display:this.state.editCity ? "block" : "none" }}
                                        className="text-input-profile-about"
                                        value={ this.props.profile.fromCity }
                                        onChange={(e) => this.updateProfileTextField("city", e.target.value)}
                                        onBlur={ () => this.updateUserProfile("city") }>
                                        { citySelectOptions }
                                    </select>
                                </td>
                            </tr>
                            <tr>
                                <td><hr/></td>
                                <td><hr/></td>
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
                                <td><label htmlFor="super-likes">Lustar likes:</label></td>
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
        editMyUserProfileDetails: (userProfileDetails) => dispatch(editMyUserProfileDetails(userProfileDetails)),
        updateUserProfileGeaolocation: (geolocation) => dispatch(updateUserProfileGeaolocation(geolocation)),
        updateUserProfileBoleanField: (newValue) => dispatch(updateUserProfileBoleanField(newValue)),
        updateUserProfileTextField: (newValue) => dispatch(updateUserProfileTextField(newValue))
    }
}

export default connect(null, mapDispatchToProps)(MyProfileMainSettings)