import * as React from 'react';
import { Route } from 'react-router-dom';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { AllUserItems } from './components/AllUserItems';
import { Authorization } from './components/Authorization';
import { UserSettings } from './components/UserSettings';
import { StatisticsPage } from './components/StatisticsPage';

export const routes =
    (<Layout>
        <Authorization>
            <Route exact path='/' component={Home} />
            <Route path='/fetchdata' component={AllUserItems} />
            <Route path='/user' component={UserSettings} />
            <Route path='/statistics' component={StatisticsPage} />
        </Authorization>
    </Layout>);