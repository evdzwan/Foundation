export function focus(elem, selector) {
    const child = elem.querySelector(selector);
    if (child !== null) {
        child.focus();
    }
}
