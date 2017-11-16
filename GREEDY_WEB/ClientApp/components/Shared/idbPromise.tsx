import * as React from 'react';
import idb from 'idb';

export const dbPromise = idbOpen();

function idbOpen() {
    var dbPromise = idb.open('greedy-db', 1, upgradeDb => {
        var keyValStore = upgradeDb.createObjectStore('keyval');     
    });
    return dbPromise;
}

export default dbPromise;