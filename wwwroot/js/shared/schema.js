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
