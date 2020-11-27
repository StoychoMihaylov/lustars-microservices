export async function changeObjectBooleanField(field, newValue, oldState) {
    let newState = Object.assign({}, oldState)

    switch (field) {
        case 'howOftenSmoke':
            newState.doingSport = newValue
            return newState
        case 'haveKids':
            newState.haveKids = newValue
            return newState
        case 'wantToHaveKids':
            newState.wantKids = newValue
            return newState
        case 'drinkAlcohol':
            newState.drinkAlcohol = newValue
            return newState
        case 'smoker':
            newState.smoker = newValue
            return newState
        case 'doSport':
            newState.doSport = newValue
            return newState
        default:
            return null
    }
}