function KSStackBarChart() {
    this.getOption = function () {
        return this.option;
    };

    this.option = {
        grid: {
            x: 80,
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
        dataZoom: {
            show: true,
            realtime: true,
            //orient: 'vertical',   // 'horizontal'
            //x: 0,
            y: 36,
            //width: 400,
            height: 20,
            //backgroundColor: 'rgba(221,160,221,0.5)',
            //dataBackgroundColor: 'rgba(138,43,226,0.5)',
            //fillerColor: 'rgba(38,143,26,0.6)',
            //handleColor: 'rgba(128,43,16,0.8)',
            //xAxisIndex:[],
            //yAxisIndex:[],
            start: 40,
            end: 60
        },
        calculable: false,
        yAxis: [
            {
                type: 'value',
                boundaryGap: [0, 0.01],
                axisLabel: {
                    show: true,
                    formatter: function (value) {
                        //千分位表示
                        return (value.toFixed(0) + '').replace(/(?=(?!\b)(\d{3})+$)/g, ',') + '万M²'
                    },
                    textStyle: {
                        color: 'white',
                    }
                },
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
                stack: '',
                type: 'bar',
                smooth: true,
                data: [],
            }
        ]
    };
};

function GCSLStackBarChart() {
    this.getOption = function () {
        return this.option;
    };

    this.option = {
        grid: {
            x: 80,
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
        calculable: false,
        yAxis: [
            {
                type: 'value',
                boundaryGap: [0, 0.01],
                axisLabel: {
                    show: true,
                    formatter: function (value) {
                        //千分位表示
                        return (value.toFixed(0) + '').replace(/(?=(?!\b)(\d{3})+$)/g, ',') + '万M²'
                    },
                    textStyle: {
                        color: 'white',
                    }
                },
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
                stack: '',
                type: 'bar',
                smooth: true,
                data: [],
            }
        ]
    };
};

