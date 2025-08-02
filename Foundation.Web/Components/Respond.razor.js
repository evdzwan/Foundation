import { Foundation } from "../foundation.js"

export function attach(component, element) {
    const state = Foundation.setState(component._id, {
        resize: () => resize(component),
        element: element
    });

    window.addEventListener("resize", state.resize);
    resize(component);
}

export function detach(component) {
    const state = Foundation.deleteState(component._id);
    if (state !== undefined) {
        window.removeEventListener("resize", state.resize);
        state.resize = null;
    }
}

async function resize(component) {
    const state = Foundation.getState(component._id);
    if (state !== undefined) {
        await component.invokeMethodAsync("UpdateBounds", state.element.parentElement.clientWidth, state.element.parentElement.clientHeight);
    }
}
