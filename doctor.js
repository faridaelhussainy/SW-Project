
document.addEventListener("DOMContentLoaded", function () {
    const tabs = document.querySelectorAll(".tabs button");
    tabs.forEach((tab) => {
        tab.addEventListener("click", () => {
            document.querySelector(".tabs .active").classList.remove("active");
            tab.classList.add("active");
        });
    });
});
