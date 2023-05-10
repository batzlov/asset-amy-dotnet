import { Form } from "../shared/form.js";
import { HttpRequest } from "../shared/request.js";
import { Toast } from "../shared/toast.js";

document.addEventListener("DOMContentLoaded", () => {
    let currentlyLoading = false;
    const schema = {
        email: {
            type: "string",
            rules: ["required", "email"],
        },
        password: {
            type: "string",
            rules: ["required"],
        },
    };

    const init = () => {
        const form = new Form(document.querySelector("form"), schema);

        const submit = document.querySelector("button");
        submit.addEventListener("click", (event) => {
            event.preventDefault();
            event.stopPropagation();

            if (form.isValid() && !currentlyLoading) {
                currentlyLoading = true;
                submit.classList.add("loading");

                HttpRequest.post("/api/sign-in", form.toObj(), {
                    onSuccess: (response) => {
                        currentlyLoading = false;
                        submit.classList.remove("loading");

                        localStorage.setItem("token", response.token);
                        window.location.href = "/dashboard";
                    },
                    onError: (error) => {
                        currentlyLoading = false;
                        submit.classList.remove("loading");

                        form.markAsInvalid();
                        Toast.show("error", error.message);
                    },
                });
            }
        });
    };

    init();
});
