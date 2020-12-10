import React from 'react'
import { connect } from 'react-redux'
import { api } from '../../../constants/endpoints'
import { addMessageInTheChat, setActiveChatConversation } from '../../../store/actions/chatMessangerActions'
import { HubConnectionBuilder } from '@microsoft/signalr'
import './Messanger.css'

class Messanger extends React.PureComponent {
    constructor(props) {
        super(props)

        this.state = {
            message: '',
            hubConnection: null,
        }
    }

    sendMessage() {
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
            this.state.hubConnection.invoke('SendMessageToTheHub', JSON.stringify(message))
                .catch((err) => console.error(err))

            this.setState({ message: ""})
    }

    onKeyPressHandler(e) {
        if(e.key === 'Enter' && !(e.key === 'Enter' && e.shiftKey)) {
            this.sendMessage()
        }
    }

    setActiveConversationAndConnectToTheHub(id) {
        const hubConnection = new HubConnectionBuilder()
            .withUrl('http://localhost:5000/hubs/chat-messanger')
            .withAutomaticReconnect([0, 1000, 2000, 3000, 3000, 3000, 3000, 3000, 3000, 3000, 3000, 3000, 3000, 3000])
            .build()

        this.setState({ hubConnection }, () => {
            this.state.hubConnection
                .start()
                .then(() => {

                    // Open hub conversation
                    this.state.hubConnection.invoke('OpenChatConversation', id)
                    .catch(function (err) {
                        console.error(err)
                    })

                    // Receiveing messages
                    this.state.hubConnection.on('ReceiveMessage', message => {
                        this.props.addMessageInTheChat(message)
                    })
                })
                .catch((err) => console.error(err))
        })

        this.props.setActiveChatConversation(id)
    }

    componentDidMount() {
        if(this.props.activeUserChatConversationId !== null) {
            let id = this.props.activeUserChatConversationId
            this.setActiveConversationAndConnectToTheHub(id)
        }
    }

    renderActiveChatCOnversation() {
        if(this.props.activeUserChatConversationId !== null && this.props.chatConversations !== undefined) {
            let activeConversation = this.props.chatConversations.find(x => x.id === this.props.activeUserChatConversationId)
            if(activeConversation === undefined) return

            return(
                <div className="individual-conversation-box">
                    <img src={api.imageAPI + activeConversation.corresponderAvatarImage} className="conversation-profile-img" alt="" />
                    <span className="conversation-profile-name">{ activeConversation.corresponderNames }</span>
                </div>
            )
        }
    }

    render() {
        return (
            <div className="converation-messanger-container">
                <div className="conversations">
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
                </div>

                <div className="messanger">
                    {
                        this.props.activeUserChatConversationId !== null
                        ?
                        <div className="messanger-scroll-bar-container">

                            {
                                this.renderActiveChatCOnversation()
                            }

                            <div className= "messanger-message-scroll-bar">
                                <div className="messanger-messages-container-box">
                                    {
                                        this.props.chatMesseges.map((message, index) => {
                                            return (
                                                message.sender === localStorage.getItem('lustars_user_id')
                                                ?
                                                <div id={index} className="messanger-my-message">
                                                    <spam>{ message.content }</spam>
                                                </div>
                                                :
                                                <div id={index} className="messanger-collocutor-message">
                                                    <spam>{ message.content }</spam>
                                                </div>
                                            )
                                        })
                                    }
                                </div>
                            </div>

                            <br/>

                            <div className="messanger-text-typing-area" >
                                <textarea
                                    id="input-typer"
                                    rows="1"
                                    value={ this.state.message }
                                    onKeyPress={(e) => this.onKeyPressHandler(e)}
                                    onChange={(e) => this.setState({ message: e.target.value })}
                                />
                                <span
                                    id="messanger-message-send-bttn"
                                    onClick={() => this.sendMessage(this.state.message)}
                                    >Send
                                </span>
                            </div>
                        </div>
                        : null
                    }
                </div>
            </div>
        )
    }
}

const mapStateToProps = state => {
    return {
        activeUserChatConversationId: state.chatMessanger.activeUserChatConversationId,
        chatConversations: state.chatMessanger.chatConversations,
        chatMesseges: state.chatMessanger.chatMesseges
    }
}

const mapDispatchToProps = dispatch => {
    return {
        addMessageInTheChat: (message) => dispatch(addMessageInTheChat(message)),
        setActiveChatConversation: (id) => dispatch(setActiveChatConversation(id)),
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(Messanger)