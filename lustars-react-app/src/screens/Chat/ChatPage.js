import React from 'react'
import ConversationsScrollBar from '../../components/chat/ConversationsScrollBar/ConversationsScrollBar'
import Messanger from '../../components/chat/Messanger/Messanger'
import './ChatPage.css'

class ChatPage extends React.PureComponent {


    render() {
        console.log('---------------------------------------------')
        console.log(this.props.location.state != undefined ? this.props.location.state.activeConversation : null)
        console.log('---------------------------------------------')
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

export default ChatPage