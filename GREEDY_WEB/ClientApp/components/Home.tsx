import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { ImageUpload } from './ImageUpload';
import { FetchData } from './FetchData';
import axios from 'axios';

interface State {
    itemList: any
}

export class Home extends React.Component<RouteComponentProps<{}>, State> {
    constructor()
    {
        super();

        this.getItemList = this.getItemList.bind(this);
        this.state = {
            itemList: []
        };
        axios.get('api/ItemData/ItemData')
            .then(res => {
                const itemList = res.data;
                this.setState({ itemList });
            });
    }

    public getItemList(items)
    {
        this.setState({ itemList: items });
    }

    public render() {
        return <div>
            <img className="img-responsive logo" src={"Logo.png"} height="100%" />
            <ImageUpload updateItemList={this.getItemList} />
            <FetchData itemList={this.state.itemList}/>
        </div>;
    }
}
