export function attach(elem, invoker) {
    if (elem !== null) {
        elem.resize = () => resize(elem, invoker);
        window.addEventListener("resize", elem.resize);
        resize(elem, invoker);
    }
}

export function detach(elem) {
    if (elem !== null) {
        window.removeEventListener("resize", elem.resize);
        elem.resize = undefined;
    }
}

async function resize(elem, invoker) {
    await invoker.invokeMethodAsync("UpdateBounds", elem.parentElement.clientWidth, elem.parentElement.clientHeight);
}
