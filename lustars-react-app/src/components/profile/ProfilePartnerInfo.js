import React, { Component } from "react"
import { connect } from "react-redux"
import { updateUserProfileBoleanField, updateUserProfileTextField } from '../../store/actions/profileActions'
import YesNoInputField from './YesNoInputField'

class ProfilePartnerInfo extends Component {
    constructor(props) {
        super(props)
    }

    updateProfileTextField(field, value) {
        let oldState = this.props.profile
        let newState = Object.assign({}, oldState)

        switch (field) {
            case 'partnerAgeRangeFrom':
                newState.partnerAgeRangeFrom = parseInt(value)
                this.props.updateUserProfileTextField(newState)
                return
            case 'partnerAgeRangeTo':
                newState.partnerAgeRangeTo = parseInt(value)
                this.props.updateUserProfileTextField(newState)
                return
            case 'partnerFigure':
                newState.partnerFigure = value
                this.props.updateUserProfileTextField(newState)
                return
            case 'partnerIncomeFrom':
                newState.partnerIncomeFrom = parseInt(value)
                this.props.updateUserProfileTextField(newState)
                return
            case 'partnerIncomeTo':
                newState.partnerIncomeTo = parseInt(value)
                this.props.updateUserProfileTextField(newState)
                return

            default:
                return
        }
    }

    updateProfileBooleanField = (field, newValue) => {
        let oldState = this.props.profile
        let newState = Object.assign({}, oldState)

        switch (field) {
            case 'partnerHaveKids':
                newState.partnerHaveKids = newValue
                this.props.updateUserProfileTextField(newState)
                return
            case 'partnerSmoke':
                newState.partnerSmoke = newValue
                this.props.updateUserProfileTextField(newState)
                return
            case 'partnerDrinkAlcohol':
                newState.partnerDrinkAlcohol = newValue
                this.props.updateUserProfileTextField(newState)
                return

            default:
                return
        }
    }

    render() {
        return(
            <div>
                <h2>Partner info</h2>
                <div>
                    <label>
                        Age range from
                        <input
                            type="text"
                            defaultValue={ this.props.profile.partnerAgeRangeFrom }
                            onChange={(e) => this.updateProfileTextField("partnerAgeRangeFrom", e.target.value)}
                        />
                    </label>
                </div>
                <div>
                    <label>
                        Age range to
                        <input
                            type="text" defaultValue={ this.props.profile.partnerAgeRangeTo }
                            onChange={(e) => this.updateProfileTextField("partnerAgeRangeTo", e.target.value)}
                        />
                    </label>
                </div>
                <div>
                    <label>
                        Figure
                        <input
                            type="text"
                            defaultValue={ this.props.profile.partnerFigure }
                            onChange={(e) => this.updateProfileTextField("partnerFigure", e.target.value)}
                        />
                    </label>
                </div>
                <div>
                    <YesNoInputField
                        label="Kids"
                        value={ this.props.profile.partnerHaveKids }
                        switchValue={(newValue) => this.updateProfileBooleanField('partnerHaveKids', newValue)}
                    />
                </div>
                <div>
                    <YesNoInputField
                        label="Smoker"
                        value={ this.props.profile.partnerSmoke }
                        switchValue={(newValue) => this.updateProfileBooleanField('partnerSmoke', newValue)}
                    />
                </div>
                <div>
                    <YesNoInputField
                        label="Drink alcohol"
                        value={ this.props.profile.partnerDrinkAlcohol }
                        switchValue={(newValue) => this.updateProfileBooleanField('partnerDrinkAlcohol', newValue)}
                    />
                </div>
                <div>
                    <label>
                        Partner income from
                        <input
                            type="text"
                            defaultValue={ this.props.profile.partnerIncomeFrom }
                            onChange={(e) => this.updateProfileTextField("partnerIncomeFrom", e.target.value)}
                        />
                    </label>
                </div>
                <div>
                    <label>
                        Partner income to
                        <input
                            type="text"
                            defaultValue={ this.props.profile.partnerIncomeTo }
                            onChange={(e) => this.updateProfileTextField("partnerIncomeTo", e.target.value)}
                        />
                    </label>
                </div>
            </div>
        )
    }
}

const mapDispatchToProps = dispatch => {
    return {
        updateUserProfileBoleanField: (newValue) => dispatch(updateUserProfileBoleanField(newValue)),
        updateUserProfileTextField: (newValue) => dispatch(updateUserProfileTextField(newValue))
    }
}

export default connect(null, mapDispatchToProps)(ProfilePartnerInfo)