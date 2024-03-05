function adjustContentMargin() {
    const tabHeader = document.querySelector('.tab-header');
    const tabBar = document.querySelector('.tab-bar');

    if (tabHeader && tabBar) {
        const mainContent = document.querySelector('.main-content');

        if (mainContent) {
            mainContent.style.marginTop = `${tabHeader.offsetHeight + 10}px`;
            mainContent.style.marginBottom = `${tabBar.offsetHeight + 10}px`;
        }
    }
}
