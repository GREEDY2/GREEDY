import * as React from "react";
import { MapHint } from "./MapHint";

interface Props {
    key: number;
    lat: number;
    lng: number;
    hover: boolean;
    click: boolean;
    shopInfo: any;
    unclick: any;
}

export default class Marker extends React.Component<Props> {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div>
                <MapHint hover={this.props.hover} click={this.props.click}
                         content={this.props.shopInfo} close={this.props.unclick}/>
                <div className="pin" style={{ cursor: "pointer" }}></div>
                <div className="pulse"></div>

            </div>
        );
    }
}