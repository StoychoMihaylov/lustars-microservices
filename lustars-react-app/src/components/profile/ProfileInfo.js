import React, { Component } from "react"
import { connect } from "react-redux"
import { push, goBack } from "connected-react-router"
import {
    infoNotification,
    successfulNotification,
    errorNotification
} from '../../store/actions/eventNotifications'
import { getMyUserProfileDetails } from '../../store/actions/profileActions'
import YesNoInputField from '../../components/profile/YesNoInputField'
import NumbersField from '../../components/profile/NumbersField'
import Avatar from '../../components/profile/Avatar'
import { editMyUserProfileDetails } from '../../store/actions/profileActions'

class ProfileInfo extends Component {
    constructor(props) {
        super(props)

        this.state = {
            updatedProfile: {}
        }
    }

    componentDidMount() {
        this.props.getMyUserProfileDetails()
    }

    updateIsProfileActive = (newValue) => {
        var newState = this.props.profile
        newState.isUserProfileActivated = newValue

        this.setState({
            updatedProfile: newState
        })
    }

    updateIsEmailSubscribed = (newValue) => {
        var newState = this.props.profile
        newState.emailNotificationsSubscribed = newValue

        this.setState({
            updatedProfile: newState
        })
    }

    render() {
        console.log(this.state)
        console.log(this.props)
        return (
            <div>
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
                <br/>
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
                <div>
                    <label>
                        Income
                        <input type="text" defaultValue={ this.props.profile.income !== null ? this.props.profile.income : "" } />
                    </label>
                </div>
                <div>
                    <label>
                        Profie Activated
                        <input type="text" defaultValue={ this.props.profile.isUserProfileActivated !== null ? this.props.profile.isUserProfileActivated : "" } />
                    </label>
                </div>
                <div>
                    <label>
                        Languages
                        <input type="text" defaultValue={ this.props.profile.languages !== null ? this.props.profile.languages : "" } />
                    </label>
                </div>
                <div>
                    <label>
                        Last Name
                        <input type="text" defaultValue={ this.props.profile.lastName !== null ? this.props.profile.lastName : "" } />
                    </label>
                </div>
                <div>
                    <label>
                        Looking for
                        <input type="text" defaultValue={ this.props.profile.lookingFor !== null ? this.props.profile.lookingFor : "" } />
                    </label>
                </div>
                <div>
                    <label>
                        Merial status
                        <input type="text" defaultValue={ this.props.profile.meritalStatus !== null ? this.props.profile.meritalStatus : "" } />
                    </label>
                </div>
                <div>
                    <label>
                        Name
                        <input type="text" defaultValue={ this.props.profile.name !== null ? this.props.profile.name : "" } />
                    </label>
                </div>
                <div>
                    <label>
                        Partner age range from
                        <input type="text" defaultValue={ this.props.profile.partnerAgeRangeFrom !== null ? this.props.profile.partnerAgeRangeFrom : "" } />
                    </label>
                </div>
                <div>
                    <label>
                        Partner age range to
                        <input type="text" defaultValue={ this.props.profile.partnerAgeRangeTo !== null ? this.props.profile.partnerAgeRangeTo : "" } />
                    </label>
                </div>
                <div>
                    <label>
                        Partner Drink Alcohol
                        <input type="text" defaultValue={ this.props.profile.partnerDrinkAlcohol !== null ? this.props.profile.partnerDrinkAlcohol : "" } />
                    </label>
                </div>
                <div>
                    <label>
                        Partner Figurel
                        <input type="text" defaultValue={ this.props.profile.partnerFigure !== null ? this.props.profile.partnerFigure : "" } />
                    </label>
                </div>
                <div>
                    <label>
                        partnerHaveKids
                        <input type="text" defaultValue={ this.props.profile.partnerHaveKids !== null ? this.props.profile.partnerHaveKids : "" } />
                    </label>
                </div>
                <div>
                    <label>
                        partnerIncomeFrom
                        <input type="text" defaultValue={ this.props.profile.partnerIncomeFrom !== null ? this.props.profile.partnerIncomeFrom : "" } />
                    </label>
                </div>
                <div>
                    <label>
                        partnerIncomeTo
                        <input type="text" defaultValue={ this.props.profile.partnerIncomeTo !== null ? this.props.profile.partnerIncomeTo : "" } />
                    </label>
                </div>
                <div>
                    <label>
                        partnerSmoke
                        <input type="text" defaultValue={ this.props.profile.partnerSmoke !== null ? this.props.profile.partnerSmoke : "" } />
                    </label>
                </div>
                <div>
                    <label>
                        smoker
                        <input type="text" defaultValue={ this.props.profile.smoker !== null ? this.props.profile.smoker : "" } />
                    </label>
                </div>
                <div>
                    <label>
                        superlikes
                        <input type="text" defaultValue={ this.props.profile.superlikes !== null ? this.props.profile.superlikes : "" } />
                    </label>
                </div>
                <div>
                    <label>
                        title
                        <input type="text" defaultValue={ this.props.profile.title !== null ? this.props.profile.title : "" } />
                    </label>
                </div>
                <div>
                    <label>
                        university
                        <input type="text" defaultValue={ this.props.profile.university !== null ? this.props.profile.university : "" } />
                    </label>
                </div>
                <div>
                    <label>
                    wantToHaveKids
                        <input type="text" defaultValue={ this.props.profile.wantToHaveKids !== null ? this.props.profile.wantToHaveKids : "" } />
                    </label>
                </div>
                <div>
                    <label>
                        weight
                        <input type="text" defaultValue={ this.props.profile.weight !== null ? this.props.profile.weight : "" } />
                    </label>
                </div>
                <div>
                    <label>
                        work
                        <input type="text" defaultValue={ this.props.profile.work !== null ? this.props.profile.work : "" } />
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
        editMyUserProfileDetails: () => dispatch(editMyUserProfileDetails()),

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