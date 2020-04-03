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
    CHANGE_USER_EMAIL_SUBSCRIBED_SUCCESS
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
        case REQUEST_EDIT_MY_PROFILE_DETAILS:
            return {
                ...state,
                isLoading: true
            }
        case REQUEST_EDIT_MY_PROFILE_DETAILS_SUCCESS:
            return {
                ...state,
                userProfileDetails: action.payload,
                isLoading: false,
            }
        case REQUEST_EDIT_MY_PROFILE_DETAILS_FAIL:
            return {
                ...state,
                error: action.payload
            }
        case CHANGE_USER_PROFILE_ACTIVE_ON_OFF:
            return {
                ...state,
                userProfileDetails: action.payload
            }
        case CHANGE_USER_PROFILE_ACTIVE_ON_OFF:
        case CHANGE_USER_PROFILE_ACTIVE_ON_OFF_SUCCESS:
            return {
                ...state,
                userProfileDetails: action.payload
            }
        case CHANGE_USER_EMAIL_SUBSCRIBED:
        case CHANGE_USER_EMAIL_SUBSCRIBED_SUCCESS:
            return {
                ...state,
                userProfileDetails: action.payload
            }

        default:
            return state
    }
}

export default profileReducer