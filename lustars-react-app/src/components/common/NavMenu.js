import React from 'react'
import { Container } from 'reactstrap'
import FormAccountLogout from '../authentication/FormAccountLogout'
import ActivitiesMenu from '../common/ActivitiesMenu'
import '../../styles/components/common/NavMenu.css'


export default class NavMenu extends React.Component {
  constructor (props) {
    super(props);

    this.toggle = this.toggle.bind(this);
    this.state = {
      isOpen: false,
      isActivitiesActive: false
    };
  }

  toggle () {
    this.setState({
      isOpen: !this.state.isOpen
    });
  }

  openActivitiesMenu() {
    if (this.state.isActivitiesActive === false) {
      this.setState({
        isActivitiesActive: true
      })
    } else {
      this.setState({
        isActivitiesActive: false
      })
    }
  }

  render () {
    return (
        <header>
            <nav className="navbar">
                <div className="navbar-logo" to="/">Lustars logo!</div>
                <ul className="navbar-ul-elements">
                  <li className="navbar-ul-elements">
                    <div className="navbar-nav-attribute">
                      <button
                        className="cursor-pointer navbar-link"
                        onClick={ this.openActivitiesMenu.bind(this) }
                        style={{ backgroundColor: this.state.isActivitiesActive ? "lightblue" : "" }}
                        >Activities
                      </button>
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
            <Container>
              {
                this.state.isActivitiesActive === true
                  ? <ActivitiesMenu />
                  : null
              }
            </Container>
        </header>
    )
  }
}
