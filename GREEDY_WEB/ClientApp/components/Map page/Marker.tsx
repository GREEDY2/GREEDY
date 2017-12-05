import * as React from 'react';

interface Props {
    lat: number;
    lng: number;
    hover: boolean;
    shopInfo: any;
}

export default class Marker extends React.Component<Props> {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div>
                {this.props.hover &&
                    <div className='map-hint'>
                    {/*TODO: This is total shit and only for what should be done...
                        I'm not very good at css so it would take me a lot of
                        time to write this component myself*/}
                        <div className='map-hint-header'>
                            {this.props.shopInfo}
                        </div>
                        <div className='map-hint-close-btn'>
                            Close
                        </div>
                        <div className='map-hint-content'>
                            Money spent this month: 30e
                        </div>
                        
                    </div>}
                <div className='pin' style={{ cursor: 'pointer'}}></div>
                <div className='pulse'></div>
                
            </div>
        );
    }
}