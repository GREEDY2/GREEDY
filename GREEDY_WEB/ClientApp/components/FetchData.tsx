import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';
import axios from 'axios';
import { Button, ButtonGroup, InputGroup, InputGroupAddon, Input, Form, FormGroup, Label, FormText } from 'reactstrap';
import { ModalContainer, ModalDialog } from 'react-modal-dialog';
import Constants from './Constants';
import { EditItem } from './EditItem';

interface Props {
    onRef: any
}

interface State {
    receiptId: number
    showItems: boolean
    itemList: any
}

export class FetchData extends React.Component<Props, State> {
    child: any;
    state = {
        receiptId: 0,
        showItems: false,
        itemList: []
    }

    componentDidMount() {
        this.props.onRef(this);
    }

    componentWillUnmount() {
        this.props.onRef(undefined);
    }

    updateList = () => {
        if (this.state.receiptId != 0) {
            this.getItemsFromPhoto(this.state.receiptId);
        }
        else {
            //TODO:
            // This is for code reusability. getAllUsersItems() method should be called here.
        }
    }

    getItemsFromPhoto(receiptId) {
        axios.get(Constants.httpRequestBasePath + 'api/GetItemsFromPostedReceipt/' + receiptId)
            .then(res => {
                const itemList = res.data;
                this.setState({ itemList, showItems: true, receiptId });
            }).catch(e => {
                console.log(e);
            })
    }

    public render() {
        return (
            <div>
                {this.state.showItems ? <div>
                    <table className="table-hover table itemTable">
                        <thead>
                            <tr>
                                <th>#</th>
                                <th className="center">Item</th>
                                <th>Price</th>
                                <th>Category</th>
                                <th>Edit</th>
                            </tr>
                        </thead>
                        <tbody>
                            {this.state.itemList.map((item, index) =>
                                <tr key={item.ItemId}>
                                    <td>{index + 1}</td>
                                    <td>{item.Name}</td>
                                    <td>{item.Price.toFixed(2)}&#8364;</td>
                                    <td>{item.Category}</td>
                                    <td><span
                                        className="glyphicon glyphicon-pencil readGlyphs"
                                        color="primary"
                                        onClick={() =>
                                            this.child.showEdit(index, item.ItemId, item.Name, item.Price, item.Category)}>
                                    </span></td>
                                </tr>
                            )}
                        </tbody>
                    </table>
                </div> : null
                }
                <EditItem onRef={ref => (this.child = ref)} updateListAfterChange={this.updateList} />
            </div>);
    }
}
