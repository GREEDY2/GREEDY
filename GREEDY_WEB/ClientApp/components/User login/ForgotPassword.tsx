import * as React from "react";
import { RouteComponentProps } from "react-router";

export class ForgotPassword extends React.Component<RouteComponentProps<{}>> {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div>
                <h1> Forgot your password? Too bad for you :( </h1>
            </div >);
    }
}