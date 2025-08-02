import { Foundation } from "../foundation.js"

export function attach(component, element, script) {
    const state = Foundation.setState(component._id, {
        resize: () => resize(component),
        element: element,
        script: script
    });

    window.addEventListener("resize", state.resize);
    if (script !== null && script.initialize !== undefined) {
        script.initialize(component, element);
    }
}

export function detach(component) {
    const state = Foundation.deleteState(component._id);
    if (state !== undefined) {
        window.removeEventListener("resize", state.resize);
        state.resize = null;

        const script = state.script;
        if (script !== null && script.cleanup !== undefined) {
            script.cleanup(component);
        }

        state.script = null;
    }
}

function resize(component) {
    const state = Foundation.getState(component._id);
    if (state !== undefined) {
        const element = state.element;

        const bounds = element.getBoundingClientRect();
        [element.width, element.height] = [bounds.width, bounds.height];

        const script = state.script;
        if (script !== null && script.resize !== undefined) {
            script.resize(component, element);
        }
    }
}
