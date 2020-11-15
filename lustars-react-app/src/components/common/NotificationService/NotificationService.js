import { HubConnectionBuilder } from '@microsoft/signalr'
import { NotificationManager } from 'react-notifications'

const EstablishNotificationConnection = () => {
    var userId = localStorage.getItem('lustars_user_id')

    if (userId !== null && userId !== undefined) {
        StartConnection(userId)
    }
}

const StartConnection = (userId) => {
    const connection = new HubConnectionBuilder()
        .withUrl('http://localhost:5004/webnotificationhub')
        .withAutomaticReconnect()
        .build()

        connection.start()
        .then(result => {
            console.log('SignalR Connected!');

            // Call the hub to save the userId
            connection.invoke('SaveUserId', userId)
            .catch(function (err) {
                console.error(err)
            })

            connection.on('user-web-event-notification', message => {
                console.log("user-web-event-notification:" + message)
                NotificationManager.success(message, '', 3000)
            })

            connection.invoke('PushWebEventNotification', userId, "BAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAM!")
        })
        .catch(err => console.log('Connection failed: ', err))
}

export default EstablishNotificationConnection;
