import { elementFromString } from "./utils.js";

export class Toast {
    static containerSelector = ".toast-container";

    static show(type, message, duration = 3000) {
        const alert = elementFromString(
            `<div class="alert alert-${type}">
                <div class="text-white font-bold">
                    <p>
                        ${message}
                    </p>
                </div>
            </div>`
        );

        document.querySelector(this.containerSelector).appendChild(alert);

        setTimeout(() => {
            alert.remove();
        }, duration);
    }
}
