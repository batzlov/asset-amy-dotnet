export function sumNumberArray(array) {
    return array.reduce((a, b) => a + b, 0);
}

export function formatCurrency(value) {
    return value.toLocaleString("de-DE", {
        style: "currency",
        currency: "EUR",
    });
}

export function renderChart(ctx, data, config, colors) {
    const chartData = {
        labels: data.map((entry) => {
            return entry.name;
        }),
        datasets: [
            {
                label: config.datasetLabel,
                data: data.map((entry) => {
                    return Number(entry.value);
                }),
                backgroundColor: Object.values(colors),
            },
        ],
    };

    const chartConfig = {
        type: config.chartType,
        data: chartData,
        options: {
            elements: {
                arc: {
                    borderWidth: 0,
                },
            },
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

    return new Chart(ctx, chartConfig);
}

export function elementFromString(htmlString) {
    const template = document.createElement("template");

    template.innerHTML = htmlString.trim();

    return template.content.firstElementChild;
}
