import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { Button, ButtonGroup, InputGroup, InputGroupAddon, Input, Form, FormGroup, Label, FormText } from 'reactstrap';
import { ModalContainer, ModalDialog } from 'react-modal-dialog';
import axios from 'axios';

interface Props {
    onRef: any;
    updateListAfterChange: any;
}

interface State {
    ItemId: number;
    ItemIndex: number;
    ItemName: string;
    ItemPrice: number;
    ItemCategory: string;
    eSuccess: boolean;
    showEdit: boolean;
    eHappened: boolean;
    showCategoryAdd: boolean;
}

export class EditItem extends React.Component<Props, State> {
    state = {
        ItemId: 0,
        ItemIndex: 0,
        ItemName: '',
        ItemPrice: 0,
        ItemCategory: '',
        eSuccess: false,
        showEdit: false,
        eHappened: false,
        showCategoryAdd: false
    }

    componentWillMount() {
        this.setState({
            showEdit: false,
            showCategoryAdd: false,
            eHappened: false
        });
    }

    componentDidMount() {
        this.props.onRef(this);
    }

    componentWillUnmount() {
        this.props.onRef(undefined);
    }

    saveItemChanges = (e) => {
        e.preventDefault();
        if (this.state.ItemId < 0 || this.state.ItemName === "") {
            this.setState({ eSuccess: false, showEdit: false });
            return;
        }
        const item = {
            itemId: this.state.ItemId,
            name: this.state.ItemName,
            price: this.state.ItemPrice,
            category: this.state.ItemCategory
        }
        //TODO: Create an API UpdateItem, that saves the changes to database
        //TODO: Make it post to "http://localhost:6967/api/UpdateItem/"
        axios.post("api/UpdateItem/", item)
            .then(response => {
                let res = response.data;
                if (res) {
                    this.setState({ eSuccess: true, eHappened: true, showEdit: false });
                    this.props.updateListAfterChange();
                }
                else {
                    this.setState({ eSuccess: false, eHappened: true, showEdit: false });
                }
            }).catch(error => {
                console.log(error);
            });
    }

    saveCategoryChanges = (e) => {
        e.preventDefault();
        //TODO: add new category to the list (make it selected aswell)
        //TODO: Create controller, that adds the new category to the database
        //TODO: Make it post to "http://localhost:6967/api/AddCategory/". Now its only for testing not to crash
        axios.post("api/AddCategory/", this.state.ItemCategory)
            .then(response => {
                let res = response.data;
                this.setState({ showCategoryAdd: false });
            }).catch(error => {
                console.log(error);
            })
    }

    hideEdit = () => {
        this.setState({ showEdit: false });
    }

    showEdit = (index, itemId, name, price, category) => {
        this.setState({
            ItemId: itemId,
            ItemIndex: index,
            ItemName: name,
            ItemPrice: price,
            ItemCategory: category,
            showEdit: true
        });
    }

    showCategoryAdd = () => {
        this.setState({ showCategoryAdd: true });
    }

    hideCategoryAdd = () => {
        this.setState({ showCategoryAdd: false });
    }

    eNameChange = (event) => {
        this.setState({ ItemName: event.target.value });
    }

    ePriceChange = (event) => {
        this.setState({ ItemPrice: event.target.value });
    }

    eCategoryChange = (event) => {
        this.setState({ ItemCategory: event.target.value });
    }

    public render() {
        return (
            <div>
                {this.state.showEdit &&
                    <ModalContainer onClose={this.hideEdit} >
                        <ModalDialog onClose={this.hideEdit} style={{ width: '80%' }}>
                            <h3>Edit Item Nr. {this.state.ItemIndex + 1}</h3>
                            <Form onSubmit={this.saveItemChanges}>
                                <FormGroup>
                                    <Label for="eItemName">Item Name</Label>
                                    <Input
                                        type="text"
                                        name="itemName"
                                        maxLength="100"
                                        required id="eItemName"
                                        defaultValue={this.state.ItemName}
                                        onChange={this.eNameChange}
                                        placeholder="Item Name" />
                                    <Label for="eItemPrice">Price</Label>
                                    <Input
                                        type="text"
                                        name="itemPrice"
                                        maxLength="8"
                                        required id="eItemPrice"
                                        defaultValue={this.state.ItemPrice}
                                        onChange={this.ePriceChange}
                                        placeholder="Item Price" />
                                    <Label for="eItemCategory">Category</Label>
                                    <Input
                                        type="select"
                                        name="eItemCategory"
                                        id="eItemCategory"
                                        defaultValue={this.state.ItemCategory}
                                        onChange={this.eCategoryChange}>
                                        <option>{this.state.ItemCategory}</option>
                                        <option>gerimai</option>
                                        <option>uztatas</option>
                                        <option>4</option>
                                        <option>5</option>
                                    </Input>
                                    <Button type="button" color="primary" onClick={this.showCategoryAdd}>
                                        Add Category
                                </Button>
                                </FormGroup>
                                <Button color="success" block>
                                    Save
                            </Button>
                            </Form>
                        </ModalDialog>
                    </ModalContainer >
                }
                {this.state.showCategoryAdd &&
                    <ModalContainer onClose={this.hideCategoryAdd}>
                        <ModalDialog onClose={this.hideCategoryAdd}>
                            <h3>Add a new category</h3>
                            <Form onSubmit={this.saveCategoryChanges}>
                                <FormGroup>
                                    <Input
                                        type="text"
                                        name="itemCategory"
                                        maxLength="100"
                                        required id="eItemCategory"
                                        placeholder="Category"
                                        onChange={this.eCategoryChange} />
                                </FormGroup>
                                <Button color="success" block>
                                    Confirm
                            </Button>
                            </Form>
                        </ModalDialog>
                    </ModalContainer>}
                {this.state.eHappened ?
                    this.state.eSuccess ?
                        <div className="text-center h4">Item Edited Successfully</div>
                        : <div className="text-center h4">Failed to Edit Item</div>
                    : null
                }
            </div>)
    }
}
