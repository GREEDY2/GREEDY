import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { ImageUpload } from './ImageUpload';
import { FetchData } from './FetchData';

interface State {
    itemList: any
}

export class Home extends React.Component<RouteComponentProps<{}>, State> {
    state = { itemList: [] };

    public getItemList = (items) => {
        this.setState({ itemList: items });
    }

    public render() {
        return (
            <div>
                <div className="container">
                    <div className="row">
                        <div className="col-xs-12 text-center">
                            <img className="img-responsive logo" src={"Logo.png"} height="100%" />
                        </div>
                    </div>
                </div>
                <ImageUpload updateItemList={this.getItemList} />
                <FetchData itemList={this.state.itemList} />
            </div>
        );
    }
}
