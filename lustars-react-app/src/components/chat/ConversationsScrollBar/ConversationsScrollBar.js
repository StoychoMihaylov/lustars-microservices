import React from 'react'
import { connect } from 'react-redux'
import { api } from '../../../constants/endpoints'
import './ConversationsScrollBar.css'

class ConversationsScrollBar extends React.PureComponent {
    render() {
        console.log(this.props.chatConversations)
        return (
            <div className="conversations-scroll-bar-container" >
                <div className="conversations-container">

                    {
                        this.props.chatConversations.map((conversation, index) => {
                            return(
                                <div className="individual-conversation-box">
                                    <img src={api.imageAPI + conversation.corresponderAvatarImage} className="conversation-profile-img" alt="" />
                                    <span className="conversation-profile-name">{ conversation.corresponderNames }</span>
                                </div>
                            )
                        })
                    }
                </div>
            </div>
        )
    }
}

const mapStateToProps = state => {
    return {
        activeUserChatConversationId: state.chatMessanger.activeUserChatConversationId,
        chatConversations: state.chatMessanger.chatConversations
    }
}

const mapDispatchToProps = dispatch => {
    return {
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(ConversationsScrollBar)