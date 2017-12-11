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
    history: any
}

interface State {
    graphData: any;
    showGraphs: boolean;
    time: number;
}

export class ItemPriceGraph extends React.Component<Props> {
    state = {
        name: 'React is cool',
        id: 'pie_chart',
        type: 'pie3d',
        width: '100%',
        height: 400,
        dataFormat: 'JSON',
        dataSource: {
            chart: {
                caption: 'Item Price',
                subcaption: 'item price pie chart',
                startingangle: '120',
                showlabels: '0',
                showlegend: '1',
                enablemultislicing: '0',
                slicingdistance: '15',
                showpercentvalues: '1',
                showpercentintooltip: '1',
                plottooltext: 'Item price: $label is $datavalue',
                theme: 'ocean'
            },
            data: [{ label: 'kazkastai', value: 78 }, { label: 'kazkastaikito', value: 45 }, { label: 'kazkastaitrecio', value: 20 }]
        }
    };


    constructor(props) {
        super(props);
    }

    componentWillMount() {
        this.readGraphData();
    }

    readGraphData(/*time*/) {
        axios.put(Constants.httpRequestBasePath + 'api/GraphData', 3600,
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


    /*onTimeButtonClick(seconds) {
        if (seconds < 1)
            seconds = 600;
    
        this.state.time = seconds;
        this.readGraphData(this.state.time);
    }*/

    render() {

        var myDataSource = {
            chart: {
                bgColor: '#FFFFFF',
                caption: "Harry's SuperMart",
                subCaption: "Top 5 stores in last month by revenue",
                numberPrefix: "$"
            }, data: [{ label: "Bakersfield Central", value: "880000" },
            { label: "Garden Groove harbour", value: "730000" }, { label: "Los Angeles Topanga", value: "590000" }, { label: "Compton-Rancho Dom", value: "520000" }, { label: "Daly City Serramonte", value: "330000" }]
        };
        var revenueChartConfigs = {
            id: "revenue-chart",
            type: "column3d",
            width: "100%",
            height: 400,
            dataFormat: "json",
            dataSource: myDataSource
        }

        return (
            <div>
                <ReactFC {...this.state} />
                <ReactFC {...revenueChartConfigs} />
            </div>)
    }
}

