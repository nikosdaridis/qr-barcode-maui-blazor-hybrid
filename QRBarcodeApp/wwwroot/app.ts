// Adjusts top and bottom content margin according to TabHeader and TabBar
function adjustContentMargin(offset = 10): void {
    const tabHeader = document.querySelector<HTMLElement>(".tab-header"),
        tabBar = document.querySelector<HTMLElement>(".tab-bar"),
        mainContent = document.querySelector<HTMLElement>(".main-content");

    if (!tabHeader || !tabBar || !mainContent)
        return;

    mainContent.style.marginTop = `${tabHeader.offsetHeight + offset}px`;
    mainContent.style.marginBottom = `${tabBar.offsetHeight + offset}px`;
}

// Plays button animation
function playButtonAnimation(arg: HTMLElement | MouseEvent, duration = 250): void {
    let element: HTMLElement;

    if (arg instanceof MouseEvent)
        element = arg.target as HTMLElement;
    else if (arg instanceof HTMLElement)
        element = arg;
    else
        return;

    if (!element)
        return;

    element.classList.add("pulse-animation");
    setTimeout(() => element.classList.remove("pulse-animation"), duration);
}

// Copies text to clipboard
function copyToClipboard(text: string): void {
    navigator.clipboard.writeText(text);
}
