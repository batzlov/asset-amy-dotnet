import { Form } from "./shared/form.js";

document.addEventListener("DOMContentLoaded", () => {
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

        document.querySelector("button").addEventListener("click", (event) => {
            event.preventDefault();
            event.stopPropagation();

            if (form.isValid()) {
                console.log("Form is valid");
            }
        });
    };

    init();
});
