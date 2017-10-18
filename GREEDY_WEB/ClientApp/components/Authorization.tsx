import * as React from 'react';
import Cookies from 'universal-cookie';
import { UserLogin } from './UserLogin';
import { UserRegistration } from './UserRegistration';
import { Switch, Route } from 'react-router-dom';

export class Authorization extends React.Component {

    constructor(props) {
        super(props);
    }

    componentUpdate() {
        this.render();
    }

    public render() {
        const cookies = new Cookies();
        let role = cookies.get('username');
        if (role) {
            return (
                <div>
                    {this.props.children}
                </div>
            );
        }
        return <Switch>
            <Route exact path='/registration' component={UserRegistration} />
            <Route path='/' component={UserLogin} />
        </Switch>
    }
}