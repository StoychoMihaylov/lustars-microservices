import React, { Component } from 'react'
import FormAccountLogin from '../../../components/authentication/Login/FormAccountLogin'
import './AccountLoginPage.css'

class AccountLoginPage extends Component {
    render() {
        return (
            <div className="account-login-page-conteiner">
                <div>
                    <div className="home-back-bttn">
                        <a href="/">Go Back to Home</a>
                    </div>
                    <FormAccountLogin />
                </div>
            </div>
        )
    }
}

export default AccountLoginPage