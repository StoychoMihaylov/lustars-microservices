import React, { Component } from "react"
import { connect } from "react-redux"
import { updateUserProfileBoleanField, editMyUserProfileDetails } from '../../../store/actions/myProfileActions'
import { NotificationManager } from 'react-notifications'
import './MyProfileLustarsQuestions.css'

class MyProfileLustarsQuestions extends Component {

    checkIfThereAreFiveBoxesChecked(field, value) {
        var boolObj = {
            "partnerVisualAppearance": this.props.profile.partnerVisualAppearance,
            "trust": this.props.profile.trust,
            "sex": this.props.profile.sex,
            "financialStability": this.props.profile.financialStability,
            "communicationAndUnderstanding": this.props.profile.communicationAndUnderstanding,
            "sameInterests": this.props.profile.sameInterests,
            "oppositeAttracs": this.props.profile.oppositeAttracs,
            "growingFamily": this.props.profile.growingFamily,
            "loveForAnimals": this.props.profile.loveForAnimals,
            "shareSameReligion": this.props.profile.shareSameReligion,
            "keepTraditions": this.props.profile.keepTraditions,
        }

        boolObj[field] = value // Update with the new value

        let counter = 0
        Object.keys(boolObj).forEach(key => {
            if (boolObj[key]) {
                counter++
            }
        })

        return counter <= 3 ? true : false
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
            })
    }

    updateProfileBooleanField = async (field, event) => {
        let value = event.target.value === "true" ? false : true

        let contrinue = this.checkIfThereAreFiveBoxesChecked(field, value)
        if (!contrinue) {
            NotificationManager.info('Only 3 Lustars answers can be chosen', ':(', 5000)
            return
        }

       let oldState = this.props.profile
        let newState = Object.assign({}, oldState)

        switch (field) {
            case 'partnerVisualAppearance':
                newState.partnerVisualAppearance = value
                await this.props.updateUserProfileBoleanField(newState)
                await this.updateUserProfile()
                return
            case 'trust':
                newState.trust = value
                await this.props.updateUserProfileBoleanField(newState)
                await this.updateUserProfile()
                return
            case 'sex':
                newState.sex = value
                await this.props.updateUserProfileBoleanField(newState)
                await this.updateUserProfile()
                return
            case 'financialStability':
                newState.financialStability = value
                await this.props.updateUserProfileBoleanField(newState)
                await this.updateUserProfile()
                return
            case 'communicationAndUnderstanding':
                newState.communicationAndUnderstanding = value
                await this.props.updateUserProfileBoleanField(newState)
                await this.updateUserProfile()
                return
            case 'sameInterests':
                newState.sameInterests = value
                await this.props.updateUserProfileBoleanField(newState)
                await this.updateUserProfile()
                return
            case 'oppositeAttracs':
                newState.oppositeAttracs = value
                await this.props.updateUserProfileBoleanField(newState)
                await this.updateUserProfile()
                return
            case 'growingFamily':
                newState.growingFamily = value
                await this.props.updateUserProfileBoleanField(newState)
                await this.updateUserProfile()
                return
            case 'loveForAnimals':
                newState.loveForAnimals = value
                await this.props.updateUserProfileBoleanField(newState)
                await this.updateUserProfile()
                return
            case 'shareSameReligion':
                newState.shareSameReligion = value
                await this.props.updateUserProfileBoleanField(newState)
                await this.updateUserProfile()
                return
            case 'keepTraditions':
                newState.keepTraditions = value
                await this.props.updateUserProfileBoleanField(newState)
                await this.updateUserProfile()
                return

            default:
                return
        }
    }

    render() {
        return (
            <div className="lustars-questions-container">
                <h2>Lustars priorities</h2>
                <div>What is the most important for you in a relationship ? (3 options)</div>
                <br/>
                <div>
                    <label className="lustars-questions-checkbox">
                        <span className="lustarts-questions-label">Partner visual appearance</span>
                        <input
                            type="checkbox"
                            checked={ this.props.profile.partnerVisualAppearance }
                            value={ this.props.profile.partnerVisualAppearance }
                            onClick={ (event) => this.updateProfileBooleanField('partnerVisualAppearance', event) }
                        />
                        <span className="lustars-questions-checkmark"></span>
                    </label>
                    <label className="lustars-questions-checkbox">
                        <span className="lustarts-questions-label">Trust</span>
                        <input
                            type="checkbox"
                            checked={ this.props.profile.trust }
                            value={ this.props.profile.trust }
                            onClick={ (event) => this.updateProfileBooleanField('trust', event) }
                        />
                        <span className="lustars-questions-checkmark"></span>
                    </label>
                    <label className="lustars-questions-checkbox">
                        <span className="lustarts-questions-label">Sex</span>
                        <input
                            type="checkbox"
                            checked={ this.props.profile.sex }
                            value={ this.props.profile.sex }
                            onClick={ (event) => this.updateProfileBooleanField('sex', event) }
                        />
                        <span className="lustars-questions-checkmark"></span>
                    </label>
                    <label className="lustars-questions-checkbox">
                    <span className="lustarts-questions-label">Financial stability</span>
                        <input
                            type="checkbox"
                            checked={ this.props.profile.financialStability }
                            value={ this.props.profile.financialStability }
                            onClick={ (event) => this.updateProfileBooleanField('financialStability', event) }
                        />
                        <span className="lustars-questions-checkmark"></span>
                    </label>
                    <label className="lustars-questions-checkbox">
                    <span className="lustarts-questions-label">Communication and understanding</span>
                        <input
                            type="checkbox"
                            checked={ this.props.profile.communicationAndUnderstanding }
                            value={ this.props.profile.communicationAndUnderstanding }
                            onClick={ (event) => this.updateProfileBooleanField('communicationAndUnderstanding', event) }
                        />
                        <span className="lustars-questions-checkmark"></span>
                    </label>
                    <label className="lustars-questions-checkbox">
                    <span className="lustarts-questions-label">Same interests</span>
                        <input
                            type="checkbox"
                            checked={ this.props.profile.sameInterests }
                            value={ this.props.profile.sameInterests }
                            onClick={ (event) => this.updateProfileBooleanField('sameInterests', event) }
                        />
                        <span className="lustars-questions-checkmark"></span>
                    </label>
                    <label className="lustars-questions-checkbox">
                    <span className="lustarts-questions-label">Opposite attracs</span>
                        <input
                            type="checkbox"
                            checked={ this.props.profile.oppositeAttracs }
                            value={ this.props.profile.oppositeAttracs }
                            onClick={ (event) => this.updateProfileBooleanField('oppositeAttracs', event) }
                        />
                        <span className="lustars-questions-checkmark"></span>
                    </label>
                    <label className="lustars-questions-checkbox">
                    <span className="lustarts-questions-label">Growing family</span>
                        <input
                            type="checkbox"
                            checked={ this.props.profile.growingFamily }
                            value={ this.props.profile.growingFamily }
                            onClick={ (event) => this.updateProfileBooleanField('growingFamily', event) }
                        />
                        <span className="lustars-questions-checkmark"></span>
                    </label>

                    <label className="lustars-questions-checkbox">
                    <span className="lustarts-questions-label">Love for animals</span>
                        <input
                            type="checkbox"
                            checked={ this.props.profile.loveForAnimals }
                            value={ this.props.profile.loveForAnimals }
                            onClick={ (event) => this.updateProfileBooleanField('loveForAnimals', event) }
                        />
                        <span className="lustars-questions-checkmark"></span>
                    </label>
                    <label className="lustars-questions-checkbox">
                    <span className="lustarts-questions-label">Share same religion</span>
                        <input
                            type="checkbox"
                            checked={ this.props.profile.shareSameReligion }
                            value={ this.props.profile.shareSameReligion }
                            onClick={ (event) => this.updateProfileBooleanField('shareSameReligion', event) }
                        />
                        <span className="lustars-questions-checkmark"></span>
                    </label>
                    <label className="lustars-questions-checkbox">
                    <span className="lustarts-questions-label">Keep traditions</span>
                        <input
                            type="checkbox"
                            checked={ this.props.profile.keepTraditions }
                            value={ this.props.profile.keepTraditions }
                            onClick={ (event) => this.updateProfileBooleanField('keepTraditions', event) }
                        />
                        <span className="lustars-questions-checkmark"></span>
                    </label>
                </div>
            </div>
        )
    }
}

const mapDispatchToProps = dispatch => {
    return {
        editMyUserProfileDetails: (userProfileDetails) => dispatch(editMyUserProfileDetails(userProfileDetails)),
        updateUserProfileBoleanField: (newValue) => dispatch(updateUserProfileBoleanField(newValue))
    }
}

export default connect(null, mapDispatchToProps)(MyProfileLustarsQuestions)