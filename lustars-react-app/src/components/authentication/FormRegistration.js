import React, { Component } from 'react'
import { connect } from 'react-redux'
import { push, goBack } from "connected-react-router"
import { registerAccount } from '../../store/actions/accountActions'
import {
    infoNotification,
    successfulNotification,
    errorNotification
} from '../../store/actions/eventNotifications'
import '../../styles/components/FormRegistration.css'

class FormRegistration extends Component {
    constructor(props) {
        super(props)

        this.state = {
            name: "",
            email: "",
            password: "",
            confirmPassword: "",

            // fields validation
            nameValidation: "",
            emailValidation: "",
            passwordValidation: "",
            confirmPasswordValidation: "",
        }
    }

    emailValidation(email) {
        var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return re.test(String(email).toLowerCase());
    }

    formValidation() {
        let isFormValid = true;

        if (this.state.name.length < 3) {
            this.setState({ nameValidation: "Name must be more than 3 charaters long!"})
            isFormValid = false
        } else {
            this.setState({ nameValidation: ""})
        }

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

        if (this.state.password !== this.state.confirmPassword) {
            this.setState({ confirmPasswordValidation: "Confirm password must be the same as your password!"})
            isFormValid = false
        } else {
            this.setState({ confirmPasswordValidation: ""})
        }

        return isFormValid
    }

    handleSubmit() {
        let isFormValid = this.formValidation()
        if (! isFormValid) {
            return;
        }

        let userData = {
            name: this.state.name,
            email: this.state.email,
            password: this.state.password,
            confirmPassword: this.state.confirmPassword,
        }

        this.props.registerAccount(userData)
            .then(response => {
                if (response.status === 201) {
                    let credentials = response.data
                    localStorage.setItem('lustars_token', credentials.token)
                    localStorage.setItem('lustars_user_id', credentials.userId)
                    localStorage.setItem('lustars_user_name', credentials.name)

                    this.props.successfulNotification("You are succesfully registered!")
                    this.props.push("/profile/home")
                    window.location.reload(false); // refresh layout
                } else if (response.response != null && response.response.status === 400) { // Bad Reguest (User already exist or wrong credentials)
                    this.props.errorNotification(response.response.data)
                } else {
                    this.props.errorNotification("Connection problem! Please try again")
                }
            })
    }

    render() {
        return (
            <div>
                <div>
                <div className="errorMessage">{this.state.nameValidation}</div>
                    <input
                        type="text"
                        placeholder="Name..."
                        name="name"
                        className="registrationField"
                        onChange={event => this.setState({ name: event.target.value })}
                    />
                </div>
                <div>
                    <div className="errorMessage">{this.state.emailValidation}</div>
                    <input
                        type="text"
                        placeholder="Email..."
                        name="email"
                        className="registrationField"
                        onChange={event => this.setState({ email: event.target.value })}
                    />
                </div>
                <div>
                    <div className="errorMessage">{this.state.passwordValidation}</div>
                    <input
                        type="password"
                        placeholder="Password..."
                        name="password"
                        className="registrationField"
                        onChange={event => this.setState({ password: event.target.value })}
                    />
                </div>
                <div>
                    <div className="errorMessage">{this.state.confirmPasswordValidation}</div>
                    <input
                        type="password"
                        placeholder="Confirm Password..."
                        name="confirmPassword"
                        className="registrationField"
                        onChange={event => this.setState({ confirmPassword: event.target.value })}
                    />
                </div>
                <div>
                    <a href="/account/login">Log-in</a>
                </div>
                <div>
                    <button
                        type="button"
                        className="saveBtn"
                        onClick={this.handleSubmit.bind(this)}>Register</button>

                    <button
                        type="button"
                        className="backBtn"
                        onClick={ this.props.goBack }>Go Back</button>
                </div>
            </div>
        )
    }
}

const mapDispatchToProps = dispatch => {
    return {
        registerAccount: (userData) => dispatch(registerAccount(userData)),

        // Navigation
        goBack: () => dispatch(goBack()),
        push: (url) => dispatch(push(url)),

         // Notifications
         infoNotification: (message) => dispatch(infoNotification(message)),
         successfulNotification: (message) => dispatch(successfulNotification(message)),
         errorNotification: (message) => dispatch(errorNotification(message))
    }
}

export default connect(null, mapDispatchToProps)(FormRegistration)