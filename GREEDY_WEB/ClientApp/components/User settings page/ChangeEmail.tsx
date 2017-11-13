import * as React from 'react';
import { Button } from 'reactstrap';
import axios from 'axios';
import DocumentTitle from 'react-document-title';
import { RegistrationForm } from 'react-stormpath';
import Constants from '../Shared/Constants';

export class ChangeEmail extends React.Component {
    constructor() {
        super();
    }

    onFormSubmit = (e, next) => {
        e.preventDefault();
        var data = e.data;

        var regex = Constants.emailRegex;

        if (data.email.length < 1) {
            return next(new Error('Email can not be empty'));
        }

        if (!regex.test(data.email)) {
            return next(new Error('Incorrect new email'));
        }

        if (data.email.length > Constants.maxAnyInputLength) {
            return next(new Error('Email is too long'));
        }

        let changeEmail = {}
        changeEmail["password"] = data.password;
        changeEmail["email"] = data.email;

        axios.put(Constants.httpRequestBasePath + 'api/ChangeEmail', changeEmail,
            {
                headers: {
                    'Authorization': 'Bearer ' + localStorage.getItem("auth")
                }
            }).then(response => {
                let res = response.data;
                if (res) {
                    this.setState({ isAccountCreated: true });
                }
                else
                    return next(new Error('Wrong password'));
            }).catch(error => {
                //TODO: add extra logic (and to all catches aswell) 
                //when authorization in each request with the server is implementated
                return next(new Error('Unable to change email at this time, try again later'));
            });
    }

    public render() {
        return (
            <DocumentTitle title={`Register`}>
                <RegistrationForm onSubmit={this.onFormSubmit.bind(this)}>
                    <h3 className="text-center">Change email</h3>
                    <div className='sp-login-form regForm'>
                        <div className="row">
                            <div className="col-xs-12">
                                <div className="form-horizontal">
                                    <div className="form-group">
                                        <label
                                            htmlFor="spPasswordChangeEmail"
                                            className="col-xs-12 col-sm-4 control-label">
                                            Password
                                </label>
                                        <div className="col-xs-12 col-sm-4">
                                            <input
                                                type="password"
                                                className="form-control"
                                                id="spPasswordChangeEmail"
                                                placeholder="Password"
                                                name="password" />
                                        </div>
                                    </div>
                                    <div className="form-group">
                                        <label
                                            htmlFor="spChangeEmail"
                                            className="col-xs-12 col-sm-4 control-label">
                                            New email
                                </label>
                                        <div className="col-xs-12 col-sm-4">
                                            <input
                                                className="form-control"
                                                id="spChangeEmail"
                                                placeholder="New email"
                                                name="email" />
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
                                                Change email
                                    </Button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </RegistrationForm >
            </DocumentTitle >
        );
    }
}
