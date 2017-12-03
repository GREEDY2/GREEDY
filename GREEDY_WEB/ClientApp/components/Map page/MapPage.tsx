import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { GoogleMaps } from './GoogleMaps';

export class MapPage extends React.Component<RouteComponentProps<{}>> {

    public render() {
        return (
            <div style={{
                /*TODO: do something about the fixed height */
                width: 'auto', height: '500px', marginLeft: '-15px', marginRight: '-15px'
            }}>
                <GoogleMaps />
            </div>
        );
    }
}
