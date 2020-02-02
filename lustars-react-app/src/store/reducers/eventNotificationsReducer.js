import {
    SET_INFO_NOTIFICATION_MESSAGE,
    CLEAR_INFO_NOTIFICATION_MESSAGE,
    SET_SUCCESSFUL_NOTIFICATION_MESSAGE,
    CLEAR_SUCCESSFUL_NOTIFICATION_MESSAGE,
    SET_ERROR_NOTIFICATION_MESSAGE,
    CLEAR_ERROR_NOTIFICATION_MESSAGE
} from '../../constants/eventNotificationsActionTypes'

const initialState = {
    infoMessage: "",
    successfulMessage: "",
    errorMessage: ""
}

const eventNotificationsReducer = (state, action) => {
    state = state || initialState

    switch (action.type) {
        case SET_INFO_NOTIFICATION_MESSAGE:
            return {
                ...state,
                infoMessage: action.payload
            }
        case CLEAR_INFO_NOTIFICATION_MESSAGE:
            return {
                ...state,
                infoMessage: ""
            }
        case SET_SUCCESSFUL_NOTIFICATION_MESSAGE:
            return {
                ...state,
                successfulMessage: action.payload
            }
        case CLEAR_SUCCESSFUL_NOTIFICATION_MESSAGE:
            return {
                ...state,
                successfulMessage: ""
            }
        case SET_ERROR_NOTIFICATION_MESSAGE:
            return {
                ...state,
                errorMessage: action.payload
            }
        case CLEAR_ERROR_NOTIFICATION_MESSAGE:
            return {
                ...state,
                errorMessage: ""
            }
        default:
            return state
    }
}

export default eventNotificationsReducer