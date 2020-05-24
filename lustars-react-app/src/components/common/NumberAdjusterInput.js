import React, { Component } from 'react'
import '../../styles/components/common/NumberAdjusterInput.css'

export default class NumberAdjusterInput extends Component {
    constructor(props) {
        super(props)

        let numberInput = this.props.numberInput

        this.state = {
            number: numberInput !== null && numberInput !== undefined
                ? numberInput
                : 0
        }
    }

    setInputNumberDown() {
        try {
            let number = parseInt(this.state.number)
            if (number > 0) {
                this.setState({
                    number: number - 1
                })

                this.props.numberResult(number - 1)
            }
        } catch (error) {
            // TO DO: show error notification that input should be number
        }
    }

    setInputNumberUp() {
        try {
            let number = parseInt(this.state.number)
            if (number < 10000) {
                this.setState({
                    number: number + 1
                })

                this.props.numberResult(number + 1)
            }
        } catch (error) {
            // TO DO: show error notification that input should be number
        }
    }

    setInputNumberDownBy10() {
        try {
            let number = parseInt(this.state.number)
            if (number - 10 >= 0) {
                this.setState({
                    number: number - 10
                })

                this.props.numberResult(number - 10)
            }
        } catch (error) {
            // TO DO: show error notification that input should be number
        }
    }

    setInputNumberUpBy10() {
        try {
            let number = parseInt(this.state.number)
            if (number + 10 <= 10000) {
                this.setState({
                    number: number + 10
                })

                this.props.numberResult(number + 10)
            }
        } catch (error) {
            // TO DO: show error notification that input should be number
        }
    }

    render() {
        return (
           <div>
                <button
                    className="number-adjuster-input-button-left"
                    onClick={ this.setInputNumberDown.bind(this) }
                    >&or;
                </button>
                <button
                    className="double-number-adjuster-input-btn-left"
                    onClick={ this.setInputNumberDownBy10.bind(this) }
                    >&#8659;
                </button>
                <input
                    type="text"
                    className="number-adjuster-input"
                    value={ this.state.number }
                />
                <button
                    className="double-number-adjuster-input-btn-right"
                    onClick={ this.setInputNumberUpBy10.bind(this) }
                    >&#8657;
                </button>
                <button
                    className="number-adjuster-input-button-right"
                    onClick={ this.setInputNumberUp.bind(this) }
                    >&and;
                </button>
            </div>
        )
    }
}