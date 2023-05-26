import { Form } from "../shared/form.js";
import { Modal } from "../shared/modal.js";
import { HttpRequest } from "../shared/request.js";
import { Toast } from "../shared/toast.js";
import { assetSchema } from "../shared/schema.js";
import {
    renderChart,
    elementFromString,
    formatCurrency,
} from "../shared/utils.js";
import { COLORS, CHART_TYPES } from "../shared/constants.js";
import { parse } from "path";

document.addEventListener("DOMContentLoaded", () => {
    let form;
    let assets = [];

    let modal;
    let confirmModal, confirmDeleteTemplate;

    let chart = null;
    let chartCanvas;
    let chartType = CHART_TYPES.PIE;

    const init = () => {
        // initialize modal
        const modalTemplate = document.getElementById("asset-modal");
        modal = new Modal(modalTemplate, {});

        // initialize confirm modal
        confirmDeleteTemplate = document.getElementById(
            "confirm-delete-asset-modal"
        );
        confirmModal = new Modal(confirmDeleteTemplate, {
            modalSelector: "#confirm-modal",
            modalToggleSelector: "#confirm-modal-toggle",
        });

        // initialize form
        form = new Form(document.querySelector("form"), assetSchema);

        const createAssetBtn = document.getElementById("create-asset-btn");
        createAssetBtn.addEventListener("click", () => {
            form.reset();
            modal.onSave = createAsset;
            modal.open();
        });

        // initialize table functionality
        const updateAssetBtns = document.querySelectorAll(".update-asset-btn");
        for (let btn of updateAssetBtns) {
            btn.addEventListener("click", updateAssetEvent);
        }

        const deleteAssetBtns = document.querySelectorAll(".delete-asset-btn");
        for (let btn of deleteAssetBtns) {
            btn.addEventListener("click", deleteAssetEvent);
        }

        // load revenues from dom
        const assetsElm = document.querySelector("[data-assets]");
        assets = JSON.parse(assetsElm.getAttribute("data-assets"));
        assetsElm.removeAttribute("data-assets");

        // initialize chart
        chartCanvas = document.getElementById("assets-chart");
        renderAssetsChart(chartType);

        const selectChartType = document.getElementById("select-chart-type");
        selectChartType.addEventListener("change", (event) => {
            chartType = event.target.value;
            renderAssetsChart(chartType);
        });
    };

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

    const createAsset = () => {
        if (!form.isValid()) {
            Toast.show("error", "Bitte überprüfe deine Eingaben");
            return;
        }

        modal.setLoading(true);
        HttpRequest.post("/api/assets", form.toObj(), {
            onSuccess: (createdAsset) => {
                modal.setLoading(false);
                assets.push(createdAsset);

                // if it was the first created asset, hide the alert and show the main content
                if (assets.length === 1) {
                    document
                        .getElementById("no-assets-alert")
                        .classList.add("hidden");
                    document
                        .getElementById("assets-container")
                        .classList.remove("hidden");
                }

                const assetsContainer =
                    document.querySelector(".assets-container");
                assetsContainer.appendChild(renderAssetRow(createdAsset));

                modal.close();
                form.reset();
                Toast.show("success", "Asset-Position wurde erstellt");

                renderAssetsChart(chartType);
                updateAssetsTableFooter();
            },
            onError: (error) => {
                modal.setLoading(false);
                Toast.show("error", error.message);
            },
        });
    };

    const updateAssetEvent = (event) => {
        let dataId =
            event.target.getAttribute("data-id") ||
            event.target.parentNode.getAttribute("data-id");

        assets.find((asset) => {
            if (asset.id == dataId) {
                form.patchValues(asset);
                modal.onSave = () => {
                    updateAsset(asset.id);
                };
                modal.open();
            }
        });
    };

    const updateAsset = (id) => {};

    const deleteAssetEvent = (event) => {};

    const deleteAsset = (id) => {};

    const renderAssetRow = (revenue) => {};

    const updateAssetsTableFooter = () => {
        const total = assets.reduce((acc, asset) => {
            return acc + parseFloat(asset.value);
        }, 0);

        const totalElm = document.querySelector(".assets-total");
        totalElm.innerText = formatCurrency(Number(total.toFixed(2)));
    };

    init();
});
