import * as React from 'react';
import { UserLogin } from './UserLogin';
import { UserRegistration } from './UserRegistration';
import { Switch, Route } from 'react-router-dom';
import { ForgotPassword } from './ForgotPassword';
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
            <Switch>
                <Route exact path='/registration' component={UserRegistration} />
                <Route exact path='/forgot' component={ForgotPassword} />
                <Route path='/' component={UserLogin} />
            </Switch>
        );
    }
}