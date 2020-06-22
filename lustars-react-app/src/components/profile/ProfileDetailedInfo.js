import React, { Component } from "react"
import { connect } from "react-redux"
import { NotificationManager} from 'react-notifications'
import { push, goBack } from "connected-react-router"
import ProfileMainSettings from './ProfileMainSettings'
import ProfileAboutMe from './ProfileAboutMe'
import ProfilePartnerInfo from './ProfilePartnerInfo'
import ProfileImagesContainer from './ProfileImagesContainer'
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
                    NotificationManager.success('Your profile has been updated successfully', 'Updated!', 3000)
                } else {
                    this.props.errorNotification("Something went wrong! Please check your connection!")
                    NotificationManager.error('Something went wrong! Please check your connection!', 'Error!', 5000, () => {
                        alert('There is some problem! Please try again or check your network!');
                      });
                }
            })
    }

    render() {
        let isProfileDifined = this.props.profile !== undefined ? true : false

        console.log(this.props.profile)
        return (
            <div className="user-profile-container">
                <div className="profile-main-settings">
                    <ProfileMainSettings profile={ isProfileDifined !== undefined ? this.props.profile : undefined } />
                    <br/>
                    <br/>
                    <ProfileImagesContainer
                        userProfileImages={isProfileDifined !== undefined ? this.props.profile.images : undefined}
                    />
                </div>

                <div className="user-profile-info-details">
                    <ProfileAboutMe
                        profile={ isProfileDifined !== undefined ? this.props.profile : undefined }
                    />
                    <br/>
                    <br/>
                    <ProfilePartnerInfo
                        profile={ isProfileDifined !== undefined ? this.props.profile : undefined }
                    />
                </div>

                <div>
                    <button className="save-profile-details-btn" onClick={this.updateUsedProfileDetails.bind(this)} >Save Details</button>
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
        push: (url) => dispatch(push(url))
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(ProfileInfo)