import * as React from 'react';
import { UserLogin } from './UserLogin';
import { UserRegistration } from './UserRegistration';
import { Switch, Route } from 'react-router-dom';
import { ForgotPassword } from './ForgotPassword';
import { GetCredentialsFromCookies, RemoveCredentialsFromCookies } from './HelperClass';
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
        let credentials = GetCredentialsFromCookies();
        if (credentials.Username && credentials.SessionId) {
            axios.put(Constants.httpRequestBasePath + "api/Authentication", credentials)
                .then(response => {

                }).catch(e => {
                    RemoveCredentialsFromCookies();
                    this.setState({});
                })
        }
    }

    public render() {
        let credentials = GetCredentialsFromCookies();
        if (credentials.Username && credentials.SessionId) {
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