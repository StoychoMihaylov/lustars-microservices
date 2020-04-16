import React, { Component } from "react"
import { connect } from "react-redux"
import { push, goBack } from "connected-react-router"
import {
    infoNotification,
    successfulNotification,
    errorNotification
} from '../../store/actions/eventNotifications'
import ProfileMainSettings from './ProfileMainSettings'
import ProfileAboutMe from './ProfileAboutMe'
import ProfilePartnerInfo from './ProfilePartnerInfo'
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
                    <ProfileAboutMe
                        profile={ isProfileDifined !== undefined ? this.props.profile : undefined }
                    />
                    <br/>
                    <ProfilePartnerInfo
                        profile={ isProfileDifined !== undefined ? this.props.profile : undefined }
                    />
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