import * as React from 'react';
import { Link, NavLink } from 'react-router-dom';
import Cookies from 'universal-cookie';
import axios from 'axios';

interface State {
    loggedIn: boolean;
    username: string;
}

export class NavMenu extends React.Component<{}, {}> {
    timer: any;
    constructor(props) {
        super(props);
        this.state = {
            loggedIn: false
        }
        this.handleLogout = this.handleLogout.bind(this);
        this.checkIfLoggedIn = this.checkIfLoggedIn.bind(this);
    }

    componentDidMount() {
        this.timer = setInterval(() => this.checkIfLoggedIn(), 100);
    }

    checkIfLoggedIn() {
        const cookies = new Cookies();
        let loggedIn = false;
        let username = cookies.get('username');
        if (username)
            loggedIn = true;
        this.setState({ loggedIn, username });
    }

    componentWillUnmount() {
        clearInterval(this.timer);
    }

    handleLogout() {
        const cookies = new Cookies();
        cookies.remove('username', { path: '/' });
        //TODO: clear session once we have one
        /*axios.post(`/api/Authentication/Logout`)
            .then(res => {
                let result = res.data;

                cookies.set('sessionId', '0', { path: '/' });
                this.setState({ loggedIn: false });
            });*/
    }

    public render() {
        return <div className='main-nav'>
            <div className='navbar navbar-inverse'>
                <div className='navbar-header'>
                    <button type='button' className='navbar-toggle' data-toggle='collapse' data-target='.navbar-collapse'>
                        <span className='sr-only'>Toggle navigation</span>
                        <span className='icon-bar'></span>
                        <span className='icon-bar'></span>
                        <span className='icon-bar'></span>
                    </button>
                    <Link className='navbar-brand' to={'/'}>GREEDY</Link>
                </div>
                <div className='clearfix'></div>
                <div className='navbar-collapse collapse'>
                    <ul className='nav navbar-nav' data-toggle="collapse" data-target=".navbar-collapse">
                        <li>
                            <NavLink to={'/'} exact activeClassName='active'>
                                <span className='glyphicon glyphicon-home'></span> Home
                            </NavLink>
                        </li>
                        {
                            (this.state as any).loggedIn ?
                                <li>
                                    <NavLink to={'/counter'} activeClassName='active'>
                                        <span className='glyphicon glyphicon-education'></span> Counter
                            </NavLink>
                                </li> : null
                        }
                        {
                            (this.state as any).loggedIn ?
                                <li>
                                    <NavLink to={'/fetchdata'} activeClassName='active'>
                                        <span className='glyphicon glyphicon-th-list'></span> Fetch data
                            </NavLink>
                                </li> : null
                        }
                        {
                            (this.state as any).loggedIn ?
                                <li className='widenSpace' /> : null
                        }
                        {
                            (this.state as any).loggedIn ? <li>
                                <NavLink to={'/'} activeClassName='inactive'>
                                    <span className='glyphicon glyphicon-user' /> Hello, {(this.state as any).username}!
                                </NavLink>
                                <NavLink to={'/'} onClick={() => this.handleLogout()} activeClassName='inactive'>
                                    <span className='glyphicon glyphicon-log-out' /> Logout
                                </NavLink>
                            </li> : null
                        }
                    </ul>
                </div>
            </div>
        </div>;
    }
}
