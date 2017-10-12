import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { ImageUpload } from './ImageUpload';
import { FetchData } from './FetchData';

export class Home extends React.Component<RouteComponentProps<{}>, {}> {
    public render() {
        return <div>
            <img className="img-responsive logo" src={"Logo.png"} height="100%" />
            <ImageUpload />
            <FetchData />
        </div>;
    }
}
