export class Form {
    constructor(form, schema, options = {}) {
        this.form = form;
        this.schema = schema;
        this.errors = [];
        this.validators = {
            required: (input, value) => {
                return value !== "";
            },
            requiredCheckboxTrue: (input, value) => {
                return input.checked === true;
            },
            email: (input, value) => {
                return value.includes("@");
            },
            number: (input, value) => {
                if (typeof value != "string") {
                    return false;
                }

                return !isNaN(value) && !isNaN(parseFloat(value));
            },
            positiveNumber: (input, value) => {
                if (typeof value != "string") {
                    return false;
                }

                return !isNaN(value) && !isNaN(parseFloat(value)) && value >= 0;
            },
            min: (input, value, min) => {
                return value.length >= min;
            },
            max: (input, value, max) => {
                return value.length <= max;
            },
            pattern: (input, value, pattern) => {
                return new RegExp(pattern).test(value);
            },
            match: (input, value, matchInputName) => {
                const matchInput = this.form.querySelector(
                    `[name="${matchInputName}"]`
                );

                if (matchInput) {
                    return value === matchInput.value;
                }

                return false;
            },
        };
        this.inputs = this.form.querySelectorAll(
            "input, select, textarea, input[type=checkbox]"
        );
        this.options = options;

        this.init();
    }

    init() {
        // initialize on blur validation for all inputs
        for (let input of this.inputs) {
            if (this.schema[input.name].validateOnBlur === false) {
                continue;
            }

            if (input.type === "checkbox" || input.type === "select") {
                input.addEventListener("change", () => {
                    this.validate(input);
                });
            } else {
                input.addEventListener("blur", () => {
                    this.validate(input);
                });
            }
        }
    }

    validate(input) {
        this.errors = this.errors.filter((e) => e.inputName !== input.name);

        const inputName = input.name;
        const inputValue = input.value;
        const validationRules = this.schema[inputName].rules;

        if (!validationRules) {
            return true;
        }

        for (let rule of validationRules) {
            let fieldIsValid;
            let validator;

            if (rule.includes(":")) {
                const [ruleName, ruleValue] = rule.split(":");
                validator = this.validators[ruleName];
                fieldIsValid = validator(input, inputValue, ruleValue);
            } else {
                validator = this.validators[rule];
                fieldIsValid = validator(input, inputValue);
            }

            if (!fieldIsValid) {
                this.errors.push({ inputName, rule });
                input.classList.add("input-error");
            } else if (
                !this.errors.some((error) => error.inputName === inputName)
            ) {
                input.classList.remove("input-error");
            }
        }

        return this.errors;
    }

    validateAll() {
        this.errors = [];

        for (let input of this.inputs) {
            this.validate(input);
        }

        return this.errors;
    }

    isValid() {
        this.validateAll();

        return this.errors.length === 0;
    }

    markAsInvalid(inputNames = []) {
        if (inputNames.length === 0) {
            for (let input of this.inputs) {
                input.classList.add("input-error");
            }

            return true;
        }

        for (let input of this.inputs) {
            if (inputNames.includes(input.name)) {
                input.classList.add("input-error");
            }
        }

        return true;
    }

    patchValues(obj) {
        for (let [key, value] of Object.entries(obj)) {
            const input = this.form.querySelector(`[name="${key}"]`);

            if (input) {
                input.value = value;
            }
        }
    }

    reset() {
        this.form.reset();
    }

    toObj() {
        const formData = new FormData(this.form);
        const obj = {};

        for (let [key, value] of formData.entries()) {
            switch (this.schema[key].type) {
                case "number":
                    obj[key] = Number(value);
                    break;
                case "boolean":
                    obj[key] = Boolean(value);
                    break;
                default:
                    obj[key] = String(value);
                    break;
            }
        }

        return obj;
    }
}
