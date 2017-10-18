import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { Button } from 'reactstrap';
import DocumentTitle from 'react-document-title';
import { LoginForm } from 'react-stormpath';
import axios from 'axios';
import { Link, NavLink } from 'react-router-dom';
import Cookies from 'universal-cookie';
import FacebookLogin from 'react-facebook-login';

export class UserLogin extends React.Component<RouteComponentProps<{}>> {
    constructor(props) {
        super(props);
        this.onFormSubmit = this.onFormSubmit.bind(this);
    }

    onFormSubmit(e, next) {
        e.preventDefault();
        var data = e.data;

        if (data.username.length < 1) {
            return next(new Error('Username/Email must not be empty.'));
        }

        if (data.username.length > 256) {
            return next(new Error('Username/Email is too long.'));
        }

        // Require passwords to be at least 10 characters.
        if (data.password.length < 1) {
            return next(new Error('Password must not be empty.'));
        }
        if (data.username.length > 256) {
            return next(new Error('Password is too long.'));
        }
        let credentials = {}
        // Force usernames to be in lowercase.
        credentials["username"] = data.username;
        credentials["password"] = data.password;
        axios.put("http://localhost:6967/api/Authentication", credentials)
            .then(response => {
                let res = response.data;
                if (res) {
                    const cookies = new Cookies();
                    cookies.set('username', res.username, { path: '/' });
                    /* set sessionId for user
                    cookies.set('sessionId', res.SessionId, { path: '/' });*/
                    (this.props as any).history.push("/");
                }
                else
                    return next(new Error('Failed to login. Make sure your username and/or password are correct'));
            }).catch(error => {
                console.log(error);
            });
    }

    responseFacebook = (response) => {
        console.log(response);
    }

    facebookButtonClick = () => {

    }

    public render() {
        return <div>
            <DocumentTitle title={`Login`}>
                <div className="container">
                    <div className="row">
                        <div className="col-xs-12 text-center loginLogo">
                            <img className="img-responsive logo" src={"Logo.png"} height="100%" />
                        </div>
                    </div>
                    <LoginForm onSubmit={this.onFormSubmit.bind(this)}>
                        <div className='sp-login-form'>
                            <div className="row">
                                <div className="col-xs-12">
                                    <div className="form-horizontal">
                                        <div className="form-group">
                                            <label htmlFor="spEmail" className="col-xs-12 col-sm-4 control-label"></label>
                                            <div className="col-xs-12 col-sm-4">
                                                <input className="form-control" id="spUsername" name="username" placeholder="Username or Email" />
                                            </div>
                                        </div>
                                        <div className="form-group">
                                            <label htmlFor="spPassword" className="col-xs-12 col-sm-4 control-label"></label>
                                            <div className="col-xs-12 col-sm-4">
                                                <input type="password" className="form-control" id="spPassword" name="password" placeholder="Password" />
                                            </div>
                                            <Link to="/forgot" className=" col-sm-offset-4 col-sm-4 pull-left loginLinkButton">Forgot password</Link>
                                        </div>
                                        <div className="form-group">
                                            <div className="col-sm-offset-4 col-sm-4 text-center">
                                                <p className="alert alert-danger" data-spIf="form.error">
                                                    <span data-spBind="form.errorMessage" />
                                                </p>
                                                <Button className="col-xs-12 col-sm-12" type="submit" color="btn btn-primary buttonText">Login</Button>
                                                <Link to="/registration" className="pull-right marginTop loginLinkButton">Don't have an account? Register</Link>
                                                { /* <Link to="/forgot" className="pull-right">Forgot Password</Link> */
                                                }

                                            </div>
                                        </div>
                                        <div className="form-group col-sm-offset-4 col-sm-12 text-center">
                                            <div className="fb-login-button " data-max-rows="1" data-size="medium" data-button-type="continue_with" data-show-faces="false" data-auto-logout-link="false" data-use-continue-as="false"></div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </LoginForm>
                </div>
            </DocumentTitle>
        </div>;
    }
}
