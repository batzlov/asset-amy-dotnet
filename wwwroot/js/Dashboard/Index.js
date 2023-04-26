document.addEventListener("DOMContentLoaded", () => {
    const COLORS = {
        red: "rgb(255, 99, 132)",
        orange: "rgb(255, 159, 64)",
        yellow: "rgb(255, 205, 86)",
        green: "rgb(75, 192, 192)",
        blue: "rgb(54, 162, 235)",
        purple: "rgb(153, 102, 255)",
        grey: "rgb(201, 203, 207)",
    };

    const CHART_TYPES = {
        PIE: "pie",
        LINE: "line",
        BAR: "bar",
        SCATTER: "scatter",
        DOUGHNUT: "doughnut",
    };

    const revenues = [
        { name: "Bafög", amount: 850 },
        { name: "Nebenjob", amount: 250 },
    ];

    const expenses = [
        { name: "Miete", amount: 350 },
        { name: "Konsumausgaben", amount: 300 },
        { name: "Krankenkasse", amount: 122.78 },
        { name: "Spotify", amount: 9.99 },
        { name: "McFit", amount: 20.0 },
        { name: "iCloud", amount: 2.99 },
        { name: "Reisekrankenversicherung", amount: 0.75 },
        { name: "Strom", amount: 39.0 },
        { name: "Mobilfunk", amount: 10.0 },
        { name: "Brillenversicherung", amount: 0.83 },
        { name: "BahnCard", amount: 3.08 },
        { name: "Hosting", amount: 5.0 },
        { name: "Internet", amount: 25.0 },
        { name: "Haftpflichtversicherung", amount: 3.48 },
    ];

    const assets = [
        { name: "Bondora Go & Grow", type: "P2P", amount: 2400 },
        { name: "LBS Bausparen", type: "Bausparen", amount: 1400 },
        { name: "ING-Depot", type: "ETF", amount: 5500 },
        { name: "Trade-Depot", type: "ETF", amount: 150 },
        { name: "Tagegeldkonto", type: "Tagesgeld", amount: 3000 },
    ];

    var revenuesCanvas = null;
    var expensesCanvas = null;
    var assetAllocationCanvas = null;

    function init() {
        revenuesCanvas = document.getElementById("revenues-chart");
        expensesCanvas = document.getElementById("expenses-chart");
        assetAllocationCanvas = document.getElementById(
            "asset-allocation-chart"
        );

        renderChart(revenuesCanvas.getContext("2d"), revenues, {
            datasetLabel: "Einnahmen",
            chartType: CHART_TYPES.PIE,
            legendPosition: "top",
            chartTitle: "Einnahmen",
        });

        renderChart(expensesCanvas.getContext("2d"), expenses, {
            datasetLabel: "Ausgaben",
            chartType: CHART_TYPES.PIE,
            legendPosition: "top",
            chartTitle: "Ausgaben",
        });

        renderChart(assetAllocationCanvas.getContext("2d"), assets, {
            datasetLabel: "Asset Allocation",
            chartType: CHART_TYPES.PIE,
            legendPosition: "top",
            chartTitle: "Asset Allocation",
        });
    }

    function renderChart(ctx, data, config) {
        const chartData = {
            labels: data.map((entry) => {
                return entry.name;
            }),
            datasets: [
                {
                    label: config.datasetLabel,
                    data: data.map((entry) => {
                        return entry.amount;
                    }),
                    backgroundColor: Object.values(COLORS),
                },
            ],
        };

        const chartConfig = {
            type: config.chartType,
            data: chartData,
            options: {
                responsive: config.responsive,
                plugins: {
                    legend: {
                        position: config.legendPosition,
                    },
                    title: {
                        display: true,
                        text: config.chartTitle,
                    },
                },
            },
        };

        new Chart(ctx, chartConfig);
    }

    init();
});
