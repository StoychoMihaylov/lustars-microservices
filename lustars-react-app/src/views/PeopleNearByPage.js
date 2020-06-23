import React, { Component } from 'react'
import { connect } from "react-redux"
import { getPeopleNearby } from '../store/actions/peopleNearbyActions'

class PeopleNearbyPage extends Component {

    componentDidMount() {
      this.props.getPeopleNearby()
    }

    render() {
      console.log(this.props.peopleNearby)
        return (
            <div>
               
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