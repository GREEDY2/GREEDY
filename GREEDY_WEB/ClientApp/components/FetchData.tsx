import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';
import axios from 'axios';
import { Button, ButtonGroup, InputGroup, InputGroupAddon, Input, Form, FormGroup, Label, FormText } from 'reactstrap';
import { ModalContainer, ModalDialog } from 'react-modal-dialog';

interface IProps {
    itemList: any
}

interface IState {
    showEdit: boolean
    showItems: boolean
    showCategoryAdd: boolean
    itemList: any
    //e - edit
    eItemId: number
    eItemName: string
    eItemPrice: number
    eItemCategory: string
    eHappened: boolean
    eSuccess: boolean
}

    //TODO: Write a controller that gets Items from the database for the user
    //TODO: Write a controller that gets categories from the database
    //TODO: Write methods to get the items and categories from BackEnd
    //TODO: Display categories (the default should be selected)
export class FetchData extends React.Component<IProps, IState> {
    componentWillMount() {
        this.setState({
            showItems: true,
            showEdit: false,
            showCategoryAdd: false,
            eHappened: false,
            itemList: this.props.itemList
        });
    }

    update = () => {
        this.getItems();
    }

    getItems = () => {

    }

    editItem = (id, name, price, category) => {
        this.setState({
            showEdit: true,
            eItemId: id,
            eItemName: name,
            eItemCategory: category,
            eItemPrice: price
        });
    }

    hideEdit = () => {
        this.setState({ showEdit: false });
    }

    showCategoryAdd = () => {
        this.setState({ showCategoryAdd: true });
    }

    hideCategoryAdd = () => {
        this.setState({ showCategoryAdd: false });
    }

    saveItemChanges = (e) => {
        e.preventDefault();
        if (this.state.eItemId < 0 || this.state.eItemName === "") {
            this.setState({ eSuccess: false, showEdit: false });
            return;
        }
        const item = {
            itemId: this.state.eItemId,
            name: this.state.eItemName,
            category: this.state.eItemCategory
        }
        //TODO: Create an API UpdateItem, that saves the changes to database
        //TODO: Make it post to "http://localhost:6967/api/UpdateItem/"
        axios.post("api/UpdateItem/", item)
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

    saveCategoryChanges = (e) => {
        e.preventDefault();
        //TODO: add new category to the list (make it selected aswell)
        //TODO: Create controller, that adds the new category to the database
        //TODO: Make it post to "http://localhost:6967/api/AddCategory/". Now its only for testing not to crash
        axios.post("api/AddCategory/", this.state.eItemCategory)
            .then(response => {
                let res = response.data;
                this.setState({ showCategoryAdd: false });
            }).catch(error => {
                console.log(error);
            })
    }

    eNameChange = (event) => {
        this.setState({ eItemName: event.target.value });
    }

    ePriceChange = (event) => {
        this.setState({ eItemPrice: event.target.value });
    }

    eCategoryChange = (event) => {
        this.setState({ eItemCategory: event.target.value });
    }

    public render() {
        return (
            <div>
                {this.state.showItems ?
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
                                    <td><span
                                        className="glyphicon glyphicon-pencil readGlyphs"
                                        color="primary"
                                        onClick={() =>
                                            this.editItem(index, item.Name, item.Price, item.Category)}>
                                    </span></td>
                                </tr>
                            )}
                        </tbody>
                    </table> : null
            }
                {this.state.eHappened ?
                    this.state.eSuccess ?
                        <div className="text-center h4">Item Edited Successfully</div>
                        : <div className="text-center h4">Failed to Edit Item</div>
                    : null
            }

            {this.state.showEdit &&
                <ModalContainer onClose={this.hideEdit}>
                    <ModalDialog onClose={this.hideEdit} style={{ width: '80%' }}>
                        <h3>Edit Item Nr. {this.state.eItemId + 1}</h3>
                        <Form onSubmit={this.saveItemChanges}>
                            <FormGroup>
                                <Label for="eItemName">Item Name</Label>
                                <Input
                                    type="text"
                                    name="itemName"
                                    maxLength="100"
                                    required id="eItemName"
                                    defaultValue={this.state.eItemName}
                                    onChange={this.eNameChange}
                                    placeholder="Item Name" />
                                <Label for="eItemPrice">Price</Label>
                                <Input
                                    type="text"
                                    name="itemPrice"
                                    maxLength="8"
                                    required id="eItemPrice"
                                    defaultValue={this.state.eItemPrice}
                                    onChange={this.ePriceChange}
                                    placeholder="Item Price" />
                                <Label for="eItemCategory">Category</Label>
                                <Input
                                    type="select"
                                    name="eItemCategory"
                                    id="eItemCategory"
                                    defaultValue={this.state.eItemCategory}
                                    onChange={this.eCategoryChange}>
                                    <option>{this.state.eItemCategory}</option>
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
                </ModalContainer>}
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
                                    onChange={this.eCategoryChange}/>
                            </FormGroup>
                            <Button color="success" block>
                                Confirm
                            </Button>
                        </Form>
                    </ModalDialog>
                </ModalContainer>}
            </div>);
    }
}
