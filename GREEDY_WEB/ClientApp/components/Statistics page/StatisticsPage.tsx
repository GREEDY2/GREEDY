import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import axios from 'axios';
import Constants from '../Shared/Constants';
import { Logo } from '../Shared/Logo';
import { ItemPriceGraph } from './ItemPriceGraph';
import { AverageStorePriceGraph } from './AverageStorePriceGraph';
import { MostBoughtItemsGraph } from './MostBoughtItemsGraph';
import { ShopVisitCountGraph } from './ShopVisitCountGraph';
import { ShopItemCountGraph } from './ShopItemCountGraph';
import { WeekVisitsGraph } from './WeekVisitsGraph';
import { ItemCategoryPieChart } from './ItemCategoryPieChart';
import { Alert } from '../Shared/Alert';

export class StatisticsPage extends React.Component<RouteComponentProps<{}>> {
    state = {
        graphData: undefined,
        showGraphs: false
    }
    constructor() {
        super();
    }

    componentWillMount() {
        this.getData();
    }

    getData() {
        axios.get(Constants.httpRequestBasePath + 'api/GraphData/' + 9999999 ,
            {
                headers: {
                    'Authorization': 'Bearer ' + localStorage.getItem("auth")
                }
            }).then(response => {
                let graphData = response.data;
                this.setState({ graphData, showGraphs: true });
            }).catch(error => {
                if (error.response)
                    if (error.response.status == 401) {
                        localStorage.removeItem('auth');
                        this.props.history.push("/");
                    }
                    else {
                        //TODO: no internet
                    }
            })
    }

    public render() {
        if (!this.state.showGraphs) {
            return <img className="img-responsive loading" src={"Rolling.gif"} />;
        }
        return (
            <div>
                <ItemCategoryPieChart data={this.state.graphData.CategoriesData} />
                {/*<Logo />
                <WeekVisitsGraph history={this.props.history} />
                <AverageStorePriceGraph history={this.props.history} />
                <MostBoughtItemsGraph history={this.props.history} />
                <ShopVisitCountGraph history={this.props.history} />
                <ShopItemCountGraph history={this.props.history} />
                <ItemPriceGraph history={this.props.history} /> */}
            </div>
        );
    }
}
