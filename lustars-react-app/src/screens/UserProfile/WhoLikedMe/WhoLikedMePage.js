import React, { Component } from 'react'
import { connect } from 'react-redux'
import { api } from '../../../constants/endpoints'
import { push, goBack } from 'connected-react-router'
import { getAllUsersWhoLikedMe } from '../../../store/actions/myProfileActions'
import './WhoLikedMePage.css'

class WhoLikedMePage extends Component {

    componentWillMount() {
        this.props.getAllUsersWhoLikedMe()
    }

    retrieveDetailsForProfile(event) {
        this.props.push(`/profile/${event.target.id}`)
    }

    render() {
       let whoLikedMeList = this.props.whoLikedMe !== undefined && this.props.whoLikedMe !== null
            ?   this.props.whoLikedMe.map((userProfile, index) => {
                    return (
                        <label
                            key={ index }
                            id={ userProfile.id }
                            onClick={ this.retrieveDetailsForProfile.bind(this) }
                        >
                            <div className="wholikedme-profile-image-container">
                                <div>{ userProfile.distance } away</div>
                                    <img
                                        id={ userProfile.id }
                                        className="wholikedme-profile-in-distance-image"
                                        src={ api.imageAPI + userProfile.avatarImage }
                                        alt=""
                                    />
                                    <span className="wholikedme-profile-nearby-camera-img-number">&#128247; { userProfile.countImages }</span>
                                    <div id={ userProfile.id } className="wholikedme-profile-name-and-location">
                                    <div>{ userProfile.nameAndAge }</div>
                                    <div>{ userProfile.location }</div>
                                </div>
                            </div>
                        </label>
                    )
                })
            :   null

        return (
            <div className="who-liked-me-page-container">
                <h1>Who liked me</h1>
                { whoLikedMeList }
            </div>
        )
    }
}

const mapStateToProps = state => {
    return {
        whoLikedMe: state.myProfile.userProfilesWhoLikedMe
    }
}

const mapDispatchToProps = dispatch => {
    return {
        getAllUsersWhoLikedMe: () => dispatch(getAllUsersWhoLikedMe()),

        // Navigation
        goBack: () => dispatch(goBack()),
        push: (url) => dispatch(push(url))
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(WhoLikedMePage)