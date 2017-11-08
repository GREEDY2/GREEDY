import * as React from 'react';
import Constants from './Constants';
import { Button } from 'reactstrap';
import axios from 'axios';
import DocumentTitle from 'react-document-title';
import { RegistrationForm } from 'react-stormpath';

interface Props {
    username: string
}

export class ChangePassword extends React.Component<Props> {
    constructor() {
        super();
    }

    onFormSubmit = (e, next) => {
        e.preventDefault();
        var data = e.data;

        if (data.givenName.length < Constants.minPasswordLength) {
            return next(new Error('Password must be longer than ' + Constants.minPasswordLength + ' characters'));
        }

        if (data.givenName.length > Constants.maxAnyInputLength) {
            return next(new Error('Password is too long'));
        }

        if (data.givenName !== data.surname) {
            return next(new Error('Passwords do not match'));
        }

        if (data.password === data.givenName) {
            return next(new Error('New password is the same as current password'));
        }

        let changePassword = {}
        changePassword["username"] = this.props.username;
        changePassword["password"] = data.password;
        changePassword["newpassword"] = data.givenName;

        axios.put(Constants.httpRequestBasePath + 'api/ChangePassword', changePassword)
            .then(response => {
                let res = response.data;
                if (res) {
                    this.setState({ isAccountCreated: true });
                }
                else
                    return next(new Error('wrong password'));
            }).catch(error => {
                return next(new Error('Unable to change password at this time, try again later'));
            });
    }

    public render() {
        return (
            <DocumentTitle title={`Register`}>
                <RegistrationForm onSubmit={this.onFormSubmit.bind(this)}>
                    <h3 className="text-center">Change password</h3>
                    <div className='sp-login-form regForm row col-xs-12 form-horizontal'>
                        <div className="form-group">
                            <label
                                htmlFor="spChangePassword"
                                className="col-xs-12 col-sm-4 control-label">
                                Password
                                </label>
                            <div className="col-xs-12 col-sm-4">
                                <input
                                    type="password"
                                    className="form-control"
                                    id="spChangePassword"
                                    placeholder="Old password"
                                    name="password" />
                            </div>
                        </div>
                        <div className="form-group">
                            <label
                                htmlFor="spChangePasswordNew"
                                className="col-xs-12 col-sm-4 control-label">
                                New password
                                </label>
                            <div className="col-xs-12 col-sm-4">
                                <input
                                    type="password"
                                    className="form-control"
                                    id="spChangePasswordNew"
                                    placeholder="New password"
                                    name="givenName" />
                            </div>
                        </div>
                        <div className="form-group">
                            <label
                                htmlFor="spChangePasswordNewRepeat"
                                className="col-xs-12 col-sm-4 control-label">
                                Repeat new password
                                </label>
                            <div className="col-xs-12 col-sm-4">
                                <input
                                    type="password"
                                    className="form-control"
                                    id="spChangePasswordNewRepeat"
                                    placeholder="Repeat new password"
                                    name="surname" />
                            </div>
                        </div>
                        <div className="form-group">
                            <div className="col-sm-offset-4 col-sm-4">
                                <p
                                    className="alert alert-danger"
                                    data-spIf="form.error">
                                    <span data-spBind="form.errorMessage" />
                                </p>
                                <Button
                                    className="col-xs-12 col-sm-12"
                                    type="submit"
                                    color="btn btn-primary buttonText">
                                    Change password
                                    </Button>
                            </div>
                        </div>
                    </div>
                </RegistrationForm >
            </DocumentTitle >
        );
    }
}
