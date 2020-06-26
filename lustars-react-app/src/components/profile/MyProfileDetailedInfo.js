import React, { Component } from "react"
import { connect } from "react-redux"
import { NotificationManager} from 'react-notifications'
import { push, goBack } from "connected-react-router"
import MyProfileMainSettings from './MyProfileMainSettings'
import MyProfileAboutMe from './MyProfileAboutMe'
import MyProfilePartnerInfo from './MyProfilePartnerInfo'
import MyProfileImagesContainer from './MyProfileImagesContainer'
import { getMyUserProfileDetails, editMyUserProfileDetails } from '../../store/actions/myProfileActions'
import '../../styles/components/profile/MyProfileDetailedInfo.css'

class MyProfileDetailedInfo extends Component {
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
                    <MyProfileMainSettings profile={ isProfileDifined !== undefined ? this.props.profile : undefined } />
                    <br/>
                    <br/>
                    <MyProfileImagesContainer
                        userProfileImages={isProfileDifined !== undefined ? this.props.profile.images : undefined}
                    />
                </div>

                <div className="user-profile-info-details">
                    <MyProfileAboutMe
                        profile={ isProfileDifined !== undefined ? this.props.profile : undefined }
                    />
                    <br/>
                    <br/>
                    <MyProfilePartnerInfo
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
        profile: state.myProfile.userProfileDetails,
        isLoading: state.myProfile.isLoading,
        error: state.myProfile.error
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

export default connect(mapStateToProps, mapDispatchToProps)(MyProfileDetailedInfo)