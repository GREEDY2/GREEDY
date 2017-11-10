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

    public imageUploadStarted = () => {
        this.child.imageUploadStarted();
    }

    public render() {
        return (
            <div>
                <Logo />
                <ImageUpload updateReceiptId={this.getReceiptId} imageUploadStarted={this.imageUploadStarted} />
                <FetchDataForUploadedReceipt onRef={ref => (this.child = ref)} />
            </div>
        );
    }
}
