import { HubConnectionBuilder } from '@microsoft/signalr'
import { NotificationManager } from 'react-notifications'

export function startConversationConnection() {
    const connection = new HubConnectionBuilder()
        .withUrl('http://localhost:5000/hubs/chat-messanger')
        .withAutomaticReconnect([0, 1000, 2000, 3000, 3000, 3000, 3000, 3000, 3000, 3000, 3000, 3000, 3000, 3000])
        .build()

    connection.start()
        .then(() => {
            let usersData = {
                ChatId: "df559f99-5ef8-4ee0-ad3d-221aa7e82f3b",
                UserAId: "78da9596-baa3-426b-96d8-deb9f541c6a5",
                UserBId: "332b8d38-f703-435c-b478-7dfbd53ab9dd",
            }

            // Call the hub to save the users data and open connection
            connection.invoke('OpenChatConversation', JSON.stringify(usersData))
            .catch(function (err) {
                console.error(err)
            })
        })
        .catch(err => {
            console.log('Connection failed: ', err)
            NotificationManager.error('Chat Connection failed', '', 3000)
        })
}