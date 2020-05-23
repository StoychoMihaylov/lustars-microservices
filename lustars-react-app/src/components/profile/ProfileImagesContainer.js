import React, { Component } from "react"
import { connect } from "react-redux"
import { api } from '../../constants/endpoints'
import {
    infoNotification,
    successfulNotification,
    errorNotification
} from '../../store/actions/eventNotifications'
import ImageCropper from '../common/ImageCropper'
import '../../styles/components/profile/ProfileImagesContainer.css'

class ProfileImagesContainer extends Component {
    constructor(props) {
        super(props)
    }

    uploadUserProfileImage(croppedImgUrl, croppedImage) {
        console.log(croppedImgUrl)
        console.log(croppedImage)
    }

    render () {
        let emptyImageTemplate = <img className="user-profile-image" src={process.env.PUBLIC_URL + '/add-image.png'} alt="" />

        return (
            <div>
                {
                    this.props.userProfileImages !== undefined
                    ?   this.props.userProfileImages.map((image, index) => {
                            return (
                                <img key={index} className="user-profile-image" src={ api.imageAPI + image } alt="" />
                            )
                        })
                    :   null
                }
                <ImageCropper
                    emptyImageTemplate={emptyImageTemplate}
                    returnCroppedUrlAndCroppedImage={(croppedImgUrl, croppedImage) => this.uploadUserProfileImage(croppedImgUrl, croppedImage)}
                />
            </div>
        )
    }
}

const mapDispatchToProps = dispatch => {
    return {
         // Notifications
        infoNotification: (message) => dispatch(infoNotification(message)),
        successfulNotification: (message) => dispatch(successfulNotification(message)),
        errorNotification: (message) => dispatch(errorNotification(message))
    }
}

export default connect(null, mapDispatchToProps)(ProfileImagesContainer)