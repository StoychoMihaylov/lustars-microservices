import React, { Component } from "react"
import './NumbersField.css'

class NumbersField extends Component {
    render() {
        let label = this.props.label !== undefined ? this.props.label : ""
        let value = this.props.value !== undefined && this.props.value !== null
            ? this.props.value
            : 0

        return (
            <label>
                { label }&nbsp;
                <input id={ this.props.id } type="button" className="profile-number-field" value={ value } />
            </label>
        )
    }
}

export default NumbersField