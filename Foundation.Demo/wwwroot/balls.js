export function initialize(canvas, invoker) {
    render(canvas);
}

export function resize(canvas) {
    render(canvas);
}

export function cleanup(canvas) {
}

function render(canvas) {
    const ctx = canvas.getContext("2d");

    ctx.fillStyle = "green";
    ctx.fillRect(10, 10, 150, 100);
}
