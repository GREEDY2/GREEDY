import * as React from 'react';

import {
    greatPlaceStyle,
    greatPlaceCircleStyle, greatPlaceCircleStyleHover,
    greatPlaceStickStyle, greatPlaceStickStyleHover, greatPlaceStickStyleShadow
} from './style';

interface Props {
    lat: number;
    lng: number;
    text: string;
    zIndex: number;
}

export default class Marker extends React.Component<Props> {
    /*static propTypes = {
        // GoogleMap pass $hover props to hovered components
        // to detect hover it uses internal mechanism, explained in x_distance_hover example
        $hover: PropTypes.bool,
        text: PropTypes.string,
        zIndex: PropTypes.number
    };*/
    constructor(props) {
        super(props);
    }


    render() {
        const { text, zIndex } = this.props;

        const style = {
            ...greatPlaceStyle,
            zIndex: (this.props as any).$hover ? 1000 : zIndex
        };

        const circleStyle = (this.props as any).$hover ? greatPlaceCircleStyleHover : greatPlaceCircleStyle;
        const stickStyle = (this.props as any).$hover ? greatPlaceStickStyleHover : greatPlaceStickStyle;

        return (
            <div style={(style as any)}>
                <div style={(greatPlaceStickStyleShadow as any)} />
                <div style={(circleStyle as any)}>
                    {text}
                </div>
                <div style={(stickStyle as any)} />
            </div>
        );
    }
}