import * as React from 'react';

interface Props {
    lat: number;
    lng: number;
}

export default class UserLocationMarker extends React.Component<Props> {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div className="markerUser">
                <div className="pinUser"></div>
                <div className="pinUser-effect"></div>
            </div>
        );
    }
}