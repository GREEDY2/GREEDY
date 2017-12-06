import * as React from 'react';
import GoogleMap from 'google-map-react';
import MapOptions from './MapOptions';
import Marker from './Marker';
import UserLocationMarker from './UserLocationMarker';

interface Props {
    shopList: any;
}

interface State {
    center: Array<number>;
    zoom: number;
    myLocation: {
        lat: number,
        lng: number
    }
    hoverKey: number;
    clickKey: number;
}

export class GoogleMaps extends React.Component<Props, State> {
    timer: number;
    state = {
        center: [54.729000, 25.272000],
        zoom: 13,
        myLocation: undefined,
        hoverKey: undefined,
        clickKey: undefined
    };

    componentWillMount() {
        navigator.geolocation.getCurrentPosition(position => {
            this.setState({
                center: [position.coords.latitude, position.coords.longitude],
                myLocation: { lat: position.coords.latitude, lng: position.coords.longitude }
            });
        });
        this.timer = setInterval(() => this.setMyLocation(), 10000);
    }

    componentWillUnmount() {
        clearInterval(this.timer);
    }

    setMyLocation = () => {
        navigator.geolocation.getCurrentPosition(position => {
            this.setState({
                myLocation: { lat: position.coords.latitude, lng: position.coords.longitude }
            });
        });
    }

    // because of marker non symmetric,
    // we transform it central point to measure distance from marker circle center
    // the distance mesurment can be changed
    _distanceToMouse = (markerPos, mousePos, markerProps) => {
        // if hovered over users location then do nothing
        if (markerProps.hover === undefined) return undefined;
        // the -6 pushes the clickable location slightly to the left for centration purposes
        const x = markerPos.x - 6;
        // the -17 pushes the clickable location to the top 
        // (because we want not the location to be clickable but the pin)
        const y = markerPos.y - 17;

        // the function makes the clickable location into something like a circle
        // the divide by 2 at the end aplifies the clickable location 2 times
        return Math.sqrt((x - mousePos.x) * (x - mousePos.x) + (y - mousePos.y) * (y - mousePos.y)) / 2;
    }

    _onBoundsChange = ({ center, zoom, bounds, ...other }) => {
        this.setState({ center });
    }

    _onChildClick = (key, childProps) => {
        this.setState({
            center: [childProps.lat, childProps.lng],
            clickKey: key
        });
    }

    onChildUnclick = () => {
        this.setState({
            clickKey: undefined
        });
    }

    _onChildMouseEnter = (key/*, childProps*/) => {
        this.setState({ hoverKey: key });
    }

    _onChildMouseLeave = (/* key, childProps */) => {
        this.setState({ hoverKey: undefined });
    }

    

    render() {
        const shopMarkers = this.props.shopList.map((shop, index) => {
            let hover = false;
            if (this.state.hoverKey === index) {
                hover = true;
            }
            let click = false;
            if (this.state.clickKey === index) {
                click = true;
            }
            return (
                <Marker
                    {...shop.Location}
                    hover={hover}
                    click={click}
                    unclick={this.onChildUnclick}
                    shopInfo={shop}
                />
            );
        });

        return (
            <GoogleMap
                bootstrapURLKeys={{ key: "AIzaSyDnbiTeBy0XOXYfuto5VRP272gkzQAX5MQ" }}
                center={this.state.center}
                zoom={this.state.zoom}
                options={MapOptions}
                hoverDistance={10}
                distanceToMouse={this._distanceToMouse}
                onChange={this._onBoundsChange}
                onChildClick={this._onChildClick}
                onChildMouseEnter={this._onChildMouseEnter}
                onChildMouseLeave={this._onChildMouseLeave}
            >
                {shopMarkers}
                {this.state.myLocation &&
                    <UserLocationMarker {...this.state.myLocation} />
                }
            </GoogleMap >
        );
    }
}