import { HubConnectionBuilder } from '@microsoft/signalr'
import { NotificationManager } from 'react-notifications'

export function startConversationConnection(conversationId, message) {
    const connection = new HubConnectionBuilder()
        .withUrl('http://localhost:5000/hubs/chat-messanger')
        .withAutomaticReconnect([0, 1000, 2000, 3000, 3000, 3000, 3000, 3000, 3000, 3000, 3000, 3000, 3000, 3000])
        .build()

    connection.start()
        .then(() => {

            // Call the hub to save user connection id in the chat group
            if(conversationId !== null) {
                connection.invoke('OpenChatConversation', conversationId)
                .catch(function (err) {
                    console.error(err)
                })
            }

            // Send message to SignalR hub
            if(message !== null) {
                connection.invoke('SendMessageToTheHub', JSON.stringify(message) )
                .catch(function (err) {
                    console.log(err)
                })
            }

           /*  connection.on('ReceiveMessage', message => {
                console.log(message)
            }) */

        })
        .catch(err => {
            console.log('Connection failed: ', err)
            NotificationManager.error('Chat Connection failed', '', 3000)
        })
}