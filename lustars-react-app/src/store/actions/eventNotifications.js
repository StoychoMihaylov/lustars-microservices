import {
    SET_INFO_NOTIFICATION_MESSAGE,
    CLEAR_INFO_NOTIFICATION_MESSAGE,
    SET_SUCCESSFUL_NOTIFICATION_MESSAGE,
    CLEAR_SUCCESSFUL_NOTIFICATION_MESSAGE,
    SET_ERROR_NOTIFICATION_MESSAGE,
    CLEAR_ERROR_NOTIFICATION_MESSAGE
} from '../../constants/eventNotificationsActionTypes'

//*************************** Info notification actions ***************************

export function infoNotification(message) {
    return function (dispatch) {
        dispatch(setInfoNotificationMessage(message))
        setTimeout(
            function () {
                dispatch(clearInfoNotificationMesage(message))
            },
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

//************************** Success notification action ****************************

export function successfulNotification(message) {
    return function (dispatch) {
        dispatch(setSuccessfulNotificationMessage(message))
        setTimeout(
            function () {
                dispatch(clearSuccessfulNotificationMessage(message))
            },
            4000
        )
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

//************************** error notification action ****************************

export function errorNotification(message) {
    return function (dispatch) {
        dispatch(setErrorNotificationMessage(message))
        setTimeout(
            function () {
            dispatch(clearErrorNotificationMessage(message))
            },
            4000
        )
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