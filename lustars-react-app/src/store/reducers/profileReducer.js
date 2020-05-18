import {
    REQUEST_MY_PROFILE_DETAILS,
    REQUEST_MY_PROFILE_DETAILS_SUCCESS,
    REQUEST_MY_PROFILE_DETAILS_FAIL,
    REQUEST_EDIT_MY_PROFILE_DETAILS,
    REQUEST_EDIT_MY_PROFILE_DETAILS_SUCCESS,
    REQUEST_EDIT_MY_PROFILE_DETAILS_FAIL,
    UPDATE_USER_PROFILE_BOOLEAN_FIELD,
    UPDATE_USER_PROFILE_BOOLEAN_FIELD_SUCCESS,
    UPDATE_USER_PROFILE_BOOLEAN_FIELD_FAIL,
    UPDATE_USER_PROFILE_TEXT_FIELD,
    UPDATE_USER_PROFILE_TEXT_FIELD_SUCCESS,
    UPDATE_USER_PROFILE_TEXT_FIELD_FAIL,
    UPDATE_USER_PROFILE_GEOLOCATION,
    UPDATE_USER_PROFILE_GEOLOCATION_SUCCESS,
    UPDATE_USER_PROFILE_GEOLOCATION_FAIL
} from '../../constants/profileActionTypes'

const initialState = {
    userProfileDetails: {},

    isLoading: false,
    error: false
}

const profileReducer = (state, action) => {
    state = state || initialState

    switch (action.type) {
        case UPDATE_USER_PROFILE_GEOLOCATION:
        case UPDATE_USER_PROFILE_GEOLOCATION_SUCCESS:
        case UPDATE_USER_PROFILE_GEOLOCATION_FAIL:
            return {
                ...state,
                error: action.payload
            }
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
                isLoading: false,
            }
        case REQUEST_EDIT_MY_PROFILE_DETAILS_FAIL:
            return {
                ...state,
                error: action.payload
            }
        case UPDATE_USER_PROFILE_BOOLEAN_FIELD:
        case UPDATE_USER_PROFILE_BOOLEAN_FIELD_SUCCESS:
            return {
                ...state,
                userProfileDetails: action.payload,
                refresh: true
            }
        case UPDATE_USER_PROFILE_BOOLEAN_FIELD_FAIL:
            return {
                ...state,
                error: action.payload
            }
        case UPDATE_USER_PROFILE_TEXT_FIELD:
        case UPDATE_USER_PROFILE_TEXT_FIELD_SUCCESS:
            return {
                ...state,
                userProfileDetails: action.payload,
                refresh: true
            }
        case UPDATE_USER_PROFILE_TEXT_FIELD_FAIL:
            return {
                ...state,
                error: action.payload
            }

        default:
            return state
    }
}

export default profileReducer