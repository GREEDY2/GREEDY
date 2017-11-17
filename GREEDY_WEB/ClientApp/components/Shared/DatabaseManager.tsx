import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import Constants from './Constants';
import idbPromise from './idbPromise';
import axios from 'axios';

export class DatabaseManager extends React.Component<RouteComponentProps<{}>> {
    dbPromise = idbPromise;
    componentDidMount() {
        this.getAllUserItems();
        this.getAllDistinctCategories();
    }

    putArrayToDb = (tableName, arrayToPut) => {
        if (this.dbPromise) {
            this.dbPromise.then(db => {
                if (!db) return;

                var tx = db.transaction(tableName, 'readwrite');
                var store = tx.objectStore(tableName);
                arrayToPut.map(arrayItem => {
                    store.put(arrayItem);
                });
            })
        }
    }

    getAllDistinctCategories = () => {
        axios.get(Constants.httpRequestBasePath + "api/GetDistinctCategories",
            {
                headers: {
                    'Authorization': 'Bearer ' + localStorage.getItem("auth")
                }
            }).then(response => {
                let res = response.data;
                this.putArrayToDb('categories', res);
            }).catch(error => {
                if (error.response)
                if (error.response.status == 401) {
                    localStorage.removeItem('auth');
                    (this.props as any).history.push("/");
                    }
                else {
                    //TODO: no internet
                }
                console.log(error);
            })
    }

    getAllUserItems() {
        axios.get(Constants.httpRequestBasePath + 'api/GetAllUserItems', {
            headers: {
                'Authorization': 'Bearer ' + localStorage.getItem("auth")
            }
        }).then(res => {
            const itemList = res.data;
            this.putArrayToDb('myItems', itemList);
            }).catch(error => {
            if (error.response)
            if (error.response.status == 401) {
                localStorage.removeItem('auth');
                (this.props as any).history.push("/");
            }
            else {
                    //TODO: no internet
                    }
            console.log(error);
        })
    }

    /*getFromDb(dbPromise) {
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
    }*/

    public render() {
        return null;
    }
}