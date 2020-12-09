import React from 'react'
import { connect } from 'react-redux'
import { api } from '../../../constants/endpoints'
import { startConversationConnection } from '../Messanger/SignalRChatConnection'
import { setActiveChatConversation } from '../../../store/actions/chatMessangerActions'
import './ConversationsScrollBar.css'

class ConversationsScrollBar extends React.PureComponent {

    setActiveConversationAndConnectToTheHub(id) {
        startConversationConnection(id, null)
        this.props.setActiveChatConversation(id)
    }

    render() {
        return (
            <div className="conversations-scroll-bar-container" >
                <div className="conversations-container">

                    {
                        this.props.chatConversations.map((conversation, index) => {
                            return(
                                <div
                                id={index} className="individual-conversation-box"
                                style={{ backgroundColor: conversation.id === this.props.activeUserChatConversationId ? "lightblue" : "" }}
                                onClick={() => this.setActiveConversationAndConnectToTheHub(conversation.id)}
                                >
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
        setActiveChatConversation: (id) => dispatch(setActiveChatConversation(id)),
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(ConversationsScrollBar)