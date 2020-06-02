import React, { Component } from "react"
import { connect } from "react-redux"
import { Form } from 'reactstrap'
import ReactCrop from 'react-image-crop'
import {
    infoNotification,
    successfulNotification,
    errorNotification
} from '../../store/actions/eventNotifications'
import '../../styles/components/common/ImageCropper.css'

class ImageCropper extends Component {
    constructor(props){
        super(props)

        this.state = {
            src: null,
            crop: {
                unit: "%",
                width: 30,
                aspect: 14/16
            },
            croppedImage: null,
            croppedImageUrl: null,
            showImageCropper: false
        }
    }

    onCropChange = (crop) => {
        this.setState({ crop })
    }

    onImageLoaded = image => {
        this.imageRef = image
    }

    onChange = (crop) => {
        this.setState({ crop })
    }

    onCropComplete = crop => {
        if (this.imageRef && crop.width && crop.height) {

            this.getCroppedImg(this.imageRef, crop)
        }
    }

    getCroppedImg(image, crop) {
        const canvas = document.createElement("canvas")
        const scaleX = image.naturalWidth / image.width
        const scaleY = image.naturalHeight / image.height
        canvas.width = crop.width
        canvas.height = crop.height
        const ctx = canvas.getContext("2d")

        ctx.drawImage(
            image,
            crop.x * scaleX,
            crop.y * scaleY,
            crop.width * scaleX,
            crop.height * scaleY,
            0,
            0,
            crop.width,
            crop.height
         )

        // As a blob
        const reader  = new FileReader()
        canvas.toBlob(blob => {
            blob.name = 'cropped.jpeg'
            reader.readAsDataURL(blob)
            reader.onloadend = () => {
                this.dataURLtoFile(reader.result, 'cropped.jpg')
            }
        }, 'image/jpeg', 1);
    }

    dataURLtoFile(dataurl, filename) {
        let arr = dataurl.split(','),
            mime = arr[0].match(/:(.*?);/)[1],
            bstr = atob(arr[1]),
            n = bstr.length,
            u8arr = new Uint8Array(n)

        while(n--){
            u8arr[n] = bstr.charCodeAt(n)
        }

        let croppedImage = new File([u8arr], filename, {type:mime})

        this.setState({
            croppedImage: croppedImage,
        })
    }

    handleSubmit = e => {
        e.preventDefault()

        var imgUrl = URL.createObjectURL(this.state.croppedImage)
        this.props.returnCroppedUrlAndCroppedImage(imgUrl, this.state.croppedImage)
    }

    async croppImage(image) {

        if (image.target.files[0].type === "image/jpeg") {
            const fileReader = new FileReader()

            fileReader.onloadend = () => {
                this.setState({src: fileReader.result })
            }

            fileReader.readAsDataURL(image.target.files[0])

            this.setState({
                showImageCropper: true
            })
        } else {
            this.props.errorNotification('The image should be in "jpeg" format!')
        }
    }

    render () {
        const { crop, profile_pic, src } = this.state

        let imageCropper = this.state.showImageCropper === true
            ?
            <div className="overlay">
                <Form onSubmit={this.handleSubmit}>
                    <label htmlFor="profile_pic"></label>
                    <div>
                        {src && (
                            <ReactCrop
                            className="image-to-crop"
                            src={src}
                            crop={crop}
                            onImageLoaded={this.onImageLoaded}
                            onComplete={this.onCropComplete}
                            onChange={this.onCropChange}
                            />)
                        }
                    </div>
                    <button type="submit" className="crop-img-btn" >Crop image</button>
                </Form>
            </div>
            : ""

        let cropperStarter =
            <div>
                <button onChange={ () => this.croppImage(this.props.imageToCropp) }></button>
            </div>

        return (
            <div>
                { imageCropper }
                { cropperStarter }
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

export default connect(null, mapDispatchToProps)(ImageCropper)