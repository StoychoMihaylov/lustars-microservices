import React, { Component } from "react"
import { connect } from "react-redux"
import { updateUserProfileBoleanField, updateUserProfileTextField } from '../../store/actions/profileActions'
import YesNoInputField from './YesNoInputField'

class ProfileAboutMe extends Component {
    constructor(props) {
        super(props)
    }

    updateProfileTextField(field, value) {
        let oldState = this.props.profile
        let newState = Object.assign({}, oldState)

        switch (field) {
            case 'title':
                newState.title = value
                this.props.updateUserProfileTextField(newState)
                return
            case 'dateOfBirth':
                newState.dateOfBirth = value
                this.props.updateUserProfileTextField(newState)
                return
            case 'gender':
                newState.gender = value
                this.props.updateUserProfileTextField(newState)
                return
            case 'meritalStatus':
                newState.meritalStatus = value
                this.props.updateUserProfileTextField(newState)
                return
            case 'lookingFor':
                newState.lookingFor = value
                this.props.updateUserProfileTextField(newState)
                return
            case 'languages':
                newState.languages = value
                this.props.updateUserProfileTextField(newState)
                return
            case 'educationDegree':
                newState.educationDegree = value
                this.props.updateUserProfileTextField(newState)
                return
            case 'university':
                newState.university = value
                this.props.updateUserProfileTextField(newState)
                return
            case 'work':
                newState.work = value
                this.props.updateUserProfileTextField(newState)
                return
            case 'income':
                newState.income = parseInt(value)
                this.props.updateUserProfileTextField(newState)
                return
            case 'biography':
                newState.biography = value
                this.props.updateUserProfileTextField(newState)
                return
            case 'height':
                newState.height = parseInt(value)
                this.props.updateUserProfileTextField(newState)
                return
            case 'weight':
                newState.weight = parseInt(value)
                this.props.updateUserProfileTextField(newState)
                return
            case 'figure':
                newState.figure = value
                this.props.updateUserProfileTextField(newState)
                return
            case 'howOftenDrinkAlcohol':
                newState.howOftenDrinkAlcohol = value
                this.props.updateUserProfileTextField(newState)
                return
            case 'howOftenSmoke':
                newState.howOftenSmoke = value
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
            case 'haveKids':
                newState.haveKids = newValue
                this.props.updateUserProfileBoleanField(newState)
                return
            case 'wantToHaveKids':
                newState.wantToHaveKids = newValue
                this.props.updateUserProfileBoleanField(newState)
                return
            case 'drinkAlcohol':
                newState.drinkAlcohol = newValue
                this.props.updateUserProfileBoleanField(newState)
                return
            case 'smoker':
                newState.smoker = newValue
                this.props.updateUserProfileBoleanField(newState)
                return

            default:
                return
        }
    }

    render() {
        return(
            <div>
                <h2>About me</h2>
                <div>
                    <label>
                        Mood
                        <input
                            type="text"
                            defaultValue={ this.props.profile.title }
                            onChange={(e) => this.updateProfileTextField("title", e.target.value)}
                        />
                    </label>
                </div>
                <div>
                    <label>
                        Birth date
                        <input
                            type="text"
                            defaultValue={ this.props.profile.dateOfBirth }
                            onChange={(e) => this.updateProfileTextField("dateOfBirth", e.target.value)}
                        />
                    </label>
                </div>
                <div>
                    <label>
                        Gender
                        <input
                            type="text"
                            defaultValue={ this.props.profile.gender }
                            onChange={(e) => this.updateProfileTextField("gender", e.target.value)}
                        />
                    </label>
                </div>
                <div>
                    <label>
                        Merial status
                        <input
                            type="text"
                            defaultValue={ this.props.profile.meritalStatus }
                            onChange={(e) => this.updateProfileTextField("meritalStatus", e.target.value)}
                        />
                    </label>
                </div>
                <div>
                    <label>
                        Looking for
                        <input
                            type="text"
                            defaultValue={ this.props.profile.lookingFor }
                            onChange={(e) => this.updateProfileTextField("lookingFor", e.target.value)}
                        />
                    </label>
                </div>
                <div>
                    <label>
                        Languages
                        <input
                            type="text"
                            defaultValue={ this.props.profile.languages }
                            onChange={(e) => this.updateProfileTextField("languages", e.target.value)}
                        />
                    </label>
                </div>
                <div>
                    <label>
                        Education degree
                        <input
                            type="text"
                            defaultValue={ this.props.profile.educationDegree }
                            onChange={(e) => this.updateProfileTextField("educationDegree", e.target.value)}
                        />
                    </label>
                </div>
                <div>
                    <label>
                        University
                        <input
                            type="text"
                            defaultValue={ this.props.profile.university }
                            onChange={(e) => this.updateProfileTextField("university", e.target.value)}
                        />
                    </label>
                </div>
                <div>
                    <label>
                        Work
                        <input
                            type="text"
                            defaultValue={ this.props.profile.work }
                            onChange={(e) => this.updateProfileTextField("work", e.target.value)}
                        />
                    </label>
                </div>
                <div>
                    <label>
                        Income
                        <input
                            type="text"
                            defaultValue={ this.props.profile.income }
                            onChange={(e) => this.updateProfileTextField("income", e.target.value)}
                        />
                    </label>
                </div>
                <div>
                    <label>
                        Biography
                        <textarea
                            defaultValue={ this.props.profile.biography }
                            onChange={(e) => this.updateProfileTextField("biography", e.target.value)}>
                        </textarea>
                    </label>
                </div>
                <br/>
                <div>
                    <YesNoInputField
                        label="Kids"
                        value={ this.props.profile.haveKids }
                        switchValue={(newValue) => this.updateProfileBooleanField('haveKids', newValue)}
                    />
                </div>
                <div>
                    <YesNoInputField
                        label="Want to have kids"
                        value={ this.props.profile.wantToHaveKids }
                        switchValue={(newValue) => this.updateProfileBooleanField('wantToHaveKids', newValue)}
                    />
                </div>
                <div>
                    <label>
                        Height
                        <input
                            type="text"
                            defaultValue={ this.props.profile.height }
                            onChange={(e) => this.updateProfileTextField("height", e.target.value)}
                        />
                    </label>
                </div>
                <div>
                    <label>
                        Weight
                        <input
                            type="text"
                            defaultValue={ this.props.profile.weight }
                            onChange={(e) => this.updateProfileTextField("weight", e.target.value)}
                        />
                    </label>
                </div>
                <div>
                    <label>
                        Figure
                        <input
                            type="text"
                            defaultValue={ this.props.profile.figure }
                            onChange={(e) => this.updateProfileTextField("figure", e.target.value)}
                        />
                    </label>
                </div>
                <div>
                    <YesNoInputField
                        label="Drink Alcohol"
                        value={ this.props.profile.drinkAlcohol }
                        switchValue={(newValue) => this.updateProfileBooleanField('drinkAlcohol', newValue)}
                    />
                </div>
                <div>
                    <label>
                        How often Drink
                        <input
                            type="text"
                            defaultValue={ this.props.profile.howOftenDrinkAlcohol }
                            onChange={(e) => this.updateProfileTextField("howOftenDrinkAlcohol", e.target.value)}
                        />
                    </label>
                </div>
                <div>
                    <YesNoInputField
                        label="Smoker"
                        value={ this.props.profile.smoker }
                        switchValue={(newValue) => this.updateProfileBooleanField('smoker', newValue)}
                    />
                </div>
                <div>
                    <label>
                        How often smoke
                        <input
                            type="text"
                            defaultValue={ this.props.profile.howOftenSmoke }
                            onChange={(e) => this.updateProfileTextField("howOftenSmoke", e.target.value)}
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

export default connect(null, mapDispatchToProps)(ProfileAboutMe)