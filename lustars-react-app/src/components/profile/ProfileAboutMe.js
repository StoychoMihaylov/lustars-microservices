import React, { Component } from "react"
import { connect } from "react-redux"
import { updateUserProfileBoleanField, updateUserProfileTextField } from '../../store/actions/profileActions'
import YesNoInputField from './YesNoInputField'
import '../../styles/components/profile/ProfileAboutMe.css'

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
                    <label for="mood">Mood:&nbsp;</label>
                        <input
                            id="mood"
                            type="text"
                            placeholder="Type how you feel"
                            className="textInputAbout"
                            defaultValue={ this.props.profile.title }
                            onChange={(e) => this.updateProfileTextField("title", e.target.value)}
                        />
                </div>
                <div>
                    <label for="dateOfBirth">Birth date:&nbsp;</label>
                        <input
                            id="dateOfBirth"
                            type="text"
                            placeholder="DD/mm/yyyy"
                            className="textInputAbout"
                            defaultValue={ this.props.profile.dateOfBirth }
                            onChange={(e) => this.updateProfileTextField("dateOfBirth", e.target.value)}
                        />
                </div>
                <div>
                    <label for="gender">Gender:&nbsp;</label>
                        <input
                            id="gender"
                            type="text"
                            placeholder="man/female/gay"
                            className="textInputAbout"
                            defaultValue={ this.props.profile.gender }
                            onChange={(e) => this.updateProfileTextField("gender", e.target.value)}
                        />
                </div>
                <div>
                    <label for="meritalStatus">Merial status:&nbsp;</label>
                        <input
                            id="meritalStatus"
                            type="text"
                            placeholder="single/meried/divorced"
                            className="textInputAbout"
                            defaultValue={ this.props.profile.meritalStatus }
                            onChange={(e) => this.updateProfileTextField("meritalStatus", e.target.value)}
                        />
                </div>
                <div>
                    <label for="lookingFor">Looking for:&nbsp;</label>
                        <input
                            id="lookingFor"
                            type="text"
                            placeholder="man/female/gay"
                            className="textInputAbout"
                            defaultValue={ this.props.profile.lookingFor }
                            onChange={(e) => this.updateProfileTextField("lookingFor", e.target.value)}
                        />
                </div>
                <div>
                    <label for="languages">Languages:&nbsp;</label>
                        <input
                            id="languages"
                            type="text"
                            placeholder="Bulgarian, Russian, English..."
                            className="textInputAbout"
                            defaultValue={ this.props.profile.languages }
                            onChange={(e) => this.updateProfileTextField("languages", e.target.value)}
                        />
                </div>
                <div>
                    <label for="educationDegree">Education degree:&nbsp;</label>
                        <input
                            id="educationDegree"
                            type="text"
                            placeholder="School/Bachelor/Master/Doctor/Professor"
                            className="textInputAbout"
                            defaultValue={ this.props.profile.educationDegree }
                            onChange={(e) => this.updateProfileTextField("educationDegree", e.target.value)}
                        />
                </div>
                <div>
                    <label for="university">University:&nbsp;</label>
                        <input
                            id="university"
                            type="text"
                            placeholder="Technical University - Sofia"
                            className="textInputAbout"
                            defaultValue={ this.props.profile.university }
                            onChange={(e) => this.updateProfileTextField("university", e.target.value)}
                        />
                </div>
                <div>
                    <label for="work">Work:&nbsp;</label>
                        <input
                            id="work"
                            type="text"
                            placeholder="Work"
                            className="textInputAbout"
                            defaultValue={ this.props.profile.work }
                            onChange={(e) => this.updateProfileTextField("work", e.target.value)}
                        />
                </div>
                <div>
                    <label for="income">Income:&nbsp;</label>
                        <input
                            id="income"
                            type="text"
                            placeholder="3000лв."
                            className="textInputAbout"
                            defaultValue={ this.props.profile.income }
                            onChange={(e) => this.updateProfileTextField("income", e.target.value)}
                        />
                </div>
                <br/>
                <div>
                    <label for="biography">Biography:&nbsp;</label>
                        <textarea
                            id="biography"
                            rows="4" cols="33"
                            placeholder="Type something that describes you."
                            className="textAreaAbout"
                            defaultValue={ this.props.profile.biography }
                            onChange={(e) => this.updateProfileTextField("biography", e.target.value)}>
                        </textarea>
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
                    <label for="height">Height:&nbsp;</label>
                        <input
                            id="height"
                            type="text"
                            placeholder="180см."
                            className="textInputAbout"
                            defaultValue={ this.props.profile.height }
                            onChange={(e) => this.updateProfileTextField("height", e.target.value)}
                        />
                </div>
                <div>
                    <label for="weight">Weight:&nbsp;</label>
                        <input
                            id="weight"
                            type="text"
                            placeholder="75кг."
                            className="textInputAbout"
                            defaultValue={ this.props.profile.weight }
                            onChange={(e) => this.updateProfileTextField("weight", e.target.value)}
                        />
                </div>
                <div>
                    <label for="figure">Figure:&nbsp;</label>
                        <input
                            id="figure"
                            type="text"
                            placeholder="normal"
                            className="textInputAbout"
                            defaultValue={ this.props.profile.figure }
                            onChange={(e) => this.updateProfileTextField("figure", e.target.value)}
                        />
                </div>
                <div>
                    <YesNoInputField
                        label="Drink Alcohol"
                        value={ this.props.profile.drinkAlcohol }
                        switchValue={(newValue) => this.updateProfileBooleanField('drinkAlcohol', newValue)}
                    />
                </div>
                <div>
                    <label for="howOftenDrinkAlcohol">How often Drink:&nbsp;</label>
                        <input
                            id="howOftenDrinkAlcohol"
                            type="text"
                            className="textInputAbout"
                            defaultValue={ this.props.profile.howOftenDrinkAlcohol }
                            onChange={(e) => this.updateProfileTextField("howOftenDrinkAlcohol", e.target.value)}
                        />
                </div>
                <div>
                    <YesNoInputField
                        label="Smoker"
                        value={ this.props.profile.smoker }
                        switchValue={(newValue) => this.updateProfileBooleanField('smoker', newValue)}
                    />
                </div>
                <div>
                    <label for="howOftenSmoke"></label>
                        <span className="inputLableAbout">How often smoke:&nbsp;</span>
                        <input
                            type="text"
                            className="textInputAbout"
                            defaultValue={ this.props.profile.howOftenSmoke }
                            onChange={(e) => this.updateProfileTextField("howOftenSmoke", e.target.value)}
                        />
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