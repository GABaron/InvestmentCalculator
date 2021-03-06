$().ready(function () {
    var self = this;

    self.getParameters = function () {
        var request = "LumpSumInvestment=" + $("#lumpSumInvestment").val() + "&MonthlyInvestment="
            + $("#monthlyInvestment").val() + "&TargetValue=" + $("#targetValue").val()
            + "&TimeScale=" + $("#timeScale").val() + "&RiskLevel=" + $("#riskLevel").val();

        return request;
    };

    self.getProjectionFigures = function () {
        $.ajax({
            type: "GET",
            url: "api/Investments/GetInvestmentProjection",
            data: self.getParameters(),
            contentType: "application/json",
            success: function (response) {
                self.drawChart(response);
            },
            error: function (response) {
                alert("Unable to draw chart due to a server error.");
            }
        });
    };

    self.createDataTable = function (data) {
        var dataTable = [['Year', 'Value']];

        $.each(data, function (fieldName, fieldProperties) {
            dataTable.push([fieldProperties.year, fieldProperties.balance]);
        });

        return dataTable;
    };

    self.drawChart = function (data) {
        var dataTable = self.createDataTable(data);
        var data = google.visualization.arrayToDataTable(dataTable);
        //var currentYear = new Date().getFullYear();
        ////var maxDateRange 

        var options = {
            title: 'Return on Investment Over Time',
            curveType: 'function',
            legend: { position: 'bottom' },
            vAxis: {                
                minValue: 1000,
            }//,
            //hAxis: {
            //    minValue: ,
            //}
        };

        var chart = new google.visualization.LineChart(document.getElementById('curve_chart'));

        chart.draw(data, options);
    }    

    self.initialise = function () {
        $("#drawButton").click(function () {
            var form = $('#investmentForm');
            $.validator.unobtrusive.parse(form);
                       
            if (form.validate().form()) {
               self.getProjectionFigures();
            }
        });
    };

    google.charts.load('current', { 'packages': ['corechart'] });
    google.charts.setOnLoadCallback(self.initialise);
});