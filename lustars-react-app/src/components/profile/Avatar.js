import React, { Component } from "react"
import { Form } from 'reactstrap'
import { api } from '../../constants/endpoints'
import ReactCrop from 'react-image-crop'
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
        this.setState({ crop });
    }

    onImageLoaded = image => {
        this.imageRef = image
    }

    onChange = (crop) => {
        this.setState({ crop });
    }

    onCropComplete = crop => {
        if (this.imageRef && crop.width && crop.height) {

            this.getCroppedImg(this.imageRef, crop)
        }
    }

    getCroppedImg(image, crop) {
        const canvas = document.createElement("canvas");
        const scaleX = image.naturalWidth / image.width;
        const scaleY = image.naturalHeight / image.height;
        canvas.width = crop.width;
        canvas.height = crop.height;
        const ctx = canvas.getContext("2d");

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
            blob.name = 'cropped.jpeg';
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
            u8arr = new Uint8Array(n);

        while(n--){
            u8arr[n] = bstr.charCodeAt(n);
        }

        let croppedImage = new File([u8arr], filename, {type:mime});

        this.setState({
            croppedImage: croppedImage,
        })
    }

    handleSubmit = e => {
        e.preventDefault()

        var imgUrl = URL.createObjectURL(this.state.croppedImage)

        this.setState({
            croppedImageUrl: imgUrl,
            showImageCropper: false
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
        }

        // TO DO: show format error
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
                            className="imageToCrop"
                            src={src}
                            crop={crop}
                            onImageLoaded={this.onImageLoaded}
                            onComplete={this.onCropComplete}
                            onChange={this.onCropChange}
                            />)
                        }
                    </div>
                    <button type="submit" className="cropImgBtn" >Crop image</button>
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
                        className="avatarImgUploadBtn"
                        onChange={ this.chooseImageToUpload.bind(this) }
                    />

                    {
                        this.state.croppedImageUrl !== null
                            ? <img className="avatarImage" src={this.state.croppedImageUrl} alt="" />
                            : <img className="avatarImage" src={process.env.PUBLIC_URL + '/empty-avatar.png'} alt="" />
                    }

                </label>
            </div>

        return (
            <div>
                { imageCropper }

                {
                    imageUser === null || imageUser === ""
                    ? emptyImage
                    : <img className="avatarImage" src={require(api + imageUser)} alt="" />
                }
            </div>
        )
    }
}

export default Avatar