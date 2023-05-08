import { Form } from "../shared/form.js";
import { HttpRequest } from "../shared/request.js";
import { Toast } from "../shared/toast.js";

document.addEventListener("DOMContentLoaded", () => {
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

        document.querySelector("button").addEventListener("click", (event) => {
            event.preventDefault();
            event.stopPropagation();

            if (form.isValid()) {
                HttpRequest.post("/api/sign-in", form.toObj(), {
                    onSuccess: (response) => {
                        localStorage.setItem("token", response.token);
                        window.location.href = "/dashboard";
                    },
                    onError: (error) => {
                        console.log(Toast);
                        form.markAsInvalid();
                        Toast.show("error", error.message);
                    },
                });
            }
        });
    };

    init();
});
