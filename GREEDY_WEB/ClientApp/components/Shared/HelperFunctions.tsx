export const smoothScroll = {
    timer: null,

    stop: function() {
        clearTimeout(this.timer);
    },

    scrollTo: function(id) {
        var settings = {
            duration: 1000,
            easing: {
                outQuint: function(x, t, b, c, d) {
                    return c * ((t = t / d - 1) * t * t * t * t + 1) + b;
                }
            }
        };
        var percentage: number;
        var startTime: number;
        const node = document.getElementById(id);
        const nodeTop = node.offsetTop;
        const nodeHeight = node.offsetHeight;
        const body = document.body;
        const html = document.documentElement;
        const height = Math.max(
            body.scrollHeight,
            body.offsetHeight,
            html.clientHeight,
            html.scrollHeight,
            html.offsetHeight
        );
        const windowHeight = window.innerHeight;
        var offset = window.pageYOffset;
        const delta = nodeTop - offset;
        const bottomScrollableY = height - windowHeight;
        var targetY = (bottomScrollableY < delta)
            ? bottomScrollableY - (height - nodeTop - nodeHeight + offset)
            : delta;

        startTime = Date.now();
        percentage = 0;

        if (this.timer) {
            clearInterval(this.timer);
        }

        function step() {
            const elapsed = Date.now() - startTime;

            if (elapsed > settings.duration) {
                clearTimeout(this.timer);
            }

            percentage = elapsed / settings.duration;

            if (percentage > 1) {
                clearTimeout(this.timer);
            } else {
                let yScroll: number | string | Object;
                yScroll = settings.easing.outQuint(0, elapsed, offset, targetY, settings.duration);
                window.scrollTo(0, yScroll);
                this.timer = setTimeout(step, 10);
            }
        }

        this.timer = setTimeout(step, 10);
    }
};