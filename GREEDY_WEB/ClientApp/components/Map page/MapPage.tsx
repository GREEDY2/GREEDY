import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { GoogleMaps } from './GoogleMaps';
import axios from 'axios';
import Constants from '../Shared/Constants';

export class MapPage extends React.Component<RouteComponentProps<{}>> {
    state = {
        userShops: undefined
    }
    componentWillMount() {
        this.getAllUserShops();
    }

    getAllUserShops() {
        axios.get(Constants.httpRequestBasePath + 'api/Shops', {
            headers: {
                'Authorization': 'Bearer ' + localStorage.getItem("auth")
            }
        }).then(res => {
            this.setState({ userShops: res.data });
            if (res.data) {
                
            }
            else {
                
                //TODO: the user has no shops
            }
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
            return <img className="img-responsive loading" src={"Rolling.gif"} />;
        }
        return (  
            <div style={{
                /*TODO: do something about the fixed height */
                width: 'auto', height: '500px', marginLeft: '-15px', marginRight: '-15px'
            }}>
                <GoogleMaps shopList={this.state.userShops} />
            </div>
        );
    }
}
