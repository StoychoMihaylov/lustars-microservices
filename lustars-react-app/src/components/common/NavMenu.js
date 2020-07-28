import React from 'react'
import { connect } from 'react-redux'
import { push, goBack } from "connected-react-router"
import FormAccountLogout from '../authentication/FormAccountLogout'
import '../../styles/components/common/NavMenu.css'


class NavMenu extends React.Component {
  constructor (props) {
    super(props);
  }

  render () {

    let activities =
      <span>
        <li>
          <div>
            <span className="navbar-vertical-line"></span>
          </div>
        </li>
        <li>
          <div className="navbar-nav-attribute">
            <button onClick={() => this.props.push('/people-nearby') } className="cursor-pointer navbar-link">
              <span>&#127757;</span> People nearby
            </button>
          </div>
        </li>
        <li>
          <div className="navbar-nav-attribute">
            <button onClick={() => this.props.push('/people-nearby') } className="cursor-pointer navbar-link">
              <span>💬</span> Messages
            </button>
          </div>
        </li>
        <li>
          <div className="navbar-nav-attribute">
            <button onClick={() => this.props.push('/people-nearby') } className="cursor-pointer navbar-link">
              <span>&#128525;</span>  Matched
            </button>
          </div>
        </li>
        <li>
          <div className="navbar-nav-attribute">
            <button onClick={() => this.props.push('/people-nearby') } className="cursor-pointer navbar-link">
              <span>&#10084;</span> Liked you
            </button>
          </div>
        </li>
        <li>
          <div className="navbar-nav-attribute">
            <button onClick={() => this.props.push('/people-nearby') } className="cursor-pointer navbar-link">
              <span>&#128065;</span> Visitors
            </button>
          </div>
        </li>
        <li>
          <div>
            <span className="navbar-vertical-line"></span>
          </div>
        </li>
      </span>
    return (
        <header>
            <nav className="navbar">
                <div className="navbar-logo" to="/">Lustars logo!</div>
                <ul className="navbar-ul-elements">
                  {
                    localStorage.getItem('lustars_token') !== null
                      ? activities
                      : null
                  }
                  <li>
                    <FormAccountLogout />
                  </li>
                </ul>
            </nav>
        </header>
    )
  }
}

const mapDispatchToProps = dispatch => {
  return {
      // Navigation
      goBack: () => dispatch(goBack()),
      push: (url) => dispatch(push(url))
  }
}

export default connect(null, mapDispatchToProps)(NavMenu)
