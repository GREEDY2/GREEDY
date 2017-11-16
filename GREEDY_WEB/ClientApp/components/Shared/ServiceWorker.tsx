import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import Alert from 'react-s-alert';
import 'react-s-alert/dist/s-alert-default.css';
import 'react-s-alert/dist/s-alert-css-effects/genie.css';
import { Button } from 'reactstrap';
import idb from 'idb';
import Constants from './Constants';

export class ServiceWorker extends React.Component<RouteComponentProps<{}>> {
    serviceWorker: any;
    componentDidMount() {
        this.registerServiceWorker();
        var dbPromise = '';
        //this.getFromDb(Constants.dbPromise);
        //this.putInDb(Constants.dbPromise);
    }

    getFromDb(dbPromise) {
        dbPromise.then(db => {
            var tx = db.transaction('keyval');
            var keyValStore = tx.objectStore('keyval');
            return keyValStore.get('hello');
        }).then(val => {
            console.log(val);
        })
    }

    putInDb(dbPromise) {
        dbPromise.then(db => {
            var tx = db.transaction('keyval', 'readwrite');
            var keyValStore = tx.objectStore('keyval');
            keyValStore.put('bar', 'foo');
            return tx.complete;
        }).then(() => {
            console.log('Added foo:bat to keyval');
        })
    }

    registerServiceWorker = () => {
        if ('serviceWorker' in navigator) {
            window.addEventListener('load', () => {
                navigator.serviceWorker.register('/ServiceWorker.js').then(reg => {
                    console.log('SW registered: ', reg);
                    if (!navigator.serviceWorker.controller) {
                        return;
                    }
                    if (reg.waiting) {
                        this.showUpdateAlert();
                        this.serviceWorker = reg.waiting;
                        return;
                    }

                    if (reg.installing) {
                        reg.installing.addEventListener('statechange', () => {
                            if (reg.installing.state == 'installed') {
                                this.showUpdateAlert();
                                this.serviceWorker = reg.installing;
                            }
                        });
                        return;
                    }

                    reg.addEventListener('updatefound', () => {
                        reg.installing.addEventListener('statechange', () => {
                            if (reg.installing) {
                                if (reg.installing.state == 'installed') {
                                    this.showUpdateAlert();
                                    this.serviceWorker = reg.installing;
                                }
                            }
                            if (reg.waiting) {
                                if (reg.waiting.state == 'installed') {
                                    this.showUpdateAlert();
                                    this.serviceWorker = reg.waiting;
                                }
                            }
                        });
                    })
                }).catch(registrationError => {
                    console.log('SW registration failed: ', registrationError);
                    });
                navigator.serviceWorker.addEventListener('controllerchange', () => {
                    window.location.reload();
                });
            });
        }
    }

    showUpdateAlert() {
        Alert.info(<div className="text-center">
            <h3>New version available</h3>
            <span className="page-update pull-left">
                <Button className="page-update" color="success" onClick={this.updateServiceWorker}>REFRESH</Button>
            </span>
            <span className="page-update pull-right">
                <Button className="page-update" color="success" onClick={this.handleOnClose}>DISMISS</Button>
            </span>
        </div>,
            {
                position: 'bottom',
                effect: 'genie',
                beep: false,
                timeout: 10000,
                offset: 0
            });
    }

    updateServiceWorker = () => {
        this.serviceWorker.postMessage({action: 'skipWaiting'});
        Alert.closeAll();
        window.location.reload(true);
    }

    handleOnClose = () => {
        Alert.closeAll();
    }

    public render() {
        return (
            <Alert stack={{ limit: 3 }} onClose={this.handleOnClose} />
        );
    }
}