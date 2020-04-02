import React, { Component } from "react"
import { connect } from "react-redux"
import { push, goBack } from "connected-react-router"
import {
    infoNotification,
    successfulNotification,
    errorNotification
} from '../../store/actions/eventNotifications'
import { getMyUserProfileDetails } from '../../store/actions/profileActions'
import { editMyUserProfileDetails } from '../../store/actions/profileActions'
import ProfileMainSettings from '../../components/profile/ProfileMainSettings'
import { changeIsUserActive, changeUserEmailSubsribed } from '../../store/actions/profileActions'

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
        var oldState = this.props.profile
        oldState.isUserProfileActivated = newValue
        var newState = oldState

        this.props.changeIsUserActive(newState)
    }

    updateIsEmailSubscribed = (newValue) => {
        var oldState = this.props.profile
        oldState.emailNotificationsSubscribed = newValue
        var newState = oldState

        this.props.changeUserEmailSubsribed(newState)
    }

    /* updateIsProfileActive = (newValue) => {
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
 */
    render() {
        console.log(this.props.profile)
        let isProfileDifined = this.props.profile !== undefined ? true : false
        return (
            <div>
               {/*  <div>
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
                </div> */}
                <ProfileMainSettings
                    profile={ isProfileDifined !== undefined ? this.props.profile : undefined }
                    updateIsProfileActive={ this.updateIsProfileActive.bind(this)}
                    updateIsEmailSubscribed={ this.updateIsEmailSubscribed.bind(this) }
                />
                <br/>
                <div>
                    <label>
                        Biography
                        <textarea defaultValue={ isProfileDifined ? this.props.profile.biography : "" }></textarea>
                    </label>
                </div>
                <div>
                    <label>
                        City:
                        <input type="text" defaultValue={ isProfileDifined ? this.props.profile.city : "" } />
                    </label>
                </div>
                <div>
                    <label>
                        Country
                        <input type="text" defaultValue={ isProfileDifined ? this.props.profile.country : "" } />
                    </label>
                </div>
                <div>
                    <label>
                        Created on
                        <input type="text" defaultValue={ isProfileDifined ? this.props.profile.createdOn : "" } />
                    </label>
                </div>
                <div>
                    <label>
                        Birth date
                        <input type="text" defaultValue={ isProfileDifined ? this.props.profile.dateOfBirth : "" } />
                    </label>
                </div>
                <div>
                    <label>
                        Drink Alcohol
                        <input type="text" defaultValue={ isProfileDifined ? this.props.profile.drinkAlcohol : "" } />
                    </label>
                </div>
                <div>
                    <label>
                        How often Drink
                        <input type="text" defaultValue={ isProfileDifined ? this.props.profile.howOftenDrinkAlcohol : "" } />
                    </label>
                </div>
                <div>
                    <label>
                        Education degree
                        <input type="text" defaultValue={ isProfileDifined ? this.props.profile.educationDegree : "" } />
                    </label>
                </div>
                <div>
                    <label>
                        Figure
                        <input type="text" defaultValue={ isProfileDifined ? this.props.profile.figure : "" } />
                    </label>
                </div>
                <div>
                    <label>
                        Gender
                        <input type="text" defaultValue={ isProfileDifined ? this.props.profile.gender : "" } />
                    </label>
                </div>
                <div>
                    <label>
                        kids
                        <input type="text" defaultValue={ isProfileDifined ? this.props.profile.haveKids : "" } />
                    </label>
                </div>
                <div>
                    <label>
                        Height
                        <input type="text" defaultValue={ isProfileDifined ? this.props.profile.height : "" } />
                    </label>
                </div>
                <div>
                    <label>
                        How often smoke
                        <input type="text" defaultValue={ isProfileDifined ? this.props.profile.howOftenSmoke : "" } />
                    </label>
                </div>
                <div>
                    <label>
                        Income
                        <input type="text" defaultValue={ isProfileDifined ? this.props.profile.income : "" } />
                    </label>
                </div>
                <div>
                    <label>
                        Profie Activated
                        <input type="text" defaultValue={ isProfileDifined ? this.props.profile.isUserProfileActivated : "" } />
                    </label>
                </div>
                <div>
                    <label>
                        Languages
                        <input type="text" defaultValue={ isProfileDifined ? this.props.profile.languages : "" } />
                    </label>
                </div>
                <div>
                    <label>
                        Last Name
                        <input type="text" defaultValue={ isProfileDifined ? this.props.profile.lastName : "" } />
                    </label>
                </div>
                <div>
                    <label>
                        Looking for
                        <input type="text" defaultValue={ isProfileDifined ? this.props.profile.lookingFor : "" } />
                    </label>
                </div>
                <div>
                    <label>
                        Merial status
                        <input type="text" defaultValue={ isProfileDifined ? this.props.profile.meritalStatus : "" } />
                    </label>
                </div>
                <div>
                    <label>
                        Name
                        <input type="text" defaultValue={ isProfileDifined ? this.props.profile.name : "" } />
                    </label>
                </div>
                <div>
                    <label>
                        Partner age range from
                        <input type="text" defaultValue={ isProfileDifined ? this.props.profile.partnerAgeRangeFrom : "" } />
                    </label>
                </div>
                <div>
                    <label>
                        Partner age range to
                        <input type="text" defaultValue={ isProfileDifined ? this.props.profile.partnerAgeRangeTo : "" } />
                    </label>
                </div>
                <div>
                    <label>
                        Partner Drink Alcohol
                        <input type="text" defaultValue={ isProfileDifined ? this.props.profile.partnerDrinkAlcohol : "" } />
                    </label>
                </div>
                <div>
                    <label>
                        Partner Figurel
                        <input type="text" defaultValue={ isProfileDifined ? this.props.profile.partnerFigure : "" } />
                    </label>
                </div>
                <div>
                    <label>
                        partnerHaveKids
                        <input type="text" defaultValue={ isProfileDifined ? this.props.profile.partnerHaveKids : "" } />
                    </label>
                </div>
                <div>
                    <label>
                        partnerIncomeFrom
                        <input type="text" defaultValue={ isProfileDifined ? this.props.profile.partnerIncomeFrom : "" } />
                    </label>
                </div>
                <div>
                    <label>
                        partnerIncomeTo
                        <input type="text" defaultValue={ isProfileDifined ? this.props.profile.partnerIncomeTo : "" } />
                    </label>
                </div>
                <div>
                    <label>
                        partnerSmoke
                        <input type="text" defaultValue={ isProfileDifined ? this.props.profile.partnerSmoke : "" } />
                    </label>
                </div>
                <div>
                    <label>
                        smoker
                        <input type="text" defaultValue={ isProfileDifined ? this.props.profile.smoker : "" } />
                    </label>
                </div>
                <div>
                    <label>
                        superlikes
                        <input type="text" defaultValue={ isProfileDifined ? this.props.profile.superlikes : "" } />
                    </label>
                </div>
                <div>
                    <label>
                        title
                        <input type="text" defaultValue={ isProfileDifined ? this.props.profile.title : "" } />
                    </label>
                </div>
                <div>
                    <label>
                        university
                        <input type="text" defaultValue={ isProfileDifined ? this.props.profile.university : "" } />
                    </label>
                </div>
                <div>
                    <label>
                    wantToHaveKids
                        <input type="text" defaultValue={ isProfileDifined ? this.props.profile.wantToHaveKids : "" } />
                    </label>
                </div>
                <div>
                    <label>
                        weight
                        <input type="text" defaultValue={ isProfileDifined ? this.props.profile.weight : "" } />
                    </label>
                </div>
                <div>
                    <label>
                        work
                        <input type="text" defaultValue={ isProfileDifined ? this.props.profile.work : "" } />
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
        changeIsUserActive: (newValue) => dispatch(changeIsUserActive(newValue)),
        changeUserEmailSubsribed: (newValue) => dispatch(changeUserEmailSubsribed(newValue)),
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