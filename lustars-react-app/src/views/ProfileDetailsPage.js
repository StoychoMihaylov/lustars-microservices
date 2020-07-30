import React, { Component } from 'react'
import { connect } from "react-redux"
import { api } from '../constants/endpoints'
import { getMyUserProfileDetails } from '../store/actions/myProfileActions'
import '../styles/views/ProfileDetailsPage.css'

class ProfileDetailsPage extends Component {
    constructor(props) {
        super(props)

        this.state = {
            imagesStart: 0,
            imagesEnd: 0
        }
    }

    moveImageBarOnTheLeft() {
        let images = this.props.profile.images !== undefined ? this.props.profile.images : null
        if (images !== null && images.length > 6 && this.state.imagesStart > 0) {
            this.setState({
                imagesStart: this.state.imagesStart - 1,
                imagesEnd: this.state.imagesEnd - 1
            })
        }
    }

    moveImageBarOnTheRight() {
        let images = this.props.profile.images !== undefined ? this.props.profile.images : null
        if (images !== null && images.length > 6 &&  this.state.imagesEnd <= images.length) {
            this.setState({
                imagesStart: this.state.imagesStart + 1,
                imagesEnd: this.state.imagesEnd + 1
            })
        }
    }


    //Calculate how many images to show
    adjustHowManyImagesToShow() {
        console.log(window.innerWidth)
        if (window.innerWidth > 1200) {
            this.setState({
                imagesEnd: this.state.imagesStart + 5
            })
        } else if (window.innerWidth < 1202 && window.innerWidth > 992) {
            this.setState({
                imagesEnd: this.state.imagesStart + 4
            })
        } else if (window.innerWidth < 992 && window.innerWidth > 768) {
            this.setState({
                imagesEnd: this.state.imagesStart + 2
            })
        } else if (window.innerWidth < 768 && window.innerWidth > 515) {
            this.setState({
                imagesEnd: this.state.imagesStart + 1
            })
        } else if (window.innerWidth < 515 && window.innerWidth > 364) {
            this.setState({
                imagesEnd: this.state.imagesStart + 1
            })
        } else if (window.innerWidth < 364) {
            this.setState({
                imagesEnd: 0
            })
        }
    }

    //Remove event listener
    componentWillUnmount() {
        window.removeEventListener("resize", this.adjustHowManyImagesToShow.bind(this));
    }

    componentDidMount() {
        this.props.getMyUserProfileDetails(this.props.match.params.id) // Takes the id from url

        //Add event listener
        this.adjustHowManyImagesToShow();
        window.addEventListener("resize", this.adjustHowManyImagesToShow.bind(this));
    }

    render() {
        let profile = this.props.profile

        let laguages = profile.languages !== undefined
            ?   profile.languages.map((language, index) => {
                    return (
                        <div key={index} >{ language.name }</div>
                    )
                })
            :   null

        let displayProfile =
            <div>
                <div className="profile-details-intro-bar">
                    <div className="profile-details-avatar-container">
                        <img className="profile-details-image-avatar" src={ api.imageAPI + profile.avatarImage } alt="" />
                    </div>
                    <div className="intro-bar-element">
                        <div className="profile-details-name">{ profile.name } { profile.lastName }</div>
                        <div>
                            Likes: { profile.likes } &#10084;
                        </div>
                    </div>
                    <div className="intro-bar-element">
                        <div className="profile-details-label">Location</div>
                        { profile.geoLocation !== undefined && profile.geoLocation !== null ? profile.geoLocation.city : null},
                        { profile.geoLocation !== undefined && profile.geoLocation !== null ? profile.geoLocation.country : null}
                    </div>
                    <div className="intro-bar-element">
                        <button className="profile-details-like-bttn">&#10084;</button>
                        <button className="profile-details-start-chating-bttn" ><span className="chat-box-icon">💬</span></button>
                    </div>
                </div>
                <hr/>
                <div className="profile-details-image-container">
                    <button id="left-angular" className="profile-details-angular-image-bar" onClick={ this.moveImageBarOnTheLeft.bind(this) }>&#10094;</button>
                    {
                        profile.images !== undefined && profile.images !== null
                        ?   profile.images.slice(this.state.imagesStart, this.state.imagesEnd).map((image, index) => {
                                return (
                                    <img key={index} className="profile-details-image" src={ api.imageAPI + image.url } alt="" />
                                )
                            })
                        :   null
                    }
                    <span className="profile-details-camera-img-number">&#128247; { profile.images !== undefined ? profile.images.length : null}</span>
                    <button id="right-angular" className="profile-details-angular-image-bar" onClick={ this.moveImageBarOnTheRight.bind(this) }>&#10095;</button>
                </div>
                {
                    profile.feelInMood && profile.feelInMood !== null
                        ?   <div>
                                <hr/>
                                <div className="profile-details-label">Mood</div>
                                <div>{ profile.feelInMood }</div>
                            </div>
                        :   null
                }
                <div>
                    <hr/>
                    <div className="profile-details-label">Looking for</div>
                    <div>
                        Here to meet with { profile.lookingFor }
                        {
                            profile.partnerAgeRangeFrom !== null &&  profile.partnerAgeRangeTo !== null
                            ?   <span>between { profile.partnerAgeRangeFrom } and { profile.partnerAgeRangeTo }</span>
                            :   null
                        }
                    </div>
                </div>
                <div>
                    <hr/>
                    <div className="profile-details-label">About me</div>
                    {
                        profile.biographyAndInterests !== null
                            ?   <div>More about me: { profile.biographyAndInterests }</div>
                            :   null
                    }
                    <div>Gender: { profile.gender }</div>
                    {
                        profile.fromCity !== null || profile.fromCountry !== null
                            ?   <div>From: { profile.fromCity }, { profile.fromCountry }</div>
                            :   null
                    }
                    {
                        profile.meritalStatus !== null
                            ?   <div>Merital status: { profile.meritalStatus }</div>
                            :   null
                    }
                    {
                        profile.height  !== null
                            ?   <div>Height: { profile.height }</div>
                            :   null
                    }
                    {
                        profile.weight !== null
                            ?   <div>weight: { profile.weight }</div>
                            :   null
                    }
                    {
                        profile.work !== null
                            ?   <div>Work: { profile.work } </div>
                            :   null
                    }
                    {
                        profile.figure !== null
                            ?   <div>Figure: { profile.figure }</div>
                            :   null
                    }
                    {
                        profile.haveKids === true
                            ?   <div>I Have kids</div>
                            :   <div>I Don't have kids</div>
                    }
                </div>
                {
                    profile.educationDegree !== null || profile.university !== null
                        ?   <div>
                            <hr/>
                                <div className="profile-details-label">Education</div>
                                <div>Degree: { profile.educationDegree }</div>
                                { profile.university !== null
                                    ? <div>University: { profile.university }</div>
                                    : null
                                }
                            </div>
                        :   null
                }
                {
                    profile.languages !== undefined && profile.languages.length > 0
                        ?   <div>
                                <hr/>
                                <div className="profile-details-label">Languages:</div>
                                { laguages }
                            </div>
                        :   null
                }
                {
                    profile.drinkAlcohol === true || profile.smoker === true
                        ?   <div>
                                <hr/>
                                <div className="profile-details-label">Habits and lifestyle</div>
                                {
                                    profile.drinkAlcohol === true
                                    ?   <div>I drink alcohol { profile.howOftenDrinkAlcohol }</div>
                                    :   null
                                }
                                {
                                    profile.smoker === true
                                    ?   <div>I smoke: { profile.howOftenSmoke }</div>
                                    :   null
                                }
                            </div>
                        : null
                }
                {
                    profile.partnerVisualAppearance !== false ||
                    profile.trust !== false ||
                    profile.sex !== false ||
                    profile.financialStability !== false ||
                    profile.communicationAndUnderstanding !== false ||
                    profile.sameInterests !== false ||
                    profile.oppositeAttracs !== false ||
                    profile.growingFamily !== false ||
                    profile.loveForAnimals !== false ||
                    profile.shareSameReligion !== false ||
                    profile.keepTraditions !== false
                        ?   <div>
                                <hr/>
                                <div className="profile-details-label">Lustars partner preferences</div>
                                <div>Most important in a relation for me:</div>
                                { profile.partnerVisualAppearance !== false ? <div>Partner visual appearance</div> : null }
                                { profile.trust !== false ? <div>Trust</div> : null }
                                { profile.sex !== false ? <div>Sex</div> : null }
                                { profile.financialStability !== false ? <div>Financial stability</div> : null }
                                { profile.communicationAndUnderstanding !== false ? <div>communication and understanding</div> : null }
                                { profile.sameInterests !== false ? <div>To share same interests</div> : null }
                                { profile.oppositeAttracs !== false ? <div>Opposite attracs</div> : null }
                                { profile.growingFamily !== false ? <div>Growing family</div> : null }
                                { profile.loveForAnimals !== false ? <div>Love for animals</div> : null }
                                { profile.shareSameReligion !== false ? <div>To share same religion</div> : null }
                                { profile.keepTraditions !== false ? <div>Keep traditions alive</div> : null }
                            </div>
                        :   null
                }
                <div>
                    <hr/>
                    <div className="profile-details-label">Desired partner</div>
                    {
                        profile.partnerFigure !== null ? <div>Partner figure: { profile.partnerFigure }</div> : null
                    }
                    {
                        profile.partnerHaveKids === true ? <div>Partner who has kids</div> : <div>Parter who has no kids</div>
                    }
                    {
                        profile.partnerDrinkAlcohol === true ? <div>Parner who drinks</div> : <div>Partner who doesn't drink</div>
                    }
                    {
                        profile.partnerSmoke === null ? <div>Partner who Smokes</div> : <div>Partner who doesn't Smoke</div>
                    }
                </div>
            </div>
        console.log(profile)
        return (

            <div className="profile-details-container">
                {
                    this.props.isLoading == false
                        ?   profile !== undefined && profile !== null
                                ? displayProfile
                                : null
                        :   <h1>LOADING...</h1>
                }
            </div>
        )
    }
}

const mapStateToProps = state => {
    return {
        profile: state.myProfile.userProfileDetails,
        isLoading: state.myProfile.isLoading,
        error: state.myProfile.error
    }
}

const mapDispatchToProps = dispatch => {
    return {
        getMyUserProfileDetails: (id) => dispatch(getMyUserProfileDetails(id)),
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(ProfileDetailsPage)
