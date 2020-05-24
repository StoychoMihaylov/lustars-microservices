import React, { Component } from 'react'
import FormRegistration from '../../components/authentication/FormRegistration'
import '../../styles/views/AccountRegistration.css'

class AccountRegistrationPage extends Component {
    render() {
        return (
            <div className="account-registration-page-container">
                <FormRegistration />
            </div>
        )
    }
}

export default AccountRegistrationPage