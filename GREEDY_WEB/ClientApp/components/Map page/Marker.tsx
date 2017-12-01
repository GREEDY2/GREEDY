import * as React from 'react';

interface Props {
    lat: number;
    lng: number;
    hover: boolean;
}

export default class Marker extends React.Component<Props> {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div>
                <div className='pin' style={{ cursor: 'pointer'}}></div>
                <div className='pulse'></div>
                {this.props.hover && <p>hover works</p>}
            </div>
        );
    }
}