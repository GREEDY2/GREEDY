import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { Button, ButtonGroup, InputGroup, InputGroupAddon, Input, Form, FormGroup, Label, FormText } from 'reactstrap';
import { ModalContainer, ModalDialog } from 'react-modal-dialog';
import axios from 'axios';
import Constants from '../Shared/Constants';
import idbPromise from '../Shared/idbPromise';
import { deleteRowFromDb } from '../Shared/DatabaseFunctions';
import { Alert } from '../Shared/Alert';

interface Props {
    onRef: any;
    updateListAfterChange: any;
}

interface State {
    Categories: Array<string>;
    ItemId: number;
    ItemIndex: number;
    ItemName: string;
    ItemPrice: number;
    ItemCategory: string;
    showEdit: boolean;
    //showCategoryAdd: boolean;
}

export class EditItem extends React.Component<Props, State> {
    child: any;
    state = {
        Categories: [],
        ItemId: 0,
        ItemIndex: 0,
        ItemName: '',
        ItemPrice: 0,
        ItemCategory: '',
        showEdit: false,
        //showCategoryAdd: false
    }

    componentWillMount() {
        this.getAllDistinctCategoriesFromDb();
    }

    componentDidMount() {
        this.props.onRef(this);
    }

    componentWillUnmount() {
        this.props.onRef(undefined);
    }

    //TODO: don't make the entire list refetch after edit, just change the item that has been edited
    saveItemChanges = (e) => {
        e.preventDefault();
        this.hideEdit();
        if (this.state.ItemId < 0 || this.state.ItemName === "") {
            return;
        }
        const item = {
            ItemId: this.state.ItemId,
            Name: this.state.ItemName,
            Price: this.state.ItemPrice,
            Category: this.state.ItemCategory
        }
        axios.put(Constants.httpRequestBasePath + "api/UpdateItem", item,
            {
                headers: {
                    'Authorization': 'Bearer ' + localStorage.getItem("auth")
                }
            }).then(response => {
                this.child.showAlert("Item edited successfully", "success");
                this.props.updateListAfterChange();
            }).catch(error => {
                this.child.showAlert("Failed to edit Item. Try again later", "error");
            });
    }

    getAllDistinctCategoriesFromDb = () => {
        if (idbPromise) {
            idbPromise.then(db => {
                if (!db) return;

                var tx = db.transaction('categories');
                var store = tx.objectStore('categories');
                return store.getAllKeys().then(categories => {
                    this.setState({ Categories: categories });
                })
            }).then(() => {
                if (this.state.Categories === []) {
                    this.getAllDistinctCategories();
                }
            })
        }
    }

    getAllDistinctCategories = () => {
        axios.get(Constants.httpRequestBasePath + "api/GetDistinctCategories",
            {
                headers: {
                    'Authorization': 'Bearer ' + localStorage.getItem("auth")
                }
            }).then(response => {
                let res = response.data;
                this.setState({ Categories: res });
            }).catch(error => {
                console.log(error);
            })
    }

    /*
    //TODO: decide if we want to let user add categories (i believe not)
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
    }*/

    deleteItem = () => {
        axios.put(Constants.httpRequestBasePath + "api/DeleteItem", this.state.ItemId,
            {
                headers: {
                    'Authorization': 'Bearer ' + localStorage.getItem("auth")
                }
            }).then(response => {
                let res = response.data;
                this.child.showAlert("Item deleted successfully", "success");
                deleteRowFromDb('myItems', this.state.ItemId);
                this.props.updateListAfterChange();
                this.setState({ showEdit: false });
            }).catch(error => {
                this.child.showAlert("Failed to delete Item. Try again later", "error");
            });
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

    /*showCategoryAdd = () => {
        this.setState({ showCategoryAdd: true });
    }

    hideCategoryAdd = () => {
        this.setState({ showCategoryAdd: false });
    }*/

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
                <Alert onRef={ref => (this.child = ref)} />
                {this.state.showEdit &&
                    <ModalContainer onClose={this.hideEdit} >
                        <ModalDialog onClose={this.hideEdit} style={{ width: '80%' }}>
                            <div className="row">
                                <h3 className="col-xs-8">Edit Item Nr. {this.state.ItemIndex + 1}</h3>
                                <Button className="col-xs-4" type="button" color="danger" onClick={this.deleteItem} style={{marginTop: "20px", right: "15px"}}>
                                    Delete Item
                                </Button>
                            </div>

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
                                        type="number"
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
                                        <option></option>
                                        {this.state.Categories.map(category =>
                                            <option key={category}>{category}</option>
                                        )}
                                    </Input>
                                    {/* We decided that we don't let the user add a category for now
                                    <Button type="button" color="primary" onClick={this.showCategoryAdd}>
                                        Add Category
                                </Button>*/}
                                </FormGroup>
                                <Button color="success" block>
                                    Save
                            </Button>

                            </Form>
                        </ModalDialog>
                    </ModalContainer >
                }
                {/*{this.state.showCategoryAdd &&
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
                    </ModalContainer>}*/}
            </div>)
    }
}
