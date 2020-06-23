import {
    REQUEST_GET_PEOPLE_NEARBY,
    REQUEST_GET_PEOPLE_NEARBY_SUCCESS,
    REQUEST_GET_PEOPLE_NEARBY_SUCCESS_FAIL
} from '../../constants/actionTypes/peopleNearby'

const initialState = {
    peopleNearby: null,

    isLoading: false,
    error: false
}

const peopleNearbyReducer = (state, action) => {
    state = state || initialState

    switch (action.type) {
        case REQUEST_GET_PEOPLE_NEARBY:
            return {
                ...state,
                isLoading: true
            }
        case REQUEST_GET_PEOPLE_NEARBY_SUCCESS:
            return {
                ...state,
                peopleNearby: action.payload,
                isLoading: false,
            }
        case REQUEST_GET_PEOPLE_NEARBY_SUCCESS_FAIL:
            return {
                ...state,
                error: action.payload,
                isLoading: false
            }
        default:
            return state
    }
}

export default peopleNearbyReducer