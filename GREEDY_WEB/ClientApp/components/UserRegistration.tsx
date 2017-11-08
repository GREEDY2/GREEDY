import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { Button } from 'reactstrap';
import DocumentTitle from 'react-document-title';
import { RegistrationForm } from 'react-stormpath';
import axios from 'axios';
import { Link, NavLink } from 'react-router-dom';
import Constants from './Constants';

interface IState {
    isAccountCreated: any;
}

export class UserRegistration extends React.Component<RouteComponentProps<{}>, IState> {
    state = { isAccountCreated: false }

    onFormSubmit = (e, next) => {
        e.preventDefault();
        var data = e.data;

        if (data.username.length < Constants.minUsernameLength) {
            return next(new Error('Username must be longer than ' + Constants.minUsernameLength + ' characters'));
        }

        if (data.email.length < 1) {
            return next(new Error('Email must not be empty'));
        }

        var regex = Constants.emailRegex;
        if (!regex.test(data.email)) {
            return next(new Error('Incorrect email'));
        }

        if (data.givenName.length < 1) {
            return next(new Error('Fullname must not be empty'));
        }

        if (data.givenName.length > Constants.maxAnyInputLength * 2) {
            return next(new Error('Fullname is too long'));
        }

        if (data.username.length > Constants.maxAnyInputLength) {
            return next(new Error('Username is too long'));
        }

        if (data.email.length > Constants.maxAnyInputLength) {
            return next(new Error('Email is too long'));
        }

        if (data.password.length < Constants.minPasswordLength) {
            return next(new Error('Password must longer than ' + Constants.minPasswordLength + ' characters'));
        }
        if (data.password.length > Constants.maxAnyInputLength) {
            return next(new Error('Password is too long'));
        }

        if (data.password !== data.surname) {
            return next(new Error('Passwords do not match'));
        }

        let credentials = {}
        credentials["username"] = data.username;
        credentials["password"] = data.password;
        credentials["fullname"] = data.givenName;
        credentials["email"] = data.email;

        axios.post(Constants.httpRequestBasePath + 'api/Registration', credentials)
            .then(response => {
                let res = response.data;
                if (res) {
                    this.setState({ isAccountCreated: true });
                }
                else
                    return next(new Error('Username or email is already in use'));
            }).catch(error => {
                return next(new Error('Unable to register new users at this time, try again later'))
            });
    }

    public render() {
        return (
            <div>
                <DocumentTitle title={`Register`}>
                    <RegistrationForm onSubmit={this.onFormSubmit.bind(this)}>
                        <div className='sp-login-form regForm'>
                            {this.state.isAccountCreated ?
                                <div className="row">
                                    <div className="col-sm-offset-4 col-xs-12 col-sm-4">
                                        <p className="alert alert-success">
                                            Your account has been created.
                                        </p>
                                        <p>
                                            <Link to="/" className="pull-right">
                                                Login Now
                                            </Link>.
                                        </p>
                                    </div>
                                </div>
                                :
                                <div className="row">
                                    <div className="col-xs-12">
                                        <div className="form-horizontal">
                                            <div className="form-group">
                                                <label
                                                    htmlFor="spEmail"
                                                    className="col-xs-12 col-sm-4 control-label">
                                                    Email*
                                                    </label>
                                                <div className="col-xs-12 col-sm-4">
                                                    <input
                                                        className="form-control"
                                                        id="spEmail"
                                                        placeholder="Email"
                                                        name="email" />
                                                </div>
                                            </div>
                                            <div className="form-group">
                                                <label
                                                    htmlFor="spFullname"
                                                    className="col-xs-12 col-sm-4 control-label">
                                                    Fullname*
                                                    </label>
                                                <div className="col-xs-12 col-sm-4">
                                                    <input
                                                        className="form-control"
                                                        id="spFirstName"
                                                        placeholder="Fullname"
                                                        name="givenName" />
                                                </div>
                                            </div>
                                            <div className="form-group">
                                                <label
                                                    htmlFor="spUsername"
                                                    className="col-xs-12 col-sm-4 control-label">
                                                    Username*
                                                    </label>
                                                <div className="col-xs-12 col-sm-4">
                                                    <input
                                                        className="form-control"
                                                        id="spUsername"
                                                        placeholder="Username"
                                                        name="username" />
                                                </div>
                                            </div>
                                            <div className="form-group">
                                                <label
                                                    htmlFor="spPassword"
                                                    className="col-xs-12 col-sm-4 control-label">
                                                    Password*
                                                    </label>
                                                <div className="col-xs-12 col-sm-4">
                                                    <input
                                                        type="password"
                                                        className="form-control"
                                                        id="spPassword"
                                                        placeholder="Password"
                                                        name="password" />
                                                </div>
                                            </div>
                                            <div className="form-group">
                                                <label
                                                    htmlFor="spPasswordRepeat"
                                                    className="col-xs-12 col-sm-4 control-label">
                                                    Repeat password*
                                                    </label>
                                                <div className="col-xs-12 col-sm-4">
                                                    <input
                                                        type="password"
                                                        className="form-control"
                                                        id="spPasswordRepeat"
                                                        placeholder="Repeat password"
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
                                                        Register
                                                        </Button>
                                                </div>
                                            </div>
                                            <div className="col-sm-offset-4 col-sm-4">
                                                <p>* Required fields </p>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            }
                        </div>
                    </RegistrationForm>
                </DocumentTitle >
            </div >
        );
    }
}