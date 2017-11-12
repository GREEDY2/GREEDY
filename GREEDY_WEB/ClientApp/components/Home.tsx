import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { ImageUpload } from './ImageUpload';
import { FetchDataForUploadedReceipt } from './FetchDataForUploadedReceipt';
import { Logo } from './Logo';
import Constants from './Constants';

export class Home extends React.Component<RouteComponentProps<{}>> {
    child: any;
    constructor()
    {
        super();
    }

    public getReceiptId = (receiptId) => {
        this.child.getItemsFromPhoto(receiptId);
    }

    public imageUploadStarted = (bool) => {
        this.child.imageUploadStarted(bool);
    }

    public render() {
        return (
            <div>
                <Logo />
                <ImageUpload updateReceiptId={this.getReceiptId} imageUploadStarted={this.imageUploadStarted}
                    history={this.props.history} />
                <FetchDataForUploadedReceipt onRef={ref => (this.child = ref)} />
            </div>
        );
    }
}
