define('vm.stats', ['ko', 'jquery', 'ajaxhelper'], function(ko, $, ajaxhelper) {
    var hello = ko.observable(),
        active = ko.observable(false),
        searchstats = ko.observableArray(),
        name = "Stats",
        init = function() {
            ajaxhelper.ajaxRequest('api/searchstats')
                .done(function(data) {
                    searchstats(data);
                    startChart();
                });
            
        },
        startChart = function() {
            var chart;
            chart = new Highcharts.Chart({
                    chart: {
                        renderTo: 'chartcontainer',
                        type: 'bar'
                    },
                    title: {
                        text: 'Search statistics'
                    },
                    subtitle: {
                        text: 'Search count by term'
                    },
                    xAxis: {
                        categories: _.map(searchstats(), function (term) {
                            return term.term;
                        }),
                        title: {
                            text: null
                        }
                    },
                    yAxis: {
                        min: 0,
                        title: {
                            text: 'Count',
                            align: 'high'
                        },
                        labels: {
                            overflow: 'justify'
                        }
                    },
                    tooltip: {
                        formatter: function () {
                            return '' +
                                this.series.name + ': ' + this.y + ' times';
                        }
                    },
                    plotOptions: {
                        bar: {
                            dataLabels: {
                                enabled: true
                            }
                        }
                    },
                    legend: {
                        layout: 'vertical',
                        align: 'right',
                        verticalAlign: 'top',
                        x: -100,
                        y: 100,
                        floating: true,
                        borderWidth: 1,
                        backgroundColor: '#FFFFFF',
                        shadow: true
                    },
                    credits: {
                        enabled: false
                    },
                    series: [{
                        name: 'Count',
                        data: _.map(searchstats(), function (term) {
                            return term.count;
                        })
                    }]
            });
        };

    active.subscribe(function(newValue) {
        newValue && init();
    });
    
    return {
        hello: hello,
        init: init,
        active: active,
        name : name
    };
});