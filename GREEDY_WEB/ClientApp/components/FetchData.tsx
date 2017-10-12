import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';
import axios from 'axios';


export class FetchData extends React.Component {
    constructor() {
        super();
        this.state = {
            readings: []
        };
        axios.get('api/ItemData/ItemData')
            .then(res => {
                const readings = res.data;
                this.setState({ readings });
            });
    }

    public render() {
        return <div>
                   <h1>Items</h1>
                   <table>
                       <tbody>
                       {(this.state as any).readings.map(reading =>
                           <tr key={reading.Name}>
                               <td>{reading.Name}</td>
                               <td>{reading.Price}</td>
                               <td>{reading.Category}</td>
                           </tr>
                       )}
                       </tbody>
                   </table>

               </div>;
    }
}
