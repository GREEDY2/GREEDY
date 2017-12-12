import * as React from 'react';
// Load the charts module
import * as charts from 'fusioncharts/fusioncharts.charts';
import ReactFC from 'react-fusioncharts';
import * as FusionCharts from "FusionCharts";

charts(FusionCharts);

interface Props {
    dataPrice: Array<{
        label: string,
        value: number
    }>;
    dataCount: Array<{
        label: string,
        value: number
    }>;
    size: {
        windowWidth: string,
        sideMargins: string
    };
}

export class WeeklyShoppingsCombinedChart extends React.Component<Props> {
    constructor(props) {
        super(props);
    }
    state = {
        name: 'Weekly shoppings',
        id: 'weekly_shoppings',
        type: 'mscolumn3dlinedy',
        renderAt: 'chart-container',
        width: this.props.size.windowWidth,
        height: '400',
        dataFormat: 'json',
        dataSource: {
            chart: {
                caption: "Week days",
                subCaption: "Times shopping and money spent by week days",
                xAxisname: "Day of the week",
                pYAxisName: "Money spent (In EUR)",
                sYAxisName: "Times shopped",
                numberSuffix: '\u20AC',
                theme: "fint"
            },
            categories: [{
                category: [{
                    label: "Monday"
                }, {
                    label: "Tuesday"
                }, {
                    label: "Wedsnesday"
                }, {
                    label: "Thursday"
                }, {
                    label: "Friday",
                }, {
                    label: "Saturday"
                }, {
                    label: "Sunday"
                }]
            }],
            dataset: [{
                seriesName: "Money spent",
                data: this.props.dataPrice
            }, {
                seriesName: "Times Shopped",
                parentYAxis: "S",
                renderAs: "line",
                showValues: "0",
                data: this.props.dataCount
            }]
        }
    };

    componentWillMount() {
        
    }

    render() {
        if (this.props.size.sideMargins !== '0px') {
            let categories = [{
                category: [{
                    label: "Mon"
                }, {
                    label: "Tue"
                }, {
                    label: "Wed"
                }, {
                    label: "Thu"
                }, {
                    label: "Fri"
                }, {
                    label: "Sat"
                }, {
                    label: "Sun"
                }]
            }]
            this.state.dataSource.categories = categories;
        }

        var day = new Date().getDay() - 1;
        if (day === -1) day = 6;
        (this.state.dataSource.categories[0].category[day] as any).labelFontBold = '1'
        
        return (
            <div style={{ marginLeft: this.props.size.sideMargins, marginRight: this.props.size.sideMargins }}>
                <ReactFC {...this.state} />
            </div>)
    }
}