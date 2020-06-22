import React, { Component } from 'react'
import { NotificationManager} from 'react-notifications'

class PeopleNearbyPage extends Component {

    createNotification = (type) => {
        return () => {
          switch (type) {
            case 'info':
              NotificationManager.info('Info message', 'Some title', 3000)
              break
            case 'success':
              NotificationManager.success('Success message', 'Title here', 3000)
              break
            case 'warning':
              NotificationManager.warning('Warning message', 'Close after 3000ms', 3000)
              break
            case 'error':
              NotificationManager.error('Error message', 'Click me!', 5000, () => {
                alert('callback')
              })
              break
          }
        }
    }

    render() {
        return (
            <div>
                <h1>Test</h1>
                <button className='btn btn-info'
                onClick={this.createNotification('info')}>Info
                </button>
                <hr/>
                <button className='btn btn-success'
                onClick={this.createNotification('success')}>Success
                </button>
                <hr/>
                <button className='btn btn-warning'
                onClick={this.createNotification('warning')}>Warning
                </button>
                <hr/>
                <button className='btn btn-danger'
                onClick={this.createNotification('error')}>Error
                </button>
            </div>
        )
    }
}

export default PeopleNearbyPage