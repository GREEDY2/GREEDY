import * as React from 'react';
import { Link, NavLink } from 'react-router-dom';
import axios from 'axios';
import Constants from './Constants';
import { clearDb } from './DatabaseFunctions';

export class NavMenu extends React.Component {
    state = {
        collapseNav: ''
    }

    constructor() {
        super();
        this.updateWindowDimensions = this.updateWindowDimensions.bind(this);
    }

    componentDidMount() {
        this.updateWindowDimensions();
        window.addEventListener('resize', this.updateWindowDimensions);
    }

    componentWillUnmount() {
        window.removeEventListener('resize', this.updateWindowDimensions);
    }

    updateWindowDimensions() {
        if (this.state.collapseNav != '' && window.innerWidth > 767) {
            this.setState({ collapseNav: '' });
        }
        else if (this.state.collapseNav != 'navbar-collapse collapse' && window.innerWidth < 768) {
            this.setState({ collapseNav: 'navbar-collapse collapse' });
        }
    }

    handleLogout = () => {
        localStorage.removeItem("auth");
        clearDb('greedy', ['categories', 'myItems']);
    }

    public render() {
        let token = localStorage.getItem("auth");
        let loggedIn = false;
        if (token) {
            try {
                let base64Url = token.split('.')[1];
                let base64 = base64Url.replace('-', '+').replace('_', '/');
                let tokenJson = JSON.parse(window.atob(base64));
                loggedIn = true;
                var username = tokenJson.unique_name;
            }
            catch (e) {
                this.handleLogout();
            }
        }
        else {
            loggedIn = false;
        }
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
                    <div className={this.state.collapseNav}>
                        <ul
                            className='nav navbar-nav'
                            data-toggle="collapse"
                            data-target=".navbar-collapse">
                            <li>
                                {
                                    loggedIn ?
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
                                loggedIn ?
                                    <li>
                                        <NavLink to={'/fetchdata'} activeClassName='active'>
                                            <span className='glyphicon glyphicon-th-list'></span>
                                            All my items
                                        </NavLink>
                                    </li> : null
                            }
                            {
                                loggedIn ?
                                    <li>
                                        <NavLink to={'/statistics'} activeClassName='active'>
                                            <span className='glyphicon glyphicon-stats'></span>
                                            Statistics
                                        </NavLink>
                                    </li> : null
                            }
                            {
                                loggedIn ?
                                    <li className='widenSpace' /> : null
                            }
                            {
                                loggedIn ?
                                    <li>
                                        <NavLink to={'/'} activeClassName='inactive'>
                                            <span className='glyphicon glyphicon-user' />
                                            Hello, {username}!
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
