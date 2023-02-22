const VisitorsPerMonthChart = document.getElementById('visitors-per-month-chart');

//LOAD EVENT
$(document).ready(function () {



    //Setup request
    var xmlHttpRequest = new XMLHttpRequest();

    xmlHttpRequest.onload = function () {
        if (xmlHttpRequest.status >= 400) {
            //ERROR
            console.log(xmlHttpRequest);
            $.notify({ icon: "fas fa-exclamation-triangle", message: "There was an error while trying to get chart data" }, { type: "danger", placement: { from: "bottom", align: "center" } });
        }
        else {
            //SUCCESS
            let data = JSON.parse(xmlHttpRequest.response);

            let arrayMonth = new Array();
            let arrayCounterOfVisitorsPlusPercentage = new Array();

            for (var i = 0; i < Object.keys(data).length; i++) {
                arrayMonth.push(data[i].Month);
                arrayCounterOfVisitorsPlusPercentage.push(data[i].CounterOfVisitors);
            }

            new Chart(VisitorsPerMonthChart, {
                type: 'bar',
                data: {
                    labels: arrayMonth,
                    datasets: [{
                        label: 'Visitors per month',
                        data: arrayCounterOfVisitorsPlusPercentage,
                        borderWidth: 1
                    }]
                },
                options: {
                    scales: {
                        y: {
                            beginAtZero: true
                        }
                    }
                }
            });
        }
    };
    //Open connection
    xmlHttpRequest.open("GET", "/api/BasicCore/VisitorCounter/1/SelectAllToVisitorsPerMonthChart", true);
    //Send request
    xmlHttpRequest.send(formData);

});