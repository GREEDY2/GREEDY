import * as React from 'react';
import Cookies from 'universal-cookie';
import { UserLogin } from './UserLogin';
import { UserRegistration } from './UserRegistration';
import { Switch, Route } from 'react-router-dom';
import { ForgotPassword } from './ForgotPassword';
import Constants from './Constants';
import axios from 'axios';

interface State {
    isAuthenticated: boolean;
}

interface Props {
    children: any;
}

export class Authorization extends React.Component<Props, State> {
    state = {
        isAuthenticated: true
    }
    constructor(props) {
        super(props);
    }

    componentDidMount() {
        const cookies = new Cookies();
        let Username = cookies.get(Constants.cookieUsername);
        if (Username) {
            let SessionId = cookies.get(Constants.cookieSessionId);
            let role = {
                Username,
                SessionId
            }
            axios.put(Constants.httpRequestBasePath + "api/Authentication", role)
                .then(response => {

                }).catch(e => {
                    cookies.remove(Constants.cookieUsername, { path: '/' });
                    cookies.remove(Constants.cookieSessionId, { path: '/' });
                    this.setState({});
                })
        }
    }

    public render() {
        const cookies = new Cookies();
        let Username = cookies.get(Constants.cookieUsername);
        let SessionId = cookies.get(Constants.cookieSessionId);
        if (Username && SessionId) {
            return (
                <div>
                    {this.props.children}
                </div>
            );
        }
        return (
            <Switch>
                <Route exact path='/registration' component={UserRegistration} />
                <Route exact path='/forgot' component={ForgotPassword} />
                <Route path='/' component={UserLogin} />
            </Switch>
        );
    }
}