import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { Logo } from '../Shared/Logo';
import { ItemPriceGraph } from './ItemPriceGraph';
import { AverageStorePriceGraph } from './AverageStorePriceGraph';
import { MostBoughtItemsGraph } from './MostBoughtItemsGraph';
import { ShopVisitCountGraph } from './ShopVisitCountGraph';
import { ShopItemCountGraph } from './ShopItemCountGraph';
import { WeekVisitsGraph } from './WeekVisitsGraph';
import { Alert } from '../Shared/Alert';

export class StatisticsPage extends React.Component<RouteComponentProps<{}>> {
    constructor() {
        super();
    }

    public render() {
        return (
            <div>
                <Logo />
                <WeekVisitsGraph history={this.props.history} />
                <AverageStorePriceGraph history={this.props.history} />
                <MostBoughtItemsGraph history={this.props.history} />
                <ShopVisitCountGraph history={this.props.history} />
                <ShopItemCountGraph history={this.props.history} />
                <ItemPriceGraph history={this.props.history} />
            </div>
        );
    }
}
