import React, { Component } from "react"
import { connect } from "react-redux"

class MyProfileLustarsQuestions extends Component {
    render() {
        return (
            <div>
                <h2>Lustars questions</h2>
                <div>What is most important for you in a relationship ?</div>
                <div>
                    <label for="love">Love</label>
                    <input type="checkbox" id="love" name="love" value={ this.props.profile.love }/>
                    <br/>
                    <label for="trust">Trust</label>
                    <input type="checkbox" id="trust" name="trust" value={ this.props.profile.trust }/>
                    <br/>
                    <label for="sex">Sex</label>
                    <input type="checkbox" id="sex" name="sex" value={ this.props.profile.sex }/>
                    <br/>
                    <label for="financial-stability">Financial stability</label>
                    <input type="checkbox" id="financial-stability" name="financial-stability" value={ this.props.profile.financialStability }/>
                    <br/>
                    <label for="respect-and-understanding">Respect and understanding</label>
                    <input type="checkbox" id="respect-and-understanding" name="respect-and-understanding" value={ this.props.profile.respectAndUnderstanding }/>
                    <br/>
                    <label for="same-interests">Same interests</label>
                    <input type="checkbox" id="same-interests" name="same-interests" value={ this.props.profile.sameInterests }/>
                    <br/>
                    <label for="opposite-attracs">Opposite attracs</label>
                    <input type="checkbox" id="opposite-attracs" name="opposite-attracs" value={ this.props.profile.oppositeAttracs }/>
                    <br/>
                    <label for="growing-family">Growing family</label>
                    <input type="checkbox" id="growing-family" name="growing-family" value={ this.props.profile.growingFamily }/>
                    <br/>
                    <label for="love-for-animals">Love for animals</label>
                    <input type="checkbox" id="love-for-animals" name="love-for-animals" value={ this.props.profile.loveForAnimals }/>
                    <br/>
                    <label for="share-same-religion">Share same religion</label>
                    <input type="checkbox" id="share-same-religion" name="share-same-religion" value={ this.props.profile.shareSameReligion }/>
                    <br/>
                    <label for="keep-traditions">Keep traditions</label>
                    <input type="checkbox" id="keep-traditions" name="keep-traditions" value={ this.props.profile.keepTraditions }/>
                    <br/>
                </div>
            </div>
        )
    }
}

const mapDispatchToProps = dispatch => {
    return {
        updateUserProfileBoleanField: (newValue) => dispatch(updateUserProfileBoleanField(newValue))
    }
}

export default connect(null, mapDispatchToProps)(MyProfileLustarsQuestions)