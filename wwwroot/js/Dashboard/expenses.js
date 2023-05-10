import { Form } from "../shared/form.js";
import { Modal } from "../shared/modal.js";
import { Toast } from "../shared/toast.js";

document.addEventListener("DOMContentLoaded", () => {
    const init = () => {
        const template = document.getElementById("expense-modal");
        const modal = new Modal(template, {});

        const createExpenseBtn = document.getElementById("create-expense-btn");
        createExpenseBtn.addEventListener("click", () => {
            modal.open();
        });
    };

    init();
});
