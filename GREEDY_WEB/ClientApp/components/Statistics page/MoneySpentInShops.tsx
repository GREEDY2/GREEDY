import * as React from 'react';
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
    size: {
        windowWidth: string,
        sideMargins: string
    };
}

export class MoneySpentInShopsBarChart extends React.Component<Props> {
    constructor(props) {
        super(props);
    }

    state = {
        name: 'Money spent in shops bar chart',
        id: 'money_spent_in_shops_bar_chart',
        type: 'column3d',
        width: this.props.size.windowWidth,
        height: 400,
        dataFormat: 'JSON',
        dataSource: {
            chart: {
                bgColor: '#FFFFFF',
                numberSuffix: '\u20AC',
                xaxisname: "Shop name",
                yaxisname: "Amount (In EUR)",
                caption: 'Money spent in shops',
                subcaption: 'Top shops by money spent',
                plottooltext: '$datavalue spent in $label'
            },
            data: this.props.data
        }
    };

    render() {
        return (
            <div style={{ marginLeft: this.props.size.sideMargins, marginRight: this.props.size.sideMargins }}>
                <ReactFC {...this.state} />
            </div>)
    }
}