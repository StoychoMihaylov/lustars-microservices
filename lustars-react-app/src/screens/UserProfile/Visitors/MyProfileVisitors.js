import React, { Component } from 'react'
import { connect } from 'react-redux'
import { api } from '../../../constants/endpoints'
import { push, goBack } from 'connected-react-router'
import { getMyProfileVisitors } from '../../../store/actions/myProfileActions'
import './MyProfileVisitors.css'

class MyProfileVisitors extends Component {

    componentWillMount() {
        this.props.getMyProfileVisitors()
    }

    retrieveDetailsForProfile(event) {
        this.props.push(`/profile/${event.target.id}`)
    }

    render() {
       let userProfileVisitorsList = this.props.userProfileVisitors !== undefined && this.props.userProfileVisitors !== null
            ?   this.props.userProfileVisitors.map((userProfile, index) => {
                    return (
                        <label
                            key={ index }
                            id={ userProfile.id }
                            onClick={ this.retrieveDetailsForProfile.bind(this) }
                        >
                            <div className="who-visited-me-profile-image-container">
                                <div>{ userProfile.distance } away</div>
                                    <img
                                        id={ userProfile.id }
                                        className="who-visited-me-profile-in-distance-image"
                                        src={ api.imageAPI + userProfile.avatarImage }
                                        alt=""
                                    />
                                    <span className="who-visited-me-profile-camera-img-number"><span>&#128247;</span> { userProfile.countImages }</span>
                                    <div id={ userProfile.id } className="who-visited-me-profile-name-and-location">
                                    <div>{ userProfile.nameAndAge }</div>
                                    <div>{ userProfile.location }</div>
                                </div>
                            </div>
                        </label>
                    )
                })
            :   null

        return (
            <div className="who-visited-me-page-container">
                <h1>Who visited me</h1>
                { userProfileVisitorsList }
            </div>
        )
    }
}

const mapStateToProps = state => {
    return {
        userProfileVisitors: state.myProfile.userProfileVisitors
    }
}

const mapDispatchToProps = dispatch => {
    return {
        getMyProfileVisitors: () => dispatch(getMyProfileVisitors()),

        // Navigation
        goBack: () => dispatch(goBack()),
        push: (url) => dispatch(push(url))
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(MyProfileVisitors)