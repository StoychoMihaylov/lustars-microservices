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

const initialState = {
    activeUserChatConversationId: null,
    chatConversations: [],
    chatMesseges: [],

    isLoading: false,
    error: false
}

const chatMessangerReducer = (state, action) => {
    state = state || initialState

    switch (action.type) {
        case GET_ALL_CONVERSATION_MESSAGES:
            return {
                ...state,
                isLoading: true
            }
        case GET_ALL_CONVERSATION_MESSAGES_SUCCESS:
            return {
                ...state,
                chatMesseges: action.payload
            }
        case GET_ALL_CONVERSATION_MESSAGES_FAIL:
            return {
                ...state,
                error: action.payload
            }
        case ADD_NEW_MESSAGE_IN_THE_CHAT:
            return {
                ...state,
                chatMesseges: state.chatMesseges.concat([action.payload])
            }
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