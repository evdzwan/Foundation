export function initialize(canvas, invoker) {
    if (canvas != null) {
        const context = canvas.getContext("2d");
        canvas.context = context;
        loop(canvas);
    }
}

export function resize(canvas) {
    if (canvas != null) {
        render(canvas);
    }
}

export function cleanup(canvas) {
    if (canvas != null) {
        canvas.context = undefined;
    }
}

function loop(canvas) {
    render(canvas);
    if (canvas.context != undefined) {
        requestAnimationFrame(() => loop(canvas));
    }
}

function render(canvas) {
    const ctx = canvas.context;
    ctx.clearRect(0, 0, ctx.canvas.width, ctx.canvas.height);
}
