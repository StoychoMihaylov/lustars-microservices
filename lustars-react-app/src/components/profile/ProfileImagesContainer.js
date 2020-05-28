import React, { Component } from "react"
import { connect } from "react-redux"
import { Form } from 'reactstrap'
import { api } from '../../constants/endpoints'
import {
    infoNotification,
    successfulNotification,
    errorNotification
} from '../../store/actions/eventNotifications'
import { uploadUserProfileImage } from '../../store/actions/profileActions'
import { getMyUserProfileDetails } from '../../store/actions/profileActions'
import '../../styles/components/profile/ProfileImagesContainer.css'

class ProfileImagesContainer extends Component {
    constructor(props) {
        super(props)

        this.state = {
            previewImage: null
        }
    }

    selectUserProfileImage(images) {
        if (images.target.files[0].type === "image/jpeg") {
            let targetImg = images.target.files[0]
            let newImg = URL.createObjectURL(targetImg)

            this.setState({
                imageFile: targetImg,
                previewImage: newImg
            })
        }
    }

    uploadUserProfileImage(event) {
        event.preventDefault()

        let formData = new FormData();
        formData.append("image", this.state.imageFile)

        this.props.uploadUserProfileImage(formData)
            .then(response => {
                if (response.status === 201) {
                    this.setState({
                        previewImage: null
                    })

                    // Will update and rerender the state with the new image
                    this.props.getMyUserProfileDetails()
                    this.props.successfulNotification("Avatar image uploaded!")
                } else {
                    this.props.errorNotification("Something went wrong! Please check your connection!")
                }
            })
    }

    addUserProfileImageAsAvatar() {
        console.log("Add user profile image as avatar")
    }

    showImageOperationOptions(event) {
        let id = event.target.id
        if (id !== "") {
            this.refs[id].style.display = 'block'
        }
    }

    hideImageOperationOptions(event) {
        let id = event.target.id
        if (id !== "") {
            this.refs[id].style.display = 'none'
        }
    }

    preventSubmitImageUpload() {
        this.setState({
            previewImage: null
        })
    }

    render () {
        let imagePreviewer = this.state.previewImage !== null
            ?   <div className="img-previewer-overlay">
                    <Form onSubmit={ this.uploadUserProfileImage.bind(this) }>
                        <img className="image-preview" src={this.state.previewImage} alt="" />
                        <br/>
                        <button
                            type="submit"
                            className="upload-img-bttn"
                        >Upload
                        </button>
                        <button
                            type="button"
                            className="exit-uplad-img-bttn"
                            onClick={ this.preventSubmitImageUpload.bind(this) }
                        >&#9587;
                        </button>
                    </Form>
                </div>
            :   null

        let userProfileImages =  this.props.userProfileImages !== undefined && this.props.userProfileImages !== null
            ?   this.props.userProfileImages.map((image, index) => {
                    return (
                        <label
                            key={index}
                            id={"image" + index}
                            className="user-profile-image-label"
                            onMouseEnter={ this.showImageOperationOptions.bind(this) }
                            onMouseLeave={ this.hideImageOperationOptions.bind(this) }
                            onClick={ this.addUserProfileImageAsAvatar.bind(this) }
                        >
                            <div className="user-profile-image-container">
                                <img
                                    ref={"image" + index}
                                    className="add-image-as-avatar"
                                    src={process.env.PUBLIC_URL + '/gear-image.png'}
                                    alt=""
                                />
                                <img
                                    id={"image" + index}
                                    className="user-profile-image"
                                    src={ api.imageAPI + image.url }
                                    onMouseEnter={ this.showImageOperationOptions.bind(this) }
                                    onMouseLeave={ this.hideImageOperationOptions.bind(this) }
                                    alt=""
                                />
                            </div>
                        </label>
                    )
                })
            :   null

        console.log(this.props.userProfileImages)
        return (
            <div className="user-profile-images-container">
                { userProfileImages }
                { imagePreviewer }
                <label>
                    <input
                        type="file"
                        multiple={false}
                        className="upload-img-input"
                        onChange={this.selectUserProfileImage.bind(this)}
                    />
                    <img className="user-profile-image" src={process.env.PUBLIC_URL + '/add-image.png'} alt="" />
                </label>
            </div>
        )
    }
}

const mapDispatchToProps = dispatch => {
    return {
        uploadUserProfileImage: (formData) => dispatch(uploadUserProfileImage(formData)),
        getMyUserProfileDetails: () => dispatch(getMyUserProfileDetails()),

         // Notifications
        infoNotification: (message) => dispatch(infoNotification(message)),
        successfulNotification: (message) => dispatch(successfulNotification(message)),
        errorNotification: (message) => dispatch(errorNotification(message))
    }
}

export default connect(null, mapDispatchToProps)(ProfileImagesContainer)