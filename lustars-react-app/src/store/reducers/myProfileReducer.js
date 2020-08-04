import {
    REQUEST_MY_PROFILE_DETAILS,
    REQUEST_MY_PROFILE_DETAILS_SUCCESS,
    REQUEST_MY_PROFILE_DETAILS_FAIL,
    REQUEST_EDIT_MY_PROFILE_DETAILS,
    REQUEST_EDIT_MY_PROFILE_DETAILS_SUCCESS,
    REQUEST_EDIT_MY_PROFILE_DETAILS_FAIL,
    UPDATE_USER_PROFILE_BOOLEAN_FIELD_SUCCESS,
    UPDATE_USER_PROFILE_TEXT_FIELD,
    UPDATE_USER_PROFILE_TEXT_FIELD_SUCCESS,
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
    REQUEST_SOME_PROFILE_DETAILS,
    REQUEST_SOME_PROFILE_DETAILS_SUCCESS,
    REQUEST_SOME_PROFILE_DETAILS_FAIL,
    REQUEST_LIKE_USER_PROFILE,
    REQUEST_LIKE_USER_PROFILE_SUCCESS,
    REQUEST_LIKE_USER_PROFILE_FAIL,
} from '../../constants/actionTypes/myProfileActionTypes'

const initialState = {
    currentUserAvatarImgURL: "",
    currentUserProfileDetails: {},
    userProfileDetails: {},

    isLoading: false,
    error: false
}

const myProfileReducer = (state, action) => {
    state = state || initialState

    switch (action.type) {
        case REQUEST_LIKE_USER_PROFILE:
            return {
                ...state,
                isLoading: true,
            }
        case REQUEST_LIKE_USER_PROFILE_SUCCESS:
            return {
                ...state,
                isLoading: false
            }
        case REQUEST_LIKE_USER_PROFILE_FAIL:
            return {
                ...state,
                isLoading: false,
                error: action.payload
            }
        case REQUEST_SOME_PROFILE_DETAILS:
            return {
                ...state,
                isLoading: true,
            }
        case REQUEST_SOME_PROFILE_DETAILS_SUCCESS:
            return {
                ...state,
                userProfileDetails: action.payload,
                isLoading: false
            }
        case REQUEST_SOME_PROFILE_DETAILS_FAIL:
            return {
                isLoading: false,
                error: action.payload
            }
        case REQUEST_GET_CURRENT_USER_AVATAR_URL:
        case REQUEST_GET_CURRENT_USER_AVATAR_URL_SUCCESS:
            return {
                ...state,
                currentUserAvatarImgURL: action.payload
            }
        case DELETE_USER_PROFILE_IMAGE:
        case DELETE_USER_PROFILE_IMAGE_SUCCESS:
            return {
                ...state
            }
        case DELETE_USER_PROFILE_IMAGE_FAIL:
            return {
                ...state,
                error: action.payload
            }
        case UPLOAD_USER_PROFILE_IMAGE:
        case UPLOAD_USER_PROFILE_IMAGE_SUCCESS:
            return {
                ...state
            }
        case UPLOAD_USER_PROFILE_IMAGE_FAIL:
            return {
                ...state,
                error: action.payload
            }
        case DELETE_COUNTRY_LANGUAGE:
            return {
                ...state,
                currentUserProfileDetails: action.payload
            }
        case ADD_COUNTRY_LANGUAGE:
            return {
                ...state
            }
        case ADD_COUNTRY_LANGUAGE_SUCCESS:
            return {
                ...state,
                currentUserProfileDetails: action.payload
            }
        case UPDATE_USER_PROFILE_GEOLOCATION:
        case UPDATE_USER_PROFILE_GEOLOCATION_SUCCESS:
        case UPDATE_USER_PROFILE_GEOLOCATION_FAIL:
            return {
                ...state,
                error: action.payload
            }
        case REQUEST_MY_PROFILE_SHORT_DATA:
            return {
                ...state,
                isLoading: true
            }
        case REQUEST_MY_PROFILE_SHORT_DATA_SUCCESS:
            return {
                ...state,
                UserProfileShortPreviewData: action.payload,
                isLoading: false
            }
        case REQUEST_MY_PROFILE_SHORT_DATA_FAIL:
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
                currentUserProfileDetails: action.payload,
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
        case UPDATE_USER_PROFILE_BOOLEAN_FIELD_SUCCESS:
            return {
                ...state,
                currentUserProfileDetails: action.payload
            }
        case UPDATE_USER_PROFILE_TEXT_FIELD:
        case UPDATE_USER_PROFILE_TEXT_FIELD_SUCCESS:
            return {
                ...state,
                currentUserProfileDetails: action.payload
            }
        default:
            return state
    }
}

export default myProfileReducer