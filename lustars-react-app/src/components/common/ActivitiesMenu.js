import React, { Component } from 'react'
import { connect } from "react-redux"
import { api } from '../../constants/endpoints'
import { getUserProfileShortPreviewData } from '../../store/actions/profileActions'

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
                        <span>{ this.props.profilePreviewData.geoLocation.city } { this.props.profilePreviewData.geoLocation.country } </span>
                        <br/>
                        <span>Creadits: { this.props.profilePreviewData.credits } </span>
                        <span>Super Likes: { this.props.profilePreviewData.superlikes }</span>
                    </div>
                    <div class="vertical-line activities-attributes"></div>
                    <div className="activities-attributes">
                        <span><a href="#" className="activities-menu-link">People nearby</a></span>
                        <br/>
                        <span><a href="#" className="activities-menu-link">Messages</a></span>
                        <br/>
                        <span><a href="#" className="activities-menu-link">Matched</a></span>
                        <br/>
                        <span><a href="#" className="activities-menu-link">Liked you</a></span>
                        <br/>
                        <span><a href="#" className="activities-menu-link">Visitors</a></span>
                    </div>
                    <div class="vertical-line activities-attributes"></div>
                </div>
            :   null

        return (
            <div className="activities-menu">
                { userProfileShortDataAndLinks }
            </div>
        )
    }
}

const mapStateToProps = state => {
    return {
        profilePreviewData: state.profile.UserProfileShortPreviewData,
    }
}

const mapDispatchToProps = dispatch => {
    return {
        getUserProfileShortPreviewData: () => dispatch(getUserProfileShortPreviewData()),
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(ActiitiesMenu)