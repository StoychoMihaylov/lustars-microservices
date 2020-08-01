import React, { Component } from "react"
import { connect } from "react-redux"
import { Form } from 'reactstrap'
import { NotificationManager} from 'react-notifications';
import { api } from '../../constants/endpoints'
import { uploadUserProfileImage, deleteUserProfileImage, getMyUserProfileDetails } from '../../store/actions/myProfileActions'
import '../../styles/components/profile/MyProfileImagesContainer.css'

class MyProfileImagesContainer extends Component {
    constructor(props) {
        super(props)

        this.state = {
            previewUploadImage: null,
            imageSettingsPreview: null,

            // show more images
            imagesLength: 9,
            showMoreImagesBttn: "see more...",
            showMoreImages: false
        }
    }

    selectUserProfileImage(images) {
        if (images.target.files[0].type === "image/jpeg") {
            let targetImg = images.target.files[0]
            let newImg = URL.createObjectURL(targetImg)

            this.setState({
                imageFile: targetImg,
                previewUploadImage: newImg
            })
        } else {
            NotificationManager.error('Only "jpeg" image format is allowed!', 'Ops...', 5000)
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
                        previewUploadImage: null
                    })

                    // Will update and rerender the state with the new image
                    this.props.getMyUserProfileDetails()
                    NotificationManager.success('Image has been uploaded successfully', 'Uploaded!', 3000)
                } else {
                    NotificationManager.error('Something went wrong! Please check your connection!', 'Error!', 5000)
                }
            })
    }

    closeImageSettingPreview() {
        this.setState({
            imageSettingsPreview: null
        })
    }

    openImagePreviewSettings(event) {
        this.setState({
            imageSettingsPreview: this.props.userProfileImages[event.target.id.substr(5)]
        })
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
            previewUploadImage: null
        })
    }

    deleteUserProfileImage() {

        let image = {
            id: this.state.imageSettingsPreview.id
        }

        this.props.deleteUserProfileImage(image)
            .then(response => {
                if (response.status === 200) {
                    this.setState({
                        imageSettingsPreview: null
                    })

                    // Will update and rerender the state with the new image
                    this.props.getMyUserProfileDetails()
                    NotificationManager.success('Image deleted!', '', 3000)
                } else {
                    this.props.errorNotification("Something went wrong! Please check your connection!")
                    NotificationManager.error('Conection propblem!', 'Ops...', 3000)
                }
            })
    }

    showAllUserImages() {
        if(this.state.showMoreImages === false) {
            this.setState({
                imagesLength: this.props.userProfileImages.length,
                showMoreImagesBttn: "see less",
                showMoreImages: true
            })
        } else {
            this.setState({
                imagesLength: 9,
                showMoreImagesBttn: "see more...",
                showMoreImages: false
            })
        }
    }

    render () {
        let imageUploadOverlay = this.state.previewUploadImage !== null
            ?   <div className="img-previewer-overlay">
                    <Form onSubmit={ this.uploadUserProfileImage.bind(this) }>
                        <img className="image-preview" src={ this.state.previewUploadImage } alt="" />
                        <br/>
                        <span className="profile-preview-bttns-overlay">
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
                        </span>
                    </Form>
                </div>
            :   null

        let userProfileImages =  this.props.userProfileImages !== undefined && this.props.userProfileImages !== null
            ?   this.props.userProfileImages.slice(0, this.state.imagesLength).map((image, index) => {
                    return (
                        <label
                            key={index}
                            id={"image" + index}
                            value={image.id}
                            className="user-profile-image-label"
                            onMouseEnter={ this.showImageOperationOptions.bind(this) }
                            onMouseLeave={ this.hideImageOperationOptions.bind(this) }
                            onClick={ this.openImagePreviewSettings.bind(this) }
                        >
                            <div className="user-profile-image-container">
                                <img
                                    ref={"image" + index}
                                    className="image-setting-icon"
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

        let imagePreviewSettingOverlay = this.state.imageSettingsPreview !== null
            ?   <div className="img-previewer-overlay">
                    <img
                        className="image-preview"
                        src={ api.imageAPI + this.state.imageSettingsPreview.url }
                        alt=""
                    />
                    <br/>
                    <div className="img-previewer-settins-bttns">
                        <button
                            type="button"
                            className="delete-img-bttn"
                            onClick={ this.deleteUserProfileImage.bind(this) }
                        >&#128465;
                        </button>
                        <button
                            type="button"
                            className="exit-uplad-img-bttn"
                            onClick={ this.closeImageSettingPreview.bind(this) }
                        >&#9587;
                    </button>
                    </div>
                </div>
            :   null

        let addImageButton = <label>
                                <input
                                    type="file"
                                    multiple={false}
                                    className="upload-img-input"
                                    onChange={this.selectUserProfileImage.bind(this)}
                                />
                                <img className="add-user-image-bttn" src={process.env.PUBLIC_URL + '/add-image.png'} alt="" />
                            </label>

        let miniAddImageButton = <label>
                                <input
                                    type="file"
                                    multiple={false}
                                    className="upload-img-input"
                                    onChange={this.selectUserProfileImage.bind(this)}
                                />
                                <img className="mini-add-user-image-bttn" src={process.env.PUBLIC_URL + '/add-image.png'} alt="" />
                            </label>

        return (
            <div className="user-profile-images-container">
                <div>
                    <div>
                        { userProfileImages }
                        {
                            this.props.userProfileImages !== undefined && this.props.userProfileImages.length < 9
                                ? addImageButton
                                : null
                        }
                    </div>
                    {
                        this.props.userProfileImages !== undefined && this.props.userProfileImages.length >= 9
                            ?   miniAddImageButton
                            :   null
                    }
                    {
                        this.props.userProfileImages !== undefined && this.props.userProfileImages.length > 9
                            ?   <button
                                    onClick={ this.showAllUserImages.bind(this) }
                                    className="show-more-images-bttn">{ this.state.showMoreImagesBttn }
                                </button>
                            :   null
                    }

                    { imageUploadOverlay }
                    { imagePreviewSettingOverlay }
                </div>
            </div>
        )
    }
}

const mapDispatchToProps = dispatch => {
    return {
        deleteUserProfileImage: (image) => dispatch(deleteUserProfileImage(image)),
        uploadUserProfileImage: (formData) => dispatch(uploadUserProfileImage(formData)),
        getMyUserProfileDetails: () => dispatch(getMyUserProfileDetails())
    }
}

export default connect(null, mapDispatchToProps)(MyProfileImagesContainer)