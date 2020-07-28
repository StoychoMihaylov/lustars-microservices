import React from 'react'
import FormAccountLogout from '../authentication/FormAccountLogout'
import '../../styles/components/common/NavMenu.css'


export default class NavMenu extends React.Component {
  constructor (props) {
    super(props);
  }

  render () {
    return (
        <header>
            <nav className="navbar">
                <div className="navbar-logo" to="/">Lustars logo!</div>
                <ul className="navbar-ul-elements">
                  <li>
                    <div className="navbar-nav-attribute">
                      <button className="cursor-pointer navbar-link">link-1</button>
                    </div>
                  </li>
                  <li>
                    <div className="navbar-nav-attribute">
                      <button className="cursor-pointer navbar-link">link-2</button>
                    </div>
                  </li>
                  <li>
                    <div className="navbar-nav-attribute">
                      <button className="cursor-pointer navbar-link">link-3</button>
                    </div>
                  </li>
                  <li>
                    <div>
                      <span className="navbar-vertical-line"></span>
                    </div>
                  </li>
                  <li>
                    <FormAccountLogout />
                  </li>
                </ul>
            </nav>
        </header>
    )
  }
}
