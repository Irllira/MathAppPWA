const cachesName = 'math-v1';

const staticAssets = [
    '/',
    '/app.css',
    '/manifest.json',
    '/_framework/blazor.web.js'
];


self.addEventListener('install', event => {
    event.waitUntil(
        caches.open(cachesName).then(async (cache) => {
            for (const asset of staticAssets) {
                try {
                    await cache.add(asset);
                    console.log(`Successfully cached: ${asset}`);
                } catch (error) {
                    console.error(`FAILED to cache: ${asset}`, error);
                }
            }
            return self.skipWaiting();
        })
    );
});


self.addEventListener('activate', event => {
    self.clients.claim();
});


self.addEventListener('fetch', event => {
    const req = event.request;
    const url = new URL(req.url);

    if (url.origin===location.origin) {
        event.respondWith(cacheFirst(req));
    } else {
        event.respondWith(networkAndCache(req));
    }
});

async function cacheFirst(req) {
    const cache = await caches.open(cachesName);
    const cached = await cache.match(req);
    return cached || fetch(req);
}

async function networkAndCache(req) {
    const cache = await caches.open(cachesName);
    try {
        const fresh = await fetch(req);
        if (req.method === 'GET' && fresh.status === 200) {
            await cache.put(req, fresh.clone());
        }
        return fresh;
    } catch (e) {
        const cached = await cache.match(req);
        if (cached) {
            return cached;
        }
        return new Response("Aplikacja jest offline i nie posiada zapisanych danych.", {
            status: 503,
            statusText: "Service Unavailable",
            headers: new Headers({ "Content-Type": "text/plain; charset=utf-8" })
        });
    }
}