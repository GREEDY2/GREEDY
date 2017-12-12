var staticCacheName = 'greedy-static-v2';

self.addEventListener('install', event => {
    var urlsToCache = [
        '/',
        'dist/vendor.js?v=PmyzAKb7yqQN3nBqgW_ZHUHtyavBfUQ1B_wirXsG8Gg',
        'dist/vendor.css?v=vf4TTnOiCp20iU10Tf4anf3dbOl_Mg07hxsMkGL_Rdw',
        'dist/main.js?v=58dgcPX6AlPzq3cB_RTPAHZINgV2qNs1EXUVs_Lvh4c',
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
                    return caches.delete(cacheName);
                })
            );
        })
    );
});

self.addEventListener('fetch', event => {
    var requestUrl = new URL(event.request.url);
    if (requestUrl.origin === location.origin) {
        if (!(requestUrl.pathname.includes('dist') ||
            requestUrl.pathname.includes('.'))) {
            event.respondWith(caches.match('/'));
            return;
        }
    }

    event.respondWith(
        caches.match(event.request).then(response => {
            if (response) {
                return response;
            }
            return fetch(event.request).catch();
        })
    );
});

self.addEventListener('message', event => {
    if (event.data.action === 'skipWaiting') {
        self.skipWaiting();
    }
});