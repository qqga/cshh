(function () {
    var canvasEl = document.createElement('canvas');
    var ctx = canvasEl.getContext("2d");
    var container = document.getElementById('paint-sky-container');
    container.appendChild(canvasEl);

    function fitCanvasToContainer() {

        canvasEl.width = window.innerWidth;
        canvasEl.height = window.innerHeight;
    }

    window.onresize = function () { fitCanvasToContainer(); };
    canvasEl.addEventListener('mousemove', function (e) {
        raf = e;
        ball.draw();
    });

    var clickCount = 0;
    canvasEl.addEventListener('click', function (e) {
        if (!clickCount++)
            ball.color = 'rgba(210,220,100, 0.1)';
        else
            ball.color = 'rgba(' + rndClr() + ', ' + rndClr() + ', ' + rndClr() + ', 0.1)';
    });
    function rndClr() { return parseInt(Math.random() * 255); }
    function onDraw(e) {

        requestAnimationFrame(onDraw);
    }
    function ClearCanvas() {
        ctx.fillStyle = 'rgba(255, 255, 255, 0.3)';
        ctx.fillRect(0, 0, canvasEl.width, canvasEl.height);
    }
    var raf = { x: 0, y: 0 };
    var ball = {
        x: 0,
        y: 0,
        vx: 5,
        vy: 2,
        radius: 25,
        color: 'rgba(0, 128, 255, 0.1)',
        draw: function () {
            this.x = raf.x || this.x;
            this.y = raf.y || this.y;

            ctx.moveTo(this.x, this.y);

            ctx.beginPath();
            //ctx.arc(this.x, this.y, this.radius, 0, Math.PI * 2, true);
            for (var i = 0; i < 5; i++) {
                //ctx.lineTo(getRnd(this.x), getRnd(this.y))
                ctx.arcTo(getRnd(this.x), getRnd(this.y), getRnd(this.x), getRnd(this.y), 25);
            }
            ctx.closePath();
            ctx.fillStyle = this.color;
            ctx.fill();
        }
    };

    function getRnd(x) {
        var size = 20;
        var rand = Math.random() * size;
        var res = Math.random() > 0.5 ? x + rand : x - rand;
        return res;
    }

    fitCanvasToContainer();
    requestAnimationFrame(onDraw);

})();