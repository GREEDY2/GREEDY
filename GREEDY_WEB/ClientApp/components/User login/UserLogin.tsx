import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { Button } from 'reactstrap';
import DocumentTitle from 'react-document-title';
import { LoginForm } from 'react-stormpath';
import axios from 'axios';
import { Link, NavLink } from 'react-router-dom';
import { Logo } from '../Shared/Logo';
import Constants from '../Shared/Constants';
import { FacebookLogin } from 'react-facebook-login-component';
import { Alert } from '../Shared/Alert';

export class UserLogin extends React.Component<RouteComponentProps<{}>> {
    child: any;
    state = {
        isLoggingIn: false
    }

    onFormSubmit = (e, next) => {
        e.preventDefault();
        var data = e.data;

        if (data.username.length < 1) {
            return next(new Error('Username/Email must not be empty.'));
        }

        if (data.username.length > 256) {
            return next(new Error('Username/Email is too long.'));
        }

        if (data.password.length < 1) {
            return next(new Error('Password must not be empty.'));
        }
        if (data.username.length > 256) {
            return next(new Error('Password is too long.'));
        }

        this.setState({ isLoggingIn: true });

        let credentials = {}
        credentials["username"] = data.username;
        credentials["password"] = data.password;
        axios.put(Constants.httpRequestBasePath + 'api/Login', credentials)
            .then(response => {
                let res = response.data;
                if (res) {
                    localStorage.setItem("auth", res);
                    this.props.history.push("/");
                }
                else {
                    this.setState({ isLoggingIn: false });
                    return next(new Error('Failed to login. Make sure your username and/or password are correct'));
                }
            }).catch(error => {
                this.setState({ isLoggingIn: false });
                return next(new Error('Failed to login. Please try again later'));
            });
    }

    responseFacebook = (response) => {
        if (!response || !response.email || !response.accessToken) return;
        this.setState({ isLoggingIn: true });
        let credentials = {}
        credentials["accessToken"] = response.accessToken;
        credentials["email"] = response.email;
        credentials["name"] = response.name;
        axios.put(Constants.httpRequestBasePath + 'api/LoginFB', credentials)
            .then(response => {
                
                let res = response.data;
                if (res) {
                    if (res)
                    {
                        localStorage.setItem("auth", res);
                        this.props.history.push("/");
                    }
                    else {
                        this.child.showAlert("Failed to login. Please try again later", "error");
                    }
                this.setState({ isLoggingIn: false });
                }
            }).catch(error => {
                this.setState({ isLoggingIn: false });
                this.child.showAlert("Failed to login. Please try again later", "error");
            });
    }

    public render() {
        return (
            <div>
                <Logo />
                <DocumentTitle title={`Login`}>
                    <LoginForm onSubmit={this.onFormSubmit.bind(this)}>
                        <div className='sp-login-form'>
                            <div className="row">
                                <div className="col-xs-12">
                                    <div className="form-horizontal">
                                        <div className="form-group">
                                            <label
                                                htmlFor="spEmail"
                                                className="col-xs-12 col-sm-4 control-label">
                                            </label>
                                            <div className="col-xs-12 col-sm-4">
                                                <input
                                                    className="form-control"
                                                    id="spUsername"
                                                    name="username"
                                                    placeholder="Username or Email" />
                                            </div>
                                        </div>
                                        <div className="form-group">
                                            <label
                                                htmlFor="spPassword"
                                                className="col-xs-12 col-sm-4 control-label">
                                            </label>
                                            <div className="col-xs-12 col-sm-4">
                                                <input
                                                    type="password"
                                                    className="form-control"
                                                    id="spPassword"
                                                    name="password"
                                                    placeholder="Password" />
                                            </div>
                                            <Link
                                                to="/forgot"
                                                className=" col-sm-offset-4 col-sm-4 pull-left loginLinkButton">
                                                Forgot password
                                                 </Link>
                                        </div>
                                        <div className="form-group">
                                            <div className="col-sm-offset-4 col-sm-4 text-center">
                                                <p className="alert alert-danger" data-spIf="form.error">
                                                    <span data-spBind="form.errorMessage" />
                                                </p>
                                                {this.state.isLoggingIn ?
                                                    <img className="img-responsive loading" src={"Rolling.gif"} /> :
                                                    <Button
                                                        className="col-xs-12 col-sm-12"
                                                        type="submit"
                                                        color="btn btn-primary buttonText">
                                                        Login
                                                    </Button>
                                                }
                                                <Link to="/registration"
                                                    className="pull-right marginTop loginLinkButton">
                                                    Don't have an account? Register
                                                    </Link>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </LoginForm>
                </DocumentTitle>
                <div className="row">
                    <div className="col-sm-offset-4 col-sm-4 text-center">
                        {this.state.isLoggingIn ?
                            null :
                            <FacebookLogin
                                socialId="132824064066510"
                                language="en_US"
                                scope="public_profile,email"
                                responseHandler={this.responseFacebook}
                                xfbml={true}
                                fields="id,email,name"
                                version="v2.11"
                                className="facebook-login btn btn-btn btn-primary"
                                buttonText="Login With Facebook"
                            />}
                    </div>
                </div>
                <Alert onRef={ref => (this.child = ref)} />
            </div>
        );
    }
}
