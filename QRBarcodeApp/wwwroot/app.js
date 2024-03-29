// Adjusts top and bottom content margin according to TabHeader and TabBar
function adjustContentMargin() {
    const tabHeader = document.querySelector(".tab-header"),
        tabBar = document.querySelector(".tab-bar"),
        mainContent = document.querySelector(".main-content");

    if (!tabHeader || !tabBar || !mainContent)
        return;

    mainContent.style.marginTop = `${tabHeader.offsetHeight + 10}px`;
    mainContent.style.marginBottom = `${tabBar.offsetHeight + 10}px`;
}

// Plays button animation
function playButtonAnimation(element) {
    if (!element)
        return;

    element.classList.add("pulse-animation");

    setTimeout(() => {
        element.classList.remove("pulse-animation");
    }, 220);
}
