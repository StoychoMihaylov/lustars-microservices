import React from 'react'
import { connect } from 'react-redux'
import { api } from '../../../constants/endpoints'
import { startConversationConnection } from './SignalRChatConnection'
import './Messanger.css'

class Messanger extends React.PureComponent {
    constructor(props) {
        super(props)

        this.state = {
            message: '',
        }
    }

    onKeyPressHandler(e) {
        if(e.key === 'Enter' && !(e.key === 'Enter' && e.shiftKey)) {

            let currentUserId = localStorage.getItem('lustars_user_id')
            let activeConversation = this.props.chatConversations.find(x => x.id === this.props.activeUserChatConversationId)

            let message = {
                id: '',
                conversationId: activeConversation.id,
                sender: currentUserId,
                recipient: currentUserId === activeConversation.chatStarterUserId ? currentUserId : activeConversation.invitedUserId,
                Content: this.state.message
            }

            // Send Message
            startConversationConnection(null, message)

            this.setState({ message: ""})
        }
    }

    updateMessageState(message) {
        this.setState({ message:message })
    }

    componentDidMount() {
        if(this.props.activeUserChatConversationId !== null) {
            startConversationConnection(this.props.activeUserChatConversationId, null)
        }
    }

    renderActiveChatCOnversation() {
        if(this.props.activeUserChatConversationId !== null && this.props.chatConversations !== undefined) {
            let activeConversation = this.props.chatConversations.find(x => x.id === this.props.activeUserChatConversationId)

            return(
                <div className="individual-conversation-box">
                    <img src={api.imageAPI + activeConversation.corresponderAvatarImage} className="conversation-profile-img" alt="" />
                    <span className="conversation-profile-name">{ activeConversation.corresponderNames }</span>
                </div>
            )
        }
    }

    render() {
        console.log(this.props.chatConversations)
        return (
            <div className="messanger-scroll-bar-container">

                    {
                        this.renderActiveChatCOnversation()
                    }

                <div className= "messanger-message-scroll-bar">
                    <div className="messanger-messages-container-box">
                        <div className="messanger-my-message">
                            <spam>Hey, how are you ?</spam>
                        </div>
                        <div className="messanger-collocutor-message">
                            <spam>I'm fine thanks!</spam>
                        </div>
                        <div className="messanger-collocutor-message">
                            <spam>And you ?</spam>
                        </div>
                        <div className="messanger-my-message">
                            <spam>Me too! I'm great!</spam>
                        </div>
                        <div className="messanger-my-message">
                            <spam>Let's meet today ?</spam>
                        </div>
                        <div className="messanger-collocutor-message">
                            <spam>Ahhh, I have soo much to do... don't know if will have time today :(</spam>
                        </div>
                        <div className="messanger-collocutor-message">
                            <spam>But wait, I think we can meet tomorrow and play something in at home ? Or watch some movie ?</spam>
                        </div>
                        <div className="messanger-my-message">
                            <spam>Ah yeaaa! Sounds great, meybe can play some PC game ?</spam>
                        </div>
                        <div className="messanger-my-message">
                            <spam>You know... you can teach me how to play dota!</spam>
                        </div>
                        <div className="messanger-my-message">
                            <spam>I have played only dota 1 so far but wanna learn dota 2 as well!</spam>
                        </div>
                        <div className="messanger-collocutor-message">
                            <spam>Yeaaa let's do it! Will be lot of fun!</spam>
                        </div>
                    </div>
                </div>
                <br/>
                <div className="messanger-text-typing-area" >
                    <textarea
                        id="input-typer"
                        rows="1"
                        value={ this.state.message }
                        onKeyPress={(e) => this.onKeyPressHandler(e)}
                        onChange={(e) => this.updateMessageState(e.target.value)}
                    />
                    <span
                        id="messanger-message-send-bttn"
                        onClick={() => this.sendMessage(this.state.message)}
                        >Send
                    </span>
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

export default connect(mapStateToProps, mapDispatchToProps)(Messanger)