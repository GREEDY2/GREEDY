import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { Button } from 'reactstrap';
import DocumentTitle from 'react-document-title';
import { RegistrationForm } from 'react-stormpath';
import axios from 'axios';
import { Link, NavLink } from 'react-router-dom';
import Cookies from 'universal-cookie';
import FacebookLogin from 'react-facebook-login';

interface IState {
    isAccountCreated: any;
}

export class UserRegistration extends React.Component<RouteComponentProps<{}>, IState> {
    state = { isAccountCreated: false }

    onFormSubmit = (e, next) => {
        e.preventDefault();
        var data = e.data;

        if (data.username.length < 1) {
            return next(new Error('Username must not be empty.'));
        }

        if (data.email.length < 1) {
            return next(new Error('Email must not be empty.'));
        }

        var regex = /^(([^<>()\[\]\.,;:\s@\"]+(\.[^<>()\[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/;
        if (!regex.test(data.email)) {
            return next(new Error('Wrong email.'));
        }

        if (data.givenName.length < 1) {
            return next(new Error('First name must not be empty.'));
        }

        if (data.surname.length < 1) {
            return next(new Error('Last name must not be empty.'));
        }

        if (data.givenName.length > 256) {
            return next(new Error('First name is too long.'));
        }

        if (data.surname.length > 256) {
            return next(new Error('Last name is too long.'));
        }

        if (data.username.length > 256) {
            return next(new Error('Username is too long.'));
        }

        if (data.email.length > 256) {
            return next(new Error('Email is too long.'));
        }

        // Require passwords to be at least 5 characters.
        if (data.password.length < 5) {
            return next(new Error('Password must longer than 5 characters.'));
        }
        if (data.username.length > 256) {
            return next(new Error('Password is too long.'));
        }
        let credentials = {}
        credentials["username"] = data.username;
        credentials["password"] = data.password;
        credentials["fullname"] = data.givenName + " " + data.surname;
        credentials["email"] = data.email;

        axios.put("http://localhost:6967/api/Registration", credentials)
            .then(response => {
                let res = response.data;
                if (res) {
                    this.setState({ isAccountCreated: true });
                }
                else
                    return next(new Error('Username or email is already in use'));
            }).catch(error => {
                console.log(error);
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
                                                    htmlFor="spUsername"
                                                    className="col-xs-12 col-sm-4 control-label">
                                                    Username
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
                                                    htmlFor="spEmail"
                                                    className="col-xs-12 col-sm-4 control-label">
                                                    Email
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
                                                    htmlFor="spFirstName"
                                                    className="col-xs-12 col-sm-4 control-label">
                                                    First Name
                                                    </label>
                                                <div className="col-xs-12 col-sm-4">
                                                    <input
                                                        className="form-control"
                                                        id="spFirstName"
                                                        placeholder="First Name"
                                                        name="givenName" />
                                                </div>
                                            </div>
                                            <div className="form-group">
                                                <label
                                                    htmlFor="spLastName"
                                                    className="col-xs-12 col-sm-4 control-label">
                                                    Last Name
                                                    </label>
                                                <div className="col-xs-12 col-sm-4">
                                                    <input
                                                        className="form-control"
                                                        id="spLastName"
                                                        placeholder="Last Name"
                                                        name="surname" />
                                                </div>
                                            </div>
                                            <div className="form-group">
                                                <label
                                                    htmlFor="spPassword"
                                                    className="col-xs-12 col-sm-4 control-label">
                                                    Password
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