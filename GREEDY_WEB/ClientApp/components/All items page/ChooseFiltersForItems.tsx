import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';
import axios from 'axios';
import { Button, ButtonGroup, InputGroup, InputGroupAddon, Input, Form, FormGroup, Label, FormText } from 'reactstrap';
import { ModalContainer, ModalDialog } from 'react-modal-dialog';
import Constants from '../Shared/Constants';
import { EditItem } from '../Photograph page/EditItem';

interface Props {
    refilter: any
    resort: any
}

interface State {
    showFilterChoice: boolean
    showSortChoice: boolean
    //f - filter options
    fPriceCompare: string
    fPrice: number
    fCategory: string
    //s - sort options
    sSortType: string
    sByPriceAsc: string
    Categories: any
}

export class ChooseFiltersForItems extends React.Component<Props, State> {
    state = {
        showSortChoice: false,
        showFilterChoice: false,
        fPriceCompare: 'All',
        fPrice: 0,
        fCategory: 'All',
        Categories: [],
        sSortType: 'No sort',
        sByPriceAsc: 'Highest first'
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

    hideFilterChoice = () => {
        this.setState({ showFilterChoice: false });
    }

    hideSortChoice = () => {
        this.setState({ showSortChoice: false });
    }

    showFilterChoice = () => {
        this.setState({ showFilterChoice: true });
    }

    showSortChoice = () => {
        this.setState({ showSortChoice: true });
    }

    saveFilterChanges = (event) => {
        event.preventDefault();
        this.hideFilterChoice();
        this.props.refilter(this.state.fPriceCompare, this.state.fPrice, this.state.fCategory);
    }

    saveSortChanges = (event) => {
        event.preventDefault();
        this.hideSortChoice();
        this.props.resort(this.state.sSortType, this.state.sByPriceAsc);
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

    sSortTypeChange = (event) => {
        this.setState({ sSortType: event.target.value });
    }

    sSortByPriceAsc = (event) => {
        this.setState({ sByPriceAsc: event.target.value });
    }

    public render() {
        return (
            <div className="filters row">
                <Button color="info" onClick={this.showFilterChoice}>
                    <span className='glyphicon glyphicon-filter'></span>
                    Change filters
                </Button>
                <Button className="pull-right" color="info" onClick={this.showSortChoice}>
                    <span className='glyphicon glyphicon-filter'></span>
                    Sort items
                </Button>
                {this.state.showSortChoice &&
                    <ModalContainer onClose={this.hideSortChoice} >
                        <ModalDialog onClose={this.hideSortChoice}>
                            <h3>Select how to sort items</h3>
                            <Form onSubmit={this.saveSortChanges}>
                                <FormGroup>
                                    <Label for="sSort">Sort by</Label>
                                    <Input
                                        type="select"
                                        name="sSortType"
                                        id="sSortType"
                                        defaultValue={this.state.sSortType}
                                        onChange={this.sSortTypeChange}>
                                        <option key={0}>No sort</option>
                                        <option key={1}>Price</option>
                                        <option key={2}>Name</option>
                                        <option key={3}>Category</option>
                                    </Input>
                                    {this.state.sSortType === 'Price' &&
                                        <Input
                                            type="select"
                                            name="sByPrice"
                                            id="sByPrice"
                                            defaultValue={this.state.sByPriceAsc}
                                            onChange={this.sSortByPriceAsc}>
                                            <option key={0}>Highest first</option>
                                            <option key={1}>Lowest first</option>
                                        </Input>
                                    }
                                </FormGroup>
                                <Button color="success" block>
                                    Sort
                            </Button>
                            </Form>
                        </ModalDialog>
                    </ModalContainer >
                }
                {this.state.showFilterChoice &&
                    <ModalContainer onClose={this.hideFilterChoice} >
                        <ModalDialog onClose={this.hideFilterChoice}>
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
                                    Filter
                            </Button>
                            </Form>
                        </ModalDialog>
                    </ModalContainer >
                }
            </div>
        );
    }
}
