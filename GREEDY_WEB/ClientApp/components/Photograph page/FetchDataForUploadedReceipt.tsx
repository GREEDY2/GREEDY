import * as React from 'react';
import axios from 'axios';
import { Button, ButtonGroup, InputGroup, InputGroupAddon, Input, Form, FormGroup, Label, FormText } from 'reactstrap';
import { ModalContainer, ModalDialog } from 'react-modal-dialog';
import Constants from '../Shared/Constants';
import { EditItem } from './EditItem';
import { Alert } from '../Shared/Alert';

interface Props {
    onRef: any
}

interface State {
    receiptId: number
    showItems: boolean
    itemList: any
    imageIsUploading: boolean;
}

export class FetchDataForUploadedReceipt extends React.Component<Props, State> {
    child: any;
    state = {
        receiptId: 0,
        showItems: false,
        itemList: [],
        imageIsUploading: false
    }

    componentDidMount() {
        this.props.onRef(this);
    }

    componentWillUnmount() {
        this.props.onRef(undefined);
    }

    updateList = () => {
        this.getItemsFromPhoto(this.state.receiptId);
    }

    imageUploadStarted(bool) {
        if (bool) {
            this.setState({ imageIsUploading: true });
        }
        else {
            this.setState({ imageIsUploading: false });
        }
    }

    getItemsFromPhoto(receiptId) {
        axios.get(Constants.httpRequestBasePath + 'api/GetItemsFromPostedReceipt/' + receiptId,
            {
                headers: {
                    'Authorization': 'Bearer ' + localStorage.getItem("auth")
                }
            }).then(res => {
                if (res.data) {
                    const itemList = res.data;
                    this.setState({ itemList, showItems: true, receiptId, imageIsUploading: false });
                }
                else {
                    this.setState({ imageIsUploading: false, showItems: false });
                    this.child.showAlert("Unable to find any items. Please retake the picture", "info");
                }
            }).catch(error => {
                this.setState({ imageIsUploading: false, showItems: false});
                this.child.showAlert("Something went wrong, please try again later", "error");
            })
    }

    public render() {
        if (this.state.imageIsUploading) {
            return <img className="img-responsive loading" src={"Rolling.gif"} />;
        }
        if (!this.state.showItems) {
            return <Alert onRef = { ref => (this.child = ref) } />
        }
        return (
            <div>
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
                <EditItem onRef={ref => (this.child = ref)} updateListAfterChange={this.updateList} />
            </div>);
    }
}