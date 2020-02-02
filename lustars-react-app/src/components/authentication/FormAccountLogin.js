import React, { Component } from "react"
import { connect } from "react-redux"
import { push, goBack } from "connected-react-router"
import {
    infoNotification,
    successfulNotification,
    errorNotification
} from '../../store/actions/eventNotifications'
import { loginAccount } from '../../store/actions/accountActions'
import '../../styles/components/FormAccountLogin.css'

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
        var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
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

                    this.props.successfulNotification("Loged in!")
                    this.props.push("/profile/home")
                    window.location.reload(false); // refresh layout
                } else {
                    this.props.errorNotification("Wrong credentials or this user doesn't exist!")
                }
            })
        }

    render() {
        return (
            <div className="loginFormContainer">
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
                <div>
                    <a href="/account/registration">Register</a>
                </div>
                    <button
                        type="button"
                        className="saveBtn"
                        onClick={this.handleSubmit.bind(this)}>&nbsp;Log in&nbsp;</button>
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
        loginAccount: (userData) => dispatch(loginAccount(userData)),
        goBack: () => dispatch(goBack()),
        push: (url) => dispatch(push(url)),

         // Notifications
        infoNotification: (message) => dispatch(infoNotification(message)),
        successfulNotification: (message) => dispatch(successfulNotification(message)),
        errorNotification: (message) => dispatch(errorNotification(message))
    }
}

export default connect(null, mapDispatchToProps)(LoginAccount)