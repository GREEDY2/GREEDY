import * as React from 'react';

import {
    greatPlaceStyle,
    greatPlaceCircleStyle, greatPlaceCircleStyleHover,
    greatPlaceStickStyle, greatPlaceStickStyleHover, greatPlaceStickStyleShadow
} from './style';

interface Props {
    name: string;
    lat: number;
    lng: number;
    hover: boolean;
}

export default class Marker extends React.Component<Props> {
    constructor(props) {
        super(props);
    }

    render() {
        const circleStyle = (this.props as any).$hover ? greatPlaceCircleStyleHover : greatPlaceCircleStyle;
        const stickStyle = (this.props as any).$hover ? greatPlaceStickStyleHover : greatPlaceStickStyle;

        return (
            <div style={(greatPlaceStyle as any)}>
                <div style={(greatPlaceStickStyleShadow as any)} />
                <div style={(circleStyle as any)}>
                    {this.props.name}
                </div>
                <div style={(stickStyle as any)} />
            </div>
        );
    }
}