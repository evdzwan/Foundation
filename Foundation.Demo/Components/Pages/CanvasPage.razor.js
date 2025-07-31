export function initialize(canvas, invoker) {
    console.log("initialize");
    console.log(canvas);
    console.log(invoker);
}

export function resize(canvas) {
    console.log("resize");
    console.log(canvas);
}

export function cleanup(canvas) {
    console.log("cleanup");
    console.log(canvas);
}
