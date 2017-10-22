import * as React from 'react';

export class Logo extends React.Component {
    public render() {
        return (
            <div className="row col-xs-12 text-center loginLogo">
                <img
                    className="img-responsive logo"
                    src={"Logo.png"}
                    height="100%" />
            </div>
        );
    }
}