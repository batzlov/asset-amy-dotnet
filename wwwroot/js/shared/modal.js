export class Modal {
    constructor(template, options = {}) {
        this.template = template;
        this.options = options;

        this.modalSelector = options.modalSelector || "#modal";
        this.modalToggleSelector =
            options.modalToggleSelector || "#modal-toggle";

        this.init();
    }

    init() {
        this.modal = document.querySelector(this.modalSelector);
        this.modalToggle = document.querySelector(this.modalToggleSelector);
        this.modal
            .querySelector(".modal-content")
            .appendChild(this.template.content.cloneNode(true));

        const circleCloseBtn = this.modal.querySelector("#circle-close-btn");
        circleCloseBtn.addEventListener("click", () => {
            this.close();

            if (this.options.onClose) {
                this.options.onClose();
            }
        });

        const closeBtn = this.modal.querySelector("#close-btn");
        closeBtn.addEventListener("click", () => {
            this.close();

            if (this.options.onClose) {
                this.options.onClose();
            }
        });

        const saveBtn = this.modal.querySelector("#save-btn");
        saveBtn.addEventListener("click", () => {
            if (this.options.onSave) {
                this.options.onSave();
            }
        });
    }

    open() {
        this.modalToggle.checked = true;
    }

    close() {
        this.modalToggle.checked = false;
    }
}
