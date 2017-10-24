import * as React from 'react';
import { Route } from 'react-router-dom';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { Counter } from './components/Counter';
import { Authorization } from './components/Authorization';
import { UserLogin } from './components/UserLogin';

export const routes =
    (<Layout>
        <Authorization>
            <Route exact path='/' component={Home} />
            <Route path='/counter' component={Counter} />
            <Route path='/fetchdata' component={Home} />
        </Authorization>
    </Layout>);