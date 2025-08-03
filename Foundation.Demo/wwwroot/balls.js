import { Foundation } from "./_content/EvdZwan.Foundation.Web/foundation.js"

export function initialize(canvas, state) {
    if (canvas !== null) {
        const scene = state.scene = {
            balls: [],
            canvas: canvas,
            context: canvas.getContext("2d"),
            gravity: -9.81,
            height: 50,
            lastTime: 0,
            width: 100
        };

        setup(scene);
        requestAnimationFrame(time => loop(state, time));
    }
}

export function cleanup(state) {
    state.scene = null;
}

function setup(scene) {
    for (var i = 0; i < 50; i++) {
        const radius = .5 + Math.random() * 2;

        scene.balls.push({
            colliding: false,
            elasticity: .7 + Math.random() * .2,
            radius: radius,
            x: Math.random() * (scene.width - 2 * radius) + radius,
            y: Math.random() * (scene.height - 2 * radius) + radius,
            vx: (Math.random() - .5) * 100,
            vy: (Math.random() - .5) * 100
        });
    }
}

function loop(state, time) {
    const scene = state.scene;
    if (scene !== null) {
        const dt = (time - scene.lastTime) / 500;
        scene.lastTime = time;

        update(scene, dt);
        render(scene);

        requestAnimationFrame(time => loop(state, time));
    }
}

function render(scene) {
    const ctx = scene.context;
    const canvas = scene.canvas;

    const style = getComputedStyle(canvas);
    const color = style.getPropertyValue("--ball-color");
    const collidingColor = style.getPropertyValue("--ball-color-colliding");
    const scale = Math.min(canvas.width / scene.width, canvas.height / scene.height);

    ctx.clearRect(0, 0, canvas.width, canvas.height);
    for (var i = 0; i < scene.balls.length; i++) {
        const ball = scene.balls[i];
        ctx.fillStyle = ball.colliding ? collidingColor : color;

        ctx.beginPath();
        ctx.arc(ball.x * scale, ball.y * scale, ball.radius * scale, 0, Math.PI * 2);
        ctx.fill();
        ctx.closePath();
    }
}

function update(scene, dt) {
    for (var i = 0; i < scene.balls.length; i++) {
        const ball = scene.balls[i];
        ball.colliding = false;
    }

    for (var i = 0; i < scene.balls.length; i++) {
        const ball = scene.balls[i];

        ball.vy += scene.gravity * dt;
        ball.x += ball.vx * dt;
        ball.y += ball.vy * dt;

        handleBallCollision(ball);
        handleWallCollision(ball);
    }

    function handleBallCollision(first) {
        for (var i = 0; i < scene.balls.length; i++) {
            const second = scene.balls[i];
            if (second !== first &&
                first.x + first.radius + second.radius > second.x &&
                first.x < second.x + first.radius + second.radius &&
                first.y + first.radius + second.radius > second.y &&
                first.y < second.y + first.radius + second.radius) {

                var dx = first.x - second.x;
                var dy = first.y - second.y;
                var d = Math.sqrt(dx * dx + dy * dy);

                if (d < first.radius + second.radius) {
                    first.colliding = second.colliding = true;

                    var nx = (second.x - first.x) / d;
                    var ny = (second.y - first.y) / d;
                    var p = 2 * (first.vx * nx + first.vy * ny - second.vx * nx - second.vy * ny) / (first.radius + second.radius);

                    var cx = ((first.x * second.radius) + (second.x * first.radius)) / (first.radius + second.radius);
                    var cy = ((first.y * second.radius) + (second.y * first.radius)) / (first.radius + second.radius);

                    first.x = cx + first.radius * (first.x - second.x) / d;
                    first.y = cy + first.radius * (first.y - second.y) / d;
                    second.x = cx + second.radius * (second.x - first.x) / d;
                    second.y = cy + second.radius * (second.y - first.y) / d;

                    first.vx -= p * first.radius * nx * first.elasticity;
                    first.vy -= p * first.radius * ny * first.elasticity;
                    second.vx += p * second.radius * nx * second.elasticity;
                    second.vy += p * second.radius * ny * second.elasticity;
                }
            }
        }
    }

    function handleWallCollision(ball) {
        if (ball.x <= ball.radius && ball.vx < 0) {
            ball.vx = Math.abs(ball.vx) * ball.elasticity;
            ball.x = ball.radius;
            ball.colliding = true;
        }
        else if (ball.x >= scene.width - ball.radius) {
            ball.vx = -Math.abs(ball.vx) * ball.elasticity;
            ball.x = scene.width - ball.radius;
            ball.colliding = true;
        }

        if (ball.y <= ball.radius && ball.vy < 0) {
            ball.vy = Math.abs(ball.vy) * ball.elasticity;
            ball.y = ball.radius;
            ball.colliding = true;
        }
        else if (ball.y >= scene.height - ball.radius) {
            ball.vy = -Math.abs(ball.vy) * ball.elasticity;
            ball.y = scene.height - ball.radius;
            ball.colliding = true;
        }
    }
}
