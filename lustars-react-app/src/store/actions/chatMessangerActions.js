import {
    REQUEST_USER_CHAT_CONVERSATIONS,
    REQUEST_USER_CHAT_CONVERSATIONS_SUCCESS,
    REQUEST_USER_CHAT_CONVERSATIONS_FAIL,
    SET_ACTIVE_USER_CHAT_CONVERSATION_ID
} from '../../constants/actionTypes/chatMessangerTypes'
import { api } from '../../constants/endpoints'
import axios from 'axios'

//*************************** Get all user chat conversations ***************************

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

//************************** Set active chat messanger ************************************

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
