import { Form } from "../shared/form.js";
import { HttpRequest } from "../shared/request.js";
import { Toast } from "../shared/toast.js";
import { passwordForgottenSchema } from "../shared/schema.js";

document.addEventListener("DOMContentLoaded", () => {
    let currentlyLoading = false;

    const init = () => {
        const form = new Form(
            document.querySelector("form"),
            passwordForgottenSchema
        );

        const submit = document.querySelector("button");
        submit.addEventListener("click", (event) => {
            event.preventDefault();
            event.stopPropagation();

            if (form.isValid() && !currentlyLoading) {
                currentlyLoading = true;
                submit.classList.add("loading");

                HttpRequest.put("/api/password-forgotten", form.toObj(), {
                    onSuccess: (response) => {
                        currentlyLoading = false;
                        submit.classList.remove("loading");
                        form.reset();

                        Toast.show(
                            "success",
                            "Die E-Mail wurde versendet. Bitte überprüfe dein Postfach."
                        );
                    },
                    onError: (error) => {
                        form.markAsInvalid(["email"]);

                        currentlyLoading = false;
                        submit.classList.remove("loading");

                        Toast.show("error", error.message);
                    },
                });
            }
        });
    };

    init();
});
