import * as React from 'react';
import { MapHint } from './MapHint';

interface Props {
    lat: number;
    lng: number;
    hover: boolean;
    click: boolean;
    shopInfo: any;
    unclick: any;
}

export default class Marker extends React.Component<Props> {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div>
                <MapHint hover={this.props.hover} click={this.props.click}
                    header={this.props.shopInfo} content={{moneySpent: 30.14, receiptCount: 5, lastPurchase: "2017-11-10"}}
                    close={this.props.unclick} />
                <div className='pin' style={{ cursor: 'pointer'}}></div>
                <div className='pulse'></div>
                
            </div>
        );
    }
}