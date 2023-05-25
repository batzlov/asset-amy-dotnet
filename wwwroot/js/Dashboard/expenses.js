import { Form } from "../shared/form.js";
import { Modal } from "../shared/modal.js";
import { HttpRequest } from "../shared/request.js";
import { Toast } from "../shared/toast.js";
import { expenseSchema } from "../shared/schema.js";
import {
    renderChart,
    elementFromString,
    formatCurrency,
} from "../shared/utils.js";
import { COLORS, CHART_TYPES } from "../shared/constants.js";

document.addEventListener("DOMContentLoaded", () => {
    let form;
    let expenses = [];

    let modal;
    let confirmModal, confirmDeleteTemplate;

    let chart = null;
    let chartCanvas;
    let chartType = CHART_TYPES.PIE;

    const init = () => {
        const template = document.getElementById("expense-modal");
        modal = new Modal(template, {});

        confirmDeleteTemplate = document.getElementById(
            "confirm-delete-expense-modal"
        );
        confirmModal = new Modal(confirmDeleteTemplate, {
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
            btn.addEventListener("click", updateExpenseEvent);
        }

        const deleteExpenseBtns = document.querySelectorAll(
            ".delete-expense-btn"
        );
        for (let btn of deleteExpenseBtns) {
            btn.addEventListener("click", deleteExpenseEvent);
        }

        // load expenses from dom
        const expensesElm = document.querySelector("[data-expenses]");
        expenses = JSON.parse(expensesElm.getAttribute("data-expenses"));
        expensesElm.removeAttribute("data-expenses");

        // render chart
        chartCanvas = document.getElementById("expenses-chart");
        renderExpenseChart(chartType);

        const selectChartType = document.getElementById("select-chart-type");
        selectChartType.addEventListener("change", (event) => {
            chartType = event.target.value;
            renderExpenseChart(chartType);
        });
    };

    const renderExpenseChart = (chartType) => {
        if (chart) {
            chart.destroy();
        }

        chart = renderChart(
            chartCanvas.getContext("2d"),
            expenses,
            {
                datasetLabel: "Ausgaben",
                chartType: CHART_TYPES[chartType.toUpperCase()],
                legendPosition: "top",
                chartTitle: "Ausgaben",
            },
            COLORS
        );
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

                // if it was the first created expense, hide the alert and show main content
                if (expenses.length == 1) {
                    document
                        .getElementById("no-expenses-alert")
                        .classList.add("hidden");
                    document
                        .getElementById("expenses-container")
                        .classList.remove("hidden");
                }

                const expensesContainer = document.querySelector(
                    ".expenses-container"
                );
                expensesContainer.appendChild(renderExpenseRow(expense));

                modal.close();
                form.reset();
                Toast.show("success", "Ausgabe wurde erstellt");

                renderExpenseChart(chartType);
                updateExpenseTableFooter();
            },
            onError: (error) => {
                Toast.show("error", error.message);
                modal.setLoading(false);
            },
        });
    };

    const updateExpenseEvent = (event) => {
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

    const updateExpense = (id) => {
        if (!form.isValid()) {
            Toast.show("error", "Bitte überprüfe deine Engaben.");
            return;
        }

        modal.setLoading(true);
        HttpRequest.put(`/api/expenses/${id}`, form.toObj(), {
            onSuccess: (updatedExpense) => {
                modal.setLoading(false);

                expenses = expenses.map((expense) => {
                    if (expense.id == id) {
                        return updatedExpense;
                    }
                    return expense;
                });

                const rowToUpdate = document.querySelector(
                    `[data-parent-of="${id}"]`
                );
                rowToUpdate.replaceWith(renderExpenseRow(updatedExpense));
                renderExpenseChart(chartType);
                updateExpenseTableFooter();

                Toast.show("success", "Ausgabe wurde geändert");
                modal.close();
                form.reset();
            },
            onError: (error) => {
                Toast.show("error", error.message);
                modal.setLoading(false);
            },
        });
    };

    const deleteExpenseEvent = (event) => {
        let dataId =
            event.target.getAttribute("data-id") ||
            event.target.parentNode.getAttribute("data-id");

        confirmModal.onSave = () => {
            deleteExpense(dataId);
        };
        confirmModal.modal.querySelector("#expense-title").innerText =
            expenses.find((expense) => expense.id == dataId).name;
        confirmModal.open();
    };

    const deleteExpense = (id) => {
        confirmModal.setLoading(true);
        HttpRequest.delete(`/api/expenses/${id}`, {
            onSuccess: (response) => {
                confirmModal.setLoading(false);

                const rowToDelete = document.querySelector(
                    `[data-parent-of="${id}"]`
                );
                rowToDelete.remove();

                confirmModal.close();
                Toast.show("success", "Die Ausgabe wurde gelöscht");

                expenses = expenses.filter((expense) => {
                    return expense.id != id;
                });

                // if there are no more expenses, show the alert and hide main content
                if (expenses.length == 0) {
                    document
                        .getElementById("no-expenses-alert")
                        .classList.remove("hidden");
                    document
                        .getElementById("expenses-container")
                        .classList.add("hidden");

                    return;
                }

                renderExpenseChart(chartType);

                updateExpenseTableFooter();
            },
            onError: (error) => {
                confirmModal.setLoading(false);

                Toast.show("error", error.message);
            },
        });
    };

    const renderExpenseRow = (expense) => {
        const row = elementFromString(`
            <tr data-parent-of="${expense.id}" class="hover">
                <th>
                    ${expense.id}
                </th>
                <td>
                    ${expense.name}
                </td>
                <td>
                    ${formatCurrency(expense.value)}
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
            updateExpenseEvent
        );

        row.querySelector(".delete-expense-btn").addEventListener(
            "click",
            deleteExpenseEvent
        );

        return row;
    };

    const updateExpenseTableFooter = () => {
        const total = expenses.reduce((acc, expense) => {
            return acc + parseFloat(expense.value);
        }, 0);

        const totalElement = document.querySelector(".expenses-total");
        totalElement.innerHTML = total;
    };

    init();
});
