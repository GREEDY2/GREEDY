import * as React from 'react';
import { Link, NavLink } from 'react-router-dom';
import Cookies from 'universal-cookie';
import axios from 'axios';
import Constants from './Constants';
import { GetCredentialsFromCookies, RemoveCredentialsFromCookies } from './HelperClass';

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
        let credentials = GetCredentialsFromCookies();
        if (credentials.Username && credentials.SessionId) {
            this.setState({ loggedIn: true, username: credentials.Username });
        }
        else {
            this.setState({ loggedIn: false });
        }
    }

    componentWillUnmount() {
        clearInterval(this.timer);
    }

    handleLogout = () => {
        RemoveCredentialsFromCookies();
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
                                <NavLink to={'/'} exact activeClassName='active'>
                                    <span className='glyphicon glyphicon-home'></span>
                                    Home
                                </NavLink>
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
