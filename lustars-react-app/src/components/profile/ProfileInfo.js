import React, { Component } from "react"
import { connect } from "react-redux"
import { push, goBack } from "connected-react-router"
import {
    infoNotification,
    successfulNotification,
    errorNotification
} from '../../store/actions/eventNotifications'
import { getMyUserProfileDetails } from '../../store/actions/profileActions'
import { api } from '../../constants/endpoints'

class ProfileInfo extends Component {
    constructor(props) {
        super(props)

        this.state = {
        }
    }

    componentDidMount() {
        this.props.getMyUserProfileDetails()
    }

    render() {
        console.log(this.props.profile)
        return (
            <div>
                <div>
                    { this.props.profile.avatarImage !== null ? <img src={api + this.props.profile.avatarImage } alt="avatar image" /> : "" }
                </div>
                <div>
                    <label>
                        Biography
                        <textarea defaultValue={ this.props.profile.biography !== null ? this.props.profile.biography : "" }></textarea>
                    </label>
                </div>
                <div>
                    <label>
                        City:
                        <input type="text" defaultValue={ this.props.profile.city !== null ? this.props.profile.city : "" } />
                    </label>
                </div>
                <div>
                    <label>
                        Country
                        <input type="text" defaultValue={ this.props.profile.country !== null ? this.props.profile.country : "" } />
                    </label>
                </div>
                <div>
                    <label>
                        Created on
                        <input type="text" defaultValue={ this.props.profile.createdOn !== null ? this.props.profile.createdOn : "" } />
                    </label>
                </div>
                <div>
                    <label>
                        Credits
                        <input type="text" defaultValue={ this.props.profile.credits !== null ? this.props.profile.credits : "" } />
                    </label>
                </div>
                <div>
                    <label>
                        Birth date
                        <input type="text" defaultValue={ this.props.profile.dateOfBirth !== null ? this.props.profile.dateOfBirth : "" } />
                    </label>
                </div>
                <div>
                    <label>
                        Drink Alcohol
                        <input type="text" defaultValue={ this.props.profile.drinkAlcohol !== null ? this.props.profile.drinkAlcohol : "" } />
                    </label>
                </div>
                <div>
                    <label>
                        How often Drink
                        <input type="text" defaultValue={ this.props.profile.howOftenDrinkAlcohol !== null ? this.props.profile.howOftenDrinkAlcohol : "" } />
                    </label>
                </div>
                <div>
                    <label>
                        Education degree
                        <input type="text" defaultValue={ this.props.profile.educationDegree !== null ? this.props.profile.educationDegree : "" } />
                    </label>
                </div>
                <div>
                    <label>
                        Subscribed for email notification
                        <input type="text" defaultValue={ this.props.profile.emailNotificationsSubscribed != null ? this.props.profile.emailNotificationsSubscribed : "" } />
                    </label>
                </div>
                <div>
                    <label>
                        Email
                        <input type="text" defaultValue={ this.props.profile.email !== null ? this.props.profile.email : "" } />
                    </label>
                </div>
                <div>
                    <label>
                        Figure
                        <input type="text" defaultValue={ this.props.profile.figure !== null ? this.props.profile.figure : "" } />
                    </label>
                </div>
                <div>
                    <label>
                        Gender
                        <input type="text" defaultValue={ this.props.profile.gender !== null ? this.props.profile.gender : "" } />
                    </label>
                </div>
                <div>
                    <label>
                        kids
                        <input type="text" defaultValue={ this.props.profile.haveKids !== null ? this.props.profile.haveKids : "" } />
                    </label>
                </div>
                <div>
                    <label>
                        Height
                        <input type="text" defaultValue={ this.props.profile.height !== null ? this.props.profile.height : "" } />
                    </label>
                </div>
                <div>
                    <label>
                        How often smoke
                        <input type="text" defaultValue={ this.props.profile.howOftenSmoke !== null ? this.props.profile.howOftenSmoke : "" } />
                    </label>
                </div>
            </div>
        )
    }
}

const mapStateToProps = state => {
    return {
        profile: state.profile.userProfileDetails,
        isLoading: state.profile.isLoading,
        error: state.profile.error
    }
}

const mapDispatchToProps = dispatch => {
    return {
        getMyUserProfileDetails: () => dispatch(getMyUserProfileDetails()),

        // Navigation
        goBack: () => dispatch(goBack()),
        push: (url) => dispatch(push(url)),

         // Notifications
        infoNotification: (message) => dispatch(infoNotification(message)),
        successfulNotification: (message) => dispatch(successfulNotification(message)),
        errorNotification: (message) => dispatch(errorNotification(message))
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(ProfileInfo)