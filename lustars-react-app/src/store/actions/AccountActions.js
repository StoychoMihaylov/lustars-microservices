import {
    REQUEST_REGISTER_NEW_ACCOUNT,
    REQUEST_REGISTER_NEW_ACCOUNT_SUCCESS,
    REQUEST_REGISTER_NEW_ACCOUNT_FAIL
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