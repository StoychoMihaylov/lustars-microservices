import React, { Component } from "react"
import { connect } from "react-redux"
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import {
    updateUserProfileBoleanField,
    updateUserProfileTextField,
    addUserCountryLanguage,
    deleteUserCountryLanguage,
    editMyUserProfileDetails
} from '../../../store/actions/myProfileActions'
import { NotificationManager } from 'react-notifications'
import YesNoInputField from '../../common/YesNoInput/YesNoInputField'
import NumberAdjusterInput from '../../common/NumberAdjusterInput/NumberAdjusterInput'
import { countryLanguages } from '../../../constants/countryLanguages'
import './MyProfileAboutMe.css'

class MyProfileAboutMe extends Component {
    constructor(props) {
        super(props)

        this.state = {
            isDatePickerClicked: false,
            timerIdentified: {},

            // Show or hide fields
            editFirstName: false,
            editLastName: false,
            editMood: false,
            editGender: false,
            editMeritalStatus: false,
            editLookingFor: false,
            editAddLanguage: false,
            editEducationDegree: false,
            editUniversity: false,
            editWork: false,
            editBiography: false,
            editHeight: false,
            editWeight: false,
            editFigure: false,
            editHowOftenDrinkAlcohol: false,
            editHowOftenDoSport: false,
            editHowOftenSmoke: false
        }
    }

    async updateBirtDayDate(date) {
        await this.updateProfileTextField("dateOfBirth", date)
        await this.updateUserProfile()
    }

    updateUserProfile(field) {
        this.props.editMyUserProfileDetails(this.props.profile)
            .then(response => {
                if (response.status === 202) {
                    NotificationManager.info('Updating you profile...', 'Processing data...', 3000)
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
            case 'name':
                this.setState({ editFirstName: false })
                return
            case 'lastName':
                this.setState({ editLastName: false })
                return
            case 'mood':
                this.setState({ editMood: false })
                return
            case 'gender':
                this.setState({ editGender: false })
                return
            case 'meritalStatus':
                this.setState({ editMeritalStatus: false })
                return
            case 'lookingFor':
                this.setState({ editLookingFor: false })
                return
            case 'addLanguage':
                this.setState({ editAddLanguage: false })
                return
            case 'educationDegree':
                this.setState({ editEducationDegree: false })
                return
            case 'university':
                this.setState({ editUniversity: false })
                return
            case 'work':
                this.setState({ editWork: false })
                return
            case 'biography':
                this.setState({ editBiography: false })
                return
            case 'figure':
                this.setState({ editFigure: false })
                return
            case 'howOftenDrinkAlcohol':
                this.setState({ editHowOftenDrinkAlcohol: false })
                return
            case 'howOftenDoSport':
                this.setState({ editHowOftenDoSport: false })
                return
            case 'howOftenSmoke':
                this.setState({ editHowOftenSmoke: false })
                return

            default:
                return
        }
    }

    async updateUserProfileWithDelay(field) {
        await this.updateUserProfile()

        switch(field) {
            case 'height':
                this.setState({ editHeight: false })
                return
            case 'weight':
                this.setState({ editWeight: false })
                return

            default:
                return
        }
    }

    updateProfileTextField(field, value) {
        if (this.props.profile === undefined ||
            this.props.profile === null ||
            Object.keys(this.props.profile).length === 0) {
            NotificationManager.error('An error ocured connection you, please try again.', ':(' , 3000)
            return
        }

        let oldState = this.props.profile
        let newState = Object.assign({}, oldState)

        switch (field) {
            case 'name':
                newState.name = value
                localStorage.setItem("lustars_user_name", value)
                this.props.updateUserProfileTextField(newState)
                return
            case 'lastName':
                newState.lastName = value
                this.props.updateUserProfileTextField(newState)
                return
            case 'mood':
                newState.feelInMood = value
                this.props.updateUserProfileTextField(newState)
                return
            case 'dateOfBirth':
                newState.dateOfBirth = value
                this.setState({
                    isDatePickerClicked: false
                })
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
            case 'howOftenDoSport':
                newState.howOftenDoSport = value
                this.props.updateUserProfileTextField(newState)
                return
            case 'addLanguage':
                let includesLanguage = false
                newState.languages.forEach(language => {
                    if (language.name === value) {
                        includesLanguage = true
                    }
                })

                if (!includesLanguage) {
                    newState.languages.push({ name: value })
                    this.props.addUserCountryLanguage(newState)
                }
                return
            case 'deleteLanguage':
                let index = 0
                oldState.languages.forEach( language => {
                    if (language.name === value) {
                        let stateWithLanguages = Object.assign({}, oldState)
                        stateWithLanguages.languages.splice(index, 1)
                        this.props.deleteUserCountryLanguage(stateWithLanguages)
                    }

                    index++
                })
                this.updateUserProfile()
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
            case 'biography':
                newState.biographyAndInterests = value
                this.props.updateUserProfileTextField(newState)
                return
            case 'height':
                newState.height = parseInt(value)
                this.props.updateUserProfileTextField(newState)
                clearTimeout(this.state.timerIdentified.height)
                this.setState({
                    timerIdentified: { height: setTimeout(() => { this.updateUserProfileWithDelay(field) }, 3000) }
                })
                return
            case 'weight':
                newState.weight = parseInt(value)
                this.props.updateUserProfileTextField(newState)
                clearTimeout(this.state.timerIdentified.weight)
                this.setState({
                    timerIdentified: { weight: setTimeout(() => { this.updateUserProfileWithDelay(field) }, 3000) }
                })
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

    updateProfileBooleanField = async (field, newValue) => {
        let oldState = this.props.profile
        let newState = Object.assign({}, oldState)

        switch (field) {
            case 'howOftenSmoke':
                newState.doingSport = newValue
                await this.props.updateUserProfileBoleanField(newState)
                await this.updateUserProfile()
                return
            case 'haveKids':
                newState.haveKids = newValue
                await this.props.updateUserProfileBoleanField(newState)
                await this.updateUserProfile()
                return
            case 'wantToHaveKids':
                newState.wantKids = newValue
                await this.props.updateUserProfileBoleanField(newState)
                await this.updateUserProfile()
                return
            case 'drinkAlcohol':
                newState.drinkAlcohol = newValue
                await this.props.updateUserProfileBoleanField(newState)
                await this.updateUserProfile()
                return
            case 'smoker':
                newState.smoker = newValue
                await this.props.updateUserProfileBoleanField(newState)
                await this.updateUserProfile()
                return
            case 'doSport':
                newState.doSport = newValue
                await this.props.updateUserProfileBoleanField(newState)
                await this.updateUserProfile()
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
                    <tbody>
                        <tr>
                            <th><label htmlFor="first-name">First name:</label></th>
                            <th>
                                <span
                                    style={{ display:!this.state.editFirstName ? "block" : "none" }}
                                    onClick={ () => this.setState({ editFirstName: true })}>{ this.props.profile.name }
                                </span>
                                <input
                                    id="first-name"
                                    type="text"
                                    placeholder="First Name"
                                    style={{ display:this.state.editFirstName ? "block" : "none" }}
                                    className="text-input-profile-about"
                                    defaultValue={ this.props.profile.name }
                                    onChange={(e) => this.updateProfileTextField("name", e.target.value)}
                                    onBlur={ () => this.updateUserProfile("name") }
                                />
                            </th>
                        </tr>
                        <tr>
                            <td><label htmlFor="last-name">Last name:</label></td>
                            <td>
                                <span
                                    style={{ display:!this.state.editLastName ? "block" : "none" }}
                                    onClick={ () => this.setState({ editLastName: true })}>{ this.props.profile.lastName }
                                </span>
                                <input
                                    id="last-name"
                                    type="text"
                                    placeholder="Last Name"
                                    style={{ display:this.state.editLastName ? "block" : "none" }}
                                    className="text-input-profile-about"
                                    defaultValue={ this.props.profile.lastName }
                                    onChange={(e) => this.updateProfileTextField("lastName", e.target.value)}
                                    onBlur={ () => this.updateUserProfile("lastName")}
                                />
                            </td>
                        </tr>
                        <tr>
                            <td><label htmlFor="mood">Mood:</label></td>
                            <td>
                                <span
                                    style={{ display:!this.state.editMood ? "block" : "none" }}
                                    onClick={ () => this.setState({ editMood: true })}>{ this.props.profile.feelInMood }
                                </span>
                                <input
                                    id="mood"
                                    type="text"
                                    placeholder="Type how you feel"
                                    style={{ display:this.state.editMood ? "block" : "none" }}
                                    className="text-input-profile-about"
                                    defaultValue={ this.props.profile.feelInMood }
                                    onChange={(e) => this.updateProfileTextField("mood", e.target.value)}
                                    onBlur={ () => this.updateUserProfile("mood")}
                                />
                            </td>
                        </tr>
                        <tr>
                            <td><label htmlFor="date-of-birth">Birth date:</label></td>
                            <td>
                                {
                                    this.props.profile.dateOfBirth !== null && this.props.profile.dateOfBirth !== undefined
                                    ?   <DatePicker
                                            id="date-of-birth"
                                            className="text-input-profile-about"
                                            showPopperArrow={false}
                                            selected=
                                            {   this.props.profile.dateOfBirth !== null &&
                                                this.props.profile.dateOfBirth !== undefined &&
                                                this.props.profile.dateOfBirth !== "0001-01-01T00:00:00"
                                                    ? new Date(this.props.profile.dateOfBirth)
                                                    : new Date()
                                            }
                                            onChange={(date) => this.updateBirtDayDate(date)}
                                        />
                                    :   <DatePicker
                                            id="date-of-birth"
                                            className="text-input-profile-about"
                                            showPopperArrow={false}
                                            selected={ new Date() }
                                            onChange={(date) => this.updateBirtDayDate(date)}
                                        />
                                }
                            </td>
                        </tr>
                        <tr>
                            <td><label htmlFor="gender">Gender:</label></td>
                            <td>
                                <span
                                    style={{ display:!this.state.editGender ? "block" : "none" }}
                                    onClick={ () => this.setState({ editGender: true })}>{ this.props.profile.gender }
                                </span>
                                <select
                                    id="gender"
                                    style={{ display:this.state.editGender ? "block" : "none" }}
                                    className="text-input-profile-about"
                                    value={ this.props.profile.gender }
                                    onChange={(e) => this.updateProfileTextField("gender", e.target.value)}
                                    onBlur={ () => this.updateUserProfile("gender")}>
                                    {
                                        this.props.profile.gender === null || this.props.profile.gender === undefined
                                            ? <option selected="selected">Select Gender</option>
                                            : null
                                    }
                                    <option value="Man">Male</option>
                                    <option value="Female">Female</option>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td><label htmlFor="merital-status">Merial status:</label></td>
                            <td>
                                <span
                                    style={{ display:!this.state.editMeritalStatus ? "block" : "none" }}
                                    onClick={ () => this.setState({ editMeritalStatus: true })}>{ this.props.profile.meritalStatus }
                                </span>
                                <select
                                    id="merital-status"
                                    style={{ display:this.state.editMeritalStatus ? "block" : "none" }}
                                    className="text-input-profile-about"
                                    value={ this.props.profile.meritalStatus }
                                    onChange={(e) => this.updateProfileTextField("meritalStatus", e.target.value)}
                                    onBlur={ () => this.updateUserProfile("meritalStatus")}>
                                    {
                                        this.props.profile.meritalStatus === null || this.props.profile.meritalStatus === undefined
                                            ? <option selected="selected">Select Marital Status</option>
                                            : null
                                    }
                                    <option value="Single">Single</option>
                                    <option value="Meried">Meried</option>
                                    <option value="Divorced">Divorced</option>
                                    <option value="Complicated">Complicated</option>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td><label htmlFor="looking-for">Looking for:</label></td>
                            <td>
                                <span
                                    style={{ display:!this.state.editLookingFor ? "block" : "none" }}
                                    onClick={ () => this.setState({ editLookingFor: true })}>{ this.props.profile.lookingFor }
                                </span>
                                <select
                                    id="looking-for"
                                    style={{ display:this.state.editLookingFor ? "block" : "none" }}
                                    className="text-input-profile-about"
                                    value={ this.props.profile.lookingFor }
                                    onChange={(e) => this.updateProfileTextField("lookingFor", e.target.value)}
                                    onBlur={ () => this.updateUserProfile("lookingFor")}>
                                    {
                                        this.props.profile.lookingFor === null || this.props.profile.lookingFor === undefined
                                            ? <option>Select Interests</option>
                                            : null
                                    }
                                    <option value="Man">Male</option>
                                    <option value="Female">Female</option>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td><hr/></td>
                            <td><hr/></td>
                        </tr>
                        <tr>
                            <td><label htmlFor="languages">Languages:</label></td>
                            <td>
                                <div className="country-languages-container">
                                    {
                                        this.props.profile.languages !== null && this.props.profile.languages !== undefined
                                            ?   this.props.profile.languages.map((language, index) => {
                                                    return (
                                                        <div
                                                            id={ language.name }
                                                            key={ index }
                                                            className="country-language"
                                                            onClick= {(e) => this.updateProfileTextField("deleteLanguage", e.target.id)}>
                                                            { language.name } &#10008;
                                                        </div>
                                                    )
                                                })
                                            :   null
                                    }
                                    <div
                                        style={{ display:!this.state.editAddLanguage ? "block" : "none" }}
                                        className="country-language"
                                        onClick={ () => this.setState({ editAddLanguage: true })}>
                                        Add more...
                                    </div>
                                </div>
                                <select
                                    id="languages"
                                    style={{ display:this.state.editAddLanguage ? "block" : "none" }}
                                    className="text-input-profile-about"
                                    onChange={(e) => this.updateProfileTextField("addLanguage", e.target.value)}
                                    onBlur={ () => this.updateUserProfile("addLanguage")}>
                                    <option selected="selected" disabled>Add language</option>
                                    {
                                        countryLanguages.map((language, index) => {
                                            return (
                                                <option
                                                    key={ index }
                                                    value={ language }>
                                                        { language }
                                                </option>
                                            )
                                        })
                                    }
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td><hr/></td>
                            <td><hr/></td>
                        </tr>
                        <tr>
                            <td><label htmlFor="education-degree">Education degree:</label></td>
                            <td>
                                <span
                                    style={{ display:!this.state.editEducationDegree ? "block" : "none" }}
                                    onClick={ () => this.setState({ editEducationDegree: true })}>{ this.props.profile.educationDegree }
                                </span>
                                <select
                                    id="education-degree"
                                    className="text-input-profile-about"
                                    style={{ display:this.state.editEducationDegree ? "block" : "none" }}
                                    value={ this.props.profile.educationDegree }
                                    onChange={(e) => this.updateProfileTextField("educationDegree", e.target.value)}
                                    onBlur={ () => this.updateUserProfile("educationDegree")}>
                                    {
                                        this.props.profile.educationDegree === null || this.props.profile.educationDegree === undefined
                                            ? <option selected="selected">Select Education degree</option>
                                            : null
                                    }
                                    <option value="School">School</option>
                                    <option value="Bachelor">Bachelor</option>
                                    <option value="Master">Master</option>
                                    <option value="PhD">PhD</option>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td><label htmlFor="university">University:</label></td>
                            <td>
                                <span
                                    style={{ display:!this.state.editUniversity ? "block" : "none" }}
                                    onClick={ () => this.setState({ editUniversity: true })}>{ this.props.profile.university }
                                </span>
                                <input
                                    id="university"
                                    type="text"
                                    placeholder="Name of University..."
                                    style={{ display:this.state.editUniversity ? "block" : "none" }}
                                    className="text-input-profile-about"
                                    defaultValue={ this.props.profile.university }
                                    onChange={(e) => this.updateProfileTextField("university", e.target.value)}
                                    onBlur={ () => this.updateUserProfile("university")}
                                />
                            </td>
                        </tr>
                        <tr>
                            <td><label htmlFor="work">Work:</label></td>
                            <td>
                                <span
                                    style={{ display:!this.state.editWork ? "block" : "none" }}
                                    onClick={ () => this.setState({ editWork: true })}>{ this.props.profile.work }
                                </span>
                                <input
                                    id="work"
                                    type="text"
                                    placeholder="Work"
                                    style={{ display:this.state.editWork ? "block" : "none" }}
                                    className="text-input-profile-about"
                                    defaultValue={ this.props.profile.work }
                                    onChange={(e) => this.updateProfileTextField("work", e.target.value)}
                                    onBlur={ () => this.updateUserProfile("work")}
                                />
                            </td>
                        </tr>
                        <tr>
                            <td><label htmlFor="biography">Few words about you and your interests:</label></td>
                            <td>
                                <span
                                    style={{ display:!this.state.editBiography ? "block" : "none" }}
                                    onClick={ () => this.setState({ editBiography: true })}>{ this.props.profile.biographyAndInterests }
                                </span>
                                <textarea
                                    id="biography"
                                    rows="4" cols="28"
                                    placeholder="Type something that describes you."
                                    style={{ display:this.state.editBiography ? "block" : "none" }}
                                    className="text-input-profile-about"
                                    defaultValue={ this.props.profile.biographyAndInterests }
                                    onChange={(e) => this.updateProfileTextField("biography", e.target.value)}
                                    onBlur={ () => this.updateUserProfile("biography") }>
                                </textarea>
                            </td>
                        </tr>
                        <tr>
                            <td><hr/></td>
                            <td><hr/></td>
                        </tr>
                        <tr>
                            <td><label htmlFor="height">Height/sm:</label></td>
                            <td>
                                <span
                                    style={{ display:!this.state.editHeight ? "block" : "none" }}
                                    onClick={ () => this.setState({ editHeight: true })}>{ this.props.profile.height }
                                </span>
                                <span style={{ display:this.state.editHeight ? "block" : "none" }}>
                                        <NumberAdjusterInput
                                            id="height"
                                            numberInput={ this.props.profile.height }
                                            numberResult={ (value) => this.updateProfileTextField("height", value) }
                                        />
                                </span>
                            </td>
                        </tr>
                        <tr>
                            <td><label htmlFor="weight">Weight/kg:</label></td>
                            <td>
                                <span
                                    style={{ display:!this.state.editWeight ? "block" : "none" }}
                                    onClick={ () => this.setState({ editWeight: true })}>{ this.props.profile.weight }
                                </span>
                                <span style={{ display:this.state.editWeight ? "block" : "none" }}>
                                    <NumberAdjusterInput
                                        id="weight"
                                        numberInput={ this.props.profile.weight }
                                        numberResult={ (value) => this.updateProfileTextField("weight", value) }
                                    />
                                </span>
                            </td>
                        </tr>
                        <tr>
                            <td><label htmlFor="figure">Figure:&nbsp;</label></td>
                            <td>
                                <span
                                    style={{ display:!this.state.editFigure ? "block" : "none" }}
                                    onClick={ () => this.setState({ editFigure: true })}>{ this.props.profile.figure }
                                </span>
                                <select
                                    id="figure"
                                    style={{ display:this.state.editFigure ? "block" : "none" }}
                                    className="text-input-profile-about"
                                    value={ this.props.profile.figure }
                                    onChange={(e) => this.updateProfileTextField("figure", e.target.value)}
                                    onBlur={ () => this.updateUserProfile("figure")}>
                                    {
                                        this.props.profile.figure === null || this.props.profile.figure === undefined
                                            ? <option selected="selected">Select your figure</option>
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
                            <td><label htmlFor="kids">Kids:</label></td>
                            <td>
                                <YesNoInputField
                                    id="kids"
                                    value={ this.props.profile.haveKids }
                                    switchValue={(newValue) => this.updateProfileBooleanField('haveKids', newValue)}
                                />
                            </td>
                        </tr>
                        <tr>
                            <td><label htmlFor="want-to-have-kids">Want to have kids:</label></td>
                            <td>
                                <YesNoInputField
                                    id="want-to-have-kids"
                                    value={ this.props.profile.wantKids }
                                    switchValue={(newValue) => this.updateProfileBooleanField('wantToHaveKids', newValue)}
                                />
                            </td>
                        </tr>
                        <tr>
                            <td><label htmlFor="do-sport">Do you sport:</label></td>
                            <td>
                                <YesNoInputField
                                    id="do-sport"
                                    value={ this.props.profile.doSport }
                                    switchValue={(newValue) => this.updateProfileBooleanField('doSport', newValue)}
                                />
                            </td>
                        </tr>
                        <tr>
                            <td><label htmlFor="drink-alcohol">Drink alcohol:</label></td>
                            <td>
                                <YesNoInputField
                                    id="drink-alcohol"
                                    value={ this.props.profile.drinkAlcohol }
                                    switchValue={(newValue) => this.updateProfileBooleanField('drinkAlcohol', newValue)}
                                />
                            </td>
                        </tr>
                        <tr>
                            <td><label htmlFor="smoker">Smoker:</label></td>
                            <td>
                                <YesNoInputField
                                    id="smoker"
                                    value={ this.props.profile.smoker }
                                    switchValue={(newValue) => this.updateProfileBooleanField('smoker', newValue)}
                                />
                            </td>
                        </tr>
                            {
                                this.props.profile.doSport !== null && this.props.profile.doSport !== undefined && this.props.profile.doSport === true
                                    ?   <tr>
                                            <td><label htmlFor="how-often-do-port">How often do sport:&nbsp;</label></td>
                                            <td>
                                                <span
                                                    style={{ display:!this.state.editHowOftenDoSport ? "block" : "none" }}
                                                    onClick={ () => this.setState({ editHowOftenDoSport: true })}>{ this.props.profile.howOftenDoSport }
                                                </span>
                                                <select
                                                    id="how-often-do-port"
                                                    style={{ display:this.state.editHowOftenDoSport ? "block" : "none" }}
                                                    className="text-input-profile-about"
                                                    value={ this.props.profile.howOftenDoSport }
                                                    onChange={(e) => this.updateProfileTextField("howOftenDoSport", e.target.value)}
                                                    onBlur={ () => this.updateUserProfile("howOftenDoSport")}>
                                                    {
                                                        this.props.profile.howOftenDoSport === null || this.props.profile.howOftenDoSport === undefined
                                                            ?   <option selected="selected">Sport activity</option>
                                                            :   null
                                                    }
                                                    <option value="Once a week">Once a week</option>
                                                    <option value="1-3 times per week">1-3 times per week</option>
                                                    <option value="Every day">Every day</option>
                                                    <option value="Few times per day">Few times per day</option>
                                                </select>
                                            </td>
                                        </tr>
                                    : null
                            }
                            {
                                this.props.profile.drinkAlcohol !== null && this.props.profile.drinkAlcohol !== undefined && this.props.profile.drinkAlcohol === true
                                    ?   <tr>
                                            <td><label htmlFor="how-often-drin-alcohol">How often Drink:&nbsp;</label></td>
                                            <td>
                                                <span
                                                    style={{ display:!this.state.editHowOftenDrinkAlcohol ? "block" : "none" }}
                                                    onClick={ () => this.setState({ editHowOftenDrinkAlcohol: true })}>{ this.props.profile.howOftenDrinkAlcohol }
                                                </span>
                                                <select
                                                    id="how-often-drin-alcohol"
                                                    style={{ display:this.state.editHowOftenDrinkAlcohol ? "block" : "none" }}
                                                    className="text-input-profile-about"
                                                    value={ this.props.profile.howOftenDrinkAlcohol }
                                                    onChange={(e) => this.updateProfileTextField("howOftenDrinkAlcohol", e.target.value)}
                                                    onBlur={ () => this.updateUserProfile("howOftenDrinkAlcohol") }>
                                                    {
                                                        this.props.profile.howOftenDrinkAlcohol === null || this.props.profile.howOftenDrinkAlcohol === undefined
                                                            ?   <option selected="selected">Drinking frequency</option>
                                                            :   null
                                                    }
                                                    <option value="Rarely">Rarely</option>
                                                    <option value="Often">Often</option>
                                                    <option value="When celebrate">When celebrate</option>
                                                    <option value="Every night">Every night</option>
                                                    <option value="All the time">All the time</option>
                                                </select>
                                            </td>
                                        </tr>
                                    : null
                            }
                            {
                                this.props.profile.smoker !== null && this.props.profile.smoker !== undefined && this.props.profile.smoker === true
                                    ?   <tr>
                                            <td><label htmlFor="how-often-smoke">How often smoke:&nbsp;</label></td>
                                            <td>
                                                <span
                                                    style={{ display:!this.state.editHowOftenSmoke ? "block" : "none" }}
                                                    onClick={ () => this.setState({ editHowOftenSmoke: true })}>{ this.props.profile.howOftenSmoke }
                                                </span>
                                                <select
                                                    id="how-often-smoke"
                                                    style={{ display:this.state.editHowOftenSmoke ? "block" : "none" }}
                                                    className="text-input-profile-about"
                                                    defaultValue={ this.props.profile.howOftenSmoke }
                                                    onChange={(e) => this.updateProfileTextField("howOftenSmoke", e.target.value)}
                                                    onBlur={ () => this.updateUserProfile("howOftenSmoke")}>
                                                    {
                                                        this.props.profile.howOftenSmoke === null || this.props.profile.howOftenSmoke === undefined
                                                            ?   <option selected="selected">Smoking frequency</option>
                                                            :   null
                                                    }
                                                    <option value="Rarely">Rarely</option>
                                                    <option value="When drink">When drink</option>
                                                    <option value="Often">Often</option>
                                                    <option value="Very often">Very often</option>
                                                    <option value="All the time">All the time</option>
                                                </select>
                                            </td>
                                        </tr>
                                    :   null
                            }
                    </tbody>
                </table>
            </div>
        )
    }
}

const mapStateToProps = state => {
    return {
        profile: state.myProfile.currentUserProfileDetails,
        isLoading: state.myProfile.isLoading,
        error: state.myProfile.error
    }
}

const mapDispatchToProps = dispatch => {
    return {
        editMyUserProfileDetails: (userProfileDetails) => dispatch(editMyUserProfileDetails(userProfileDetails)),
        deleteUserCountryLanguage: (newValue) => dispatch(deleteUserCountryLanguage(newValue)),
        addUserCountryLanguage: (newValue) => dispatch(addUserCountryLanguage(newValue)),
        updateUserProfileBoleanField: (newValue) => dispatch(updateUserProfileBoleanField(newValue)),
        updateUserProfileTextField: (newValue) => dispatch(updateUserProfileTextField(newValue))
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(MyProfileAboutMe)