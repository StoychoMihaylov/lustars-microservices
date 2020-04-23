import React, { Component } from "react"
import { connect } from "react-redux"
import { updateUserProfileBoleanField, updateUserProfileTextField } from '../../store/actions/profileActions'
import YesNoInputField from './YesNoInputField'
import '../../styles/components/profile/ProfilePartnerInfo.css'

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
                <table className="profile-partner-info-table">
                    <tbody>
                        <tr>
                            <th></th>
                            <th></th>
                        </tr>
                        <tr>
                            <td><label htmlFor="partner-age-range-from" >Age range from:</label></td>
                            <td>
                                <input
                                    id="partner-age-range-from"
                                    type="text"
                                    placeholder="Partner age from"
                                    className="text-input-profile-about"
                                    defaultValue={ this.props.profile.partnerAgeRangeFrom }
                                    onChange={(e) => this.updateProfileTextField("partnerAgeRangeFrom", e.target.value)}
                                />
                            </td>
                        </tr>
                        <tr>
                            <td><label htmlFor="partner-age-range-to">Age range to:</label></td>
                            <td>
                                <input
                                    id="partner-age-range-to"
                                    type="text"
                                    placeholder="Partner age to"
                                    className="text-input-profile-about"
                                    defaultValue={ this.props.profile.partnerAgeRangeTo }
                                    onChange={(e) => this.updateProfileTextField("partnerAgeRangeTo", e.target.value)}
                                />
                            </td>
                        </tr>
                        <tr>
                            <td><label htmlFor="partner-figure">Figure:</label></td>
                            <td>
                                <input
                                    id="partner-figure"
                                    type="text"
                                    placeholder="Partner figure"
                                    className="text-input-profile-about"
                                    defaultValue={ this.props.profile.partnerFigure }
                                    onChange={(e) => this.updateProfileTextField("partnerFigure", e.target.value)}
                                />
                            </td>
                        </tr>
                        <tr>
                            <td><hr/></td>
                            <td><hr/></td>
                        </tr>
                        <tr>
                            <td><label htmlFor="partner-kids">Kids:</label></td>
                            <td>
                                <YesNoInputField
                                    id="partner-kids"
                                    value={ this.props.profile.partnerHaveKids }
                                    switchValue={(newValue) => this.updateProfileBooleanField('partnerHaveKids', newValue)}
                                />
                            </td>
                        </tr>
                        <tr>
                            <td><label htmlFor="partner-smoker">Smoker:</label></td>
                            <td>
                                <YesNoInputField
                                    id="partner-smoker"
                                    value={ this.props.profile.partnerSmoke }
                                    switchValue={(newValue) => this.updateProfileBooleanField('partnerSmoke', newValue)}
                                />
                            </td>
                        </tr>
                        <tr>
                            <td><label htmlFor="parther-drink-alcohol">Drink alcohol:</label></td>
                            <td>
                                <YesNoInputField
                                    id="parther-drink-alcohol"
                                    value={ this.props.profile.partnerDrinkAlcohol }
                                    switchValue={(newValue) => this.updateProfileBooleanField('partnerDrinkAlcohol', newValue)}
                                />
                            </td>
                        </tr>
                        <tr>
                            <td><hr/></td>
                            <td><hr/></td>
                        </tr>
                        <tr>
                            <td><label htmlFor="partner-income-from">Partner income from:</label></td>
                            <td>
                                <input
                                    id="partner-income-from"
                                    type="text"
                                    placeholder="800€"
                                    className="text-input-profile-about"
                                    defaultValue={ this.props.profile.partnerIncomeFrom }
                                    onChange={(e) => this.updateProfileTextField("partnerIncomeFrom", e.target.value)}
                                />
                            </td>
                        </tr>
                        <tr>
                            <td><label htmlFor="partner-income-to">Partner income to:</label></td>
                            <td>
                                <input
                                    id="partner-income-to"
                                    type="text"
                                    placeholder="1800€"
                                    className="text-input-profile-about"
                                    defaultValue={ this.props.profile.partnerIncomeTo }
                                    onChange={(e) => this.updateProfileTextField("partnerIncomeTo", e.target.value)}
                                />
                            </td>
                        </tr>
                    </tbody>
                </table>
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