import {
    REQUEST_MY_PROFILE_DETAILS,
    REQUEST_MY_PROFILE_DETAILS_SUCCESS,
    REQUEST_MY_PROFILE_DETAILS_FAIL
} from '../../constants/profileActionTypes'

const initialState = {
    userProfileDetails: {},

    isLoading: false,
    error: false
}

const profileReducer = (state, action) => {
    state = state || initialState

    switch (action.type) {
        case REQUEST_MY_PROFILE_DETAILS:
            return {
                ...state,
                isLoading: true
            }
        case REQUEST_MY_PROFILE_DETAILS_SUCCESS:
            return {
                ...state,
                userProfileDetails: action.payload,
                isLoading: false,
            }
        case REQUEST_MY_PROFILE_DETAILS_FAIL:
            return {
                ...state,
                error: action.payload
            }
        default:
            return state
    }
}

export default profileReducer