import React, { Component } from "react"
import { connect } from "react-redux"
import { push, goBack } from "connected-react-router"
import { NotificationManager } from 'react-notifications'
import { loginAccount } from '../../../store/actions/accountActions'
import './FormAccountLogin.css'

class LoginAccount extends Component {
    constructor(props) {
        super(props)

        this.state = {
            email: "",
            password: "",

            // fields validation
            emailValidation: "",
            passwordValidation: "",
        }
    }

    emailValidation(email) {
        var re = /^(([^<>()\]\\.,;:\s@"]+(\.[^<>()\]\\.,;:\s@"]+)*)|(".+"))@(([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return re.test(String(email).toLowerCase());
    }

    formValidation() {
        let isFormValid = true;

        let isMailValid = this.emailValidation(this.state.email)
        if (! isMailValid) {
            this.setState({ emailValidation: "Email is not in a valid format!" })
            isFormValid = false
        } else {
            this.setState({ emailValidation: "" })
        }

        if (this.state.password.length < 3) {
            this.setState({ passwordValidation: "Password must be more than 3 charaters long!"})
            isFormValid = false
        } else {
            this.setState({ passwordValidation: ""})
        }

        return isFormValid
    }

    handleSubmit() {
        let isFormValid = this.formValidation()
        if (! isFormValid) {
            return;
        }

        let userData = {
            email: this.state.email,
            password: this.state.password,
        }

        this.props.loginAccount(userData)
            .then(response => {
                console.log(response)
                if (response.status === 200) {
                    let credentials = response.data
                    localStorage.setItem('lustars_token', credentials.token)
                    localStorage.setItem('lustars_user_id', credentials.userId)
                    localStorage.setItem('lustars_user_name', credentials.name)

                    NotificationManager.success('Loged in!', '', 3000)
                    this.props.push("/my-profile") // redirection!
                    window.location.reload(false); // refresh layout
                } else {
                    NotificationManager.error("Wrong credentials or this user doesn't exist!", '', 3000)
                }
            })
        }

    render() {
        return (
            <div className="login-form-container">
                <div>
                    <h1>Logo Here!</h1>
                </div>
                <div>
                    <div className="errorMessage">{this.state.emailValidation}</div>
                    <input
                        type="text"
                        placeholder="Email..."
                        name="email"
                        className="login-field"
                        onChange={event => this.setState({ email: event.target.value })}
                    />
                </div>
                <div>
                    <div className="errorMessage">{this.state.passwordValidation}</div>
                    <input
                        type="password"
                        placeholder="Password..."
                        name="password"
                        className="login-field"
                        onChange={event => this.setState({ password: event.target.value })}
                    />
                </div>
                <div>
                <div>
                    <a href="/account/registration">Register</a>
                </div>
                    <button
                        type="button"
                        className="login-bttn"
                        onClick={this.handleSubmit.bind(this)}>&nbsp;Log in&nbsp;</button>
                    <button
                        type="button"
                        className="login-back-bttn"
                        onClick={ this.props.goBack }>Go Back</button>
                </div>
            </div>
        )
    }
}

const mapDispatchToProps = dispatch => {
    return {
        loginAccount: (userData) => dispatch(loginAccount(userData)),

        // Navigation
        goBack: () => dispatch(goBack()),
        push: (url) => dispatch(push(url))
    }
}

export default connect(null, mapDispatchToProps)(LoginAccount)