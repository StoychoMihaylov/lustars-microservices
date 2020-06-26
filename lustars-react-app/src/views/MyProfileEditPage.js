import React, { Component } from 'react'
import MyProfileDetailedInfo from '../components/profile/MyProfileDetailedInfo'
import '../styles/views/MyProfileEditPage.css'

class MyProfileEditPage extends Component {
    render() {
        return (
            <div className="profile-edit-page-container">
                <div className="profile-edit-page">
                    <MyProfileDetailedInfo />
                </div>
            </div>
        )
    }
}

export default MyProfileEditPage