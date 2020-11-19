import React from 'react'
import { HubConnectionBuilder } from '@microsoft/signalr'
import { NotificationManager } from 'react-notifications'

class NotificationService extends React.Component {

    componentDidMount() {
        var userId = localStorage.getItem('lustars_user_id')

        if (userId !== null && userId !== undefined) {
            this.StartConnection(userId)
        }
    }

    StartConnection(userId) {
        const connection = new HubConnectionBuilder()
            .withUrl('http://localhost:5004/hubs/web-event-notification')
            .withAutomaticReconnect([0, 1000, 2000, 3000, 3000, 3000, 3000, 3000, 3000, 3000, 3000, 3000, 3000, 3000])
            .build()

        connection.start()
            .then(() => {

                // Call the hub to save the userId
                connection.invoke('SaveUserId', userId)
                .catch(function (err) {
                    console.error(err)
                })

                connection.on('ServerEventNotification', message => {
                    console.log(message)
                    NotificationManager.success(message, '', 3000)
                })

            })
            .catch(err => {
                console.log('Connection failed: ', err)
            })
    }

    render() {
        return null
     }
}

export default NotificationService
