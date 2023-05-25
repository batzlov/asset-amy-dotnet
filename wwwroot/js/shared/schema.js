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
};
