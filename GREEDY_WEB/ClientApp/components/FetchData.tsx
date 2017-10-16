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
                   <h1>Items</h1>
                   <table>
                <tbody>
                    {(this.props as any).itemList.map(item =>
                           <tr key={item.Name}>
                               <td>{item.Name}</td>
                               <td>{item.Price}</td>
                               <td>{item.Category}</td>
                           </tr>
                       )} 
                       </tbody>
                   </table>

               </div>;
    }
}
