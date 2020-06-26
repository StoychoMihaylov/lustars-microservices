import { applyMiddleware, combineReducers, compose, createStore } from 'redux'
import thunk from 'redux-thunk'
import { connectRouter, routerMiddleware } from 'connected-react-router'

// Reducers
import accountReducer from './reducers/accountReducer'
import myProfileReducer from './reducers/myProfileReducer'
import peopleNearbyReducer from './reducers/peopleNearbyReducer'


export default function configureStore (history, initialState) {

  const reducers = {
    account: accountReducer,
    myProfile: myProfileReducer,
    peopleNearby: peopleNearbyReducer
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
