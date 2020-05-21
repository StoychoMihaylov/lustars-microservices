import React, { Component } from 'react'
import '../../styles/components/common/NumberAdjusterInput.css'

export default class NumberAdjusterInput extends Component {
    constructor(props) {
        super(props)

        let numberInput = this.props.numberInput

        this.state = {
            number: numberInput !== null && numberInput !== undefined
                ? numberInput
                : 0,

            disableDownBtn: false,
            disableDownBtn: false
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

    render() {
        return (
           <div>
               <button className="number-adjuster-input-button-left" onClick={ this.setInputNumberDown.bind(this) }>&or;</button>
               <input
                    type="text"
                    className="number-adjuster-input"
                    value={ this.state.number }
                />
                <button className="number-adjuster-input-button-right" onClick={ this.setInputNumberUp.bind(this) }>&and;</button>
            </div>
        )
    }
}