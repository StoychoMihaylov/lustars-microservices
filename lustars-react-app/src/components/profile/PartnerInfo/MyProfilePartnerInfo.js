import React, { Component } from "react"
import { connect } from "react-redux"
import { NotificationManager } from 'react-notifications'
import { updateUserProfileTextField, editMyUserProfileDetails } from '../../../store/actions/myProfileActions'
import YesNoInputField from '../../common/YesNoInput/YesNoInputField'
import NumberAdjusterInput from '../../common/NumberAdjusterInput/NumberAdjusterInput'
import './MyProfilePartnerInfo.css'

class MyProfilePartnerInfo extends Component {
    constructor(props) {
        super(props)

        this.state = {
            timerIdentified: {},

            // Show/hide fields
            editPartnerAgeRangeFrom: false,
            editPartnerAgeRangeTo: false,
            editPartnerFigure: false
        }
    }

    updateUserProfile(field) {
        this.props.editMyUserProfileDetails(this.props.profile)
            .then(response => {
                if (response.status === 200) {
                    NotificationManager.success('Your profile has been updated successfully', 'Updated!', 3000)
                } else {
                    NotificationManager.error('Something went wrong! Please check your connection!', 'Error!', 5000, () => {
                        alert('There is some problem! Please try again or check your network!');
                      });
                }

                this.closeFieldForModification(field)
            })
    }

    closeFieldForModification(field) {
        switch (field) {
            case 'partnerAgeRangeFrom':
                this.setState({ editPartnerAgeRangeFrom: false })
                return
            case 'partnerAgeRangeTo':
                this.setState({ editPartnerAgeRangeTo: false })
                return
            case 'partnerFigure':
                this.setState({ editPartnerFigure: false })
                return

            default:
                return
        }
    }


    async updateUserProfileWithDelay(field) {
        await this.updateUserProfile()

        switch(field) {
            case 'partnerAgeRangeFrom':
                this.setState({ editPartnerAgeRangeFrom: false })
                return
            case 'partnerAgeRangeTo':
                this.setState({ editPartnerAgeRangeTo: false })
                return

            default:
                return
        }
    }

    updateProfileTextField(field, value) {
        let oldState = this.props.profile
        let newState = Object.assign({}, oldState)

        switch (field) {
            case 'partnerAgeRangeFrom':
                newState.partnerAgeRangeFrom = parseInt(value)
                this.props.updateUserProfileTextField(newState)
                clearTimeout(this.state.timerIdentified.partnerAgeRangeFrom)
                this.setState({
                    timerIdentified: { partnerAgeRangeFrom: setTimeout(() => { this.updateUserProfileWithDelay(field) }, 3000) }
                })
                return
            case 'partnerAgeRangeTo':
                newState.partnerAgeRangeTo = parseInt(value)
                this.props.updateUserProfileTextField(newState)
                clearTimeout(this.state.timerIdentified.partnerAgeRangeTo)
                this.setState({
                    timerIdentified: { partnerAgeRangeTo: setTimeout(() => { this.updateUserProfileWithDelay(field) }, 3000) }
                })
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
                            <td>
                                <span
                                    style={{ display:!this.state.editPartnerAgeRangeFrom ? "block" : "none" }}
                                    onClick={ () => this.setState({ editPartnerAgeRangeFrom: true })}>{ this.props.profile.partnerAgeRangeFrom }
                                </span>
                                <span style={{ display:this.state.editPartnerAgeRangeFrom ? "block" : "none" }}>
                                        <NumberAdjusterInput
                                            id="partner-age-range-from"
                                            numberInput={ this.props.profile.partnerAgeRangeFrom }
                                            numberResult={ (value) => this.updateProfileTextField("partnerAgeRangeFrom", value) }
                                        />
                                </span>
                            </td>
                        </tr>
                        <tr>
                            <td><label htmlFor="partner-age-range-to">Age range to:</label></td>
                            <td>
                                <span
                                    style={{ display:!this.state.editPartnerAgeRangeTo ? "block" : "none" }}
                                    onClick={ () => this.setState({ editPartnerAgeRangeTo: true })}>{ this.props.profile.partnerAgeRangeTo }
                                </span>
                                <span style={{ display:this.state.editPartnerAgeRangeTo ? "block" : "none" }}>
                                    <NumberAdjusterInput
                                        id="partner-age-range-to"
                                        numberInput={ this.props.profile.partnerAgeRangeTo }
                                        numberResult={ (value) => this.updateProfileTextField("partnerAgeRangeTo", value) }
                                    />
                                </span>
                            </td>
                        </tr>
                        <tr>
                            <td><label htmlFor="partner-figure">Figure:</label></td>
                            <td>
                                <span
                                    style={{ display:!this.state.editPartnerFigure ? "block" : "none" }}
                                    onClick={ () => this.setState({ editPartnerFigure: true })}>{ this.props.profile.partnerFigure }
                                </span>
                                <select
                                    id="partner-figure"
                                    style={{ display:this.state.editPartnerFigure ? "block" : "none" }}
                                    className="text-input-profile-about"
                                    value={ this.props.profile.partnerFigure }
                                    onChange={(e) => this.updateProfileTextField("partnerFigure", e.target.value)}
                                    onBlur={ () => this.updateUserProfile("partnerFigure") }>
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
        editMyUserProfileDetails: (userProfileDetails) => dispatch(editMyUserProfileDetails(userProfileDetails)),
        updateUserProfileTextField: (newValue) => dispatch(updateUserProfileTextField(newValue))
    }
}

export default connect(null, mapDispatchToProps)(MyProfilePartnerInfo)