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
        var dataTable = [];

        $.each(data, function (fieldName, fieldProperties) {
            dataTable.push([new Date(fieldProperties.year, 0), fieldProperties.balance]);
        });

        return dataTable;
    };    

    self.drawChart = function (data) {
        var dataTable = self.createDataTable(data);       
        var data = new google.visualization.DataTable();
        data.addColumn('date', 'Year');
        data.addColumn('number', 'Value (£)');    
        data.addRows(dataTable)      
       
        var options = {
            title: 'Return on Investment Over Time',
            curveType: 'function',
            legend: { position: 'bottom' },
            vAxis: {                
                minValue: $("#targetValue").val(),
            },
            hAxis: {
                format: 'yyyy',
                ticks: data.getDistinctValues(0)
            }
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