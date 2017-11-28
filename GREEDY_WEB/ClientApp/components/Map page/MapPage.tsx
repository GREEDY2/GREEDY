import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { GoogleMaps } from './GoogleMaps';

export class MapPage extends React.Component<RouteComponentProps<{}>> {

    public render() {
        return (
            <div style={{
                width: 'auto', height: '400px', marginLeft: '-15px', marginRight: '-15px'
            }}>
                <GoogleMaps />
            </div>
        );
    }
}
