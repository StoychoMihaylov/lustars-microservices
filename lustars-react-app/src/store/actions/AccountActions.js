import {
    REQUEST_REGISTER_NEW_ACCOUNT,
    REQUEST_REGISTER_NEW_ACCOUNT_SUCCESS,
    REQUEST_REGISTER_NEW_ACCOUNT_FAIL,
    REQUEST_ACCOUNT_LOGIN,
    REQUEST_ACCOUNT_LOGIN_SUCCESS,
    REQUEST_ACCOUNT_LOGIN_FAIL
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