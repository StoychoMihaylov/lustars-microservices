import React, { Component } from "react"
import '../../styles/components/common/Footer.css'

class Footer extends Component {
    render() {
        return (
            <footer className="layout-footer">
                <div className="footer-content-container">
                    <p>Some text or maybe a link</p>
                    <p>Some text or maybe a link</p>
                    <p>Some text or maybe a link</p>
                    <p>Some text or maybe a link</p>
                    <h3>Some LOGO 1</h3>
                </div>
                <div className="footer-content-container">
                    <p>Some text or maybe a link</p>
                    <p>Some text or maybe a link</p>
                    <p>Some text or maybe a link</p>
                    <p>Some text or maybe a link</p>
                    <h3>Some LOGO 2</h3>
                </div>
                <div className="footer-content-container">
                    <p>Some text or maybe a link</p>
                    <p>Some text or maybe a link</p>
                    <p>Some text or maybe a link</p>
                    <p>Some text or maybe a link</p>
                    <h3>Some LOGO 3</h3>
                </div>
            </footer>
        )
    }
}

export default Footer