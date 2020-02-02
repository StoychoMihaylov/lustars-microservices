import {
    REQUEST_REGISTER_NEW_ACCOUNT,
    REQUEST_REGISTER_NEW_ACCOUNT_SUCCESS,
    REQUEST_REGISTER_NEW_ACCOUNT_FAIL,
    REQUEST_ACCOUNT_LOGIN,
    REQUEST_ACCOUNT_LOGIN_SUCCESS,
    REQUEST_ACCOUNT_LOGIN_FAIL,
    REQUEST_ACCOUNT_LOGOUT,
    REQUEST_ACCOUNT_LOGOUT_SUCCESS,
    REQUEST_ACCOUNT_LOGOUT_FAIL
} from '../../constants/accountActionTypes'

const initialState = {
    credentials: {},

    isLoading: false,
    error: false
}

const accountReducer = (state, action) => {
    state = state || initialState

    switch (action.type) {
        case REQUEST_REGISTER_NEW_ACCOUNT:
            return {
                ...state,
                isLoading: true
            }
        case REQUEST_REGISTER_NEW_ACCOUNT_SUCCESS:
            return {
                ...state,
                credentials: action.payload,
                isLoading: false,
            }
        case REQUEST_REGISTER_NEW_ACCOUNT_FAIL:
            return {
                ...state,
                error: action.payload
            }
            case REQUEST_ACCOUNT_LOGIN:
                return {
                    ...state,
                    isLoading: true
                }
            case REQUEST_ACCOUNT_LOGIN_SUCCESS:
                return {
                    ...state,
                    credentials: action.payload,
                    isLoading: false,
                }
            case REQUEST_ACCOUNT_LOGIN_FAIL:
                return {
                    ...state,
                    error: action.payload
                }
            case REQUEST_ACCOUNT_LOGOUT:
                return {
                    ...state,
                    isLoading: true
                }
            case REQUEST_ACCOUNT_LOGOUT_SUCCESS:
                return {
                    ...state,
                    isLoading: false,
                }
            case REQUEST_ACCOUNT_LOGOUT_FAIL:
                return {
                    ...state,
                    error: action.payload
                }
        default:
            return state
    }
}

export default accountReducer