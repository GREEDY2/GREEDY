import * as React from 'react';
import { UserLogin } from '../User login/UserLogin';
import { UserRegistration } from '../User login/UserRegistration';
import { Switch, Route } from 'react-router-dom';
import { ForgotPassword } from '../User login/ForgotPassword';
import { ServiceWorker } from './ServiceWorker';
import Constants from './Constants';

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

    public render() {
        let credentials = localStorage.getItem("auth");
        if (credentials) {
            return (
                <div>
                    {this.props.children}
                </div>
            );
        }
        return (
            <div>
                <Route path='/' component={ServiceWorker} />
                <Switch>
                    <Route exact path='/registration' component={UserRegistration} />
                    <Route exact path='/forgot' component={ForgotPassword} />
                    <Route path='/' component={UserLogin} />
                </Switch>
            </div>
        );
    }
}