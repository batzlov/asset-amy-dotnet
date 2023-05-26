import { Form } from "../shared/form.js";
import { Modal } from "../shared/modal.js";
import { HttpRequest } from "../shared/request.js";
import { Toast } from "../shared/toast.js";
import { revenueSchema } from "../shared/schema.js";
import {
    renderChart,
    elementFromString,
    formatCurrency,
} from "../shared/utils.js";
import { COLORS, CHART_TYPES } from "../shared/constants.js";

document.addEventListener("DOMContentLoaded", () => {
    let form;
    let revenues = [];

    let modal;
    let confirmModal, confirmDeleteTemplate;

    let chart = null;
    let chartCanvas;
    let chartType = CHART_TYPES.PIE;

    const init = () => {
        // initialize modal
        const modalTemplate = document.getElementById("revenue-modal");
        modal = new Modal(modalTemplate, {});

        // initialize confirm modal
        confirmDeleteTemplate = document.getElementById(
            "confirm-delete-revenue-modal"
        );
        confirmModal = new Modal(confirmDeleteTemplate, {
            modalSelector: "#confirm-modal",
            modalToggleSelector: "#confirm-modal-toggle",
        });

        // initialize form
        form = new Form(document.querySelector("form"), revenueSchema);

        const createRevenueBtn = document.getElementById("create-revenue-btn");
        createRevenueBtn.addEventListener("click", () => {
            form.reset();
            modal.onSave = createRevenue;
            modal.open();
        });

        // initialize table functionality
        const updateRevenueBtns = document.querySelectorAll(
            ".update-revenue-btn"
        );
        for (let btn of updateRevenueBtns) {
            btn.addEventListener("click", updateRevenueEvent);
        }

        const deleteRevenueBtns = document.querySelectorAll(
            ".delete-revenue-btn"
        );
        for (let btn of deleteRevenueBtns) {
            btn.addEventListener("click", deleteRevenueEvent);
        }

        // load revenues from dom
        const revenuesElm = document.querySelector("[data-revenues]");
        revenues = JSON.parse(revenuesElm.getAttribute("data-revenues"));
        revenuesElm.removeAttribute("data-revenues");

        // initialize chart
        chartCanvas = document.getElementById("revenues-chart");
        renderRevenuesChart(chartType);

        const selectChartType = document.getElementById("select-chart-type");
        selectChartType.addEventListener("change", (event) => {
            chartType = event.target.value;
            renderRevenuesChart(chartType);
        });
    };

    const renderRevenuesChart = (chartType) => {
        if (chart) {
            chart.destroy();
        }

        chart = renderChart(
            chartCanvas.getContext("2d"),
            revenues,
            {
                datasetLabel: "Einnahmen",
                chartType: CHART_TYPES[chartType.toUpperCase()],
                legendPosition: "top",
                chartTitle: "Einnahmen",
            },
            COLORS
        );
    };

    const createRevenue = () => {
        if (!form.isValid()) {
            Toast.show("error", "Bitte überprüfe deine Engaben.");
            return;
        }

        modal.setLoading(true);
        HttpRequest.post("/api/revenues", form.toObj(), {
            onSuccess: (createdRevenue) => {
                modal.setLoading(false);
                revenues.push(createdRevenue);

                // if it was the first created revenue, hide the alert and show main content
                if (revenues.length == 1) {
                    document
                        .getElementById("no-revenues-alert")
                        .classList.add("hidden");
                    document
                        .getElementById("revenues-container")
                        .classList.remove("hidden");
                }

                const revenuesContainer = document.querySelector(
                    ".revenues-container"
                );
                revenuesContainer.appendChild(renderRevenueRow(createdRevenue));

                modal.close();
                form.reset();
                Toast.show("success", "Einnahme wurde erstellt");

                renderRevenueChart(chartType);
                updateRevenueTableFooter();
            },
            onError: (error) => {
                Toast.show("error", error.message);
                modal.setLoading(false);
            },
        });
    };

    const updateRevenueEvent = (event) => {
        let dataId =
            event.target.getAttribute("data-id") ||
            event.target.parentNode.getAttribute("data-id");

        revenues.find((revenue) => {
            if (revenue.id == dataId) {
                form.patchValues(revenue);
                modal.onSave = () => {
                    updateRevenue(revenue.id);
                };
                modal.open();
            }
        });
    };

    const updateRevenue = (id) => {
        if (!form.isValid()) {
            Toast.show("error", "Bitte überprüfe deine Engaben.");
            return;
        }

        modal.setLoading(true);
        HttpRequest.put(`/api/revenues/${id}`, form.toObj(), {
            onSuccess: (updatedRevenue) => {
                modal.setLoading(false);

                revenues = revenues.map((revenue) => {
                    if (revenue.id == updatedRevenue.id) {
                        return updatedRevenue;
                    }
                    return revenue;
                });

                const rowToUpdate = document.querySelector(
                    `[data-parent-of="${updatedRevenue.id}"]`
                );
                rowToUpdate.replaceWith(renderRevenueRow(updatedRevenue));
                renderRevenuesChart(chartType);
                updateRevenuesTableFooter();

                Toast.show("success", "Einnahme wurde aktualisiert");
                modal.close();
                form.reset();
            },
            onError: (error) => {
                Toast.show("error", error.message);
                modal.setLoading(false);
            },
        });
    };

    const deleteRevenueEvent = (event) => {
        let dataId =
            event.target.getAttribute("data-id") ||
            event.target.parentNode.getAttribute("data-id");

        confirmModal.onSave = () => {
            deleteRevenue(dataId);
        };
        confirmModal.modal.querySelector("#revenue-title").innerText =
            revenues.find((revenue) => revenue.id == dataId).name;
        confirmModal.open();
    };

    const deleteRevenue = (id) => {
        confirmModal.setLoading(true);

        HttpRequest.delete(`/api/revenues/${id}`, {
            onSuccess: () => {
                confirmModal.setLoading(false);

                const rowToDelete = document.querySelector(
                    `[data-parent-of="${id}"]`
                );
                rowToDelete.remove();

                confirmModal.close();
                Toast.show("success", "Einnahme wurde gelöscht");

                revenues = revenues.filter((revenue) => {
                    return revenue.id != id;
                });

                if (revenues.length == 0) {
                    document
                        .getElementById("no-revenues-alert")
                        .classList.remove("hidden");
                    document
                        .getElementById("revenues-container")
                        .classList.add("hidden");

                    return;
                }

                renderRevenuesChart(chartType);

                updateRevenuesTableFooter();
            },
            onError: (error) => {
                confirmModal.setLoading(false);
                Toast.show("error", error.message);
            },
        });
    };

    const renderRevenueRow = (revenue) => {
        const row = elementFromString(`
            <tr data-parent-of="${revenue.id}" class="hover">
                <th>
                    ${revenue.id}
                </th>
                <td>
                    ${revenue.name}
                </td>
                <td>
                    ${formatCurrency(revenue.value)}
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
                                class="update-revenue-btn"
                                data-id="${revenue.id}"
                            >
                                <a>
                                    bearbeiten
                                </a>
                            </li>
                            <li
                                class="delete-revenue-btn"
                                data-id="${revenue.id}"
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

        row.querySelector(".update-revenue-btn").addEventListener(
            "click",
            updateRevenueEvent
        );

        row.querySelector(".delete-revenue-btn").addEventListener(
            "click",
            deleteRevenueEvent
        );

        return row;
    };

    const updateRevenuesTableFooter = () => {
        const total = revenues.reduce((acc, revenue) => {
            return acc + parseFloat(revenue.value);
        }, 0);

        const totalElm = document.querySelector(".revenues-total");
        totalElm.innerText = formatCurrency(Number(total.toFixed(2)));
    };

    init();
});
