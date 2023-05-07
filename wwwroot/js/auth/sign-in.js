import { Form } from "../shared/form.js";
import { HttpRequest } from "../shared/request.js";

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
                HttpRequest.post("/api/sign-in", form.toObj())
                    .then((response) => {
                        console.log(response);
                        localStorage.setItem("token", response.token);
                        window.location.href = "/dashboard";
                    })
                    .catch((error) => {
                        console.log(error);
                    });
            }
        });
    };

    init();
});
