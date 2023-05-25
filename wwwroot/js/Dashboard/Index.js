import { COLORS, CHART_TYPES } from "../shared/constants.js";
import { renderChart } from "../shared/utils.js";

document.addEventListener("DOMContentLoaded", () => {
    let revenues = null;
    let expenses = null;
    let assets = null;
    let revenuesCanvas = null;
    let expensesCanvas = null;
    let assetAllocationCanvas = null;

    function init() {
        revenuesCanvas = document.getElementById("revenues-chart");
        revenues = JSON.parse(revenuesCanvas.getAttribute("data-revenues"));
        revenuesCanvas.removeAttribute("data-revenues");

        expensesCanvas = document.getElementById("expenses-chart");
        expenses = JSON.parse(expensesCanvas.getAttribute("data-expenses"));
        expensesCanvas.removeAttribute("data-expenses");

        assetAllocationCanvas = document.getElementById(
            "asset-allocation-chart"
        );
        assets = JSON.parse(assetAllocationCanvas.getAttribute("data-assets"));
        assetAllocationCanvas.removeAttribute("data-assets");

        renderChart(
            revenuesCanvas.getContext("2d"),
            revenues,
            {
                datasetLabel: "Einnahmen",
                chartType: CHART_TYPES.BAR,
                legendPosition: "top",
                chartTitle: "Einnahmen",
            },
            COLORS
        );

        renderChart(
            expensesCanvas.getContext("2d"),
            expenses,
            {
                datasetLabel: "Ausgaben",
                chartType: CHART_TYPES.BAR,
                legendPosition: "top",
                chartTitle: "Ausgaben",
            },
            COLORS
        );

        renderChart(
            assetAllocationCanvas.getContext("2d"),
            assets,
            {
                datasetLabel: "Asset Allocation",
                chartType: CHART_TYPES.BAR,
                legendPosition: "top",
                chartTitle: "Asset Allocation",
            },
            COLORS
        );
    }

    init();
});
