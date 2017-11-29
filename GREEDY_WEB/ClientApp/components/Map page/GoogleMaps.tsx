import * as React from 'react';
import GoogleMap from 'google-map-react';
import MapOptions from './MapOptions';
import Marker from './Marker';
import { K_CIRCLE_SIZE, K_STICK_SIZE } from './style';
import UserLocationMarker from './UserLocationMarker';

const AnyReactComponent = ({ lat, lng, text }) => <div style={{
    position: 'relative', color: 'red', background: 'green',
    height: 40, width: 60, top: -20, left: -30,
}}>{text}</div>;

interface State {
    center: Array<number>;
    zoom: number;
    myLocation: {
        lat: number,
        lng: number
    }
}

export class GoogleMaps extends React.Component<{}, State> {
    state = {
        center: [54.729000, 25.272000],
        zoom: 14,
        shopMarkers: [
            { name: 'Norfa', lat: 54.736675, lng: 25.267515 },
            { name: 'IKI', lat: 54.728862, lng: 25.269294 },
        ],
        myLocation: undefined
    };

    componentWillMount() {
        navigator.geolocation.getCurrentPosition(position => {
            this.setState({
                center: [position.coords.latitude, position.coords.longitude],
                myLocation: { lat: position.coords.latitude, lng: position.coords.longitude }
            });
        });
    }

    _distanceToMouse = (markerPos, mousePos, markerProps) => {
        const x = markerPos.x;
        // because of marker non symmetric,
        // we transform it central point to measure distance from marker circle center
        // you can change distance function to any other distance measure
        const y = markerPos.y - K_STICK_SIZE - K_CIRCLE_SIZE / 2;

        // and i want that hover probability on markers with text === 'A' be greater than others
        // so i tweak distance function (for example it's more likely to me that user click on 'A' marker)
        // another way is to decrease distance for 'A' marker
        // this is really visible on small zoom values or if there are a lot of markers on the map
        const distanceKoef = markerProps.text !== 'A' ? 1.5 : 1;

        // it's just a simple example, you can tweak distance function as you wish
        return distanceKoef * Math.sqrt((x - mousePos.x) * (x - mousePos.x) + (y - mousePos.y) * (y - mousePos.y));
    }

    _onBoundsChange = ({ center, zoom, bounds, ...other }) => {
        console.log(center);
        console.log(other);
    }

    _onChildClick = (key, childProps) => {
        this.setState({
            center: [childProps.lat, childProps.lng]
        });
        console.log(childProps);
    }

    _onChildMouseEnter = (key /*, childProps */) => {
        console.log(key);
    }

    _onChildMouseLeave = (/* key, childProps */) => {
        console.log('leave');
    }

    render() {
        const shopMarkers = this.state.shopMarkers.map(place => {
            const { name, ...coords } = place;
            return (
                <Marker
                    name={name}
                    {...coords}
                    hover={(this.props as any).hoverKey === name}
                />
            );
        });

        return (
            <GoogleMap
                bootstrapURLKeys={{ key: "AIzaSyDnbiTeBy0XOXYfuto5VRP272gkzQAX5MQ" }}
                center={this.state.center}
                zoom={this.state.zoom}
                options={MapOptions}
                hoverDistance={K_CIRCLE_SIZE / 2}
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