// Set new default font family and font color to mimic Bootstrap's default styling
Chart.defaults.global.defaultFontFamily = '-apple-system,system-ui,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue",Arial,sans-serif';
Chart.defaults.global.defaultFontColor = '#292b2c';

window.onload = loadClassRiskAssessment();

function DistinctRecords(MYJSON, prop) {
    return MYJSON.filter((obj, pos, arr) => {
        return arr.map(map0bj => map0bj[prop]).indexOf(obj[prop]) === pos;
    })
}

function loadClassRiskAssessment() {
    $.ajax({
        type: 'GET',
        url: '/Home/cargarIndicadores',
        data: {
            'idUsuario': 1
        }, success: function (data) {

            let assetList = data['resumenClasesRiesgo'];

            //Get the unique classes from the data set. 
            classes = DistinctRecords(assetList, "descripcionClase");

            riskVariables = ['L', 'M', 'H']
            classList = [];
            riskL = [];
            riskM = [];
            riskH = [];
            i = 0;


            for (let assetClass in classes) {

                //Complete an array with the classes names
                selectedClass = assetList[assetClass]['descripcionClase']


                //Filter the json data set with current class data
                assetListByClass = assetList.filter(element => element.descripcionClase == selectedClass);

                //Complete the class list array
                classList[i] = selectedClass

                //Complete the risk assessment array
                for (let riskVariable in riskVariables) {

                    //Get the risk level
                    riskLevel = riskVariables[riskVariable]
                    assetListByRiskVariable = assetListByClass.filter(element => element.categorizacionRiesgo == riskLevel);

                    //Get the final value
                    if (assetListByRiskVariable.length > 0) {
                        value = parseInt(assetListByRiskVariable[0]['totalActivos'])
                    } else {
                        value = 0
                    }

                    //Assing the value to the correct array
                    if (riskLevel == 'L') {
                        riskL[i] = value
                    } else if (riskLevel == 'M') {
                        riskM[i] = value
                    } else {
                        riskH[i] = value
                    }

                }

                i++
            }

            drawClasRiskAssessment(classList, riskL, riskM, riskH)



        }, error: function (data) {
            alert("Error calling the data, review your connection")
        }
    })
}


function drawClasRiskAssessment(labels, riskL, riskM, riskH) {


    // Bar Chart Example
    var ctx = document.getElementById("myBarChart");
    var myLineChart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: labels,
            datasets: [{
                label: 'Low Risk',
                //borderColor: '#1C9645',
                backgroundColor: '#008f39',
                data: riskL,
                borderWidth: 1,
            }, {
                label: 'Medium Risk',
                //borderColor: '#F3F732',
                backgroundColor: '#FFFF00',
                data: riskM,
                borderWidth: 1,
            }, {
                label: 'High Risk',
                //borderColor: '#DC164A',
                backgroundColor: '#FF0000',
                data: riskH,
                borderWidth: 1,
            }
            ],
        },
        options: {
            responsive: true,
            plugins: {
                title: {
                    display: false
                }, legend: {
                    display: false
                },
                customCanvasBackgroundColor: {
                    color: 'white',
                }
            },
            scales: {
                xAxes: [{
                    gridLines: {
                        display: false
                    },
                    ticks: {
                        maxTicksLimit: 6
                    }
                }],
                yAxes: [{
                    stacked: true,
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
                    stacked: true,
                    grid: {
                        color: 'white'
                    }
                },
                y: {
                    stacked: true,
                    grid: {
                        color: 'white'
                    }
                 },
            }
        }
    });

}

