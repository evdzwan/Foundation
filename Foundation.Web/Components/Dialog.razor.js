import { Foundation } from "../foundation.js"

export function attach(component, dialog) {
    const state = Foundation.setState(component._id, {
        toggle: evt => onToggle(component, evt),
        dialog: dialog
    });

    if (dialog !== null) {
        dialog.addEventListener("toggle", state.toggle);
    }
}

export function detach(component) {
    const state = Foundation.deleteState(component._id);
    if (state !== undefined) {
        const dialog = state.dialog;
        if (dialog !== null) {
            dialog.removeEventListener("toggle", state.toggle);
        }

        state.toggle = null;
    }
}

export function hideDialog(dialog) {
    if (dialog !== null) {
        dialog.hidePopover();
    }
}

export function showDialog(dialog, source) {
    if (dialog !== null) {
        if (source !== null) {
            dialog.showPopover({ source: source });
        } else {
            dialog.showPopover();
        }
    }
}

async function onToggle(component, args) {
    if (component !== null) {
        await component.invokeMethodAsync("OnDialogToggled", args.newState == "open");
    }
}
