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
                    <img className="profile-details-image-avatar" src={ api.imageAPI + profile.avatarImage } alt="" />
                    <div className="intro-bar-element">
                        <div className="profile-details-name">{ profile.name } { profile.lastName }</div>
                        <div>
                            Likes: 3604 &#10084;
                        </div>
                    </div>
                    <div className="intro-bar-element">
                        <div className="profile-details-label">Location</div>
                        { profile.geoLocation !== undefined ? profile.geoLocation.city : null},
                        { profile.geoLocation !== undefined ? profile.geoLocation.country : null}
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
                <hr/>
                <div>
                    <div className="profile-details-label">Mood</div>
                    <div>{ profile.feelInMood }</div>
                </div>
                <hr/>
                <div>
                    <div className="profile-details-label">Looking for</div>
                    <div>Here to meet with { profile.lookingFor } between { profile.partnerAgeRangeFrom } and { profile.partnerAgeRangeTo }</div>
                </div>
                <hr/>
                <div>
                    <div className="profile-details-label">About me</div>
                    <div>Gernder: { profile.gender }</div>
                    <div>From: { profile.fromCity }, { profile.fromCountry }</div>
                    <div>Merital status: { profile.meritalStatus }</div>
                    <div>More about me: { profile.biography }</div>
                    <div>weight: { profile.weight }</div>
                    <div>Work: { profile.work } </div>
                    <div>Figure: { profile.figure }</div>
                    <div>Kids: { profile.haveKids }</div>
                    <div>Height:{ profile.height }</div>
                </div>
                <hr/>
                    <div>
                        <div className="profile-details-label">Education</div>
                        <div>Degree: { profile.educationDegree }</div>
                        <div>University: { profile.university}</div>
                    </div>
                {
                    profile.languages !== undefined && profile.languages.length > 0
                        ?   <hr/>
                        :   null
                }
                {
                    profile.languages !== undefined && profile.languages.length > 0
                        ?   <div>
                                <div className="profile-details-label">Languages:</div>
                                { laguages }
                            </div>
                        :   null
                }
                {
                    profile.drinkAlcohol === true || profile.smoker === true
                        ?   <hr/>
                        : null
                }
                {
                    profile.drinkAlcohol === true || profile.smoker === true
                        ?   <div>
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
                <hr/>
                <div>
                    <div className="profile-details-label">Desired partner</div>
                    <div>Parner drink: { profile.partnerDrinkAlcohol }</div>
                    <div>Partner figure: { profile.partnerFigure }</div>
                    <div>Partner has kids: { profile.partnerHaveKids }</div>
                    <div>Partner Smoke: { profile.partnerSmoke }</div>
                    <div>Want kids: { profile.wantToHaveKids }</div>
                </div>
                <hr/>
            </div>

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
