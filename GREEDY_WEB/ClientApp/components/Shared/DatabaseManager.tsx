import * as React from "react";
import { RouteComponentProps } from "react-router";
import Constants from "./Constants";
import axios from "axios";
import { putArrayToDb, putDistinctValuesArrayToDb } from "./DatabaseFunctions";

export class DatabaseManager extends React.Component<RouteComponentProps<{}>> {
    componentDidMount() {
        this.getAllUserItems();
        this.getAllDistinctCategories();
    }

    getAllDistinctCategories = () => {
        axios.get(Constants.httpRequestBasePath + "api/GetDistinctCategories",
            {
                headers: {
                    'Authorization': `Bearer ${localStorage.getItem("auth")}`
                }
            }).then(response => {
            const res = response.data;
            putDistinctValuesArrayToDb("categories", res);
        }).catch(error => {
            if (error.response)
                if (error.response.status == 401) {
                    localStorage.removeItem("auth");
                    this.props.history.push("/");
                } else {
                    //TODO: no internet
                }
            console.log(error);
        });
    };

    getAllUserItems() {
        axios.get(Constants.httpRequestBasePath + "api/GetAllUserItems",
            {
                headers: {
                    'Authorization': `Bearer ${localStorage.getItem("auth")}`
                }
            }).then(res => {
            const itemList = res.data;
            putArrayToDb("myItems", itemList);
        }).catch(error => {
            if (error.response)
                if (error.response.status == 401) {
                    localStorage.removeItem("auth");
                    this.props.history.push("/");
                } else {
                    //TODO: no internet
                }
            console.log(error);
        });
    }

    render() {
        return null;
    }
}