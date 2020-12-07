import React from 'react'
import { connect } from 'react-redux'
import { getAllUserConversations } from '../../store/actions/chatMessangerActions'
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
                <div className="converation-messanger-container">
                    <div className="conversations">
                        <ConversationsScrollBar />
                    </div>
                    <div className="messanger">
                        <Messanger />
                    </div>
                </div>
            </div>
        )
    }
}

const mapDispatchToProps = dispatch => {
    return {
        getAllUserConversations: () => dispatch(getAllUserConversations()),
    }
}

export default connect(null, mapDispatchToProps)(ChatPage)