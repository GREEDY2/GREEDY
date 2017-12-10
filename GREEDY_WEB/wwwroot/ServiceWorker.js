var staticCacheName = 'greedy-static-v1';

self.addEventListener('install', event => {
    var urlsToCache = [
        '/',
        'dist/vendor.js?v=OjVxDpV6p_Jfz2P38F_R2lc3pjVsUisUejeIABZq7AE',
        'dist/vendor.css?v=b_M7vdGvPSJOo55_XCEeI_fYCVztjxk08tEeZj5UyoU',
        'dist/main.js?v=QL_VUut2SibsVZZVnhk7js8hhs0vNluBuA63Vs_97Pc',
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