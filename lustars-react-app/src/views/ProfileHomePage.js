import React, { Component } from 'react'
import ProfileDetailedInfo from '../components/profile/ProfileDetailedInfo'

class ProfileHomePage extends Component {
    render() {
        return (
            <div>
                <h1>My profile</h1>
                <div>
                    <div>
                        <ProfileDetailedInfo />
                    </div>
                </div>
            </div>
        )
    }
}

export default ProfileHomePage