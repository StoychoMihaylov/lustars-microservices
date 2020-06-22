import { applyMiddleware, combineReducers, compose, createStore } from 'redux'
import thunk from 'redux-thunk'
import { connectRouter, routerMiddleware } from 'connected-react-router'

import accountReducer from './reducers/accountReducer'
import profileReducer from './reducers/profileReducer'


export default function configureStore (history, initialState) {

  const reducers = {
    account: accountReducer,
    profile: profileReducer
  }

  const middleware = [
    thunk,
    routerMiddleware(history)
  ];

  const enhancers = []
  const isDevelopment = process.env.NODE_ENV === 'development'
  if (isDevelopment && typeof window !== 'undefined' && window.devToolsExtension) {
    enhancers.push(window.devToolsExtension());
  }

  const rootReducer = combineReducers({
    ...reducers,
    router: connectRouter(history)
  });

  return createStore(
    rootReducer,
    initialState,
    compose(applyMiddleware(...middleware), ...enhancers)
  );
}
