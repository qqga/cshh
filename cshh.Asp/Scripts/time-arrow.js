function Arrow(name, len, width, color, center, ctx) {
    this.name = name;
    this.len = len;
    this.width = width;
    this.color = color;
    this.center = center;
    this.ctx = ctx
}

Arrow.prototype = {
    draw: function () {
        this.ctx.beginPath();
        this.ctx.strokeStyle = this.color;
        this.ctx.lineWidth = this.width;
        this.ctx.moveTo(this.center.x, this.center.y);
        let point = timeToPoint(this.len, this.name);
        this.ctx.lineTo(point.x + this.center.x, point.y + this.center.y);
        this.ctx.stroke();
        this.ctx.closePath();
    }
}

function ArrowClock(containerId, r, padding, imgUrl, arrows) {

    let self = this;
    this.r = r || 180;
    this.padding = padding || 20;

    this.container = document.getElementById(containerId);
    this.canvas = document.createElement('canvas');
    this.container.appendChild(this.canvas);
    this.ctx = this.canvas.getContext('2d');
    this.canvas.width = this.r + this.padding;
    this.canvas.height = this.r + this.padding;

    this.center = { x: (this.r + this.padding) / 2 };
    this.center.y = this.center.x;

    this.img = document.createElement('img');
    this.img.addEventListener('load', function () { self.allowDrowImage = true; })
    this.img.src = imgUrl || 'clock.png';

    this.arrows = arrows || [
        new Arrow('h', r * 0.22, 3, 'black', null, null),
        new Arrow('m', r * 0.27, 2, 'green', null, null),
        new Arrow('s', r * 0.4, 1, 'red', null, null),
    ];

    for (var i = 0; i < this.arrows.length; i++) {
        let arrow = this.arrows[i];
        arrow.center = this.center;
        arrow.ctx = this.ctx;
    }
    //for (let arrow of this.arrows) {
    //    arrow.center = this.center;
    //    arrow.ctx = this.ctx;
    //}
}

ArrowClock.prototype = {

    draw: function () {

        this.ctx.clearRect(0, 0, this.canvas.width, this.canvas.height);

        this.drawImage();

        for (var i = 0; i < this.arrows.length; i++) {
            this.arrows[i].draw();
        }
        //for (let arrow of this.arrows) {
        //    arrow.draw();
        //}
        this.arrows[0].draw();
        requestAnimationFrame(this.draw.bind(this));
    },

    drawImage: function () {
        if (this.allowDrowImage)
            this.ctx.drawImage(this.img, 0, 0, this.canvas.height, this.canvas.width);
    }
}


function timeToPoint(r, part, dateTime) {
    const minuteRad = 2 * Math.PI / 60;
    const hourRad = 2 * Math.PI / 12;
    const date = dateTime || new Date();
    const hour = ((date.getHours() + 11) % 12 + 1);
    const min = date.getMinutes();
    const sec = date.getSeconds();

    let res;
    switch (part) {
        case 'h': res = hour * hourRad - hourRad * 3; res += min / 60 * hourRad; break;
        case 'm': res = min * minuteRad - minuteRad * 15; break;
        case 's': res = sec * minuteRad - minuteRad * 15; break;
    }

    let y = Math.sin(res) * r;
    let x = Math.cos(res) * r;

    return { x: x, y: y };
}
