import * as React from 'react';
import ImageUploader from 'react-images-upload';
import axios from 'axios';

interface IProps {
    updateItemList: any;
    username: string;
}

export class ImageUpload extends React.Component<IProps> {
    state = { itemList: [] };

    _handleImageChange(e) {
        e.preventDefault();

        let reader = new FileReader();
        let file = e.target.files[0];

        this.state = {
            itemList: []
        };

        axios.put("http://localhost:6967/api/ImagePost", file, {
            headers: {
                'Content-Type': file.type,
                'Authorization': 'Basic ' + this.props.username
            }
        }).then(res => {
            const itemList = res.data;
            this.setState({ itemList });
            this.props.updateItemList((this.state as any).itemList);
        }).catch(error => {
            console.log(error);
        });
    }

    render() {
        return (
            <div>
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
