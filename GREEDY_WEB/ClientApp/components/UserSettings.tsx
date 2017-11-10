import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { Logo } from './Logo';
import { ChangeEmail } from './ChangeEmail';
import { ChangePassword } from './ChangePassword'

export class UserSettings extends React.Component<RouteComponentProps<{}>> {
    constructor() {
        super();
    }

    public render() {
        return (
            <div>
                <Logo />
                <ChangeEmail />
                <ChangePassword />
            </div>
        );
    }
}
