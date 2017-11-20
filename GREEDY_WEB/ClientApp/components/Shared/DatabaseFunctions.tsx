import * as React from 'react';
import Constants from './Constants';
import idbPromise from './idbPromise';
import idb from 'idb';

export function putArrayToDb(tableName, arrayToPut) {
    if (idbPromise) {
        idbPromise.then(db => {
            if (!db) return;

            var tx = db.transaction(tableName, 'readwrite');
            var store = tx.objectStore(tableName);
            arrayToPut.map(arrayItem => {
                store.put(arrayItem);
            });
        })
    }
}

export function putDistinctValuesArrayToDb(tableName, ditinctValueArrayToPut) {
    if (idbPromise) {
        idbPromise.then(db => {
            if (!db) return;

            var tx = db.transaction(tableName, 'readwrite');
            var store = tx.objectStore(tableName);
            ditinctValueArrayToPut.map(arrayItem => {
                store.put(Constants.dummyValue, arrayItem);
            });
        })
    }
}

export function clearDb(dbName, tableNamesArray) {
    if (idbPromise) {
        idbPromise.then(db => {
            if (!db) return;
            tableNamesArray.map(tableName => {
                db.transaction(tableName, 'readwrite').objectStore(tableName).clear();
            })
        })
    }
}