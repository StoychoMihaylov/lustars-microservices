import React, { Component } from "react"
import './YesNoInputField.css'

class YesNoInputField extends Component {

    switchValue() {
        var userActive = this.props.value

        if (userActive) {
            userActive = false
        } else {
            userActive = true
        }

        this.props.switchValue(userActive)
    }

    render() {
        let label = this.props.label !== undefined ? this.props.label : ""
        let value = this.props.value !== undefined && this.props.value !== null
            ? this.props.value === true ? "Yes" : "No"
            : "No"

        return (
            <label>
                { label }&nbsp;
                <input id={ this.props.id } type="button" className="profile-switch-field" value={ value } onClick={ this.switchValue.bind(this) } />
            </label>
        )
    }
}

export default YesNoInputField