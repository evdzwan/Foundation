export function attach(elem, invoker) {
    elem.toggle = evt => toggle(invoker, evt);
    elem.addEventListener("toggle", elem.toggle);
    toggle(elem, invoker);
}

export function detach(elem) {
    elem.removeEventListener("toggle", elem.toggle);
    elem.toggle = undefined;
}

async function toggle(invoker, args) {
    await invoker.invokeMethodAsync("OnPopoverToggled", args.newState == "open");
}

export function showPopover(elem, trigger) {
    elem.showPopover({ source: trigger });
}

export function hidePopover(elem) {
    elem.hidePopover();
}
