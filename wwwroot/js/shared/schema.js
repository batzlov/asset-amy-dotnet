export const expenseSchema = {
    name: {
        type: "string",
        rules: ["required", "min:2"],
    },
    value: {
        type: "number",
        rules: ["required", "pattern:/[0-9]/"],
    },
};
