import * as React from "react";
// Load the charts module
import * as charts from "fusioncharts/fusioncharts.charts";
import ReactFC from "react-fusioncharts";
import * as FusionCharts from "FusionCharts";

charts(FusionCharts);

interface Props {
    data: Array<{
        label: string,
        value: number;
    }>;
    size: {
        windowWidth: string,
        sideMargins: string;
    };
}

export class ItemCategoryPieChart extends React.Component<Props> {
    constructor(props) {
        super(props);
    }

    state = {
        name: "Category pie chart",
        id: "category_pie_chart",
        type: "pie3d",
        width: this.props.size.windowWidth,
        height: 400,
        dataFormat: "JSON",
        dataSource: {
            chart: {
                caption: "Categories",
                subcaption: "Purchased items by categories",
                showlabels: "0",
                toolTipBgAlpha: "80",
                showlegend: "1",
                showpercentvalues: "1",
                plottooltext: "$datavalue items of category $label"
            },
            data: this.props.data
        }
    };

    render() {
        return (
            <div style={{ marginLeft: this.props.size.sideMargins, marginRight: this.props.size.sideMargins }}>
                <ReactFC {...this.state}/>
            </div>);
    }
}