export class Toast {
    static containerSelector = ".toast-container";

    static show(type, message, duration) {
        console.log("alert toast");
        const alert = document.createElement("div");

        alert.classList.add(`alert-${type}`);
        alert.innerText = message;
        document.querySelector(this.containerSelector).appendChild(alert);

        setTimeout(() => {
            alert.remove();
        }, duration);
    }
}
