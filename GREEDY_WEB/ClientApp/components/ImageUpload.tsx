import * as React from 'react';
import ImageUploader from 'react-images-upload';

export class ImageUpload extends React.Component {
    constructor(props) {
        super(props);
        this.state = { file: '', imagePreviewUrl: '' };
    }

    _handleSubmit(e) {
        e.preventDefault();
        // TODO: do something with -> this.state.file
        console.log('handle uploading-', (this.state as any).file);
    }

    _handleImageChange(e) {
        e.preventDefault();

        let reader = new FileReader();
        let file = e.target.files[0];

        reader.onloadend = () => {
            this.setState({
                file: file,
                imagePreviewUrl: reader.result
            });
        }

        reader.readAsDataURL(file)
    }

    render() {
        return (
            <div>
                <label className="btn btn-primary active btn-file center-block">
                    Upload receipt <input type="file" onChange={(e) => this._handleImageChange(e)}/>
                </label>
            </div>
        )
    }
}
