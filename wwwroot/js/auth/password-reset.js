import { Form } from "../shared/form.js";
import { HttpRequest } from "../shared/request.js";
import { Toast } from "../shared/toast.js";
import { passwordResetSchema } from "../shared/schema.js";

document.addEventListener("DOMContentLoaded", () => {
    let currentlyLoading = false;
    let passwordResetHash;

    const init = () => {
        const form = new Form(
            document.querySelector("form"),
            passwordResetSchema
        );

        passwordResetHash = document.querySelector("#passwordResetHash").value;

        const submit = document.querySelector("button");
        submit.addEventListener("click", (event) => {
            event.preventDefault();
            event.stopPropagation();

            if (form.isValid() && !currentlyLoading) {
                currentlyLoading = true;
                submit.classList.add("loading");

                HttpRequest.put(
                    `/api/password-reset/${passwordResetHash}`,
                    form.toObj(),
                    {
                        onSuccess: (response) => {
                            currentlyLoading = false;
                            submit.classList.remove("loading");
                            form.reset();

                            Toast.show(
                                "success",
                                "Dein Passwort wurde erfolgreich zurückgesetzt."
                            );

                            setTimeout(() => {
                                window.location.href = "/sign-in";
                            }, 3000);
                        },
                        onError: (error) => {
                            form.markAsInvalid(["password", "passwordRepeat"]);

                            currentlyLoading = false;
                            submit.classList.remove("loading");

                            Toast.show("error", error.message);
                        },
                    }
                );
            }
        });
    };

    init();
});
