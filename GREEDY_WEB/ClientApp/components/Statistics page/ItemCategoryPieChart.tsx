import * as React from 'react';
import axios from 'axios';
import { ComposedChart, Area, Bar, LineChart, Line, XAxis, YAxis, CartesianGrid, Tooltip, Legend } from 'recharts';
import { Button, ButtonGroup } from 'reactstrap';
import Constants from '../Shared/Constants';

// Load the charts module
import * as charts from 'fusioncharts/fusioncharts.charts';
import ReactFC from 'react-fusioncharts';
import * as FusionCharts from "FusionCharts";

charts(FusionCharts);

interface Props {
    data: Array<{
        label: string,
        value: number
    }>;
}

export class ItemCategoryPieChart extends React.Component<Props> {
    constructor(props) {
        super(props);
    }

    state = {
        name: 'Category pie chart',
        id: 'category_pie_chart',
        type: 'pie3d',
        width: '100%',
        height: 400,
        dataFormat: 'JSON',
        dataSource: {
            chart: {
                caption: 'Categories',
                subcaption: 'Purchased items by categories',
                showlabels: '0',
                showlegend: '1',
                showpercentvalues: '1',
                plottooltext: '$datavalue items of category $label'
            },
            data: this.props.data
        }
    };

    render() {
        return (
            <div>
                <ReactFC {...this.state} />
            </div>)
    }
}