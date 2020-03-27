import React, { Component } from 'react'
import FormAccountLogin from '../../components/authentication/FormAccountLogin'
import '../../styles/views/AccountLoginPage.css'

class AccountLoginPage extends Component {
    render() {
        return (
            <div className='accountloginPageConteiner'>
                <div>
                    <div>
                        <h1>Logo Here!</h1>
                    </div>
                    <FormAccountLogin />
                </div>
            </div>
        )
    }
}

export default AccountLoginPage