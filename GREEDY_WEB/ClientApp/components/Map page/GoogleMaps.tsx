import * as React from 'react';
import GoogleMap from 'google-map-react';
import MapOptions from './MapOptions';
import Marker from './Marker';
import UserLocationMarker from './UserLocationMarker';

interface State {
    center: Array<number>;
    zoom: number;
    myLocation: {
        lat: number,
        lng: number
    }
    hoverKey: number;
}

export class GoogleMaps extends React.Component<{}, State> {
    timer: number;
    state = {
        center: [54.729000, 25.272000],
        zoom: 14,
        shopMarkers: [
            { name: 'Norfa', lat: 54.736675, lng: 25.267515 },
            { name: 'IKI', lat: 54.728862, lng: 25.269294 },
        ],
        myLocation: undefined,
        hoverKey: undefined
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
        // the divide by 1.3 at the end aplifies the clickable location 1.3 times
        return Math.sqrt((x - mousePos.x) * (x - mousePos.x) + (y - mousePos.y) * (y - mousePos.y)) / 1.3;
    }

    _onBoundsChange = ({ center, zoom, bounds, ...other }) => {
        console.log(center);
        console.log(other);
    }

    _onChildClick = (key, childProps) => {
        this.setState({
            center: [childProps.lat, childProps.lng]
        });
        console.log("mouse clicked")
        console.log(childProps);
    }

    _onChildMouseEnter = (key/*, childProps*/) => {
        this.setState({ hoverKey: key });
        console.log("mouse entered");
        console.log(key);
    }

    _onChildMouseLeave = (/* key, childProps */) => {
        this.setState({ hoverKey: undefined });
        console.log('mouse left');
    }

    render() {
        const shopMarkers = this.state.shopMarkers.map((place, index) => {
            const { name, ...coords } = place;
            let hover = false;
            if (this.state.hoverKey === index) {
                hover = true;
            }
            return (
                <Marker
                    {...coords}
                    hover={hover}
                />
            );
        });

        return (
            <GoogleMap
                bootstrapURLKeys={{ key: "AIzaSyDnbiTeBy0XOXYfuto5VRP272gkzQAX5MQ" }}
                center={this.state.center}
                zoom={this.state.zoom}
                options={MapOptions}
                hoverDistance={5}
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