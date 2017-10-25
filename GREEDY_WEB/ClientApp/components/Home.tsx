import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { ImageUpload } from './ImageUpload';
import { FetchData } from './FetchData';
import { Logo } from './Logo';
import Cookies from 'universal-cookie';
import Constants from './Constants';

interface State {
    itemList: any;
    username: string;
}

export class Home extends React.Component<RouteComponentProps<{}>, State> {
    constructor()
    {
        super();
        const cookies = new Cookies();
        let username = cookies.get(Constants.cookieUsername);
        this.state = {
            itemList: [],
            username: username
        }
    }   

    public getItemList = (items) => {
        this.setState({ itemList: items });
    }

    public render() {
        return (
            <div>
                <Logo />
                <ImageUpload updateItemList={this.getItemList} username={this.state.username} />
                <FetchData itemList={this.state.itemList} />
            </div>
        );
    }
}
