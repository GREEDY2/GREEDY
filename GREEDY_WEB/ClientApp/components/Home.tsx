import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { ImageUpload } from './ImageUpload';
import { FetchData } from './FetchData';
import { Logo } from './Logo';
import Cookies from 'universal-cookie';
import Constants from './Constants';

interface State {
    username: string;
}

export class Home extends React.Component<RouteComponentProps<{}>, State> {
    child: any;
    constructor()
    {
        super();
        const cookies = new Cookies();
        let username = cookies.get(Constants.cookieUsername);
        this.state = {
            username
        }
    }   

    public getReceiptId = (receiptId) => {
        this.child.getItemsFromPhoto(receiptId);
    }

    public render() {
        return (
            <div>
                <Logo />
                <ImageUpload updateReceiptId={this.getReceiptId} username={this.state.username} />
                <FetchData onRef={ref => (this.child = ref)} />
            </div>
        );
    }
}
