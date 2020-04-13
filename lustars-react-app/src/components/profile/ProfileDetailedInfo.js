import React, { Component } from "react"
import { connect } from "react-redux"
import { push, goBack } from "connected-react-router"
import {
    infoNotification,
    successfulNotification,
    errorNotification
} from '../../store/actions/eventNotifications'
import ProfileMainSettings from './ProfileMainSettings'
import { getMyUserProfileDetails } from '../../store/actions/profileActions'
import { editMyUserProfileDetails } from '../../store/actions/profileActions'
import '../../styles/components/profile/ProfileDetailedInfo.css'

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

    updateUsedProfileDetails() {
        //let props = this.props.profile
        /* let userProfileDetails = {
            id: props.id,
            name: props.name,
            email: props.email,
            gender: props.gender,
            dateOfBirth: props.dateOfBirth,
            biography: props.biography,
            city: props.city,
            lookingFor: props.lookingFor,
            partnerAgeRangeFrom: props.partnerAgeRangeFrom,
            partnerAgeRangeTo: props.partnerAgeRangeTo,
            wantToHaveKids: props.wantToHaveKids,
            educationDegree: props.educationDegree,
            university: props.university,
            work: props.work,
            languages: props.languages,
            emailNotificationsSubscribed: props.emailNotificationsSubscribed,
            isUserProfileActivated: props.isUserProfileActivated
        } */

        this.props.editMyUserProfileDetails(this.props.profile)
            .then(response => {
                if (response.status === 200) {
                    this.props.successfulNotification("Profile updated!")
                } else {
                    this.props.errorNotification("Something went wrong! Please chekc your connection!")
                }
            })
    }

    render() {
        console.log(this.props.profile)
        let isProfileDifined = this.props.profile !== undefined ? true : false
        return (
            <div className="userProfileContainer">

                <div className="profileMainSettings">
                    <ProfileMainSettings profile={ isProfileDifined !== undefined ? this.props.profile : undefined } />
                </div>

                <div className="userProfileInfoDetails">
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

                <div>
                    <button className="saveProfileDetailsBtn" onClick={this.updateUsedProfileDetails.bind(this)} >Save Details</button>
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
        editMyUserProfileDetails: (userProfileDetails) => dispatch(editMyUserProfileDetails(userProfileDetails)),

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