import * as React from 'react';
import { Route } from 'react-router-dom';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { UserLogin } from './components/UserLogin';
import { Counter } from './components/Counter';

export const routes =
    <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/counter' component={Counter} />
        <Route path='/fetchdata' component={Home} />
        <Route path='/login' component={UserLogin} />
    </Layout>;
