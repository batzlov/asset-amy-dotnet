document.addEventListener("DOMContentLoaded", () => {
    const init = () => {
        initCarousel();
        initCountdown();
    };

    const initCarousel = () => {
        const changeImageBtns = document.querySelectorAll(
            ".change-preview-image"
        );

        const previewImage = document.querySelector(".preview-image");
        for (const btn of changeImageBtns) {
            btn.addEventListener("click", (e) => {
                document
                    .querySelector(".btn-active")
                    .classList.remove("btn-active");

                const target = e.target;
                const imageId = target.getAttribute("data-switch-to-image");
                target.classList.add("btn-active");

                previewImage.setAttribute(
                    "src",
                    `/assets/screenshots/aa-screenshot-${imageId}.png`
                );
            });
        }
    };

    const initCountdown = () => {
        let now = new Date();
        let release = new Date("2023-07-11");

        const counterDays = document.querySelector(".counter-days");
        const counterHours = document.querySelector(".counter-hours");
        const counterMinutes = document.querySelector(".counter-minutes");
        const counterSeconds = document.querySelector(".counter-seconds");

        let days = 0;
        let hours = 0;
        let minutes = 0;
        let seconds = 0;

        // we dont need to init the countdown if the release date is in the past
        if (now > release) {
            return;
        }

        const updateTimer = () => {
            now = new Date();

            const timeDiff = release - now;
            days = Math.floor(timeDiff / (1000 * 60 * 60 * 24));
            hours = Math.floor(
                (timeDiff % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60)
            );
            minutes = Math.floor((timeDiff % (1000 * 60 * 60)) / (1000 * 60));
            seconds = Math.floor((timeDiff % (1000 * 60)) / 1000);

            counterDays.style.setProperty("--value", days);
            counterHours.style.setProperty("--value", hours);
            counterMinutes.style.setProperty("--value", minutes);
            counterSeconds.style.setProperty("--value", seconds);
        };

        // setup the timer
        updateTimer();

        // set the intervall so the timers updates
        setInterval(() => {
            updateTimer();
        }, 1000);
    };

    init();
});
