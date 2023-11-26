// Set new default font family and font color to mimic Bootstrap's default styling
Chart.defaults.global.defaultFontFamily = '-apple-system,system-ui,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue",Arial,sans-serif';
Chart.defaults.global.defaultFontColor = '#292b2c';

window.onload = loadClassData();

function loadClassData() {
    $.ajax({
        type: 'GET',
        url: '/Home/cargarIndicadores',
        data: {
            'idUsuario': 1
        }, success: function (data) {

            let assetList = data['resumenClases'];
            let Class = [];
            let Totals = [];

            for (var i = 0; i < assetList.length; i++) {

                Class[i] = assetList[i]['descripcionClase'];
                Totals[i] = assetList[i]['totalActivos'];

            }

            drawClassAssetResume(Class, Totals);

        }, error: function (data) {
            alert("Error calling the data, review your connection")
        }
    })
}


function drawClassAssetResume(Class, Totals) {


    // Bar Chart Example
    var ctx = document.getElementById("myAreaChart");
    var myLineChart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: Class,
            datasets: [{
                label: "Cantidad Activos por Clase",
                backgroundColor: "rgba(2,117,216,1)",
                borderColor: "rgba(2,117,216,1)",
                data: Totals,
            }],
        },
        options: {
            plugins: {
                title: {
                    display: false
                }, legend: {
                    display: false
                }
            },
            scales: {
                xAxes: [{
                    time: {
                        unit: 'class'
                    },
                    gridLines: {
                        display: false
                    },
                    ticks: {
                        maxTicksLimit: 6
                    }
                }],
                yAxes: [{
                    ticks: {
                        min: 0,
                        max: 15000,
                        maxTicksLimit: 5
                    },
                    gridLines: {
                        display: false
                    }
                }],
                x: {
                    grid: {
                        color: 'white'
                    }
                },
                y: {
                    grid: {
                        color: 'white'
                    }
                },
            },
            legend: {
                display: false
            }
        }
    });

}

