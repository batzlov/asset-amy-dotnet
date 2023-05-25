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
    let expenses = [];

    let modal;
    let confirmModal, confirmDeleteTemplate;

    let chart = null;
    let chartCanvas;
    let chartType = CHART_TYPES.PIE;

    const init = () => {};

    const renderRevenuesChart = (chartType) => {
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

    const createRevenue = () => {};

    const updateRevenueEvent = (event) => {};

    const updateRevenue = (id) => {};

    const deleteRevenueEvent = (event) => {};

    const deleteRevenue = (id) => {};

    const renderRevenueRow = (revenue) => {};

    const updateRevenuesTableFooter = () => {};

    init();
});
