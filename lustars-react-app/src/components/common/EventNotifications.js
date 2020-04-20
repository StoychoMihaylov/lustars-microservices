import React, { Component } from "react"
import { connect } from "react-redux"
import "../../styles/components/common/EventNotification.css"

class EventNotification extends Component {
    render () {
        return(
            <div>

                {
                    this.props.infoMessage.length > 0
                    ? <div className="info-notification-message">{this.props.infoMessage}</div>
                    : null
                }

                {
                    this.props.successfulMessage.length > 0
                    ? <div className="success-notification-message">{this.props.successfulMessage}</div>
                    : null
                }

                {
                    this.props.errorMessage.length > 0
                    ? <div className="error-notification-message">{this.props.errorMessage}</div>
                    : null
                }

            </div>
        )
    }
}

const mapStateToProps = state => {
    return {
        infoMessage: state.eventNotifications.infoMessage,
        successfulMessage: state.eventNotifications.successfulMessage,
        errorMessage: state.eventNotifications.errorMessage
    }
  }

export default connect(mapStateToProps, null)(EventNotification)