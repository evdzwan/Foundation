export function attach(elem, invoker) {
    if (elem !== null) {
        elem.toggle = evt => toggle(invoker, evt);
        elem.addEventListener("toggle", elem.toggle);
        toggle(elem, invoker);
    }
}

export function detach(elem) {
    if (elem !== null) {
        elem.removeEventListener("toggle", elem.toggle);
        elem.toggle = undefined;
    }
}

async function toggle(invoker, args) {
    if (invoker !== null) {
        await invoker.invokeMethodAsync("OnPopoverToggled", args.newState == "open");
    }
}

export function showMenu(elem) {
    if (elem !== null) {
        elem.showPopover();
    }
}

export function hideMenu(elem) {
    if (elem !== null) {
        elem.hidePopover();
    }
}
