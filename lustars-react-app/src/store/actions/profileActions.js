import {
    REQUEST_MY_PROFILE_DETAILS,
    REQUEST_MY_PROFILE_DETAILS_SUCCESS,
    REQUEST_MY_PROFILE_DETAILS_FAIL
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

