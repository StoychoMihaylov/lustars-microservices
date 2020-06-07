import React, { Component } from 'react'
import Avatar from '../profile/Avatar'

class ActiitiesMenu extends Component {
    render() {
        return (
            <div>
               <div>
                   <Avatar class="main-page-avatar" />
                   <a href="#">People nearby</a>
                   <a href="#">Messages</a>
                   <a href="#">Matched</a>
                   <a href="#">Liked you</a>
                   <a href="#">Visitors</a>
                </div>
            </div>
        )
    }
}

export default ActiitiesMenu