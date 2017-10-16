import * as React from 'react';
import ImageUploader from 'react-images-upload';
import axios from 'axios';

interface Props {
    updateItemList: any;
}

export class ImageUpload extends React.Component<Props> {
    constructor(props) {
        super(props);
        this.state = {
            itemList: []
        };
    }

    _handleImageChange(e) {
        e.preventDefault();

        let reader = new FileReader();
        let file = e.target.files[0];

        this.state = {
            itemList: []
        };

        axios.put("http://localhost:6967/api/ImagePost", file, {
            headers: {
                'Content-Type': file.type
            }
        }).then(res => {
                const itemList = res.data;
                console.log(itemList);
                this.setState({ itemList });
            });
        console.log((this.state as any).itemList);
        this.props.updateItemList((this.state as any).itemList);
    }

    render() {
        return (
            <div>
                <label className="btn btn-primary active btn-file center-block">
                    Upload receipt <input type="file" accept="image/*" onChange={(e) => this._handleImageChange(e)}/>
                </label>
            </div>
        )
    }
}
