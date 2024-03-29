import {
    REQUEST_MY_PROFILE_DETAILS,
    REQUEST_MY_PROFILE_DETAILS_SUCCESS,
    REQUEST_MY_PROFILE_DETAILS_FAIL,
    REQUEST_SOME_PROFILE_DETAILS,
    REQUEST_SOME_PROFILE_DETAILS_SUCCESS,
    REQUEST_SOME_PROFILE_DETAILS_FAIL,
    REQUEST_EDIT_MY_PROFILE_DETAILS,
    REQUEST_EDIT_MY_PROFILE_DETAILS_SUCCESS,
    REQUEST_EDIT_MY_PROFILE_DETAILS_FAIL,
    UPDATE_USER_PROFILE_BOOLEAN_FIELD_SUCCESS,
    UPDATE_USER_PROFILE_TEXT_FIELD,
    UPDATE_USER_PROFILE_TEXT_FIELD_SUCCESS,
    REQUEST_UPLOAD_AVATAR_IMAGE,
    REQUEST_UPLOAD_AVATAR_IMAGE_SUCCESS,
    REQUEST_UPLOAD_AVATAR_IMAGE_FAIL,
    UPDATE_USER_PROFILE_GEOLOCATION,
    UPDATE_USER_PROFILE_GEOLOCATION_SUCCESS,
    UPDATE_USER_PROFILE_GEOLOCATION_FAIL,
    ADD_COUNTRY_LANGUAGE,
    ADD_COUNTRY_LANGUAGE_SUCCESS,
    DELETE_COUNTRY_LANGUAGE,
    UPLOAD_USER_PROFILE_IMAGE,
    UPLOAD_USER_PROFILE_IMAGE_SUCCESS,
    UPLOAD_USER_PROFILE_IMAGE_FAIL,
    DELETE_USER_PROFILE_IMAGE,
    DELETE_USER_PROFILE_IMAGE_SUCCESS,
    DELETE_USER_PROFILE_IMAGE_FAIL,
    REQUEST_MY_PROFILE_SHORT_DATA,
    REQUEST_MY_PROFILE_SHORT_DATA_SUCCESS,
    REQUEST_MY_PROFILE_SHORT_DATA_FAIL,
    REQUEST_GET_CURRENT_USER_AVATAR_URL,
    REQUEST_GET_CURRENT_USER_AVATAR_URL_SUCCESS,
    REQUEST_LIKE_USER_PROFILE,
    REQUEST_LIKE_USER_PROFILE_SUCCESS,
    REQUEST_LIKE_USER_PROFILE_FAIL,
    REQUEST_GET_WHO_LIKED_ME,
    REQUEST_GET_WHO_LIKED_ME_SUCCESS,
    REQUEST_GET_WHO_LIKED_ME_FAIL,
    REQUEST_GET_PROFILE_VISITORS,
    REQUEST_GET_PROFILE_VISITORS_SUCCESS,
    REQUEST_GET_PROFILE_VISITORS_FAIL
} from '../../constants/actionTypes/myProfileActionTypes'
import { NotificationManager} from 'react-notifications'
import { api } from '../../constants/endpoints'
import axios from 'axios'

//************************** Chech if both users like each other ****************

export function openChatIfUsersLikeEachOther(id) {
    let url = "profile/open-conversation" + (id !== "" ? `?ID=${id}` : "")

    return () => {
        return axios.post(api.domain + url, {}, {
            headers: {
                'Authorization': 'Bearer ' + localStorage.getItem('lustars_token'),
                'Access-Control-Allow-Origin': '*'
            }
        })
        .then(response => {
            return response
        })
        .catch(err => {
            return err
        })
    }
}

//*************************** Get my profile visitors ***************************

export function getMyProfileVisitors() {
    return dispatch => {
        dispatch(requestGetMyProfileVisitors())

        axios.get(api.domain + 'user-profile/visitors', {
            headers: {
                'Authorization': 'Bearer ' + localStorage.getItem('lustars_token'),
                'Access-Control-Allow-Origin': '*'
            }
        })
        .then(response => {
            dispatch(requestGetMyProfileVisitorsSuccess(response.data))
        })
        .catch(err => {
            dispatch(requestGetMyProfileVisitorsFail(err))
        })
    }
}

export function requestGetMyProfileVisitors() {
    return {
        type: REQUEST_GET_PROFILE_VISITORS
    }
}

export function requestGetMyProfileVisitorsSuccess(data) {
    return {
        type: REQUEST_GET_PROFILE_VISITORS_SUCCESS,
        payload: data
    }
}

export function requestGetMyProfileVisitorsFail(error) {
    return {
        type: REQUEST_GET_PROFILE_VISITORS_FAIL,
        payload: error
    }
}

//*************************** Get Who liked me ***************************

export function getAllUsersWhoLikedMe() {
    return dispatch => {
        dispatch(requestGetWhoLikedMe())

        axios.get(api.domain + 'user-profile/likes', {
            headers: {
                'Authorization': 'Bearer ' + localStorage.getItem('lustars_token'),
                'Access-Control-Allow-Origin': '*'
            }
        })
        .then(response => {
            dispatch(requestGetWhoLikedMeSuccess(response.data))
        })
        .catch(err => {
            dispatch(requestGetWhoLikedMeFail(err))
        })
    }
}

export function requestGetWhoLikedMe() {
    return {
        type: REQUEST_GET_WHO_LIKED_ME
    }
}

export function requestGetWhoLikedMeSuccess(data) {
    return {
        type: REQUEST_GET_WHO_LIKED_ME_SUCCESS,
        payload: data
    }
}

export function requestGetWhoLikedMeFail(error) {
    return {
        type: REQUEST_GET_WHO_LIKED_ME_FAIL,
        payload: error
    }
}

//*************************** Like User Profile ***************************

export function likeUserProfile(id) {
    return dispatch => {
        dispatch(requestLikeUserProfile())

        return axios.post(api.domain + `user-profile/like?ID=${id}`, {}, {
            headers: {
                'Authorization': 'Bearer ' + localStorage.getItem('lustars_token'),
                'Access-Control-Allow-Origin': '*'
            }
        })
        .then(response => {
            dispatch(requestLikeUserProfileSuccess())
            return response
        })
        .catch(err => {
            dispatch(requestLikeUserProfileFail(err))
            return err
        })
    }
}

export function requestLikeUserProfile() {
    return {
        type: REQUEST_LIKE_USER_PROFILE
    }
}

export function requestLikeUserProfileSuccess() {
    return {
        type: REQUEST_LIKE_USER_PROFILE_SUCCESS
    }
}

export function requestLikeUserProfileFail(error) {
    return {
        type: REQUEST_LIKE_USER_PROFILE_FAIL,
        payload: error
    }
}

//*************************** Get get some profile details by ID ***************************

export function getSomeUserProfileDetailsById(id) {
    let url = "user-profile" + (id !== "" ? `?ID=${id}` : "")

    return dispatch => {
        dispatch(requestUserProfileDetailsById(id))

        axios.get(api.domain + url, {
            headers: {
                'Authorization': 'Bearer ' + localStorage.getItem('lustars_token'),
                'Access-Control-Allow-Origin': '*'
            }
        })
        .then(response => {
            dispatch(requestUserProfileDetailsByIdSuccess(response.data))
        })
        .catch(err => {
            dispatch(requestMUserProfileDetailsByIdFail(err))
        })
    }
}

export function requestUserProfileDetailsById() {
    return {
        type: REQUEST_SOME_PROFILE_DETAILS
    }
}

export function requestUserProfileDetailsByIdSuccess(data) {
    return {
        type: REQUEST_SOME_PROFILE_DETAILS_SUCCESS,
        payload: data
    }
}

export function requestMUserProfileDetailsByIdFail(error) {
    return {
        type: REQUEST_SOME_PROFILE_DETAILS_FAIL,
        payload: error
    }
}

//*************************** Get current user avatar image url ***************************

export function getCurrentUserAvatarImageURL() {
    return dispatch => {
        dispatch(requestGetCurrentUserAvatarImageURL())
            axios.get(api.domain + 'user-profile/avatar-image', {
            headers: {
                'Authorization': 'Bearer ' + localStorage.getItem('lustars_token'),
                'Access-Control-Allow-Origin': '*'
            }
        })
        .then(response => {
            dispatch(requestGetCurrentUserAvatarImageURLSuccess(response.data))
        })
        .catch(() => {
            NotificationManager.info('Please upload "Avatar Image" to complete your profile', `Hello ${localStorage.getItem("lustars_user_name")}`, 10000);
        })
    }
}

export function requestGetCurrentUserAvatarImageURL() {
    return {
        type: REQUEST_GET_CURRENT_USER_AVATAR_URL
    }
}

export function requestGetCurrentUserAvatarImageURLSuccess(url) {
    return {
        type: REQUEST_GET_CURRENT_USER_AVATAR_URL_SUCCESS,
        payload: url
    }
}

//*************************** Delete user profile image ***************************

export function deleteUserProfileImage(image) {
    return dispatch => {
        dispatch(requestDeleteUserProfileImage())
        return axios.post(api.domain + 'user-profile/image/delete', image, {
            headers: {
                'Authorization': 'Bearer ' + localStorage.getItem('lustars_token'),
                'Access-Control-Allow-Origin': '*'
            }
        })
        .then(response => {
            dispatch(requestDeleteUserProfileImageSuccess())
            return response
        })
        .catch(err => {
            dispatch(requestDeleteUserProfileImageFail(err))
            return err
        })
    }
}

export function requestDeleteUserProfileImage() {
    return {
        type: DELETE_USER_PROFILE_IMAGE
    }
}

export function requestDeleteUserProfileImageSuccess() {
    return {
        type: DELETE_USER_PROFILE_IMAGE_SUCCESS,
    }
}

export function requestDeleteUserProfileImageFail(error) {
    return {
        type: DELETE_USER_PROFILE_IMAGE_FAIL,
        payload: error
    }
}

//*************************** Upload user profile image ***************************

export function uploadUserProfileImage(imageFormData) {
    return dispatch => {
        dispatch(requestUploadUserProfileImage())

        return axios.post(api.domain + 'user-profile/image/upload', imageFormData, {
            headers: {
                'Authorization': 'Bearer ' + localStorage.getItem('lustars_token'),
                'Access-Control-Allow-Origin': '*'
            }
        })
        .then(response => {
            dispatch(requestUploadUserProfileImageSuccess())
            return response
        })
        .catch(err => {
            dispatch(requestUploadUserProfileImageFail(err))
            return err
        })
    }
}

export function requestUploadUserProfileImage() {
    return {
        type: UPLOAD_USER_PROFILE_IMAGE
    }
}

export function requestUploadUserProfileImageSuccess() {
    return {
        type: UPLOAD_USER_PROFILE_IMAGE_SUCCESS,
    }
}

export function requestUploadUserProfileImageFail(error) {
    return {
        type: UPLOAD_USER_PROFILE_IMAGE_FAIL,
        payload: error
    }
}

//*************************** Delete country language ***************************

export function deleteUserCountryLanguage(newValues) {
    return dispatch => {
        dispatch(deleteLanguage(newValues))
    }
}

export function deleteLanguage(newValues) {
    return {
        type: DELETE_COUNTRY_LANGUAGE,
        payload: newValues
    }
}

//*************************** Add country language ***************************

export function addUserCountryLanguage(newValues) {
    return dispatch => {
        dispatch(addLanguage())
        dispatch(addLanguageSuccess(newValues))
    }
}

export function addLanguage() {
    return {
        type: ADD_COUNTRY_LANGUAGE
    }
}

export function addLanguageSuccess(newValues) {
    return {
        type: ADD_COUNTRY_LANGUAGE_SUCCESS,
        payload: newValues
    }
}

//*************************** Update profile geaolocation ***************************

export function updateUserProfileGeaolocation(geolocation) {
    return dispatch => {
        dispatch(requestupdateUserProfileGeaolocation())

        axios.post(api.domain + 'user-profile/geolocation/update', geolocation, {
            headers: {
                'Authorization': 'Bearer ' + localStorage.getItem('lustars_token'),
                'Access-Control-Allow-Origin': '*'
            }
        })
        .then(response => {
            dispatch(requestupdateUserProfileGeaolocationSuccess())
            return response
        })
        .catch(err => {
            dispatch(requestupdateUserProfileGeaolocationFail(err))
            return err
        })
    }
}

export function requestupdateUserProfileGeaolocation() {
    return {
        type: UPDATE_USER_PROFILE_GEOLOCATION
    }
}

export function requestupdateUserProfileGeaolocationSuccess() {
    return {
        type: UPDATE_USER_PROFILE_GEOLOCATION_SUCCESS,
    }
}

export function requestupdateUserProfileGeaolocationFail(error) {
    return {
        type: UPDATE_USER_PROFILE_GEOLOCATION_FAIL,
        payload: error
    }
}

//*************************** Get my user profile preview short data ***************************

export function getUserProfileShortPreviewData() {
    return dispatch => {
        dispatch(requestMyUserProfileShortPreviewData())

        axios.get(api.domain + 'user-profile/short-preview-data', {
            headers: {
                'Authorization': 'Bearer ' + localStorage.getItem('lustars_token'),
            }
        })
        .then(response => {
            dispatch(requestMyUserProfileShortPreviewDataSuccess(response.data))
        })
        .catch(err => {
            dispatch(requestUserProfileShortPreviewDataFail(err))

        })
    }
}

export function requestMyUserProfileShortPreviewData() {
    return {
        type: REQUEST_MY_PROFILE_SHORT_DATA
    }
}

export function requestMyUserProfileShortPreviewDataSuccess(data) {
    return {
        type: REQUEST_MY_PROFILE_SHORT_DATA_SUCCESS,
        payload: data
    }
}

export function requestUserProfileShortPreviewDataFail(error) {
    return {
        type: REQUEST_MY_PROFILE_SHORT_DATA_FAIL,
        payload: error
    }
}

//*************************** Get my user profile details ***************************

export function getMyUserProfileDetails() {
    return dispatch => {
        dispatch(requestMyUserProfileDetails())

        axios.get(api.domain + 'user-profile/current', {
            headers: {
                'Authorization': 'Bearer ' + localStorage.getItem('lustars_token'),
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
                    'Authorization': 'Bearer ' + localStorage.getItem('lustars_token'),
                }
            })
            .then(response => {
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

//*************************** Update user profile bolean field ***************************

export function updateUserProfileBoleanField(newProfileData) {
    return dispatch => {
        console.log(newProfileData)
        dispatch(updateBoleanFieldSuccess(newProfileData))
    }
}

export function updateBoleanFieldSuccess(newProfileData) {
    return {
        type: UPDATE_USER_PROFILE_BOOLEAN_FIELD_SUCCESS,
        payload: newProfileData
    }
}


//*************************** Upload avatar image ***************************

export function uploadAvatarImage(formdData) {
    return dispatch => {
        dispatch(requestUploadAvatarImage(formdData))

        return axios.post(api.domain + 'user-profile/avatar-image/upload', formdData, {
            headers: {
                'Authorization': 'Bearer ' + localStorage.getItem('lustars_token'),
            }
        })
        .then(response => {
            dispatch(requestUploadAvatarImageSuccess())
            return response
        })
        .catch(err => {
            dispatch(requestUploadAvatarImageFail(err))
            return err
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

//*************************** Update user profile text field ***************************

export function updateUserProfileTextField(newProfileData) {
    return dispatch => {
        dispatch(editTextField())
        dispatch(editTextFieldSuccess(newProfileData))
    }
}

export function editTextField() {
    return {
        type: UPDATE_USER_PROFILE_TEXT_FIELD
    }
}

export function editTextFieldSuccess(newProfileData) {
    return {
        type: UPDATE_USER_PROFILE_TEXT_FIELD_SUCCESS,
        payload: newProfileData
    }
}

//*************************** Get user location from GOOGLE ***************************

export function getUserLocationFromGOOGLE(latitude, longitude) {
    return axios.post('https://maps.googleapis.com/maps/api/geocode/json?latlng=' + latitude + ',' + longitude + '&sensor=fase&key=AIzaSyCReeeqP32sURxShaQ2XHxtirN6AWGDkAY')
}
