import * as React from "react";
import { RouteComponentProps } from "react-router";
import { Logo } from "../Shared/Logo";
import { ChangeEmail } from "./ChangeEmail";
import { ChangePassword } from "./ChangePassword"

export class UserSettings extends React.Component<RouteComponentProps<{}>> {
    constructor() {
        super();
    }

    render() {
        return (
            <div>
                <Logo/>
                <ChangeEmail/>
                <ChangePassword/>
            </div>
        );
    }
}