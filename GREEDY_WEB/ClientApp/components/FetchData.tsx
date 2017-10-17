import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';
import axios from 'axios';

interface Props {
    itemList: any
}

export class FetchData extends React.Component<Props> {
    constructor() {
        super();

        this.state = {
            itemList: []
        };
        axios.get('api/ItemData/ItemData')
            .then(res => {
                const itemList = res.data;
                this.setState({ itemList });
            });
    }

    public render() {
        return <div>
            <table className="table-hover table itemTable">
                <thead>
                    <tr>
                        <th>#</th>
                        <th className="center">Item</th>
                        <th>Price</th>
                        <th>Category</th>
                    </tr>
                </thead>
                <tbody>
                    {this.props.itemList.map((item, index) =>
                        <tr key={index}>
                            <td>{index + 1}</td>
                            <td>{item.Name}</td>
                            <td>{item.Price.toFixed(2)}&#8364;</td>
                            <td>{item.Category}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        </div>;
    }
}
