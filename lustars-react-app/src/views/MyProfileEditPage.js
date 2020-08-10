import React, { Component } from 'react'
import MyProfileDetailedInfo from '../components/profile/MyProfileDetailedInfo'
import '../styles/views/MyProfileEditPage.css'

class MyProfileEditPage extends Component {
    render() {
        return (
            <div className="profile-edit-page">
                <h1>My Profile</h1>
                <MyProfileDetailedInfo />
            </div>
        )
    }
}

export default MyProfileEditPage