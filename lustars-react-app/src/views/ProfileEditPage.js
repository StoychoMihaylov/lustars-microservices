import React, { Component } from 'react'
import ProfileDetailedInfo from '../components/profile/ProfileDetailedInfo'
import '../styles/views/ProfileEditPage.css'

class ProfileEditPage extends Component {
    render() {
        return (
            <div className="profile-edit-page-container">
                <div className="profile-edit-page">
                    <ProfileDetailedInfo />
                </div>
            </div>
        )
    }
}

export default ProfileEditPage