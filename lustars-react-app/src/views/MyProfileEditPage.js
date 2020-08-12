import React, { Component } from "react"
import { connect } from "react-redux"
import { push, goBack } from "connected-react-router"
import MyProfileMainSettings from '../components/profile/MyProfileMainSettings'
import MyProfileAboutMe from '../components/profile/MyProfileAboutMe'
import MyProfilePartnerInfo from '../components/profile/MyProfilePartnerInfo'
import MyProfileImagesContainer from '../components/profile/MyProfileImagesContainer'
import MyProfileLustarsQuestions from '../components/profile/MyProfileLustarsQuestions'
import { getMyUserProfileDetails } from '../store/actions/myProfileActions'
import '../styles/views/MyProfileEditPage.css'

class MyProfileEditPage extends Component {
    constructor(props) {
        super(props)

        this.state = {
            updatedProfile: {}
        }
    }

    componentWillMount() {
        this.props.getMyUserProfileDetails()
    }

    render() {
        let isProfileDifined = this.props.profile !== undefined ? true : false
        return (
            <div className="profile-edit-page">
                <div className="user-profile-container">
                    <div className="profile-main-settings">
                        <MyProfileMainSettings profile={ isProfileDifined !== undefined ? this.props.profile : undefined } />
                        <br/>
                        <br/>
                        <MyProfileImagesContainer
                            userProfileImages={ isProfileDifined !== undefined ? this.props.profile.images : undefined }
                        />
                        <br/>
                        <MyProfileLustarsQuestions profile={ isProfileDifined !== undefined ? this.props.profile : undefined } />
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
                </div>
            </div>
        )
    }
}

const mapStateToProps = state => {
    return {
        profile: state.myProfile.currentUserProfileDetails,
        isLoading: state.myProfile.isLoading,
        error: state.myProfile.error
    }
}

const mapDispatchToProps = dispatch => {
    return {
        getMyUserProfileDetails: () => dispatch(getMyUserProfileDetails()),

        // Navigation
        goBack: () => dispatch(goBack()),
        push: (url) => dispatch(push(url))
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(MyProfileEditPage)