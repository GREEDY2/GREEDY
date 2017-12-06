﻿import * as React from 'react';

interface Props {
    hover: boolean;
    click: boolean;
    header: string;
    content: any;
    close: any;
}

export class MapHint extends React.Component<Props> {
    constructor(props) {
        super(props);
    }


    render() {
        if (this.props.hover || this.props.click) {
            return (
                <div>
                <div className='map-hint-arrow' />
                <div className='map-hint row'>
                    
                    <div className='map-hint-header col-xs-11'>
                        {this.props.header}
                    </div>
                    {this.props.click ?
                        <div className='map-hint-close-btn col-xs-1' onClick={this.props.close}>
                            X
                        </div> : null}
                    {this.props.click ?
                        <div className='map-hint-content'>
                            <div className='col-xs-12'>
                                Money spent: {this.props.content.moneySpent}
                            </div>
                            <div className='col-xs-12'>
                                Total purchases: {this.props.content.receiptCount}
                            </div>
                            <div className='col-xs-12'>
                                Last Purchase: {this.props.content.lastPurchase}
                            </div>
                        </div>
                        :
                        <div className='map-hint-content col-xs-12'>
                            Click to see more info
                        </div>
                    }

                </div>
                    </div>
            );
        }
        else return null;

    }
}