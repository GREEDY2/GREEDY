import * as React from 'react';
import Constants from './Constants'

export class Logo extends React.Component {
    public render() {
        return (
            <div className="col-xs-12 text-center loginLogo">
                <img
                    className="img-responsive logo"
                    src={Constants.logoName}
                    height="100%" />
            </div>
        );
    }
}