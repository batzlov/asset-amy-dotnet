export class Toast {
    static containerSelector = ".toast-container";

    static show(type, message, duration) {
        const alert = document.createElement("div");
        let alertType;

        switch (type) {
            case "success":
                alertType = "alert-success";
                break;
            case "error":
                alertType = "alert-error";
                break;
            default:
                alertType = "alert-info";
                break;
        }

        alert.classList.add("alert", alertType);
        alert.innerText = message;

        document.querySelector(this.containerSelector).appendChild(alert);

        setTimeout(() => {
            alert.remove();
        }, duration);
    }
}
