import React from 'react'
import { startConversationConnection } from './SignalRChatConnection'
import './Messanger.css'

class Messanger extends React.PureComponent {

    componentDidMount() {
        startConversationConnection()
    }

    render() {
        return (
            <div className="messanger-scroll-bar-container">

                 <div className="messinger-active-conversation">
                    <img src="https://placeimg.com/50/50/people?1" className="conversation-profile-img" />
                    <span className="conversation-profile-name">Pesho</span>
                </div>

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
                    <textarea id="input-typer" rows="1"/>
                    <span id="messanger-message-send-bttn" >Send</span>
                </div>
            </div>
        )
    }
}

export default Messanger