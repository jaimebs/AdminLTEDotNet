app.controller("HomeController", function($scope, $http) {
    $http.get("/Boleto/GetBoleto").success(function (dados) {
        charts(dados, "ColumnChart"); // Coluna do grafico
        charts(dados, "PieChart"); // grafico de torta
        charts(dados, "BarChart"); // grafico de barras
        charts(dados, "GeoChart"); // Geo Charts
    });

    function charts(data, ChartType) {
        var c = ChartType;
        var jsonData = data;
        google.load("visualization", "1", { packages: ["corechart"], callback: drawVisualization });
        function drawVisualization() {
            var data = new google.visualization.DataTable();
            data.addColumn('string', 'Usuario');
            data.addColumn('number', 'Carneiros');
            $.each(jsonData, function (i, jsonData) {
                var value = jsonData.Valor;
                var name = jsonData.NomeSacado;
                data.addRows([[name, value]]);
            });

            var options = {
                //title: "Quantidade de carneiros comprados",
                animation: {
                    duration: 1000,
                    easing: 'out',
                    startup: true
                },
                colorAxis: { colors: ['#54C492', '#cc0000'] },
                datalessRegionColor: '#dedede',
                defaultColor: '#dedede'
            };

            var chart;
            if (c === "ColumnChart") // grafico de colunas
                chart = new google.visualization.ColumnChart(document.getElementById('chart_div'));
            else if (c === "PieChart") // grafico em torta
                chart = new google.visualization.PieChart(document.getElementById('piechart_div'));
            else if (c === "BarChart") // grafico em barras
                chart = new google.visualization.BarChart(document.getElementById('bar_div'));
            else if (c === "GeoChart") // Geo Charts
                chart = new google.visualization.GeoChart(document.getElementById('regions_div'));

            chart.draw(data, options);
        }
    }
});