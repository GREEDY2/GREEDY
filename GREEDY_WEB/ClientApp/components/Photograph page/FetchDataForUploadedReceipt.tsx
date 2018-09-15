import * as React from "react";
import axios from "axios";
import { Button } from "reactstrap";
import Constants from "../Shared/Constants";
import { EditItem } from "./EditItem";
import { AddItem } from "./AddItem";
import { Alert } from "../Shared/Alert";
import * as idb from "../Shared/DatabaseFunctions";

interface Props {
    onRef: any
}

interface State {
    receiptId: number;
    showItems: boolean;
    itemList: Array<{
        ItemId: number,
        Name: string,
        Price: number,
        Category: string;
    }>,
    shopInfo: {
        name: string,
        address: string;
    },
    receiptInfo: {
        total: number,
        date: string;
    },
    imageIsUploading: boolean;
}

export class FetchDataForUploadedReceipt extends React.Component<Props, State> {
    child2: any;
    child: any;
    state = {
        receiptId: undefined,
        showItems: false,
        itemList: undefined,
        shopInfo: undefined,
        receiptInfo: undefined,
        imageIsUploading: false
    };

    componentDidMount() {
        this.props.onRef(this);
    }

    componentWillUnmount() {
        this.props.onRef(undefined);
    }

    updateList = () => {
        this.getItemsFromPhoto(this.state.receiptId);
    };

    showAdd = () => {
        this.child2.showAdd(this.state.receiptId);
    };

    imageUploadStarted(bool) {
        if (bool) {
            this.setState({ imageIsUploading: true });
        } else {
            this.setState({ imageIsUploading: false });
        }
    }

    getItemsFromPhoto(receiptId) {
        axios.get(Constants.httpRequestBasePath + "api/GetItemsFromPostedReceipt/" + receiptId,
            {
                headers: {
                    'Authorization': `Bearer ${localStorage.getItem("auth")}`
                }
            }).then(res => {
            if (res.data) {
                const itemList = res.data.list;
                const shopInfo = {
                    name: res.data.shopName,
                    address: res.data.shopAdress,
                };
                const receiptInfo = {
                    date: res.data.receiptDate,
                    total: res.data.total
                };
                this.setState({
                    itemList,
                    showItems: true,
                    receiptId,
                    imageIsUploading: false,
                    shopInfo,
                    receiptInfo
                });
                idb.putArrayToDb("myItems", itemList);
            } else {
                this.setState({ imageIsUploading: false, showItems: false });
                this.child.showAlert("Unable to find any items. Please retake the picture", "info");
            }
        }).catch(error => {
            console.log(error);
            this.setState({ imageIsUploading: false, showItems: false });
            this.child.showAlert("Something went wrong, please try again later", "error");
        });
    }

    render() {
        if (this.state.imageIsUploading) {
            return <img className="img-responsive loading" src={"Rolling.gif"}/>;
        }
        if (!this.state.showItems) {
            return <Alert onRef={ref => (this.child = ref)}/>;
        }
        return (
            <div>
                {
                    this.state.shopInfo.name &&
                        <div>
                            <div className="text-center receipt-name">
                                {this.state.shopInfo.name}
                            </div>
                            {
                            this.state.shopInfo.address &&
                                <div className="text-center receipt-address">
                                    {this.state.shopInfo.address}
                                </div>
                        }
                        </div>
                }
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
                            <td>
                                <span
                                    className="glyphicon glyphicon-pencil readGlyphs"
                                    color="primary"
                                    onClick={() =>
                                        this.child.showEdit(index, item.ItemId, item.Name, item.Price, item.Category)}>
                                </span>
                            </td>
                        </tr>
                    )}
                    <tr className="total-price">
                        <td></td>
                        <td>Total Price:</td>
                        <td>{this.state.receiptInfo.total}&#8364;</td>
                    </tr>
                    </tbody>
                </table>
                <div>
                    <Button className="col-xs-5 col-md-3" color="success" onClick={this.showAdd} style={{
                        textAlign: "center",
                        marginBottom: "5px"
                    }}>
                        Add a new item
                    </Button>
                    {
                        this.state.receiptInfo.date &&
                            <div className="nowrap text-right">
                                Date of purchase: {this.state.receiptInfo.date}
                            </div>
                    }
                </div>
                <AddItem onRef={ref => (this.child2 = ref)} updateListAfterChange={this.updateList}/>
                <EditItem onRef={ref => (this.child = ref)} updateListAfterChange={this.updateList}/>
            </div>);
    }
}