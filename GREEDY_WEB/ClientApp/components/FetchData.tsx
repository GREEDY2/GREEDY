import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';
import axios from 'axios';
import { Button, ButtonGroup, InputGroup, InputGroupAddon, Input, Form, FormGroup, Label, FormText } from 'reactstrap';
import { ModalContainer, ModalDialog } from 'react-modal-dialog';

interface Props {
    itemList: any
}

interface State {
    showEdit: boolean
    showItems: boolean
    itemList: any
    //e - edit
    eItemId: number
    eItemName: string
    eItemPrice: number
    eItemCategory: string
    eHappened: boolean
    eSuccess: boolean
}

export class FetchData extends React.Component<Props, State> {
    constructor() {
        super();
        /*axios.get('http://localhost:6967/api/ItemData')
            .then(res => {
                const data = res.data;
                this.setState({ itemList: data, showItems: true });
            });*/
        this.editItem = this.editItem.bind(this);
        this.getItems = this.getItems.bind(this);
        this.hideEdit = this.hideEdit.bind(this);
        this.saveItemChanges = this.saveItemChanges.bind(this);
        this.eCategoryChange = this.eCategoryChange.bind(this);
        this.eNameChange = this.eNameChange.bind(this);
        this.ePriceChange = this.ePriceChange.bind(this);
    }

    componentWillMount() {
        this.setState({ showItems: true, showEdit: false, eHappened: false, itemList: this.props.itemList });
    }


    update() {
        this.getItems();
    }

    getItems() {
        /*axios.get('http://localhost:6967/api/ItemData')
            .then(res => {
                const data = res.data;
                this.setState({ itemList: data, showItems: true });
            });*/
    }

    editItem(id, name, price, category) {
        this.setState({ showEdit: true, eItemId: id, eItemName: name, eItemCategory: category, eItemPrice: price });
    }

    hideEdit() {
        this.setState({ showEdit: false });
    }

    saveItemChanges(e) {
        e.preventDefault();
        if (this.state.eItemId < 0 || this.state.eItemName === "") {
            this.setState({ eSuccess: false, showEdit: false });
            return;
        }
        const item = { itemId: this.state.eItemId, name: this.state.eItemName, category: this.state.eItemCategory }
        axios.post(`/api/UpdateItem/`, item)
            .then(response => {
                let res = response.data;
                if (res) {
                    this.setState({ eSuccess: true, eHappened: true, showEdit: false });
                    this.update();
                }
                else {
                    this.setState({ eSuccess: false, eHappened: true, showEdit: false });
                }
            }).catch(error => {
                console.log(error);
            });
    }

    eNameChange(event) {
        this.setState({ eItemName: event.target.value });
    }

    ePriceChange(event) {
        this.setState({ eItemPrice: event.target.value });
    }

    eCategoryChange(event) {
        this.setState({ eItemCategory: event.target.value });
    }

    public render() {
        return <div>
            {
                this.state.showItems ?
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
                            {this.props.itemList.map((item, index) =>
                                <tr key={index}>
                                    <td>{index + 1}</td>
                                    <td>{item.Name}</td>
                                    <td>{item.Price.toFixed(2)}&#8364;</td>
                                    <td>{item.Category}</td>
                                    <td><span className="glyphicon glyphicon-pencil readGlyphs" color="primary" onClick={() =>
                                        this.editItem(index, item.Name, item.Price, item.Category)}></span></td>
                                </tr>
                            )}
                        </tbody>
                    </table> : null
            }
            {
                this.state.eHappened ?
                    this.state.eSuccess ?
                        <div className="text-center h4">Item Edited Successfully</div>
                        : <div className="text-center h4">Failed to Edit Item</div>
                    : null
            }


            {
                this.state.showEdit &&
                <ModalContainer onClose={this.hideEdit}>
                    <ModalDialog onClose={this.hideEdit} style={{ width: '80%' }}>
                        <h3>Edit Item Nr. {this.state.eItemId + 1}</h3>
                        <Form onSubmit={this.saveItemChanges}>
                            <FormGroup>
                                <Label for="eItemName">Item Name</Label>
                                <Input type="text" name="itemName" maxLength="100" required id="eItemName"
                                    defaultValue={this.state.eItemName} onChange={this.eNameChange}
                                    placeholder="Item Name" />
                                <Label for="eItemPrice">Price</Label>
                                <Input type="text" name="itemPrice" maxLength="8" required id="eItemPrice"
                                    defaultValue={this.state.eItemPrice} onChange={this.ePriceChange}
                                    placeholder="Item Price" />
                                <Label for="eItemCategory">Category</Label>
                                <Input type="select" name="eItemCategory" id="eItemCategory"
                                    defaultValue={this.state.eItemCategory} onChange={this.eCategoryChange}>
                                    <option>{this.state.eItemCategory}</option>
                                    <option>gerimai</option>
                                    <option>uztatas</option>
                                    <option>4</option>
                                    <option>5</option>
                                </Input>
                                <Button type="button" color="primary">Add Category</Button>
                            </FormGroup>
                            <Button color="success" block>Save</Button>
                        </Form>
                    </ModalDialog>
                </ModalContainer>
            }
        </div>;
    }
}
