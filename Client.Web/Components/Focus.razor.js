export function focus(elem, selector) {
    if (elem !== null && selector !== null) {
        const child = elem.querySelector(selector);
        if (child !== null) {
            child.focus();
        }
    }
}
