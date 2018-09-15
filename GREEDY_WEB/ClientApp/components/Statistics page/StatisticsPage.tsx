import * as React from "react";
import { RouteComponentProps } from "react-router";
import axios from "axios";
import Constants from "../Shared/Constants";
import { Button } from "reactstrap";
import { ItemCategoryPieChart } from "./ItemCategoryPieChart";
import { MoneySpentInShopsBarChart } from "./MoneySpentInShops";
import { WeeklyShoppingsCombinedChart } from "./WeeklyShoppingsCombinedChart";

export class StatisticsPage extends React.Component<RouteComponentProps<{}>> {
    state = {
        graphData: undefined,
        showGraphs: false,
        size: {
            windowWidth: "100%",
            sideMargins: "0px"
        },
        hourStampForData: 0
    };

    constructor() {
        super();
    }

    componentWillMount() {
        this.getData(this.state.hourStampForData);
    }

    componentDidMount() {
        this.updateWindowDimensions();
        window.addEventListener("resize", this.updateWindowDimensions);
    }

    componentWillUnmount() {
        window.removeEventListener("resize", this.updateWindowDimensions);
    }

    updateWindowDimensions = () => {
        if (this.state.size.windowWidth != "70%" && window.innerWidth >= 1000) {
            this.setState({ size: { windowWidth: "70%", sideMargins: "0px" } });
        } else if (this.state.size.sideMargins == "0px" && window.innerWidth < 768) {
            this.setState({ size: { windowWidth: "100%", sideMargins: "-15px" } });
        } else if ((this.state.size.windowWidth != "100%" || this.state.size.sideMargins != "0px") &&
            window.innerWidth < 1000 &&
            window.innerWidth >= 768) {
            this.setState({ size: { windowWidth: "100%", sideMargins: "0px" } });
        }
    };

    getData(hours) {
        this.setState({ showGraphs: false });
        axios.get(Constants.httpRequestBasePath + "api/GraphData/" + hours,
            {
                headers: {
                    'Authorization': `Bearer ${localStorage.getItem("auth")}`
                }
            }).then(response => {
            const graphData = response.data;
            this.setState({ graphData, showGraphs: true, hourStampForData: hours });
        }).catch(error => {
            if (error.response)
                if (error.response.status == 401) {
                    localStorage.removeItem("auth");
                    this.props.history.push("/");
                } else {
                    //TODO: no internet
                }
        });
    }

    render() {
        if (!this.state.showGraphs) {
            return <img className="img-responsive loading" src={"Rolling.gif"}/>;
        }
        return (
            <div style={{ textAlign: "center" }}>
                <div className="row" style={{ marginTop: "7px" }}>
                    <div className="btn-group inline">
                        <Button type="button" className="btn btn-primary" disabled={this.state.hourStampForData === 0
} onClick={() => this.getData(0)}>All time</Button>
                        <Button type="button" className="btn btn-primary" disabled={this.state.hourStampForData ===
                            524160} onClick={() => this.getData(524160)}>Last year</Button>
                        <Button type="button" className="btn btn-primary" disabled={this.state.hourStampForData ===
                            43200} onClick={() => this.getData(43200)}>Last month</Button>
                    </div>
                    <div className="btn-group inline">
                        <Button type="button" className="btn btn-primary" disabled={this.state.hourStampForData ===
                            10080} onClick={() => this.getData(10080)}>Last week</Button>
                        <Button type="button" className="btn btn-primary" disabled={this.state.hourStampForData ===
                            4320} onClick={() => this.getData(4320)}>Last 3 days</Button>
                        <Button type="button" className="btn btn-primary" disabled={this.state.hourStampForData ===
                            1440} onClick={() => this.getData(1440)}>Last day</Button>
                    </div>
                </div>
                <ItemCategoryPieChart data={this.state.graphData.CategoriesData} size={this.state.size}/>
                <MoneySpentInShopsBarChart data={this.state.graphData.MoneySpentInShops} size={this.state.size}/>
                <WeeklyShoppingsCombinedChart dataCount={this.state.graphData.WeekShoppingCount} dataPrice={this.state
                    .graphData.WeekShoppingPrice} size={this.state.size}/>
            </div>
        );
    }
}