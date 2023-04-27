import {
    REVENUES,
    EXPENSES,
    ASSETS,
    COLORS,
    CHART_TYPES,
} from "../dashboard/constants.js";
import { renderChart } from "../dashboard/utils.js";

document.addEventListener("DOMContentLoaded", () => {
    var revenuesCanvas = null;
    var expensesCanvas = null;
    var assetAllocationCanvas = null;

    function init() {
        revenuesCanvas = document.getElementById("revenues-chart");
        expensesCanvas = document.getElementById("expenses-chart");
        assetAllocationCanvas = document.getElementById(
            "asset-allocation-chart"
        );

        renderChart(
            revenuesCanvas.getContext("2d"),
            REVENUES,
            {
                datasetLabel: "Einnahmen",
                chartType: CHART_TYPES.PIE,
                legendPosition: "top",
                chartTitle: "Einnahmen",
            },
            COLORS
        );

        renderChart(
            expensesCanvas.getContext("2d"),
            EXPENSES,
            {
                datasetLabel: "Ausgaben",
                chartType: CHART_TYPES.PIE,
                legendPosition: "top",
                chartTitle: "Ausgaben",
            },
            COLORS
        );

        renderChart(
            assetAllocationCanvas.getContext("2d"),
            ASSETS,
            {
                datasetLabel: "Asset Allocation",
                chartType: CHART_TYPES.PIE,
                legendPosition: "top",
                chartTitle: "Asset Allocation",
            },
            COLORS
        );
    }

    init();
});