export const passwordForgottenSchema = {
    email: {
        type: "string",
        rules: ["required", "email"],
    },
};

export const passwordResetSchema = {
    password: {
        type: "string",
        rules: ["required", "min:6"],
    },
    confirmPassword: {
        type: "string",
        rules: ["required", "min:6", "match:password"],
    },
};

export const expenseSchema = {
    name: {
        type: "string",
        rules: ["required", "min:2"],
    },
    value: {
        type: "number",
        rules: ["required", "number"],
    },
};

export const revenueSchema = {
    name: {
        type: "string",
        rules: ["required", "min:2"],
    },
    value: {
        type: "number",
        rules: ["required", "number"],
    },
};

export const assetSchema = {
    name: {
        type: "string",
        rules: ["required", "min:2"],
    },
    value: {
        type: "number",
        rules: ["required", "number"],
    },
    type: {
        type: "string",
        rules: [],
    },
};
