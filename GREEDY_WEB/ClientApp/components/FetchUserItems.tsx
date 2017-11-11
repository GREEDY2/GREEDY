import * as React from 'react';
import axios from 'axios';
import { Button, ButtonGroup, InputGroup, InputGroupAddon, Input, Form, FormGroup, Label, FormText } from 'reactstrap';
import { ModalContainer, ModalDialog } from 'react-modal-dialog';
import Constants from './Constants';
import { EditItem } from './EditItem';

interface Props {
    onRef: any
    history: any
}

interface State {
    showItems: boolean
    itemList: any
    filter: any
    sort: any
}

export class FetchUserItems extends React.Component<Props, State> {
    child: any;
    state = {
        showItems: false,
        itemList: [],
        filter: undefined,
        sort: undefined
    }

    componentWillMount() {
        this.updateList();
    }

    componentDidMount() {
        this.props.onRef(this);
    }

    componentWillUnmount() {
        this.props.onRef(undefined);
    }

    updateFilter(filter) {
        this.setState({ filter });
    }

    updateSort(sort) {
        this.setState({ sort });
    }

    updateList = () => {
        this.getAllUserItems();
    }

    getAllUserItems() {
        axios.get(Constants.httpRequestBasePath + 'api/GetAllUserItems', {
            headers: {
                'Authorization': 'Bearer ' + localStorage.getItem("auth")
            }
        }).then(res => {
            if (res) {
                const itemList = res.data;
                this.setState({ itemList, showItems: true });
            }
            else {
                //TODO: display that the user has no items.
            }
            }).catch(error => {
                if (error.response.status == 401) {
                    localStorage.removeItem('auth');
                    (this.props as any).history.push("/");
                }
                console.log(error);
        })
    }

    sort = (a, b) => {
        switch (this.state.sort.sortType) {
            case 'Price':
                if (a.Price * this.state.sort.byPriceAsc > b.Price * this.state.sort.byPriceAsc) return -1;
                return 1;
            case 'Name':
                if (a.Name > b.Name) return 1;
                return -1;
            case 'Category':
                if (a.Category > b.Category) return -1;
                return 1;
            default:
                return 0;
        }
    }

    populateTableWithItems() {
        let itemList = this.state.itemList;
        //This filters the items, if new filters for data are added need to add logic here.
        if (this.state.filter !== undefined) {
            if (this.state.filter.priceCompare !== 'All') {
                itemList = itemList.filter(x => x.Price == this.state.filter.price
                    || x.Price * this.state.filter.priceCompare > this.state.filter.price * this.state.filter.priceCompare);
            }
            if (this.state.filter.category !== 'All') {
                itemList = itemList.filter(x => x.Category === this.state.filter.category);
            }
        }
        if (this.state.sort !== undefined) {
            itemList.sort(this.sort);
        }
        return (
            <tbody>
                {itemList.map((item, index) =>
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
            </tbody>);
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
                        {this.populateTableWithItems()}
                    </table>
                </div> :
                    <img className="img-responsive loading" src={"Rolling.gif"} />
                }
                <EditItem onRef={ref => (this.child = ref)} updateListAfterChange={this.updateList} />
            </div>);
    }
}
