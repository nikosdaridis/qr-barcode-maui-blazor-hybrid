// Adjusts top and bottom content margin according to TabHeader and TabBar
function adjustContentMargin(): void {
    const tabHeader = document.querySelector<HTMLElement>(".tab-header"),
        tabBar = document.querySelector<HTMLElement>(".tab-bar"),
        mainContent = document.querySelector<HTMLElement>(".main-content");

    if (!tabHeader || !tabBar || !mainContent) return;

    mainContent.style.marginTop = `${tabHeader.offsetHeight + 10}px`;
    mainContent.style.marginBottom = `${tabBar.offsetHeight + 10}px`;
}

// Plays button animation
function playButtonAnimation(element: HTMLElement, duration = 220): void {
    if (!element) return;

    element.classList.add("pulse-animation");
    setTimeout(() => element.classList.remove("pulse-animation"), duration);
}
