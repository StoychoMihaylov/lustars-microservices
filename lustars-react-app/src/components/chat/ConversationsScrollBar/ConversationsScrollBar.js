import React from 'react'
import './ConversationsScrollBar.css'

class ConversationsScrollBar extends React.PureComponent {
    render() {
        return (
            <div className="conversations-scroll-bar-container" >
                <div className="conversations-container">
                    <div className="individual-conversation-box">
                        <img src="https://placeimg.com/50/50/people?1" className="conversation-profile-img" />
                        <span className="conversation-profile-name">Pesho</span>
                    </div>
                    <div className="individual-conversation-box">
                        <img src="https://placeimg.com/50/50/people?2" className="conversation-profile-img" />
                        <div className="conversation-profile-name">Mesho</div>
                    </div>
                    <div className="individual-conversation-box">
                        <img src="https://placeimg.com/50/50/people?3" className="conversation-profile-img" />
                        <div className="conversation-profile-name">Bibo</div>
                    </div>
                    <div className="individual-conversation-box">
                        <img src="https://placeimg.com/50/50/people?4" className="conversation-profile-img" />
                        <div className="conversation-profile-name">Stacil</div>
                    </div>
                    <div className="individual-conversation-box">
                        <img src="https://placeimg.com/50/50/people?5" className="conversation-profile-img" />
                        <div className="conversation-profile-name">Strahomir</div>
                    </div>
                    <div className="individual-conversation-box">
                        <img src="https://placeimg.com/50/50/people?6" className="conversation-profile-img" />
                        <div className="conversation-profile-name">Mila</div>
                    </div>
                    <div className="individual-conversation-box">
                        <img src="https://placeimg.com/50/50/people?7" className="conversation-profile-img" />
                        <div className="conversation-profile-name">Brat Pit</div>
                    </div>
                    <div className="individual-conversation-box">
                        <img src="https://placeimg.com/50/50/people?8" className="conversation-profile-img" />
                        <div className="conversation-profile-name">Bill</div>
                    </div>
                    <div className="individual-conversation-box">
                        <img src="https://placeimg.com/50/50/people?9" className="conversation-profile-img" />
                        <div className="conversation-profile-name">John</div>
                    </div>
                    <div className="individual-conversation-box">
                        <img src="https://placeimg.com/50/50/people?10" className="conversation-profile-img" />
                        <div className="conversation-profile-name">Milanov</div>
                    </div>
                    <div className="individual-conversation-box">
                        <img src="https://placeimg.com/50/50/people?11" className="conversation-profile-img" />
                        <div className="conversation-profile-name">Theamir</div>
                    </div>
                </div>
            </div>
        )
    }
}

export default ConversationsScrollBar