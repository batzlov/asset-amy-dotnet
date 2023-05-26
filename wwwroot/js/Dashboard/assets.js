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
    let assets = [];

    let modal;
    let confirmModal, confirmDeleteTemplate;

    let chart = null;
    let chartCanvas;
    let chartType = CHART_TYPES.PIE;

    const init = () => {};

    const renderAssetsChart = (chartType) => {
        if (chart) {
            chart.destroy();
        }

        chart = renderChart(
            chartCanvas.getContext("2d"),
            assets,
            {
                datasetLabel: "Assets",
                chartType: CHART_TYPES[chartType.toUpperCase()],
                legendPosition: "top",
                chartTitle: "Asset Allocation",
            },
            COLORS
        );
    };

    const createAsset = () => {};

    const updateAssetEvent = (event) => {};

    const updateAsset = (id) => {};

    const deleteAssetEvent = (event) => {};

    const deleteAsset = (id) => {};

    const renderAssetRow = (revenue) => {};

    const updateAssetsTableFooter = () => {};

    init();
});
