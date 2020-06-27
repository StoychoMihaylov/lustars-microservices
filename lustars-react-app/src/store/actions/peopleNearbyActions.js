import {
    REQUEST_GET_PEOPLE_NEARBY,
    REQUEST_GET_PEOPLE_NEARBY_SUCCESS,
    REQUEST_GET_PEOPLE_NEARBY_SUCCESS_FAIL,
    REQUEST_GET_PROFILE_NEARBY_DETAILED_DATA,
    REQUEST_GET_PROFILE_NEARBY_DETAILED_DATA_SUCCESS,
    REQUEST_GET_PROFILE_NEARBY_DETAILED_DATA_FAIL
} from '../../constants/actionTypes/peopleNearby'
import { api } from '../../constants/endpoints'
import axios from 'axios'

//*************************** Get people nearby ***************************

export function getPeopleNearby() {
    return dispatch => {
        dispatch(requestPeopleNearby())

        axios.get(api.domain + 'user-profile/people-nearby', {
            headers: {
                'Authorization': 'Bearer ' + localStorage.getItem('lustars_token'),
            }
        })
        .then(response => {
            dispatch(requestPeopleNearbySuccess(response.data))
        })
        .catch(err => {
            dispatch(requestPeopleNearbyFail(err))

        })
    }
}

export function requestPeopleNearby() {
    return {
        type: REQUEST_GET_PEOPLE_NEARBY
    }
}

export function requestPeopleNearbySuccess(data) {
    return {
        type: REQUEST_GET_PEOPLE_NEARBY_SUCCESS,
        payload: data
    }
}

export function requestPeopleNearbyFail(error) {
    return {
        type: REQUEST_GET_PEOPLE_NEARBY_SUCCESS_FAIL,
        payload: error
    }
}

//*************************** Get nearby profile detailed data ***************************

export function getProfileNearbyDetailedData(id) {
    return dispatch => {
        dispatch(requestProfileNearbyDetailedData(id))

        axios.get(api.domain + 'user-profile/profile-nearby-details', {
            headers: {
                'Authorization': 'Bearer ' + localStorage.getItem('lustars_token'),
            }
        })
        .then(response => {
            dispatch(requestProfileNearbyDetailedDataSuccess(response.data))
        })
        .catch(err => {
            dispatch(requestProfileNearbyDetailedDataFail(err))

        })
    }
}

export function requestProfileNearbyDetailedData(id) {
    return {
        type: REQUEST_GET_PROFILE_NEARBY_DETAILED_DATA
    }
}

export function requestProfileNearbyDetailedDataSuccess(data) {
    return {
        type: REQUEST_GET_PROFILE_NEARBY_DETAILED_DATA_SUCCESS,
        payload: data
    }
}

export function requestProfileNearbyDetailedDataFail(error) {
    return {
        type: REQUEST_GET_PROFILE_NEARBY_DETAILED_DATA_FAIL,
        payload: error
    }
}


