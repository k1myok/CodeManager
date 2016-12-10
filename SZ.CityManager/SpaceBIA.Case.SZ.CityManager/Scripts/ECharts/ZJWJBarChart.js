function ZJWJBarChart() {
    this.getOption = function () {
        return this.option;
    };

    this.option = {
        grid: {
            x: 60,
            y: 40,
            x2: 40,
            y2: 40,
        },
        tooltip: {
            trigger: 'axis'
        },
        legend: {
            data: [],
            textStyle: {
                color: 'white'
            }
        },
        toolbox: {
            show: false,
            feature: {
                mark: { show: true },
                dataView: { show: true, readOnly: false },
                magicType: { show: true, type: ['line', 'bar'] },
                restore: { show: true },
                saveAsImage: { show: true }
            }
        },
        calculable: true,
        yAxis: [
            {
                type: 'value',
                boundaryGap: [0, 0.01],
                axisLabel: {
                    formatter: function (value) {
                        //千分位表示
                        return ((value /100000).toFixed(0) + '').replace(/(?=(?!\b)(\d{3})+$)/g, ',') + '公顷'
                    }, textStyle: { color: 'white' },
                }
            }
        ],
        xAxis: [
            {
                type: 'category',
                data: [],
                axisLabel: { textStyle: { color: 'white' } }
            }
        ],
        series: [
            {
                name: '',
                type: 'bar',
                smooth: true,
                data: []
            }
        ]
    };
};
