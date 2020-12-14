import {
    REQUEST_USER_CHAT_CONVERSATIONS,
    REQUEST_USER_CHAT_CONVERSATIONS_SUCCESS,
    REQUEST_USER_CHAT_CONVERSATIONS_FAIL,
    SET_ACTIVE_USER_CHAT_CONVERSATION_ID,
    ADD_NEW_MESSAGE_IN_THE_CHAT,
    GET_ALL_CONVERSATION_MESSAGES,
    GET_ALL_CONVERSATION_MESSAGES_SUCCESS,
    GET_ALL_CONVERSATION_MESSAGES_FAIL
} from '../../constants/actionTypes/chatMessangerTypes'
import { api } from '../../constants/endpoints'
import axios from 'axios'

//********************** Get all user chat conversations ***************************

export function getAllUserConversations() {
    return dispatch => {
        dispatch(requestGetAllUserConversations())

        axios.get(api.domain + 'profile/conversations', {
            headers: {
                'Authorization': 'Bearer ' + localStorage.getItem('lustars_token'),
                'Access-Control-Allow-Origin': '*'
            }
        })
        .then(response => {
            dispatch(requestGetAllUserConversationsSuccess(response.data))
        })
        .catch(err => {
            dispatch(requestGetAllUserConversationsFail(err))
        })
    }
}

export function requestGetAllUserConversations() {
    return {
        type: REQUEST_USER_CHAT_CONVERSATIONS
    }
}

export function requestGetAllUserConversationsSuccess(data) {
    return {
        type: REQUEST_USER_CHAT_CONVERSATIONS_SUCCESS,
        payload: data
    }
}

export function requestGetAllUserConversationsFail(error) {
    return {
        type: REQUEST_USER_CHAT_CONVERSATIONS_FAIL,
        payload: error
    }
}

//************************** Set active chat messanger *****************************

export function setActiveChatConversation(id) {
    return dispatch => {
        dispatch(updateActiveChatConversationId(id))
    }
}

export function updateActiveChatConversationId(id) {
    return {
        type: SET_ACTIVE_USER_CHAT_CONVERSATION_ID,
        payload: id
    }
}

//************************** Update messanger ************************************

export function addMessageInTheChat(message) {
    return dispatch => {
        dispatch(addNewReceiveMessage(message))
    }
}

export function addNewReceiveMessage(message) {
    return {
        type: ADD_NEW_MESSAGE_IN_THE_CHAT,
        payload: message
    }
}

//********************** Get all conversation messages **************************

export function getAllConversationMessages(id) {
    return dispatch => {
        dispatch(requestAllConversationMessages())

        axios.get(api.domain + `profile/conversation-messages?ID=${id}`, {
            headers: {
                'Authorization': 'Bearer ' + localStorage.getItem('lustars_token'),
                'Access-Control-Allow-Origin': '*'
            }
        })
        .then(response => {
            dispatch(requestAllConversationMessagesSuccess(response.data))
        })
        .catch(err => {
            if (err.response.status === 404) {
                dispatch(requestAllConversationMessagesSuccess([]))
            }
            dispatch(requestAllConversationMessagesFail(err))
        })
    }
}

export function requestAllConversationMessages() {
    return {
        type: GET_ALL_CONVERSATION_MESSAGES
    }
}

export function requestAllConversationMessagesSuccess(data) {
    return {
        type: GET_ALL_CONVERSATION_MESSAGES_SUCCESS,
        payload: data
    }
}

export function requestAllConversationMessagesFail(error) {
    return {
        type: GET_ALL_CONVERSATION_MESSAGES_FAIL,
        payload: error
    }
}

