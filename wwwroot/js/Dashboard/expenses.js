import { Form } from "../shared/form.js";
import { Modal } from "../shared/modal.js";
import { Toast } from "../shared/toast.js";
import { expenseSchema } from "../shared/schema.js";
import { renderChart } from "./utils.js";
import { COLORS, CHART_TYPES, EXPENSES } from "./constants.js";

document.addEventListener("DOMContentLoaded", () => {
    let form;
    let expenses = [];

    const init = () => {
        const template = document.getElementById("expense-modal");
        const modal = new Modal(template, {});

        const confirmDeleteTemplate = document.getElementById(
            "confirm-delete-expense-modal"
        );
        const confirmModal = new Modal(confirmDeleteTemplate, {
            modalSelector: "#confirm-modal",
            modalToggleSelector: "#confirm-modal-toggle",
        });

        form = new Form(document.querySelector("form"), expenseSchema);

        const createExpenseBtn = document.getElementById("create-expense-btn");
        createExpenseBtn.addEventListener("click", () => {
            modal.open();
        });

        const updateExpenseBtns = document.querySelectorAll(
            ".update-expense-btn"
        );
        for (let btn of updateExpenseBtns) {
            btn.addEventListener("click", () => {
                expenses.find((expense) => {
                    if (expense.id == btn.getAttribute("data-id")) {
                        form.patchValues(expense);
                        modal.open();
                    }
                });
            });
        }

        const deleteExpenseBtns = document.querySelectorAll(
            ".delete-expense-btn"
        );
        for (let btn of deleteExpenseBtns) {
            btn.addEventListener("click", () => {
                confirmModal.open();
            });
        }

        // load expenses from dom
        const expensesElm = document.querySelector("[data-expenses]");
        expenses = JSON.parse(expensesElm.getAttribute("data-expenses"));
        expensesElm.removeAttribute("data-expenses");

        // render chart
        const expensesCanvas = document.getElementById("expenses-chart");
        let chart = renderChart(
            expensesCanvas.getContext("2d"),
            expenses,
            {
                datasetLabel: "Ausgaben",
                chartType: CHART_TYPES.PIE,
                legendPosition: "top",
                chartTitle: "Ausgaben",
            },
            COLORS
        );

        const selectChartType = document.getElementById("select-chart-type");
        selectChartType.addEventListener("change", (event) => {
            chart.destroy();

            const chartType = event.target.value;
            chart = renderChart(
                expensesCanvas.getContext("2d"),
                expenses,
                {
                    datasetLabel: "Ausgaben",
                    chartType: CHART_TYPES[chartType.toUpperCase()],
                    legendPosition: "top",
                    chartTitle: "Ausgaben",
                },
                COLORS
            );
        });
    };

    init();
});
