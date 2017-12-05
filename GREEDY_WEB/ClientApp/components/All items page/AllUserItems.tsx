import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { FetchUserItems } from './FetchUserItems';
import { ChooseFiltersForItems } from './ChooseFiltersForItems';
import Constants from '../Shared/Constants';

export class AllUserItems extends React.Component<RouteComponentProps<{}>> {
    child: any;
    constructor() {
        super();
    }

    updateFilters = (priceCompare, price, category) => {
        switch (priceCompare) {
            case 'All':
                break;
            case 'More than':
                priceCompare = 1;
                break;
            case 'Less than':
                priceCompare = -1;
                break;
            case 'Equal':
                priceCompare = 0;
                break;
            default:
                priceCompare = 'All';
        }
        let filter = {
            priceCompare,
            price,
            category
        }
        this.child.updateFilter(filter);
    }

    updateSort = (sortType, byPriceAsc) => {
        if (sortType === undefined) return;
        switch (byPriceAsc) {
            case 'Highest first':
                byPriceAsc = 1
                break;
            case 'Lowest first':
                byPriceAsc = -1
                break;
            default:
                byPriceAsc = 1;
        }
        let sort = {
            sortType,
            asc: byPriceAsc
        }
        this.child.updateSort(sort);
    }

    public render() {
        return (
            <div>
                <ChooseFiltersForItems refilter={this.updateFilters} resort={this.updateSort} />
                <FetchUserItems onRef={ref => (this.child = ref)} history={this.props.history} />
            </div>
        );
    }
}
