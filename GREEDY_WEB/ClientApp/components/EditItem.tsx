import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { Button, ButtonGroup, InputGroup, InputGroupAddon, Input, Form, FormGroup, Label, FormText } from 'reactstrap';
import { ModalContainer, ModalDialog } from 'react-modal-dialog';
import axios from 'axios';

interface IProps {
    hideEdit: any;
    //e - edit
    eItemId: number;
    eItemName: string;
    eItemPrice: number;
    eItemCategory: string;
    eSuccess: boolean;
}

export class EditItem extends React.Component<IProps> {
    constructor(props) {
        super(props);
    }

    saveItemChanges(e) {
        e.preventDefault();
        if (this.props.eItemId < 0 || this.props.eItemName === "") {
            this.setState({ eSuccess: false, showEdit: false });
            return;
        }
        const item = {
            itemId: this.props.eItemId,
            name: this.props.eItemName,
            category: this.props.eItemCategory
        }

        axios.post(`/api/UpdateItem/`, item)
            .then(response => {
                let res = response.data;
                if (res) {
                    this.setState({ eSuccess: true, eHappened: true, showEdit: false });
                    //this.update();
                }
                else {
                    this.setState({ eSuccess: false, eHappened: true, showEdit: false });
                }
            }).catch(error => {
                console.log(error);
            });
    }

    eNameChange = (event) => {
        this.setState({ eItemName: event.target.value });
    }

    ePriceChange = (event) =>  {
        this.setState({ eItemPrice: event.target.value });
    }

    eCategoryChange = (event) =>  {
        this.setState({ eItemCategory: event.target.value });
    }

    public render() {
        return (
        <ModalContainer onClose={this.props.hideEdit}>
            <ModalDialog onClose={this.props.hideEdit} style={{ width: '80%' }}>
                <h3>Edit Item Nr. {this.props.eItemId + 1}</h3>
                <Form onSubmit={this.saveItemChanges}>
                    <FormGroup>
                        <Label for="eItemName">Item Name</Label>
                        <Input
                                type="text"
                                name="itemName"
                                maxLength="100"
                                required id="eItemName"
                                defaultValue={this.props.eItemName}
                                onChange={this.eNameChange}
                                placeholder="Item Name" />
                        <Label for="eItemPrice">Price</Label>
                        <Input
                                type="text"
                                name="itemPrice"
                                maxLength="8"
                                required id="eItemPrice"
                                defaultValue={this.props.eItemPrice}
                                onChange={this.ePriceChange}
                                placeholder="Item Price" />
                        <Label for="eItemCategory">Category</Label>
                        <Input
                                type="select"
                                name="eItemCategory"
                                id="eItemCategory"
                                defaultValue={this.props.eItemCategory}
                                onChange={this.eCategoryChange}>
                            <option>{this.props.eItemCategory}</option>
                            <option>gerimai</option>
                            <option>uztatas</option>
                            <option>4</option>
                            <option>5</option>
                        </Input>
                        <Button type="button" color="primary">
                                Add Category
                        </Button>
                    </FormGroup>
                    <Button color="success" block>Save</Button>
                </Form>
            </ModalDialog>
        </ModalContainer>)
    }
}
