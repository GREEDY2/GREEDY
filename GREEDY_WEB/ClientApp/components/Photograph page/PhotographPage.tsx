import * as React from "react";
import { RouteComponentProps } from "react-router";
import { ImageUpload } from "./ImageUpload";
import { FetchDataForUploadedReceipt } from "./FetchDataForUploadedReceipt";
import { Logo } from "../Shared/Logo";

export class PhotographPage extends React.Component<RouteComponentProps<{}>> {
    child: any;

    constructor() {
        super();
    }

    getReceiptId = (receiptId) => {
        this.child.getItemsFromPhoto(receiptId);
    };

    imageUploadStarted = (bool) => {
        this.child.imageUploadStarted(bool);
    };

    render() {
        return (
            <div>
                <Logo/>
                <ImageUpload updateReceiptId={this.getReceiptId} imageUploadStarted={this.imageUploadStarted}
                             history={this.props.history}/>
                <FetchDataForUploadedReceipt onRef={ref => (this.child = ref)}/>
            </div>
        );
    }
}