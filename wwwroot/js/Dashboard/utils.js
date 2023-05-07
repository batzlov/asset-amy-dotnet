export function sumNumberArray(array) {
    return array.reduce((a, b) => a + b, 0);
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
                    return entry.amount;
                }),
                backgroundColor: Object.values(colors),
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

export function elementFromString(htmlString) {
    const template = document.createElement("template");

    template.innerHTML = htmlString.trim();

    return template.content.firstElementChild;
}
