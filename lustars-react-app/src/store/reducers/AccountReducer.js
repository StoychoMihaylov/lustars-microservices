import {
    REQUEST_REGISTER_NEW_ACCOUNT,
    REQUEST_REGISTER_NEW_ACCOUNT_SUCCESS,
    REQUEST_REGISTER_NEW_ACCOUNT_FAIL
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
        default:
            return state
    }
}

export default accountReducer