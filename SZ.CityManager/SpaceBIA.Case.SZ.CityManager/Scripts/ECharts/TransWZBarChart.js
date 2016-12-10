﻿function TransWZBarChart() {
    this.getOption = function () {
        return this.option;
    };

    this.option = {
        grid: {
            x:250,
            y: 40,
            x2: 40,
            y2: 40,
        },
        tooltip: {
            trigger: 'axis'
        },
        legend: {
            show: false,
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
                type: 'category',
                data: [],
                axisLabel: {
                    interval: 0,
                    textStyle: { color: 'white' },
                    //formatter: function (data) {
                    //    return data.split("").join("\n");
                    //}
                 
                }

               
            }
        ],
        xAxis: [
            {
                type: 'value',
                boundaryGap: [0, 0.01],
                axisLabel: { formatter: '{value} 次', textStyle: { color: 'white' } }

            }
        ],
        series: [
            {
                name: '',
                type: 'bar',
                itemStyle: {
                    normal: {
                        color: function (params) {
                            var colorList = ['#C1232B', '#B5C334', '#FCCE10', '#E87C25', '#27727B',
                                '#FE8463', '#9BCA63', '#FAD860', '#F3A43B', '#60C0DD',
                                '#D7504B', '#C6E579', '#F4E001', '#F0805A', '#26C0C0'];


                            return colorList[params.dataIndex]
                        }
                    }
                },
                smooth: true,
                data: []
            }
        ]
    };
};

