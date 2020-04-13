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
import { api } from '../../constants/endpoints'
import axios from 'axios'

//*************************** Register account actions ***************************

export function registerAccount(userModel) {
    return dispatch => {
        dispatch(requestRegisterAccount())

        return axios.post(api.domain + 'account/register', userModel)
            .then(response => {
                dispatch(requestRegisterAccountSuccess(response))
                return response
            })
            .catch(err => {
                dispatch(requestRegisterAccountFail(err))
                return err
            })
    }
}

export function requestRegisterAccount() {
    return {
        type: REQUEST_REGISTER_NEW_ACCOUNT
    }
}

export function requestRegisterAccountSuccess(data) {
    return {
        type: REQUEST_REGISTER_NEW_ACCOUNT_SUCCESS,
        payload: data
    }
}

export function requestRegisterAccountFail(error) {
    return {
        type: REQUEST_REGISTER_NEW_ACCOUNT_FAIL,
        payload: error
    }
}

//*************************** Login account actions ***************************

export function loginAccount(userModel) {
    return dispatch => {
        dispatch(requestLoginAccount())
        return axios.post(api.domain + 'account/login', userModel)
        .then(response => {
            dispatch(requestLoginAccountSuccess(response))
            return response
        })
        .catch(err => {
            dispatch(requestLoginAccountFail(err))
            return err
        })
    }
}

export function requestLoginAccount() {
    return {
        type: REQUEST_ACCOUNT_LOGIN
    }
}

export function requestLoginAccountSuccess(data) {
    return {
        type: REQUEST_ACCOUNT_LOGIN_SUCCESS,
        payload: data
    }
}

export function requestLoginAccountFail(error) {
    return {
        type: REQUEST_ACCOUNT_LOGIN_FAIL,
        payload: error
    }
}

//*************************** Logout account actions ***************************

export function logoutAccount(userModel) {
    return dispatch => {
        dispatch(requestLogoutAccount())
        return axios.post(api.domain + 'account/logout', userModel, {
            headers: {
                'Authorization': 'Bearer ' + localStorage.getItem('lustars_token')
            }
        })
        .then(response => {
            dispatch(requestLogoutAccountSuccess(response))
            return response
        })
        .catch(err => {
            dispatch(requestLogoutAccountFail(err))
            return err
        })
    }
}

export function requestLogoutAccount() {
    return {
        type: REQUEST_ACCOUNT_LOGOUT
    }
}

export function requestLogoutAccountSuccess(data) {
    return {
        type: REQUEST_ACCOUNT_LOGOUT_SUCCESS
    }
}

export function requestLogoutAccountFail(error) {
    return {
        type: REQUEST_ACCOUNT_LOGOUT_FAIL,
        payload: error
    }
}
