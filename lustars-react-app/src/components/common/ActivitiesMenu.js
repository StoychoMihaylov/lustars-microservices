import React, { Component } from 'react'
import '../../styles/components/common/ActivitiesMenu.css'

class ActiitiesMenu extends Component {
    render() {
        return (
            <div className="activities-menu">
                <div>
                    <a href="#" className="activities-menu-link">People nearby</a>
                    <br/>
                    <a href="#" className="activities-menu-link">Messages</a>
                    <br/>
                    <a href="#" className="activities-menu-link">Matched</a>
                    <br/>
                    <a href="#" className="activities-menu-link">Liked you</a>
                    <br/>
                    <a href="#" className="activities-menu-link">Visitors</a>
                </div>
            </div>
        )
    }
}

export default ActiitiesMenu