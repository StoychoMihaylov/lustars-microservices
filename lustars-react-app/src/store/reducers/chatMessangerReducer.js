import {
    REQUEST_USER_CHAT_CONVERSATIONS,
    REQUEST_USER_CHAT_CONVERSATIONS_SUCCESS,
    REQUEST_USER_CHAT_CONVERSATIONS_FAIL,
    SET_ACTIVE_USER_CHAT_CONVERSATION_ID
} from '../../constants/actionTypes/chatMessangerTypes'

const initialState = {
    activeUserChatConversationId: null,
    chatConversations: [],

    isLoading: false,
    error: false
}

const chatMessangerReducer = (state, action) => {
    state = state || initialState

    switch (action.type) {
        case REQUEST_USER_CHAT_CONVERSATIONS:
            return {
                ...state,
                isLoading: true
            }
        case REQUEST_USER_CHAT_CONVERSATIONS_SUCCESS:
            return {
                ...state,
                chatConversations: action.payload,
                isLoading: false,
            }
        case REQUEST_USER_CHAT_CONVERSATIONS_FAIL:
            return {
                ...state,
                error: action.payload
            }
        case SET_ACTIVE_USER_CHAT_CONVERSATION_ID:
            return {
                ...state,
                activeUserChatConversationId: action.payload
            }

        default:
            return state
    }
}

export default chatMessangerReducer