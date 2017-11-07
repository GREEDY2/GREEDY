import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { FetchUserItems } from './FetchUserItems';
import { GetCredentialsFromCookies } from './HelperClass';
import Constants from './Constants';

export class AllUserItems extends React.Component<RouteComponentProps<{}>> {
    constructor() {
        super();
    }

    public render() {
        let username = GetCredentialsFromCookies().Username;
        return (
            <div>
                <FetchUserItems username={ username } />
            </div>
        );
    }
}
