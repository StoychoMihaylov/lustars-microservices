import React, { Component } from "react"
import { connect } from "react-redux"
import { Form } from 'reactstrap'
import { api } from '../../constants/endpoints'
import ReactCrop from 'react-image-crop'
import { NotificationManager} from 'react-notifications'
import { uploadAvatarImage } from '../../store/actions/myProfileActions'
import 'react-image-crop/dist/ReactCrop.css'
import '../../styles/components/profile/Avatar.css'

class Avatar extends Component {
    constructor(props) {
        super(props)

        this.state = {
            previewAvatarImage: null,
            src: null,
            crop: {
                unit: "%",
                width: 30,
                aspect: 14/16
            },
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

        let formData = new FormData();
        formData.append("image", this.state.croppedImage)

        this.props.uploadAvatarImage(formData)
            .then(response => {
                if (response.status === 201) {
                    console.log(response)
                    this.setState({
                        croppedImageUrl: imgUrl,
                        showImageCropper: false
                    })
                    localStorage.setItem("avatar-img-url", response.data)
                    NotificationManager.success('Avatar image uploaded!', 'Congrats!', 3000)
                } else {
                    NotificationManager.error('Something went wrong! Please check your connection!', 'Please try again!', 5000, () => {
                        alert('Something went wrong! Please check your connection!')
                      })
                }
            })
    }

    async chooseImageToUpload(image) {

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
            NotificationManager.error('The image should be in "jpeg" format!', 'Invalid image format!', 5000, () => {
                alert('Please upload images only in jpg(jpeg) format!');
              })
        }
    }

    render() {
        const { crop, profile_pic, src } = this.state

        let url = this.props.imageUrl
        let imageUser =  url !== null && url !== undefined ? url : null

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

        let emptyImage =
            <div>
                <label>
                    <input
                        type="file"
                        multiple={false}
                        id='profile_pic'
                        value={profile_pic}
                        className="avatar-img-upload-btn"
                        onChange={ this.chooseImageToUpload.bind(this) }
                    />

                    {
                        this.state.croppedImageUrl !== null
                            ? <img className="avatar-image" src={this.state.croppedImageUrl} alt="" />
                            : <img className="avatar-image" src={process.env.PUBLIC_URL + '/empty-avatar.png'} alt="" />
                    }

                </label>
            </div>

        let avatarImage =
            <div>
                <label>
                    <input
                        type="file"
                        multiple={false}
                        id='profile_pic'
                        value={profile_pic}
                        className="avatar-img-upload-btn"
                        onChange={ this.chooseImageToUpload.bind(this) }
                    />

                    {
                        this.state.croppedImageUrl !== null
                            ? <img className="avatar-image" src={this.state.croppedImageUrl} alt="" />
                            : <img className="avatar-image" src={ api.imageAPI + imageUser } alt="" />
                    }

                </label>
            </div>

        return (
            <div>
                { imageCropper }

                {
                    imageUser === null || imageUser === ""
                    ? emptyImage
                    : avatarImage
                }
            </div>
        )
    }
}

const mapDispatchToProps = dispatch => {
    return {
        uploadAvatarImage: (imageFile) => dispatch(uploadAvatarImage(imageFile))
    }
}

export default connect(null, mapDispatchToProps)(Avatar)