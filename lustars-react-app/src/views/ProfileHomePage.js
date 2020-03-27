import React, { Component } from 'react'
import ProfileInfo from '../components/profile/ProfileInfo'

class ProfileHomePage extends Component {
    render() {
        return (
            <div>
                <h1>Here is a private page!</h1>
                <div>
                    <h2>Home Profile Page!</h2>
                    <div>
                        <ProfileInfo />
                    </div>
                </div>
            </div>
        )
    }
}

export default ProfileHomePage