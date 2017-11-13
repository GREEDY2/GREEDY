import * as React from 'react';
import { Route } from 'react-router-dom';
import { Layout } from './components/Shared/Layout';
import { PhotographPage } from './components/Photograph Page/PhotographPage';
import { AllUserItems } from './components/All items page/AllUserItems';
import { Authorization } from './components/Shared/Authorization';
import { UserSettings } from './components/User settings page/UserSettings';
import { StatisticsPage } from './components/Statistics page/StatisticsPage';

export const routes =
    (<Layout>
        <Authorization>
            <Route exact path='/' component={PhotographPage} />
            <Route path='/fetchdata' component={AllUserItems} />
            <Route path='/user' component={UserSettings} />
            <Route path='/statistics' component={StatisticsPage} />
        </Authorization>
    </Layout>);