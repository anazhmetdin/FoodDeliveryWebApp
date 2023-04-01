$(document).ready(function () {

    let sales_year_line = null;
    let income_year_line = null;
    let sales_category_pie = null;
    let sales_product_pie = null;

    const colors = ['#4AAFD5', '#91B187', '#E7A339', '#D96459', '#8C4646']

    function drawChart(chartInstance, canvasId, chartSettings) {

        if (chartInstance != null) {
            //update chart with new configuration
            chartInstance.options = { ...chartSettings.options };
            chartInstance.data = { ...chartSettings.data };

            chartInstance.update();
            return chartInstance;
        } else {
            //create new chart.
            var ctx = document.getElementById(canvasId).getContext('2d');
            return new Chart(ctx, chartSettings);
        }
    }

    function buildSelectFilter(years, currentYear) {
        //populate years option

        var selectOptionsFilterHtml = "";

        if (years) {

            years.forEach((year) => {
                selectOptionsFilterHtml += `<option value="${year}" 
                      ${currentYear == year ? 'selected' : ''}>${year}</option>`
            });
        }

        $("#filterByYear").html(selectOptionsFilterHtml);
    }

    function drawLineChart(chartInstance, canvasId, data, titleText, yAxisTitle) {

        let settings = {
            // The type of chart we want to create
            type: 'line',
            // The data for our dataset
            data: {
                datasets: [{
                    backgroundColor: 'rgba(0,0,0,0)',
                    borderColor: "#FFA500",
                    data: data
                }]
            },

            // Configuration options go here
            options: {
                plugins: {
                    title: {
                        display: true,
                        text: titleText,
                        font: {
                            size: 16
                        }
                    },
                    legend: {
                        display: false
                    }
                },
                scales: {
                    x: {
                        type: 'time',
                        time: {
                            unit: 'month',
                            displayFormats: {
                                month: 'MM YYYY'
                            }
                        },
                        beginAtZero: true,
                        title: {
                            display: true,
                            text: 'Month',
                            font: {
                                size: 16
                            }
                        },

                        min: data[0]["x"].clone().subtract(1, 'week'),
                        max: data.slice(-1)[0]["x"].clone().add(1, 'week'),
                    },
                    y: {
                        beginAtZero: true,

                        title: {
                            display: true,
                            text: yAxisTitle,
                            font: {
                                size: 16
                            }
                        },
                    }
                }
            }
        };

        return drawChart(chartInstance, canvasId, settings);
    }

    function drawPieChart(chartInstance, canvasId, data, labels, titleText) {

        var settings = {
            // The type of chart we want to create
            type: 'pie',


            // The data for our dataset
            data: {
                labels: labels,
                datasets: [{
                    backgroundColor: colors,
                    borderColor: colors,
                    data: data
                }],
            },

            // Configuration options go here
            options: {
                plugins: {
                    title: {
                        display: true,
                        text: titleText,
                        font: {
                            size: 16
                        }
                    }
                },
                tooltips: {
                    callbacks: {
                        label: function (tooltipItem, data) {
                            //create custom display.
                            var label = data.labels[tooltipItem.index] || '';
                            var currentData = data.datasets[0].data[tooltipItem.index];

                            if (label) {
                                label = `${label} ${Number(currentData)} %`;
                            }

                            return label;
                        }
                    },
                },
            }
        };

        return drawChart(chartInstance, canvasId, settings);
    }

    function filterDashboardDataByYear(currentYear) {

        currentYearURL = currentYear || '';
        currentYearURL = currentYear == '' ? '' : '?year=' + currentYear;
        let url = `Dashboard/Data${currentYearURL}`;

        $.get(url, function (data) {

            if (data.years.length == 0)
                return;

            if (!currentYear) {
                //pick the last year.
                currentYear = data.years[0];
            }

            buildSelectFilter(data.years, currentYear);

            let salesPerYear_data = [];
            if (data.salesPerYear) {
                salesPerYear_data = data.salesPerYear.map
                    (u => { return { "x": moment(u.x, "YYYY-MM-DDThh:mm:ss").date(1), "y": u.y } });
            }

            let incomePerYear_data = [];
            if (data.incomePerYear) {
                incomePerYear_data = data.incomePerYear.map
                    (u => { return { "x": moment(u.x, "YYYY-MM-DDThh:mm:ss").date(1), "y": u.y } });
            }

            let salesPerProduct_data = [];
            let salesPerProduct_labels = [];
            if (data.salesPerProduct) {
                salesPerProduct_data = data.salesPerProduct.map(u => u.value);
                salesPerProduct_labels = data.salesPerProduct.map(u => u.label);
            }

            let salesPerCategory_data = [];
            let SalesPerCategory_labels = [];
            if (data.salesPerCategory) {
                salesPerCategory_data = data.salesPerCategory.map(u => u.value);
                SalesPerCategory_labels = data.salesPerCategory.map(u => u.label);
            }

            sales_year_line = drawLineChart(sales_year_line, "sales-per-year-chart", salesPerYear_data,
                `Number of sold products per month on ${currentYear}`, "Products");

            income_year_line = drawLineChart(income_year_line, "income-per-year-chart", incomePerYear_data,
                `Income per month on ${currentYear}`, "Income");

            sales_category_pie = drawPieChart(sales_category_pie, "sales-per-category", salesPerCategory_data, SalesPerCategory_labels,
                `Number of sold products on ${currentYear} in top 5 categories`);

            sales_product_pie = drawPieChart(sales_product_pie, "sales-per-product", salesPerProduct_data, salesPerProduct_labels,
                `Number of sales on ${currentYear} for top 5 products`);

        });
    }

    filterDashboardDataByYear();

    $("#filterByYear").on("change", function () {
        filterDashboardDataByYear(parseInt($(this).val()));
    });
});