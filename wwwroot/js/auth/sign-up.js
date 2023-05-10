import { Form } from "../shared/form.js";
import { HttpRequest } from "../shared/request.js";
import { Toast } from "../shared/toast.js";

document.addEventListener("DOMContentLoaded", () => {
    let currentlyLoading = false;

    const schema = {
        firstName: {
            type: "string",
            rules: ["required", "min:2", "max:10"],
        },
        lastName: {
            type: "string",
            rules: ["required"],
        },
        email: {
            type: "string",
            rules: ["required", "email"],
        },
        password: {
            type: "string",
            rules: ["required"],
        },
        confirmPassword: {
            type: "string",
            rules: ["required", "match:password"],
        },
        privacyPolicyAccepted: {
            type: "boolean",
            rules: ["requiredCheckboxTrue"],
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

                HttpRequest.post("/api/sign-up", form.toObj(), {
                    onSuccess: (response) => {
                        currentlyLoading = false;
                        submit.classList.remove("loading");

                        window.location.href = "/sign-in";
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
