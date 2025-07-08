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

export function getBounds(elem) {
    if (elem !== null) {
        var computedStyle = window.getComputedStyle(elem, null);
        return {
            client: {
                width: window.innerWidth,
                height: window.innerHeight
            },
            padding: {
                left: computedStyle.getPropertyValue("padding-left"),
                top: computedStyle.getPropertyValue("padding-top"),
                right: computedStyle.getPropertyValue("padding-right"),
                bottom: computedStyle.getPropertyValue("padding-bottom")
            }
        };
    }
}

export function getPaddingRight(elem) {
    return window.getComputedStyle(elem, null).getPropertyValue("padding-right");
}

async function toggle(invoker, args) {
    if (invoker !== null) {
        await invoker.invokeMethodAsync("OnPopoverToggled", args.newState == "open");
    }
}

export function showPopover(elem, trigger) {
    if (elem !== null) {
        if (trigger !== null) {
            elem.showPopover({ source: trigger });
        }
        else {
            elem.showPopover();
        }
    }
}

export function hidePopover(elem) {
    if (elem !== null) {
        elem.hidePopover();
    }
}
