import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { Logo } from './Logo';
import { ItemPriceGraph } from './ItemPriceGraph';

export class StatisticsPage extends React.Component<RouteComponentProps<{}>> {
    constructor() {
        super();
    }

    public render() {
        return (
            <div>
                <Logo />
                <ItemPriceGraph history={this.props.history} />
            </div>
        );
    }
}
