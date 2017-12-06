import * as React from 'react';
import axios from 'axios';
import Constants from '../Shared/Constants';
import { Alert } from '../Shared/Alert';

interface Props {
    updateReceiptId: any;
    imageUploadStarted: any;
    history: any;
}

interface State {
    imageUploading: boolean;
}

export class ImageUpload extends React.Component<Props, State> {
    state = {
        imageUploading: false
    }
    child: any;
    _handleImageChange(e) {
        e.preventDefault();

        let reader = new FileReader();
        let file = e.target.files[0];

        if (file !== undefined) {
            this.setState({ imageUploading: true });
            this.props.imageUploadStarted(true);
            axios.post(Constants.httpRequestBasePath + 'api/ImageUpload', file, {
                headers: {
                    'Content-Type': file.type,
                    'Authorization': 'Bearer ' + localStorage.getItem("auth")
                }
            }).then(response => {
                if (response.data) {
                    this.setState({ imageUploading: false });
                    this.props.updateReceiptId(response.data);
                }
                else {
                    this.setState({ imageUploading: false });
                    this.props.imageUploadStarted(false);
                    this.child.showAlert("Unable to find any items. Please retake the picture", "info");
                }
                }).catch(error => {
                    this.setState({ imageUploading: false });
                    this.props.imageUploadStarted(false);
                    if (error.response)
                    if (error.response.status == 401) {
                        localStorage.removeItem('auth');
                        this.props.history.push("/");
                        }
                    else {
                        this.child.showAlert("Can't upload receipt with no internet", "error");
                    }
                    this.child.showAlert("Something went wrong, please try again later", "error");
            });
        }
    }

    render() {
        return (
            <div className="col-xs-12">
                {this.state.imageUploading ? 
                    <label className="btn btn-primary active btn-file center-block disabled">
                        Upload receipt
                    </label>
                    :
                    <label className="btn btn-primary active btn-file center-block">
                        Upload receipt
                    <input
                            type="file"
                            accept="image/*"
                            onChange={(e) => this._handleImageChange(e)} />
                    </label>
                    }
                
                <Alert onRef={ref => (this.child = ref)} />
            </div>
        )
    }
}
