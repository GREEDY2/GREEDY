import * as React from 'react';
import { Link, NavLink } from 'react-router-dom';
import axios from 'axios';
import Constants from './Constants';

interface State {
    loggedIn: boolean;
    username: string;
}

export class NavMenu extends React.Component<{}, State> {
    timer: any;
    state = {
        loggedIn: false,
        username: ""
    }

    componentDidMount() {
        this.timer = setInterval(() => this.checkIfLoggedIn(), Constants.checkIfUserlogedInTimer);
    }

    checkIfLoggedIn = () => {
        let token = localStorage.getItem("auth");
        if (token) {
            let base64Url = token.split('.')[1];
            let base64 = base64Url.replace('-', '+').replace('_', '/');
            let tokenJson = JSON.parse(window.atob(base64));
            this.setState({ loggedIn: true, username: tokenJson.unique_name });
        }
        else {
            this.setState({ loggedIn: false });
        }
    }

    componentWillUnmount() {
        clearInterval(this.timer);
    }

    handleLogout = () => {
        localStorage.removeItem("auth");
    }

    public render() {
        return (
            <div className='main-nav'>
                <div className='navbar navbar-inverse'>
                    <div className='navbar-header'>
                        <button
                            type='button'
                            className='navbar-toggle'
                            data-toggle='collapse'
                            data-target='.navbar-collapse'>
                            <span className='sr-only'>Toggle navigation</span>
                            <span className='icon-bar'></span>
                            <span className='icon-bar'></span>
                            <span className='icon-bar'></span>
                        </button>
                        <Link className='navbar-brand' to={'/'}>GREEDY</Link>
                    </div>
                    <div className='clearfix'></div>
                    <div className='navbar-collapse collapse'>
                        <ul
                            className='nav navbar-nav'
                            data-toggle="collapse"
                            data-target=".navbar-collapse">
                            <li>
                                {
                                    this.state.loggedIn ?
                                        <NavLink to={'/'} exact activeClassName='active'>
                                            <span className='glyphicon glyphicon-camera'></span>
                                            Photograph receipt
                                        </NavLink> :
                                        <NavLink to={'/'} exact activeClassName='active'>
                                            <span className='glyphicon glyphicon-log-in'></span>
                                            Login
                                        </NavLink>
                                }
                            </li>
                            {
                                this.state.loggedIn ?
                                    <li>
                                        <NavLink to={'/fetchdata'} activeClassName='active'>
                                            <span className='glyphicon glyphicon-th-list'></span>
                                            All my items
                                        </NavLink>
                                    </li> : null
                            }
                            {
                                this.state.loggedIn ?
                                    <li>
                                        <NavLink to={'/statistics'} activeClassName='active'>
                                            <span className='glyphicon glyphicon-stats'></span>
                                            Statistics
                                        </NavLink>
                                    </li> : null
                            }
                            {
                                this.state.loggedIn ?
                                    <li className='widenSpace' /> : null
                            }
                            {
                                this.state.loggedIn ?
                                    <li>
                                        <NavLink to={'/'} activeClassName='inactive'>
                                            <span className='glyphicon glyphicon-user' />
                                            Hello, {this.state.username}!
                                        </NavLink>
                                        <NavLink
                                            to={'/user'}
                                            activeClassName='inactive'>
                                            <span className='glyphicon glyphicon-wrench' />
                                            My account
                                        </NavLink>
                                        <NavLink
                                            to={'/'}
                                            onClick={() => this.handleLogout()}
                                            activeClassName='inactive'>
                                            <span className='glyphicon glyphicon-log-out' />
                                            Logout
                                        </NavLink>
                                        
                                    </li> : null
                            }
                        </ul>
                    </div>
                </div>
            </div>
        );
    }
}
