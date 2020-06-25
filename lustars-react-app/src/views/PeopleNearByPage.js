import React, { Component } from 'react'
import { connect } from "react-redux"
import { getPeopleNearby } from '../store/actions/peopleNearbyActions'
import { api } from '../constants/endpoints'
import '../styles/views/PeopleNearbyPage.css'

class PeopleNearbyPage extends Component {

    componentDidMount() {
      this.props.getPeopleNearby()
    }

    render() {
      console.log(this.props.peopleNearby)
        return (
            <div>
              {
                this.props.peopleNearby !== undefined && this.props.peopleNearby !== null
                  ? this.props.peopleNearby.map((userProfile, index) => {
                      return (
                        <label key={ index } id={ userProfile.id }>
                          <div className="profile-image-container">
                            <div>{ userProfile.distance } away</div>
                            <img
                                className="profile-in-distance-image"
                                src={ api.imageAPI + userProfile.avatarImage }
                                alt=""
                            />
                            <div className="profile-name-and-location">
                              <div>{ userProfile.nameAndAge }</div>
                              <div>{ userProfile.location }</div>
                            </div>
                          </div>
                        </label>
                      )
                    })
                  : null
              }
            </div>
        )
    }
}

const mapStateToProps = state => {
  return {
    peopleNearby: state.peopleNearby.peopleNearby,
    isLoading: state.profile.isLoading,
    error: state.profile.error
  }
}

const mapDispatchToProps = dispatch => {
  return {
    getPeopleNearby: () => dispatch(getPeopleNearby())
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(PeopleNearbyPage)