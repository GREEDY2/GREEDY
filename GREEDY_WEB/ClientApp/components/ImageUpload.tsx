import * as React from 'react';
import ImageUploader from 'react-images-upload';
import axios from 'axios';
import Constants from './Constants';

interface Props {
    updateReceiptId: any;
    username: string;
}

export class ImageUpload extends React.Component<Props> {
    _handleImageChange(e) {
        e.preventDefault();

        let reader = new FileReader();
        let file = e.target.files[0];

        if (file !== undefined) {
            axios.post(Constants.httpRequestBasePath + 'api/ImageUpload', file, {
                headers: {
                    'Content-Type': file.type,
                    'Authorization': 'Basic ' + this.props.username
                }
            }).then(res => {
                if (res) {
                    this.props.updateReceiptId(res.data);
                }
                else {
                    //TODO: message for user to upload again, because items not read
                }
                }).catch(error => {
                //TODO: message for user that try again later because there is a problem with the server
                console.log(error);
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
            </div>
        )
    }
}
