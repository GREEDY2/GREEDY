import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { GetCredentialsFromCookies } from './HelperClass';
import { Logo } from './Logo';
import { ChangeEmail } from './ChangeEmail';
import { ChangePassword } from './ChangePassword'

export class UserSettings extends React.Component<RouteComponentProps<{}>> {
    constructor() {
        super();
    }

    public render() {
        let username = GetCredentialsFromCookies().Username;
        return (
            <div>
                <Logo />
                <ChangeEmail username={username} />
                <ChangePassword username={username} />
            </div>
        );
    }
}
