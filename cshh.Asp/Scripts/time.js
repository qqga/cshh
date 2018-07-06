(function clock(timeId, dateId, interval) {

    var timeEl = document.getElementById(timeId);
    var dateEl = document.getElementById(dateId);
    var timer = setInterval(timerElapsed, interval);

    function timerElapsed() {
        var dt = new Date();
        if (timeEl)
            timeEl.innerHTML = dt.toLocaleTimeString();
        if (dateEl)
            dateEl.innerHTML = dt.toLocaleDateString();
    }
})("timeDisplay", "dateDisplay", 1000);
