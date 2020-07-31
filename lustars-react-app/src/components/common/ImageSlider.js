import React, { Component } from 'react'
import { api } from '../../constants/endpoints'
import '../../styles/components/common/ImageSlider.css'

export default class ImageSlider extends Component {
    constructor(props) {
        super(props)

        this.state = {
            position: this.props.position
        }
    }

    moveImageBarOnTheLeft() {
        let images = this.props.images !== undefined ? this.props.images : null
        if (images !== null && this.state.position > 0) {
            this.setState({
                position: (this.state.position - 1),
            })
        }
    }

    moveImageBarOnTheRight() {
        let images = this.props.images !== undefined ? this.props.images : null
        if (images !== null && this.state.position < images.length - 1) {
            this.setState({
                position: (this.state.position + 1),
            })
        }
    }

    closeOverlay() {
        this.props.closeCurrentOverlay()
    }

    render() {
        let imageCount = this.props.images !== undefined  && this.props.images !== null ? this.props.images.length : 0

        let overlay = this.props.images !== undefined && this.props.images !== null
            ?   <div className="image-slider-overlay">
                    <button
                        id="image-slider-left-angular"
                        className="image-slider-angular-bttn"
                        onClick={ this.moveImageBarOnTheLeft.bind(this) }>&#10094;
                    </button>
                    <img
                        className="image-slider-image"
                        src={ api.imageAPI + this.props.images[this.state.position].url }
                        alt=""
                    />
                    <span className="image-slider-camera-img-number">&#128247; { this.state.position + 1}/{ imageCount }
                        <button onClick={ this.closeOverlay.bind(this) } >X</button>
                    </span>
                    <button
                        id="image-slider-right-angular"
                        className="image-slider-angular-bttn"
                        onClick={ this.moveImageBarOnTheRight.bind(this) }>&#10095;
                    </button>
                </div>
            :   null

        return (
            <div>
                { overlay }
            </div>
        )
    }
}