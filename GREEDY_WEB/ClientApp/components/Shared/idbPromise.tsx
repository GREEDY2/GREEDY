import * as React from 'react';
import idb from 'idb';

export const dbPromise = idbOpen();

function idbOpen() {
    if (!navigator.serviceWorker) {
        return undefined;
    }
    return idb.open('greedy', 1, upgradeDb => {
        upgradeDb.createObjectStore('categories', { autoIncrement: true });
        upgradeDb.createObjectStore('myItems', { keyPath: 'ItemId' });
    });
}

export default dbPromise;