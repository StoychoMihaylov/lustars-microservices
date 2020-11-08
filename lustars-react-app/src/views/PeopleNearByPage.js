import React, { Component } from 'react'
import { connect } from "react-redux"
import { push } from "connected-react-router"
import { getPeopleNearby } from '../store/actions/peopleNearbyActions'
import { api } from '../constants/endpoints'
import '../styles/views/PeopleNearbyPage.css'

class PeopleNearbyPage extends Component {
  
  componentDidMount() {
    this.props.getPeopleNearby()
  }

  retrieveDetailsForProfile(event) {
    this.props.push(`/profile/${event.target.id}`)
  }

  render() {
    let userProfiles = this.props.peopleNearby !== undefined && this.props.peopleNearby !== null
      ? this.props.peopleNearby.map((userProfile, index) => {
          return (
            <label
              key={ index }
              id={ userProfile.id }
              onClick={ this.retrieveDetailsForProfile.bind(this) }
            >
              <div className="profile-image-container">
                <div>{ userProfile.distance } away</div>
                <img
                    id={ userProfile.id }
                    className="profile-in-distance-image"
                    src={ api.imageAPI + userProfile.avatarImage }
                    alt=""
                />
                <span className="profile-nearby-camera-img-number">&#128247; { userProfile.countImages }</span>
                <div id={ userProfile.id } className="profile-name-and-location">
                  <div>{ userProfile.nameAndAge }</div>
                  <div>{ userProfile.location }</div>
                </div>
              </div>
            </label>
          )
        })
      : null

      return (
          <div className="people-nearby-page-container">
            <h1>People nearby</h1>
            { userProfiles }
          </div>
      )
  }
}

const mapStateToProps = state => {
  return {
    peopleNearby: state.peopleNearby.peopleNearby,
    isLoading: state.peopleNearby.isLoading,
    error: state.peopleNearby.error
  }
}

const mapDispatchToProps = dispatch => {
  return {
    getPeopleNearby: () => dispatch(getPeopleNearby()),
    push: (url) => dispatch(push(url))
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(PeopleNearbyPage)