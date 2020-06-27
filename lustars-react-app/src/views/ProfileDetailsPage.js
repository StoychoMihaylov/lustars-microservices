import React, { Component } from 'react'
import { connect } from "react-redux"
import { api } from '../constants/endpoints'
import { getMyUserProfileDetails } from '../store/actions/myProfileActions'

class ProfileDetailsPage extends Component {

    componentWillMount() {
        this.props.getMyUserProfileDetails(this.props.match.params.id) // Takes the id from url
    }

    render() {
        let profile = this.props.profile

        let displayProfile =
            <div>
                <img className="avatar-image" src={ api.imageAPI + profile.avatarImage } alt="" />
                <div>Full name: { profile.name } { profile.lastName }</div>
                <div>Biography: { profile.biography }</div>
                <div>Drink: { profile.drinkAlcohol }</div>
                <div>Education: { profile.educationDegree }</div>
                <div>Feel in mood: { profile.feelInMood }</div>
                <div>Figure: { profile.figure }</div>
                <div>From: { profile.fromCity }, { profile.fromCountry }</div>
                <div>Gernder: { profile.gender }</div>
                <div>Kids: { profile.haveKids }</div>
                <div>Height:{ profile.height }</div>
                <div>howOftenDrinkAlcohol: { profile.howOftenDrinkAlcohol }</div>
                <div>howOftenSmoke: { profile.howOftenSmoke }</div>
                <div>
                    Languages:

                    {
                        profile.languages !== undefined
                            ?   profile.languages.map((language, index) => {
                                    return (
                                        <span key={index} >{ language }</span>
                                    )
                                })
                            :   null
                    }
                </div>
                <div>Looking for: { profile.lookingFor }</div>
                <div>Merital status: { profile.meritalStatus }</div>
                <div>Partner age: from { profile.partnerAgeRangeFrom } to { profile.partnerAgeRangeTo }</div>
                <div>Parner drink: { profile.partnerDrinkAlcohol }</div>
                <div>Partner figure: { profile.partnerFigure }</div>
                <div>Partner has kids: { profile.partnerHaveKids }</div>
                <div>Partner Smoke: { profile.partnerSmoke }</div>
                <div>University: { profile.university}</div>
                <div>Want kids: { profile.wantToHaveKids }</div>
                <div>weight: { profile.weight }</div>
                <div>Work: { profile.work } </div>
            </div>

        console.log(profile)
        return (
            <div>
                {
                    this.props.isLoading == false
                        ?   displayProfile
                        :   null
                }
            </div>
        )
    }
}

const mapStateToProps = state => {
    return {
        profile: state.myProfile.userProfileDetails,
        isLoading: state.myProfile.isLoading,
        error: state.myProfile.error
    }
}

const mapDispatchToProps = dispatch => {
    return {
        getMyUserProfileDetails: (id) => dispatch(getMyUserProfileDetails(id)),
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(ProfileDetailsPage)
