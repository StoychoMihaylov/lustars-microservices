import React, { Component } from "react"
import { connect } from "react-redux"
import { updateUserProfileBoleanField } from '../../store/actions/myProfileActions'
import { NotificationManager } from 'react-notifications'
import '../../styles/components/profile/MyProfileLustarsQuestions.css'

class MyProfileLustarsQuestions extends Component {

    checkIfThereAreFiveBoxesChecked(field, value) {
        var boolObj = {
            "love": this.props.profile.love,
            "trust": this.props.profile.trust,
            "sex": this.props.profile.sex,
            "financialStability": this.props.profile.financialStability,
            "respectAndUnderstanding": this.props.profile.respectAndUnderstanding,
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

        return counter <= 5 ? true : false
    }

    updateProfileBooleanField = (field, event) => {
        let value = event.target.value === "true" ? false : true

        let contrinue = this.checkIfThereAreFiveBoxesChecked(field, value)
        if (!contrinue) {
            NotificationManager.info('Only 5 Lustars questions can be chosen', ':(', 5000)
            return
        }

       let oldState = this.props.profile
        let newState = Object.assign({}, oldState)

        switch (field) {
            case 'love':
                newState.love = value
                this.props.updateUserProfileBoleanField(newState)
                return
            case 'trust':
                newState.trust = value
                this.props.updateUserProfileBoleanField(newState)
                return
            case 'sex':
                newState.sex = value
                this.props.updateUserProfileBoleanField(newState)
                return
            case 'financialStability':
                newState.financialStability = value
                this.props.updateUserProfileBoleanField(newState)
                return
            case 'respectAndUnderstanding':
                newState.respectAndUnderstanding = value
                this.props.updateUserProfileBoleanField(newState)
                return
            case 'sameInterests':
                newState.sameInterests = value
                this.props.updateUserProfileBoleanField(newState)
                return
            case 'oppositeAttracs':
                newState.oppositeAttracs = value
                this.props.updateUserProfileBoleanField(newState)
                return
            case 'growingFamily':
                newState.growingFamily = value
                this.props.updateUserProfileBoleanField(newState)
                return
            case 'loveForAnimals':
                newState.loveForAnimals = value
                this.props.updateUserProfileBoleanField(newState)
                return
            case 'shareSameReligion':
                newState.shareSameReligion = value
                this.props.updateUserProfileBoleanField(newState)
                return
            case 'keepTraditions':
                newState.keepTraditions = value
                this.props.updateUserProfileBoleanField(newState)
                return

            default:
                return
        }
    }

    render() {
        return (
            <div className="lustars-questions-container">
                <h2>Lustars questions</h2>
                <div>What is the most important for you in a relationship ?</div>
                <br/>
                <div>
                    <label className="lustars-questions-checkbox">
                        <span className="lustarts-questions-label">Love</span>
                        <input
                            type="checkbox"
                            checked={ this.props.profile.love }
                            value={ this.props.profile.love }
                            onClick={ (event) => this.updateProfileBooleanField('love', event) }
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
                    <span className="lustarts-questions-label">Respect and understanding</span>
                        <input
                            type="checkbox"
                            checked={ this.props.profile.respectAndUnderstanding }
                            value={ this.props.profile.respectAndUnderstanding }
                            onClick={ (event) => this.updateProfileBooleanField('respectAndUnderstanding', event) }
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
        updateUserProfileBoleanField: (newValue) => dispatch(updateUserProfileBoleanField(newValue))
    }
}

export default connect(null, mapDispatchToProps)(MyProfileLustarsQuestions)