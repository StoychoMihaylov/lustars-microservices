import React, { Component } from "react"
import '../../styles/components/profile/NumbersField.css'

class NumbersField extends Component {
    render() {
        let label = this.props.label !== undefined ? this.props.label : ""
        let value = this.props.value !== undefined && this.props.value !== null
            ? this.props.value
            : 0

        return (
            <label>
                { label }&nbsp;
                <input type="button" className="profileNumberField" value={ value } />
            </label>
        )
    }
}

export default NumbersField