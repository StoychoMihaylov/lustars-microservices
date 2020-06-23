import {
    REQUEST_GET_PEOPLE_NEARBY,
    REQUEST_GET_PEOPLE_NEARBY_SUCCESS,
    REQUEST_GET_PEOPLE_NEARBY_SUCCESS_FAIL
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