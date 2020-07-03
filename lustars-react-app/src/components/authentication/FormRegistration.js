import React, { Component } from 'react'
import { connect } from 'react-redux'
import { push, goBack } from "connected-react-router"
import { NotificationManager } from 'react-notifications'
import { registerAccount } from '../../store/actions/accountActions'
import '../../styles/components/authentication/FormRegistration.css'
import '../../styles/components/common/InputFields.css'

class FormRegistration extends Component {
    constructor(props) {
        super(props)

        this.state = {
            name: "",
            gender: "",
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
        var re = /^(([^<>()\]\\.,;:\s@"]+(\.[^<>()\]\\.,;:\s@"]+)*)|(".+"))@(([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
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

        if (this.state.gender === "") {
            this.setState({ genderValidation: "Please select gender!"})
            isFormValid = false
        } else {
            this.setState({ genderValidation: ""})
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

                    NotificationManager.success('You are succesfully registered!', '', 3000)
                    this.props.push("/my-profile")
                    window.location.reload(false); // refresh layout
                } else if (response.response != null && response.response.status === 400) { // Bad Reguest (User already exist or wrong credentials)
                    NotificationManager.error(response.response.data, "", 3000)
                } else {
                    this.props.errorNotification("")
                    NotificationManager.error('Connection problem! Please try again!', '', 3000)
                }
            })
    }

    render() {
        console.log(this.state)
        return (
            <div className="form-registration-container">
                <div>
                    <h1>Logo Here!</h1>
                </div>
                <div>
                    <div className="error-field-message">{this.state.nameValidation}</div>
                        <input
                            type="text"
                            placeholder="Name..."
                            name="name"
                            className="registration-field"
                            onChange={event => this.setState({ name: event.target.value })}
                        />
                    </div>
                <div>
                <div className="error-field-message">{this.state.genderValidation}</div>
                    <select
                        className="registration-field"
                        onChange={event => this.setState({ gender: event.target.value })}>
                        {
                            this.state.gender === ""
                                ? <option selected="selected">Select Gender</option>
                                : null
                        }
                        <option value="Man">Man</option>
                        <option value="Female">Female</option>
                        <option value="Bisexsual">Bisexsual</option>
                        <option value="Man-gay">Man-gay</option>
                        <option value="Female-gay">Female-gay</option>
                        <option value="Trans">Trans</option>
                    </select>
                </div>
                <div>
                    <div className="error-field-message">{this.state.emailValidation}</div>
                    <input
                        type="text"
                        placeholder="Email..."
                        name="email"
                        className="registration-field"
                        onChange={event => this.setState({ email: event.target.value })}
                    />
                </div>
                <div>
                    <div className="error-field-message">{this.state.passwordValidation}</div>
                    <input
                        type="password"
                        placeholder="Password..."
                        name="password"
                        className="registration-field"
                        onChange={event => this.setState({ password: event.target.value })}
                    />
                </div>
                <div>
                    <div className="error-field-message">{this.state.confirmPasswordValidation}</div>
                    <input
                        type="password"
                        placeholder="Confirm Password..."
                        name="confirmPassword"
                        className="registration-field"
                        onChange={event => this.setState({ confirmPassword: event.target.value })}
                    />
                </div>
                <div>
                    <a href="/account/login">Log-in</a>
                </div>
                <div>
                    <button
                        type="button"
                        className="register-save-btn"
                        onClick={this.handleSubmit.bind(this)}>Register</button>

                    <button
                        type="button"
                        className="register-back-btn"
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
        push: (url) => dispatch(push(url))
    }
}

export default connect(null, mapDispatchToProps)(FormRegistration)