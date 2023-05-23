import { Form } from "../shared/form.js";
import { Modal } from "../shared/modal.js";
import { HttpRequest } from "../shared/request.js";
import { elementFromString } from "./utils.js";
import { Toast } from "../shared/toast.js";
import { expenseSchema } from "../shared/schema.js";
import { renderChart } from "./utils.js";
import { COLORS, CHART_TYPES, EXPENSES } from "./constants.js";

document.addEventListener("DOMContentLoaded", () => {
    let form;
    let expenses = [];
    let modal;

    const init = () => {
        const template = document.getElementById("expense-modal");
        modal = new Modal(template, {});

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
            form.reset();
            modal.onSave = () => {
                createExpense();
            };
            modal.open();
        });

        const updateExpenseBtns = document.querySelectorAll(
            ".update-expense-btn"
        );
        for (let btn of updateExpenseBtns) {
            btn.addEventListener("click", updateExpenseEventListener);
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

    const createExpense = () => {
        if (!form.isValid()) {
            Toast.show("error", "Bitte überprüfe deine Engaben.");
            return;
        }

        modal.setLoading(true);
        HttpRequest.post("/api/expenses", form.toObj(), {
            onSuccess: (expense) => {
                modal.setLoading(false);

                expenses.push(expense);
                const expensesContainer = document.querySelector(
                    ".expenses-container"
                );
                expensesContainer.appendChild(renderExpenseRow(expense));

                modal.close();
                form.reset();
                Toast.show("success", "Ausgabe wurde erstellt.");
            },
            onError: (error) => {
                Toast.show("error", error.message);
                modal.setLoading(false);
            },
        });
    };

    const updateExpense = (id) => {
        if (!form.isValid()) {
            Toast.show("error", "Bitte überprüfe deine Engaben.");
            return;
        }

        modal.setLoading(true);
        HttpRequest.put(`/api/expenses/${id}`, form.toObj(), {
            onSuccess: (response) => {
                // TODO: add expense to dom
                console.log(response);
                modal.setLoading(false);
            },
            onError: (error) => {
                Toast.show("error", error.message);
                modal.setLoading(false);
            },
        });
    };

    const updateExpenseEventListener = (event) => {
        let dataId =
            event.target.getAttribute("data-id") ||
            event.target.parentNode.getAttribute("data-id");

        expenses.find((expense) => {
            if (expense.id == dataId) {
                form.patchValues(expense);
                modal.onSave = () => {
                    updateExpense(expense.id);
                };
                modal.open();
            }
        });
    };

    const deleteExpenseEventListener = (event) => {
        let dataId =
            event.target.getAttribute("data-id") ||
            event.target.parentNode.getAttribute("data-id");

        rowToDelete = document.querySelector(`[data-parent-of="${dataId}"]`);
        rowToDelete.remove();
    };

    const renderExpenseRow = (expense) => {
        const row = elementFromString(`
            <tr data-parent-of="${expense.id}">
                <th>
                    ${expense.id}
                </th>
                <td>
                    ${expense.name}
                </td>
                <td>
                    ${expense.value}
                </td>
                <td
                    class="flex justify-end"
                >
                    <div class="dropdown dropdown-bottom dropdown-end ml-auto">
                        <label tabindex="0" class="btn btn-ghost btn-sm normal-case btn-square ">
                            <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" aria-hidden="true" class="w-5">
                                <path stroke-linecap="round" stroke-linejoin="round" d="M12 6.75a.75.75 0 110-1.5.75.75 0 010 1.5zM12 12.75a.75.75 0 110-1.5.75.75 0 010 1.5zM12 18.75a.75.75 0 110-1.5.75.75 0 010 1.5z"></path>
                            </svg>
                        </label>
                        <ul tabindex="0" class="dropdown-content menu menu-compact  p-2 shadow bg-base-100 rounded-box w-52">
                            <li
                                class="update-expense-btn"
                                data-id="${expense.id}"
                            >
                                <a>
                                    bearbeiten
                                </a>
                            </li>
                            <li
                                class="delete-expense-btn"
                                data-id="${expense.id}"
                            >
                                <a>
                                    löschen
                                </a>
                            </li>
                        </ul>
                    </div> 
                </td>
            </tr>
        `);

        row.querySelector(".update-expense-btn").addEventListener(
            "click",
            updateExpenseEventListener
        );

        row.querySelector(".delete-expense-btn").addEventListener(
            "click",
            deleteExpenseEventListener
        );

        return row;
    };

    init();
});
