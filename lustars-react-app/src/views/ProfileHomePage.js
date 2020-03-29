import React, { Component } from 'react'
import ProfileInfo from '../components/profile/ProfileInfo'

class ProfileHomePage extends Component {
    render() {
        return (
            <div>
                <h1>My profile</h1>
                <div>
                    <div>
                        <ProfileInfo />
                    </div>
                </div>
            </div>
        )
    }
}

export default ProfileHomePage