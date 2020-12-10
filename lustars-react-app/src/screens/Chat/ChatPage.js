import React from 'react'
import { connect } from 'react-redux'
import {
    getAllUserConversations,
    setActiveChatConversation
} from '../../store/actions/chatMessangerActions'
import ConversationsScrollBar from '../../components/chat/ConversationsScrollBar/ConversationsScrollBar'
import Messanger from '../../components/chat/Messanger/Messanger'
import './ChatPage.css'

class ChatPage extends React.PureComponent {
    componentDidMount() {
        this.props.getAllUserConversations()
    }

    render() {
        return (
            <div className="chat-page-container">
                <h1>My messages</h1>
                <Messanger />
            </div>
        )
    }
}

const mapDispatchToProps = dispatch => {
    return {
        getAllUserConversations: () => dispatch(getAllUserConversations()),
        setActiveChatConversation: (id) => dispatch(setActiveChatConversation(id)),
    }
}

export default connect(null, mapDispatchToProps)(ChatPage)