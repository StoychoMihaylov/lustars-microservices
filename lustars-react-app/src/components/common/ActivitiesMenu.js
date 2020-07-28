import React, { Component } from 'react'
import { connect } from "react-redux"
import { push, goBack } from "connected-react-router"
import { api } from '../../constants/endpoints'
import { getUserProfileShortPreviewData } from '../../store/actions/myProfileActions'
import '../../styles/components/common/ActivitiesMenu.css'

class ActiitiesMenu extends Component {
    constructor(props) {
        super(props)
    }

    componentDidMount() {
        this.props.getUserProfileShortPreviewData()
    }

    render() {
        let userProfileShortDataAndLinks = this.props.profilePreviewData !== undefined && this.props.profilePreviewData !== null
            ?   <div >
                    <div className="activities-attributes">
                        <img
                            className="user-profile-short-data-avatar"
                            src={ api.imageAPI + this.props.profilePreviewData.avatarImage }
                            alt=""
                        />
                    </div>
                    <div className="activities-attributes">
                        <span>{ this.props.profilePreviewData.name } </span>
                        <span>{ this.props.profilePreviewData.lastName }</span>
                        <br/>
                        {
                            this.props.profilePreviewData.geoLocation !== undefined && this.props.profilePreviewData.geoLocation !== null
                            ?   <span>{ this.props.profilePreviewData.geoLocation.city } { this.props.profilePreviewData.geoLocation.country } </span>
                            :   null
                        }
                        <br/>
                        <span>Creadits: { this.props.profilePreviewData.credits } </span>
                        <span>Super Likes: { this.props.profilePreviewData.superlikes }</span>
                    </div>
                    <hr className="hr-activities-menu" />
                    <div className="activities-attributes">
                        <span onClick={() => this.props.push('/people-nearby') } className="activities-menu-link">People nearby</span>
                        <br/>
                        <span className="activities-menu-link">Messages</span>
                        <br/>
                        <span className="activities-menu-link">Matched</span>
                        <br/>
                        <span className="activities-menu-link">Liked you</span>
                        <br/>
                        <span className="activities-menu-link">Visitors</span>
                    </div>
                </div>
            :   null

        return (
            <section className="activities-menu">
                { userProfileShortDataAndLinks }
            </section>
        )
    }
}

const mapStateToProps = state => {
    return {
        profilePreviewData: state.myProfile.UserProfileShortPreviewData,
    }
}

const mapDispatchToProps = dispatch => {
    return {
        getUserProfileShortPreviewData: () => dispatch(getUserProfileShortPreviewData()),

        // Navigation
        goBack: () => dispatch(goBack()),
        push: (url) => dispatch(push(url)),
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(ActiitiesMenu)