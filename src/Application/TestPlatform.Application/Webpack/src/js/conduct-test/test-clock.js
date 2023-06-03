$(() => {
    const clock = $(".js-test-clock");
    const endTime = clock.attr("data-end-date");
    const dateRegexPattern = /[0-9]+/g;
    const fiveMinutes = 5 * 60 * 1000;

    const [day2, month2, year2, hour2, minute2] = endTime.matchAll(dateRegexPattern);
    const parsedEndTime = new Date(year2, month2 - 1, day2, hour2, minute2);

    const tick = setInterval(tickClock, 1000);

    function tickClock() {
        const startTime = Date.now();

        const timeDiff = Math.abs(parsedEndTime.getTime() - startTime);

        const time = "Remaining: " + formatTimeSpan(timeDiff);

        if (timeDiff === fiveMinutes) {
            setRedFrame();
        }

        clock.html(time);
    }

    function formatTimeSpan(timespan) {
        const totalSeconds = Math.floor(timespan / 1000);
        const hours = Math.floor(totalSeconds / 3600);
        const minutes = Math.floor((totalSeconds % 3600) / 60);
        const seconds = totalSeconds % 60;
      
        const formattedHours = String(hours).padStart(2, '0');
        const formattedMinutes = String(minutes).padStart(2, '0');
        const formattedSeconds = String(seconds).padStart(2, '0');
      
        return `${formattedHours}:${formattedMinutes}:${formattedSeconds}`;
    }

    function setRedFrame() {
        clock.parent().addClass("red-frame");
    }
});
