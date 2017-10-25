import * as React from 'react';
import Cookies from 'universal-cookie';
import { UserLogin } from './UserLogin';
import { UserRegistration } from './UserRegistration';
import { Switch, Route } from 'react-router-dom';
import { ForgotPassword } from './ForgotPassword';
import Constants from './Constants';

export class Authorization extends React.Component {

    constructor(props) {
        super(props);
    }

    componentUpdate = () => {
        this.render();
    }

    public render() {
        const cookies = new Cookies();
        let role = cookies.get(Constants.cookieUsername);
        if (role) {
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