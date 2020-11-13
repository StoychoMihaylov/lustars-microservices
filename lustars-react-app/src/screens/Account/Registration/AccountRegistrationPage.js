import React, { Component } from 'react'
import FormRegistration from '../../../components/authentication/Registration/FormRegistration'
import './AccountRegistrationPage.css'

class AccountRegistrationPage extends Component {
    render() {
        return (
            <div className="account-registration-page-container">
                <div className="home-back-bttn">
                    <a href="/">Go Back to Home</a>
                </div>
                <FormRegistration />
            </div>
        )
    }
}

export default AccountRegistrationPage