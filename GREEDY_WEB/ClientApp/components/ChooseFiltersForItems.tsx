import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';
import axios from 'axios';
import { Button, ButtonGroup, InputGroup, InputGroupAddon, Input, Form, FormGroup, Label, FormText } from 'reactstrap';
import { ModalContainer, ModalDialog } from 'react-modal-dialog';
import Constants from './Constants';
import { EditItem } from './EditItem';

interface Props {
    refilter: any
}

interface State {
    showChoice: boolean
    fPriceCompare: string
    fPrice: number
    fCategory: string
    Categories: any
}

export class ChooseFiltersForItems extends React.Component<Props, State> {
    state = {
        showChoice: false,
        fPriceCompare: 'All',
        fPrice: 0,
        fCategory: 'All',
        Categories: []
    }

    constructor() {
        super();
        this.getAllDistinctCategories();
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

    hideChoose = () => {
        this.setState({ showChoice: false });
    }

    showChoice = () => {
        this.setState({ showChoice: true });
    }

    saveFilterChanges = (event) => {
        event.preventDefault();
        this.hideChoose();
        this.props.refilter(this.state.fPriceCompare, this.state.fPrice, this.state.fCategory);
    }

    fPriceCompareChange = (event) => {
        this.setState({ fPriceCompare: event.target.value });
    }

    fPriceChange = (event) => {
        this.setState({ fPrice: event.target.value });
    }

    fCategoryChange = (event) => {
        this.setState({ fCategory: event.target.value });
    }

    fPriceType() {
        if (this.state.fPriceCompare === 'All') {
            return null;
        }
        else {
            return (<Input
                type="number"
                name="fPrice"
                maxLength="8"
                required id="fPrice"
                defaultValue={this.state.fPrice}
                onChange={this.fPriceChange}
                placeholder="Price" />);
        }
    }

    public render() {
        return (
            <div>
                <Button color="info" onClick={this.showChoice}>
                    Change filters
                </Button>
                {this.state.showChoice &&
                    <ModalContainer onClose={this.hideChoose} >
                        <ModalDialog onClose={this.hideChoose}>
                            <h3>Select how to filter items</h3>
                            <Form onSubmit={this.saveFilterChanges}>
                                <FormGroup>
                                    <Label for="fPrice">Price</Label>
                                    <Input
                                        type="select"
                                        name="fPriceCompare"
                                        id="fPriceCompare"
                                        defaultValue={this.state.fPriceCompare}
                                        onChange={this.fPriceCompareChange}>
                                        <option key={0}>All</option>
                                        <option key={1}>More than</option>
                                        <option key={2}>Less than</option>
                                        <option key={3}>Equal</option>
                                        >
                                    </Input>
                                    {this.fPriceType()}

                                    <Label for="fCategory">Category</Label>
                                    <Input
                                        type="select"
                                        name="fCategory"
                                        id="fCategory"
                                        defaultValue={this.state.fCategory}
                                        onChange={this.fCategoryChange}>
                                        <option>All</option>
                                        <option></option>
                                        {this.state.Categories.map(category =>
                                            <option key={category}>{category}</option>
                                        )}
                                    </Input>
                                </FormGroup>
                                <Button color="success" block>
                                    Confirm filters
                            </Button>
                            </Form>
                        </ModalDialog>
                    </ModalContainer >
                }
            </div>
        );
    }
}
