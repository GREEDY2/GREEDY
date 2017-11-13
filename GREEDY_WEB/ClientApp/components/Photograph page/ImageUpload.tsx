import * as React from 'react';
import ImageUploader from 'react-images-upload';
import axios from 'axios';
import Constants from '../Shared/Constants';
import { Alert } from '../Shared/Alert';

interface Props {
    updateReceiptId: any;
    imageUploadStarted: any;
    history: any;
}

export class ImageUpload extends React.Component<Props> {
    child: any;
    _handleImageChange(e) {
        e.preventDefault();

        let reader = new FileReader();
        let file = e.target.files[0];

        if (file !== undefined) {
            this.props.imageUploadStarted(true);
            axios.post(Constants.httpRequestBasePath + 'api/ImageUpload', file, {
                headers: {
                    'Content-Type': file.type,
                    'Authorization': 'Bearer ' + localStorage.getItem("auth")
                }
            }).then(response => {
                if (response.data) {
                    this.props.updateReceiptId(response.data);
                }
                else {
                    this.props.imageUploadStarted(false);
                    this.child.showAlert("Unable to find any items. Please retake the picture", "info");
                }
                }).catch(error => {
                    if (error.response.status == 401) {
                        localStorage.removeItem('auth');
                        (this.props as any).history.push("/");
                    }
                    this.props.imageUploadStarted(false);
                    this.child.showAlert("Something went wrong, please try again later", "error");
            });
        }
    }

    render() {
        return (
            <div className="col-xs-12">
                <label className="btn btn-primary active btn-file center-block">
                    Upload receipt
                    <input
                        type="file"
                        accept="image/*"
                        onChange={(e) => this._handleImageChange(e)} />
                </label>
                <Alert onRef={ref => (this.child = ref)} />
            </div>
        )
    }
}
