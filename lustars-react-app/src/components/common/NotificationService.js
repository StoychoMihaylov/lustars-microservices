import { HubConnectionBuilder } from '@microsoft/signalr'

const EstablishNotificationConnection = () => {

    const connection = new HubConnectionBuilder()
        .withUrl('http://localhost:5004/webnotificationhub')
        .withAutomaticReconnect()
        .build()

    connection.start()
        .then(result => {
            console.log('Connected!');
            console.log(result)

            connection.on('Notification', message => {
                console.log("Notification:" + message)
            })

            connection.on('PushNotification', message => {
                console.log("PushNotification:" + message)
            })
        })
        .catch(err => console.log('Connection failed: ', err))
}

export default EstablishNotificationConnection;
