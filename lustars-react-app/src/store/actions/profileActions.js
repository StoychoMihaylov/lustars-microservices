import {
    REQUEST_MY_PROFILE_DETAILS,
    REQUEST_MY_PROFILE_DETAILS_SUCCESS,
    REQUEST_MY_PROFILE_DETAILS_FAIL,

    REQUEST_EDIT_MY_PROFILE_DETAILS,
    REQUEST_EDIT_MY_PROFILE_DETAILS_SUCCESS,
    REQUEST_EDIT_MY_PROFILE_DETAILS_FAIL,

    CHANGE_USER_PROFILE_ACTIVE_ON_OFF,
    CHANGE_USER_PROFILE_ACTIVE_ON_OFF_SUCCESS,

    CHANGE_USER_EMAIL_SUBSCRIBED,
    CHANGE_USER_EMAIL_SUBSCRIBED_SUCCESS,

    REQUEST_UPLOAD_AVATAR_IMAGE,
    REQUEST_UPLOAD_AVATAR_IMAGE_SUCCESS,
    REQUEST_UPLOAD_AVATAR_IMAGE_FAIL
} from '../../constants/profileActionTypes'
import { api } from '../../constants/endpoints'
import axios from 'axios'

//*************************** Get my user profile details ***************************

export function getMyUserProfileDetails() {
    return dispatch => {
        dispatch(requestMyUserProfileDetails())
        axios.get(api.domain + 'user-profile', {
            headers: {
                'Authorization': 'Bearer ' + localStorage.getItem('lustars_token')
            }
        })
        .then(response => {
            dispatch(requestMyUserProfileDetailsSuccess(response.data))
        })
        .catch(err => {
            dispatch(requestMyUserProfileDetailsFail(err))

        })
    }
}

export function requestMyUserProfileDetails() {
    return {
        type: REQUEST_MY_PROFILE_DETAILS
    }
}

export function requestMyUserProfileDetailsSuccess(data) {
    return {
        type: REQUEST_MY_PROFILE_DETAILS_SUCCESS,
        payload: data
    }
}

export function requestMyUserProfileDetailsFail(error) {
    return {
        type: REQUEST_MY_PROFILE_DETAILS_FAIL,
        payload: error
    }
}

//*************************** Edit my profile details ***************************

export function editMyUserProfileDetails(userProfileDetails) {
    return dispatch => {
        dispatch(requestEditMyUserProfileDetails())

        return axios.post(api.domain + 'user-profile/edit', userProfileDetails, {
                headers: {
                    'Authorization': 'Bearer ' + localStorage.getItem('lustars_token')
                }
            })
            .then(response => {
                console.log(response)
                dispatch(requestEditMyUserProfileDetailsSuccess())
                return response
            })
            .catch(err => {
                dispatch(requestEditMyUserProfileDetailsFail(err))
                return err
            })
    }
}

export function requestEditMyUserProfileDetails() {
    return {
        type: REQUEST_EDIT_MY_PROFILE_DETAILS
    }
}

export function requestEditMyUserProfileDetailsSuccess() {
    return {
        type: REQUEST_EDIT_MY_PROFILE_DETAILS_SUCCESS
    }
}

export function requestEditMyUserProfileDetailsFail(error) {
    return {
        type: REQUEST_EDIT_MY_PROFILE_DETAILS_FAIL,
        payload: error
    }
}

//*************************** Change wheter user profile is active on/off ***************************

export function changeIsUserActive(newProfileData) {
    return dispatch => {
        dispatch(changeWetherUserIsActiveOrNot())
        dispatch(changeWetherUserIsActiveOrNotSuccess(newProfileData))
    }
}

export function changeWetherUserIsActiveOrNot() {
    return {
        type: CHANGE_USER_PROFILE_ACTIVE_ON_OFF
    }
}

export function changeWetherUserIsActiveOrNotSuccess(newProfileData) {
    return {
        type: CHANGE_USER_PROFILE_ACTIVE_ON_OFF_SUCCESS,
        payload: newProfileData
    }
}

//*************************** Change wheter user is email subscribed on/off ***************************

export function changeUserEmailSubsribed(newProfileData) {
    return dispatch => {
        dispatch(changeWetherUserIsEmailSubscribed())
        dispatch(changeWetherUserIsEmailSubscribedSuccess(newProfileData))
    }
}

export function changeWetherUserIsEmailSubscribed() {
    return {
        type: CHANGE_USER_EMAIL_SUBSCRIBED
    }
}

export function changeWetherUserIsEmailSubscribedSuccess(newProfileData) {
    return {
        type: CHANGE_USER_EMAIL_SUBSCRIBED_SUCCESS,
        payload: newProfileData
    }
}

//*************************** Upload avatar image ***************************

export function uploadAvatarImage(formdData) {
    return dispatch => {
        dispatch(requestUploadAvatarImage(formdData))
        axios.post(api.domain + 'user-profile/avatar-image/upload', formdData, {
            headers: {
                'Authorization': 'Bearer ' + localStorage.getItem('lustars_token')
            }
        })
        .then(
            dispatch(requestUploadAvatarImageSuccess())
        )
        .catch(err => {
            dispatch(requestUploadAvatarImageFail(err))
        })
    }
}

export function requestUploadAvatarImage() {
    return {
        type: REQUEST_UPLOAD_AVATAR_IMAGE
    }
}

export function requestUploadAvatarImageSuccess() {
    return {
        type: REQUEST_UPLOAD_AVATAR_IMAGE_SUCCESS,
    }
}

export function requestUploadAvatarImageFail(error) {
    return {
        type: REQUEST_UPLOAD_AVATAR_IMAGE_FAIL,
        payload: error
    }
}
