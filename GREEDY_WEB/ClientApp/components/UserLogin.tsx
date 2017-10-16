import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { Button } from 'reactstrap';
import DocumentTitle from 'react-document-title';
import { LoginForm } from 'react-stormpath';
import axios from 'axios';
import { Link, NavLink } from 'react-router-dom';
import Cookies from 'universal-cookie';

export class UserLogin extends React.Component<RouteComponentProps<{}>, {}> {
    constructor() {
        super();
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
        axios.post(`/api/Authentication/Login/`, credentials)
            .then(response => {
                let res = response.data;
                if (res) {
                    const cookies = new Cookies();
                    cookies.set('role', res.role, { path: '/' });
                    cookies.set('username', res.username, { path: '/' });
                    cookies.set('sessionId', res.SessionId, { path: '/' });
                    this.props.history.push("/");
                }
                else
                    return next(new Error('Failed to login. Make sure your username and/or password are correct'));
            }).catch(error => {
                console.log(error);
            });
    }

    public render() {
        return <div>
                   <DocumentTitle title={`Login`}>
                       <div className="container">
                           <div className="row">
                               <div className="col-xs-12 text-center loginLogo">
                                   <img className="img-responsive logo" src={"Logo.png"} height="100%"/>
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
                                                       <input className="form-control" id="spUsername" name="username" placeholder="Username or Email"/>
                                                   </div>
                                               </div>
                                               <div className="form-group">
                                                   <label htmlFor="spPassword" className="col-xs-12 col-sm-4 control-label"></label>
                                                   <div className="col-xs-12 col-sm-4">
                                                       <input type="password" className="form-control" id="spPassword" name="password" placeholder="Password"/>
                                                   </div>
                                               </div>
                                               <div className="form-group">
                                                   <div className="col-sm-offset-4 col-sm-4 text-center">
                                                <p className="alert alert-danger" data-spIf="form.error">
                                                    <span data-spBind="form.errorMessage"/>
                                                       </p>
                                                       <Button type="submit" color="btn btn-default buttonText">Login</Button>
                                                       { /* <Link to="/forgot" className="pull-right">Forgot Password</Link> */
                                                       }
                                                   </div>
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
