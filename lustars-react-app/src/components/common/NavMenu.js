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
              <span><img className="navbar-menu-icons" src={process.env.PUBLIC_URL + '/nearby.PNG'} alt="" /></span> People nearby
            </button>
          </div>
        </li>
        <li>
          <div className="navbar-nav-attribute">
            <button onClick={() => this.props.push('/people-nearby') } className="cursor-pointer navbar-link">
              <span><img className="navbar-menu-icons" src={process.env.PUBLIC_URL + '/messages.PNG'} alt="" /></span> Messages
            </button>
          </div>
        </li>
        <li>
          <div className="navbar-nav-attribute">
            <button onClick={() => this.props.push('/people-nearby') } className="cursor-pointer navbar-link">
              <span><img className="navbar-menu-icons" src={process.env.PUBLIC_URL + '/matched.PNG'} alt="" /></span>  Matched
            </button>
          </div>
        </li>
        <li>
          <div className="navbar-nav-attribute">
            <button onClick={() => this.props.push('/who-liked-me') } className="cursor-pointer navbar-link">
              <span><img className="navbar-menu-icons" src={process.env.PUBLIC_URL + '/likes.PNG'} alt="" /></span> Liked you
            </button>
          </div>
        </li>
        <li>
          <div className="navbar-nav-attribute">
            <button onClick={() => this.props.push('/who-visited-me') } className="cursor-pointer navbar-link">
              <span><img className="navbar-menu-icons" src={process.env.PUBLIC_URL + '/visitors.PNG'} alt="" /></span> Visitors
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
