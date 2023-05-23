export class Modal {
    constructor(template, options = {}) {
        this.template = template;
        this.options = options;

        this.onSave = options.onSave || null;
        this.onClose = options.onClose || null;

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

            if (this.onClose) {
                this.onClose();
            }
        });

        const closeBtn = this.modal.querySelector("#close-btn");
        closeBtn.addEventListener("click", () => {
            this.close();

            if (this.onClose) {
                this.onClose();
            }
        });

        const saveBtn = this.modal.querySelector("#save-btn");
        saveBtn.addEventListener("click", () => {
            if (this.onSave) {
                this.onSave();
            }
        });
    }

    open() {
        this.modalToggle.checked = true;
    }

    close() {
        this.modalToggle.checked = false;
    }

    setLoading(isLoading) {
        const saveBtn = this.modal.querySelector("#save-btn");

        if (isLoading) {
            saveBtn.classList.add("loading");
        } else {
            saveBtn.classList.remove("loading");
        }
    }
}
