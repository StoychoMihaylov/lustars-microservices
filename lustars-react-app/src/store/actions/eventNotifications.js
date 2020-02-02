import {
    SET_INFO_NOTIFICATION_MESSAGE,
    CLEAR_INFO_NOTIFICATION_MESSAGE,
    SET_SUCCESSFUL_NOTIFICATION_MESSAGE,
    CLEAR_SUCCESSFUL_NOTIFICATION_MESSAGE,
    SET_ERROR_NOTIFICATION_MESSAGE,
    CLEAR_ERROR_NOTIFICATION_MESSAGE
} from '../../constants/eventNotificationsActionTypes'

//*************************** Notification actions ***************************

export function infoNotification(message) {
    return function (dispatch) {
        dispatch(setInfoNotificationMessage(message))
        setTimeout(
            function () {
                dispatch(clearInfoNotificationMesage(message))
            }.bind(this),
            4000
        )
    }
}

export function successfulNotification(message) {
    return function (dispatch) {
        dispatch(setSuccessfulNotificationMessage(message))
        setTimeout(
            function () {
                dispatch(clearSuccessfulNotificationMessage(message))
            }.bind(this),
            4000
        )
    }
}

export function errorNotification(message) {
    return function (dispatch) {
        dispatch(setErrorNotificationMessage(message))
        setTimeout(
            function () {
            dispatch(clearErrorNotificationMessage(message))
            }.bind(this),
            4000
        )
    }
}

export function setInfoNotificationMessage(message) {
    return {
        type: SET_INFO_NOTIFICATION_MESSAGE,
        payload: message
    }
}

export function clearInfoNotificationMesage() {
    return {
        type: CLEAR_INFO_NOTIFICATION_MESSAGE,
    }
}

export function setSuccessfulNotificationMessage(message) {
    return {
        type: SET_SUCCESSFUL_NOTIFICATION_MESSAGE,
        payload: message
    }
}

export function clearSuccessfulNotificationMessage() {
    return {
        type: CLEAR_SUCCESSFUL_NOTIFICATION_MESSAGE,
    }
}

export function setErrorNotificationMessage(message) {
    return {
        type: SET_ERROR_NOTIFICATION_MESSAGE,
        payload: message
    }
}

export function clearErrorNotificationMessage() {
    return {
        type: CLEAR_ERROR_NOTIFICATION_MESSAGE,
    }
}


//***********************************************************************************