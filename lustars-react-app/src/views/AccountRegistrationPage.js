import React, { Component } from 'react'
import FormRegistration from '../components/authentication/FormRegistration'
import '../styles/views/AccountRegistration.css'

class AccountRegistrationPage extends Component {
    render() {
        return (
            <div className="accountRegistrationContainer">
                <div>
                    <h1>Logo Here!</h1>
                </div>
                <FormRegistration />
            </div>
        )
    }
}

export default AccountRegistrationPage