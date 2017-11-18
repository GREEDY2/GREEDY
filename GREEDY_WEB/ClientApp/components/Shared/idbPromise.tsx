import * as React from 'react';
import idb from 'idb';

export const dbPromise = idbOpen();

function idbOpen() {
    if (!navigator.serviceWorker) {
        return undefined;
    }
    return idb.open('greedy', 1, upgradeDb => {
        //On deployment every db change need to add a case with new commands (no breaks)
        switch (upgradeDb.oldVersion) {
            case 0:
                upgradeDb.createObjectStore('categories');
                upgradeDb.createObjectStore('myItems', { keyPath: 'ItemId' });
        }
    });
}

export default dbPromise;