﻿import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { FetchUserItems } from './FetchUserItems';
import { GetCredentialsFromCookies } from './HelperClass';
import { ChooseFiltersForItems } from './ChooseFiltersForItems';
import Constants from './Constants';

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

    public render() {
        let username = GetCredentialsFromCookies().Username;
        return (
            <div>
                <ChooseFiltersForItems refilter={this.updateFilters} />
                <FetchUserItems onRef={ref => (this.child = ref)} username={username}/>
            </div>
        );
    }
}
