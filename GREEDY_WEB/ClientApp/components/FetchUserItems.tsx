import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';
import axios from 'axios';
import { Button, ButtonGroup, InputGroup, InputGroupAddon, Input, Form, FormGroup, Label, FormText } from 'reactstrap';
import { ModalContainer, ModalDialog } from 'react-modal-dialog';
import Constants from './Constants';
import { EditItem } from './EditItem';
import { GetCredentialsFromCookies } from './HelperClass';

interface Props {
    username: string
}

interface State {
    showItems: boolean
    itemList: any
}

export class FetchUserItems extends React.Component<Props, State> {
    child: any;
    state = {
        showItems: false,
        itemList: []
    }

    componentWillMount() {
        this.updateList();
    }

    updateList = () => {
        this.getAllUserItems(this.props.username);
    }

    getAllUserItems(username) {
        axios.get(Constants.httpRequestBasePath + 'api/GetAllUserItems/', {
            headers: {
                'Authorization': 'Basic ' + this.props.username
            }
        }).then(res => {
            if (res) {
                const itemList = res.data;
                this.setState({ itemList, showItems: true });
            }
            else {
                //TODO: display that the user has no items.
            }
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
