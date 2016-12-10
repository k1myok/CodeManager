function DLJGBarChart() {
    this.getOption = function () {
        return this.option;
    };

    this.option = {
        grid: {
            x: 100,
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
                axisLabel: { formatter: function (value) { console.log(value); return value / 1000 + ',' + (value - (value / 1000) * 1000) + '00' + '万元' }, textStyle: { color: 'white' } }
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
                itemStyle: {
                    normal: {
                        color: function (params) {
                            var colorList = ['#C1232B', '#B5C334'
                            ];
                            if (params.dataIndex % 2 == 0) {
                                return colorList[0]
                            }
                            else {
                                return colorList[1]
                            }

                            
                            
                        }
                    }
                },
                smooth: true,
                data: []
            }
        ]
    };
};
