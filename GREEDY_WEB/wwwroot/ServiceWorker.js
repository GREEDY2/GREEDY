var staticCacheName = 'greedy-static-v2';

self.addEventListener('install', event => {
    var urlsToCache = [
        'dist/vendor.js',
        'dist/vendor.css',
        'dist/main.js',
        'Rolling.gif',
        'Logo.png'
    ];

    event.waitUntil(
        caches.open(staticCacheName).then(cache => {
            return cache.addAll(urlsToCache);
        })
    );
});

self.addEventListener('activate', event => {
    event.waitUntil(
        caches.keys().then(cacheNames => {
            return Promise.all(
                cacheNames.filter(cacheName => {
                    return cacheName.startsWith('greedy-') &&
                        cacheName !== staticCacheName;
                }).map(cacheName => {
                    return cache.delete(cacheName);
                })
            );
        })
    );
});

self.addEventListener('fetch', event => {
    if (event.request.url.includes('main.js')) event.request.url = 'dist/main.js';
    event.respondWith(
        caches.match(event.request).then(response => {
            if (response) {
                return response;
            }
            return fetch(event.request);
        })
    );
});