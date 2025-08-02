import { Foundation } from "../foundation.js"

export function attach(component, element) {
    const state = Foundation.setState(component._id, {
        toggle: evt => toggle(component, evt),
        element: element
    });

    if (element !== null) {
        element.addEventListener("toggle", state.toggle);
    }
}

export function detach(component) {
    const state = Foundation.deleteState(component._id);
    if (state !== undefined) {
        const element = state.element;
        if (element !== null) {
            element.removeEventListener("toggle", state.toggle);
        }

        state.toggle = null;
    }
}

export function showMenu(element) {
    if (element !== null) {
        element.showPopover();
    }
}

export function hideMenu(element) {
    if (element !== null) {
        element.hidePopover();
    }
}

async function toggle(component, args) {
    if (component !== null) {
        await component.invokeMethodAsync("OnPopoverToggled", args.newState == "open");
    }
}
