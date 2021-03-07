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

    self.getEndYear = function () {
        var timeScale = parseInt($("#timeScale").val());
        var yearInvestmentEnds = new Date().getFullYear() + timeScale;       
        return new Date(yearInvestmentEnds, 0)    
    };

    self.createDataTable = function (data) {
        var dataTable = [];
        var yearInvestmentEnds = self.getEndYear();
        var targetValue = parseInt($("#targetValue").val());

        dataTable.push([yearInvestmentEnds, null, null, targetValue]);

        $.each(data, function (fieldName, fieldProperties) {                 
            dataTable.push([new Date(fieldProperties.year, 0), fieldProperties.balance, fieldProperties.totalAmountInvested, null]);
        });
       
        return dataTable;
    };    

    self.drawChart = function (data) {
        var dataTable = self.createDataTable(data);       
        var data = new google.visualization.DataTable();
        data.addColumn("date", "Year");
        data.addColumn("number", "Value (£)");            
        data.addColumn("number", "Amount Invested (£)");       
        data.addColumn("number", "Target Value (£)");
        data.addRows(dataTable);       
       
        var options = {
            title: "Return on Investment Over Time",
            curveType: "function",
            legend: { position: 'bottom' },
            vAxis: { 
                format: "currency",
                title: "Value (£)",
                minValue: $("#targetValue").val(),
            },
            hAxis: {
                title: "Years",
                format: "yyyy",
                ticks: data.getDistinctValues(0)
            },
            pointSize: 5
        };

        var chart = new google.visualization.LineChart(document.getElementById("curve_chart"));

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

    google.charts.load("current", { "packages": ["corechart"], "language": "en-GB" });
    google.charts.setOnLoadCallback(self.initialise);
});