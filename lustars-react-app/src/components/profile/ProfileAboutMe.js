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
            case 'name':
                newState.name = value
                this.props.updateUserProfileTextField(newState)
                return
            case 'lastName':
                newState.lastName = value
                this.props.updateUserProfileTextField(newState)
                return
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
                <table className="profile-about-me-table">
                    <tr>
                        <th><label for="first-name">First name:</label></th>
                        <th>
                            <input
                                id="first-name"
                                type="text"
                                placeholder="First Name"
                                className="text-input-profile-about"
                                defaultValue={ this.props.profile.name }
                                onChange={(e) => this.updateProfileTextField("name", e.target.value)}
                            />
                        </th>
                    </tr>
                    <tr>
                        <td><label for="last-name">Last name:</label></td>
                        <td>
                            <input
                                id="last-name"
                                type="text"
                                placeholder="Last Name"
                                className="text-input-profile-about"
                                defaultValue={ this.props.profile.lastName }
                                onChange={(e) => this.updateProfileTextField("lastName", e.target.value)}
                            />
                        </td>
                    </tr>
                    <tr>
                        <td><label for="mood">Mood:</label></td>
                        <td>
                            <input
                                id="mood"
                                type="text"
                                placeholder="Type how you feel"
                                className="text-input-profile-about"
                                defaultValue={ this.props.profile.title }
                                onChange={(e) => this.updateProfileTextField("title", e.target.value)}
                            />
                        </td>
                    </tr>
                    <tr>
                        <td><label for="date-of-birth">Birth date:</label></td>
                        <td>
                            <input
                                id="date-of-birth"
                                type="text"
                                placeholder="DD/mm/yyyy"
                                className="text-input-profile-about"
                                defaultValue={ this.props.profile.dateOfBirth }
                                onChange={(e) => this.updateProfileTextField("dateOfBirth", e.target.value)}
                            />
                        </td>
                    </tr>
                    <tr>
                        <td><label for="gender">Gender:</label></td>
                        <td>
                            <input
                                id="gender"
                                type="text"
                                placeholder="man/female/gay"
                                className="text-input-profile-about"
                                defaultValue={ this.props.profile.gender }
                                onChange={(e) => this.updateProfileTextField("gender", e.target.value)}
                            />
                        </td>
                    </tr>
                    <tr>
                        <td><label for="merital-status">Merial status:</label></td>
                        <td>
                            <input
                                id="merital-status"
                                type="text"
                                placeholder="single/meried/divorced"
                                className="text-input-profile-about"
                                defaultValue={ this.props.profile.meritalStatus }
                                onChange={(e) => this.updateProfileTextField("meritalStatus", e.target.value)}
                            />
                        </td>
                    </tr>
                    <tr>
                        <td><label for="looking-for">Looking for:</label></td>
                        <td>
                            <input
                                id="looking-for"
                                type="text"
                                placeholder="man/female/gay"
                                className="text-input-profile-about"
                                defaultValue={ this.props.profile.lookingFor }
                                onChange={(e) => this.updateProfileTextField("lookingFor", e.target.value)}
                            />
                        </td>
                    </tr>
                    <tr>
                        <td><label for="languages">Languages:</label></td>
                        <td>
                            <input
                                id="languages"
                                type="text"
                                placeholder="Bulgarian, Russian, English..."
                                className="text-input-profile-about"
                                defaultValue={ this.props.profile.languages }
                                onChange={(e) => this.updateProfileTextField("languages", e.target.value)}
                            />
                        </td>
                    </tr>
                    <tr>
                        <td><label for="education-degree">Education degree:</label></td>
                        <td>
                            <input
                                id="education-degree"
                                type="text"
                                placeholder="School/Bachelor/Master/Doctor/Professor"
                                className="text-input-profile-about"
                                defaultValue={ this.props.profile.educationDegree }
                                onChange={(e) => this.updateProfileTextField("educationDegree", e.target.value)}
                            />
                        </td>
                    </tr>
                    <tr>
                        <td><label for="university">University:</label></td>
                        <td>
                            <input
                                id="university"
                                type="text"
                                placeholder="Technical University - Sofia"
                                className="text-input-profile-about"
                                defaultValue={ this.props.profile.university }
                                onChange={(e) => this.updateProfileTextField("university", e.target.value)}
                            />
                        </td>
                    </tr>
                    <tr>
                        <td><label for="work">Work:</label></td>
                        <td>
                            <input
                            id="work"
                            type="text"
                            placeholder="Work"
                            className="text-input-profile-about"
                            defaultValue={ this.props.profile.work }
                            onChange={(e) => this.updateProfileTextField("work", e.target.value)}
                            />
                        </td>
                    </tr>
                    <tr>
                        <td><label for="income">Income:</label></td>
                        <td>
                            <input
                                id="income"
                                type="text"
                                placeholder="3000‎€"
                                className="text-input-profile-about"
                                defaultValue={ this.props.profile.income }
                                onChange={(e) => this.updateProfileTextField("income", e.target.value)}
                            />
                        </td>
                    </tr>
                    <tr>
                        <td><label for="biography">Biography:</label></td>
                        <td>
                            <textarea
                                id="biography"
                                rows="4" cols="28"
                                placeholder="Type something that describes you."
                                className="text-input-profile-about"
                                defaultValue={ this.props.profile.biography }
                                onChange={(e) => this.updateProfileTextField("biography", e.target.value)}>
                            </textarea>
                        </td>
                    </tr>
                    <tr>
                        <td><hr/></td>
                        <td><hr/></td>
                    </tr>
                    <tr>
                        <td><label for="kids">Kids:</label></td>
                        <td>
                            <YesNoInputField
                                id="kids"
                                value={ this.props.profile.haveKids }
                                switchValue={(newValue) => this.updateProfileBooleanField('haveKids', newValue)}
                            />
                        </td>
                    </tr>
                    <tr>
                        <td><label for="want-to-have-kids">Want to have kids:</label></td>
                        <td>
                            <YesNoInputField
                                id="want-to-have-kids"
                                value={ this.props.profile.wantToHaveKids }
                                switchValue={(newValue) => this.updateProfileBooleanField('wantToHaveKids', newValue)}
                            />
                        </td>
                    </tr>
                    <tr>
                        <td><label for="drink-alcohol">Drink alcohol:</label></td>
                        <td>
                            <YesNoInputField
                                id="drink-alcohol"
                                value={ this.props.profile.drinkAlcohol }
                                switchValue={(newValue) => this.updateProfileBooleanField('drinkAlcohol', newValue)}
                            />
                        </td>
                    </tr>
                    <tr>
                        <td><label for="smoker">Smoker:</label></td>
                        <td>
                            <YesNoInputField
                                id="smoker"
                                value={ this.props.profile.smoker }
                                switchValue={(newValue) => this.updateProfileBooleanField('smoker', newValue)}
                            />
                        </td>
                    </tr>
                    <tr>
                        <td><hr/></td>
                        <td><hr/></td>
                    </tr>
                    <tr>
                        <td><label for="height">Height:&nbsp;</label></td>
                        <td>
                            <input
                                id="height"
                                type="text"
                                placeholder="180sm"
                                className="text-input-profile-about"
                                defaultValue={ this.props.profile.height }
                                onChange={(e) => this.updateProfileTextField("height", e.target.value)}
                            />
                        </td>
                    </tr>
                    <tr>
                        <td><label for="weight">Weight:&nbsp;</label></td>
                        <td>
                            <input
                                id="weight"
                                type="text"
                                placeholder="75kg"
                                className="text-input-profile-about"
                                defaultValue={ this.props.profile.weight }
                                onChange={(e) => this.updateProfileTextField("weight", e.target.value)}
                            />
                        </td>
                    </tr>
                    <tr>
                        <td><label for="figure">Figure:&nbsp;</label></td>
                        <td>
                            <input
                                id="figure"
                                type="text"
                                placeholder="normal"
                                className="text-input-profile-about"
                                defaultValue={ this.props.profile.figure }
                                onChange={(e) => this.updateProfileTextField("figure", e.target.value)}
                            />
                        </td>
                    </tr>
                    <tr>
                        <td><label for="how-often-drin-alcohol">How often Drink:&nbsp;</label></td>
                        <td>
                            <input
                                id="how-often-drin-alcohol"
                                type="text"
                                placeholder="rarely/often"
                                className="text-input-profile-about"
                                defaultValue={ this.props.profile.howOftenDrinkAlcohol }
                                onChange={(e) => this.updateProfileTextField("howOftenDrinkAlcohol", e.target.value)}
                            />
                        </td>
                    </tr>
                    <tr>
                        <td><label for="how-often-smoke">How often smoke:&nbsp;</label></td>
                        <td>
                            <input
                                id="how-often-smoke"
                                type="text"
                                placeholder="rarely/often/when drink"
                                className="text-input-profile-about"
                                defaultValue={ this.props.profile.howOftenSmoke }
                                onChange={(e) => this.updateProfileTextField("howOftenSmoke", e.target.value)}
                            />
                        </td>
                    </tr>
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

export default connect(null, mapDispatchToProps)(ProfileAboutMe)