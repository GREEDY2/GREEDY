import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { GoogleMaps } from './GoogleMaps';
import axios from 'axios';
import Constants from '../Shared/Constants';

export class MapPage extends React.Component<RouteComponentProps<{}>> {
    state = {
        userShops: undefined,
        mapHeight: 500
    }
    componentWillMount() {
        this.getAllUserShops();
        if (window.innerWidth < 767) {
            this.setState({ mapHeight: window.innerHeight - 51 })
        }
        else {
            this.setState({ mapHeight: window.innerHeight - 1 })
        }      
    }

    getAllUserShops() {
        axios.get(Constants.httpRequestBasePath + 'api/Shops', {
            headers: {
                'Authorization': 'Bearer ' + localStorage.getItem("auth")
            }
        }).then(res => {
            this.setState({ userShops: res.data });
        }).catch(error => {
            if (error.response)
                if (error.response.status == 401) {
                    localStorage.removeItem('auth');
                    this.props.history.push("/");
                }
                else {
                    //TODO: no internet
                }
            console.log(error);
        })
    }

    public render() {
        if (this.state.userShops === undefined) {
            return <img className="img-responsive loading" src={"Rolling.gif"} style={{top: '50%', left: '50%'}} />;
        }
        return (  
            <div style={{
                width: 'auto', height: this.state.mapHeight +'px', marginLeft: '-15px', marginRight: '-15px'
            }}>
                <GoogleMaps shopList={this.state.userShops} />
            </div>
        );
    }
}
