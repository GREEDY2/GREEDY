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
            <span className="glyphicon glyphicon-asterisk" style={{ fontSize: "20px" }} />
        );
    }
}