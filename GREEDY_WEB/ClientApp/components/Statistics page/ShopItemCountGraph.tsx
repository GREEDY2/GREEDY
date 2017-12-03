import * as React from 'react';
import axios from 'axios';
import { ComposedChart, Area, Bar, LineChart, BarChart, Line, XAxis, YAxis, CartesianGrid, Tooltip, Legend } from 'recharts';
import { Button, ButtonGroup } from 'reactstrap';
import Constants from '../Shared/Constants';

interface Props {
    history: any
}

interface State {
    graphData: any;
    showGraphs: boolean;
    time: number;
}

export class ShopItemCountGraph extends React.Component<Props, State> {
    state = {
        graphData: [],
        showGraphs: false,
        time: 3600
    }
    constructor(props) {
        super(props);
    }

    componentWillMount() {
        this.readGraphData();
    }

    readGraphData(/*time*/) {
        axios.put(Constants.httpRequestBasePath + 'api/ShopItemCountGraphData', this.state.time,
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
        return (
            <div>
                {this.state.showGraphs ?
                    <div className="row">
                        <div className="col-md-6 dataGraph">
                            <h3 className="graphTitles h3">Shop items count</h3>
                            <BarChart width={450} height={150} data={this.state.graphData}>
                                {
                                    /*this.state.time < 3600 * 12 ?
                                        <XAxis dataKey="Time" /> :*/
                                    <XAxis dataKey="ShopName" />
                                }
                                <Tooltip />
                                <YAxis />
                                <Legend />
                                <Bar name="Items count" type='monotone' dot={false} dataKey='ItemsCount' stroke='#8884d8' strokeWidth={2} fill='#ffbfbf' />
                            </BarChart>
                        </div>
                    </div> :
                    <img className="img-responsive loading" src={"Rolling.gif"} />
                }
            </div>)
    }
}

