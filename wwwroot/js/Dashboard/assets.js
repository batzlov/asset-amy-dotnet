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
        renderAssetChart(chartType);

        const selectChartType = document.getElementById("select-chart-type");
        selectChartType.addEventListener("change", (event) => {
            chartType = event.target.value;
            renderAssetChart(chartType);
        });
    };

    const renderAssetChart = (chartType) => {
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

                renderAssetChart(chartType);
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

    const updateAsset = (id) => {
        if (!form.isValid()) {
            Toast.show("error", "Bitte überprüfe deine Engaben.");
            return;
        }

        modal.setLoading(true);
        HttpRequest.put(`/api/assets/${id}`, form.toObj(), {
            onSuccess: (updatedAsset) => {
                modal.setLoading(false);

                assets = assets.map((asset) => {
                    if (asset.id == id) {
                        return updatedAsset;
                    }
                    return asset;
                });

                const rowToUpdate = document.querySelector(
                    `[data-parent-of="${id}"]`
                );
                rowToUpdate.replaceWith(renderAssetRow(updatedAsset));
                renderAssetChart(chartType);
                updateAssetsTableFooter();

                Toast.show("success", "Vermögens-Position wurde geändert");
                modal.close();
                form.reset();
            },
            onError: (error) => {
                Toast.show("error", error.message);
                modal.setLoading(false);
            },
        });
    };

    const deleteAssetEvent = (event) => {
        let dataId =
            event.target.getAttribute("data-id") ||
            event.target.parentNode.getAttribute("data-id");

        confirmModal.onSave = () => {
            deleteAsset(dataId);
        };
        confirmModal.modal.querySelector("#asset-title").innerText =
            assets.find((asset) => asset.id == dataId).name;
        confirmModal.open();
    };

    const deleteAsset = (id) => {
        confirmModal.setLoading(true);
        HttpRequest.delete(`/api/assets/${id}`, {
            onSuccess: (response) => {
                confirmModal.setLoading(false);

                const rowToDelete = document.querySelector(
                    `[data-parent-of="${id}"]`
                );
                rowToDelete.remove();

                confirmModal.close();
                Toast.show("success", "Die Vermögens-Position wurde gelöscht");

                assets = assets.filter((asset) => {
                    return asset.id != id;
                });

                // if there are no more expenses, show the alert and hide main content
                if (assets.length == 0) {
                    document
                        .getElementById("no-assets-alert")
                        .classList.remove("hidden");
                    document
                        .getElementById("assets-container")
                        .classList.add("hidden");

                    return;
                }

                renderAssetChart(chartType);

                updateAssetsTableFooter();
            },
            onError: (error) => {
                confirmModal.setLoading(false);

                Toast.show("error", error.message);
            },
        });
    };

    const renderAssetRow = (asset) => {
        const row = elementFromString(`
            <tr data-parent-of="${asset.id}" class="hover">
                <th>
                    ${asset.id}
                </th>
                <td>
                    ${asset.name}
                </td>
                <td>
                    ${formatCurrency(asset.value)}
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
                                class="update-asset-btn"
                                data-id="${asset.id}"
                            >
                                <a>
                                    bearbeiten
                                </a>
                            </li>
                            <li
                                class="delete-asset-btn"
                                data-id="${asset.id}"
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

        row.querySelector(".update-asset-btn").addEventListener(
            "click",
            updateAssetEvent
        );

        row.querySelector(".delete-asset-btn").addEventListener(
            "click",
            deleteAssetEvent
        );

        return row;
    };

    const updateAssetsTableFooter = () => {
        const total = assets.reduce((acc, asset) => {
            return acc + parseFloat(asset.value);
        }, 0);

        const totalElm = document.querySelector(".assets-total");
        totalElm.innerText = formatCurrency(Number(total.toFixed(2)));
    };

    init();
});
