import React, { Component } from "react"
import { connect } from "react-redux"
import { updateUserProfileBoleanField, updateUserProfileTextField } from '../../store/actions/myProfileActions'
import YesNoInputField from './YesNoInputField'
import NumberAdjusterInput from '../common/NumberAdjusterInput'
import '../../styles/components/profile/MyProfilePartnerInfo.css'

class MyProfilePartnerInfo extends Component {
    updateProfileTextField(field, value) {
        let oldState = this.props.profile
        let newState = Object.assign({}, oldState)

        switch (field) {
            case 'partnerAgeRangeFrom':
                console.log(value)
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
            case 'partnerDoSport':
                newState.partnerDoSport = newValue
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
                            {
                                this.props.profile.partnerAgeRangeFrom !== undefined
                                    ?   <td>
                                            <NumberAdjusterInput
                                                id="partner-age-range-from"
                                                numberInput={ this.props.profile.partnerAgeRangeFrom }
                                                numberResult={ (value) => this.updateProfileTextField("partnerAgeRangeFrom", value) }
                                            />
                                        </td>
                                    :   null
                            }
                        </tr>
                        <tr>
                            <td><label htmlFor="partner-age-range-to">Age range to:</label></td>
                            {
                                this.props.profile.partnerAgeRangeTo !== undefined
                                    ?   <td>
                                            <NumberAdjusterInput
                                                id="partner-age-range-to"
                                                numberInput={ this.props.profile.partnerAgeRangeTo }
                                                numberResult={ (value) => this.updateProfileTextField("partnerAgeRangeTo", value) }
                                            />
                                        </td>
                                    :   null
                            }
                        </tr>
                        <tr>
                            <td><label htmlFor="partner-figure">Figure:</label></td>
                            <td>
                                <select
                                    id="partner-figure"
                                    className="text-input-profile-about"
                                    value={ this.props.profile.partnerFigure }
                                    onChange={(e) => this.updateProfileTextField("partnerFigure", e.target.value)}>
                                    {
                                        this.props.profile.partnerFigure === null || this.props.profile.partnerFigure === undefined
                                            ? <option selected="selected">Partner figure</option>
                                            : null
                                    }
                                    <option value="Thin">Thin</option>
                                    <option value="Normal">Normal</option>
                                    <option value="Athletic">Athletic</option>
                                    <option value="Muscular">Muscular</option>
                                    <option value="Fluffy">Fluffy</option>
                                </select>
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
                            <td><label htmlFor="parther-do-sport">Do sport:</label></td>
                            <td>
                                <YesNoInputField
                                    id="parther-do-sport"
                                    value={ this.props.profile.partnerDoSport }
                                    switchValue={(newValue) => this.updateProfileBooleanField('partnerDoSport', newValue)}
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

export default connect(null, mapDispatchToProps)(MyProfilePartnerInfo)