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
    receiptId: number;
    Categories: Array<string>;
    ItemName: string;
    ItemPrice: number;
    ItemCategory: string;
    showAdd: boolean;
    modalWidth: string;
}

export class AddItem extends React.Component<Props, State> {
    child: any;
    state = {
        receiptId: undefined,
        Categories: [],
        ItemName: undefined,
        ItemPrice: undefined,
        ItemCategory: undefined,
        showAdd: false,
        modalWidth: "80%"
    }

    componentWillMount() {
        this.getAllDistinctCategoriesFromDb();
        this.updateModelWidth();
    }

    componentDidMount() {
        this.props.onRef(this);
    }

    componentWillUnmount() {
        this.props.onRef(undefined);
    }

    updateModelWidth = () => {
        if (this.state.modalWidth !== "80%" && window.innerWidth < 768) {
            this.setState({ modalWidth: "80%" });
        }
        else if (this.state.modalWidth !== "40%" && window.innerWidth >= 768) {
            this.setState({ modalWidth: "40%" });
        }
    }

    //TODO: don't make the entire list refetch after edit, just change the item that has been added
    saveItemAdd = (e) => {
        e.preventDefault();
        this.hideAdd();
        if (this.state.ItemPrice === undefined || this.state.ItemName === undefined || this.state.receiptId === undefined) {
            return;
        }
        let itemCategory = this.state.ItemCategory || '';
        const item = {
            Name: this.state.ItemName,
            Price: this.state.ItemPrice,
            Category: itemCategory,
            ReceiptId: this.state.receiptId
        }
        axios.put(Constants.httpRequestBasePath + "api/AddItem", item,
            {
                headers: {
                    'Authorization': 'Bearer ' + localStorage.getItem("auth")
                }
            }).then(response => {
                this.child.showAlert("Item added successfully", "success");
                this.props.updateListAfterChange();
            }).catch(error => {
                this.child.showAlert("Failed to add Item. Try again later", "error");
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

    hideAdd = () => {
        this.setState({ showAdd: false });
    }

    showAdd = (receiptId) => {
        this.setState({ receiptId, showAdd: true , ItemName: undefined, ItemPrice: undefined, ItemCategory: undefined});
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
                <Alert onRef={ref => (this.child = ref)} />
                {this.state.showAdd &&
                    <ModalContainer onClose={this.hideAdd} >
                        <ModalDialog onClose={this.hideAdd} style={{ width: this.state.modalWidth }}>
                                <h3>Add new Item</h3>
                            <Form onSubmit={this.saveItemAdd}>
                                <FormGroup>
                                    <Label for="eItemName">Item Name</Label>
                                    <Input
                                        type="text"
                                        name="itemName"
                                        maxLength="100"
                                        required id="eItemName"
                                        onChange={this.eNameChange}
                                        placeholder="Item Name" />
                                    <Label for="eItemPrice">Price</Label>
                                    <Input
                                        type="number"
                                        name="itemPrice"
                                        maxLength="8"
                                        required id="eItemPrice"
                                        onChange={this.ePriceChange}
                                        placeholder="Item Price" />
                                    <Label for="eItemCategory">Category</Label>
                                    <Input
                                        type="select"
                                        name="eItemCategory"
                                        id="eItemCategory"
                                        onChange={this.eCategoryChange}>
                                        <option></option>
                                        {this.state.Categories.map(category =>
                                            <option key={category}>{category}</option>
                                        )}
                                    </Input>
                                </FormGroup>
                                <Button color="success" block>
                                    Save new item
                            </Button>
                            </Form>
                        </ModalDialog>
                    </ModalContainer >
                }
            </div>)
    }
}
