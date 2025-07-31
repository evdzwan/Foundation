export function attach(elem, script, invoker) {
    if (elem !== null) {
        elem.resize = () => resize(elem);
        elem.script = script;

        window.addEventListener("resize", elem.resize);
        if (script !== null) {
            script.initialize(elem, invoker);
        }

        resize(elem);
    }
}

export function detach(elem) {
    if (elem !== null) {
        window.removeEventListener("resize", elem.resize);

        elem.resize = undefined;
        if (elem.script !== null) {
            elem.script.cleanup(elem);
            elem.script = null;
        }
    }
}

async function resize(elem) {
    const rect = elem.getBoundingClientRect();
    [elem.width, elem.height] = [rect.width, rect.height];
    if (elem.script !== null) {
        elem.script.resize(elem);
    }
}
