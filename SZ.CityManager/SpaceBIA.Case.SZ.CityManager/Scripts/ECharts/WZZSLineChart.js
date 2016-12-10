function WZZSLineChart() {
    this.getOption = function () {
        return this.option;
    };
    
    this.option = {
        grid: {
            x: 80,
            y: 40,
            x2: 10,
            y2: 30,
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
                type: 'value',
                boundaryGap: [0, 0.01],
                axisLabel: { textStyle: { color: 'white' }, formatter: '{value} 次' }
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
                type: 'line',
                smooth: true,
                data: [],
                markPoint: {
                    data: [
                    { name: '2012年违章数量', value: 38, xAxis: 0, yAxis: 38 },
                    { name: '2013年违章数量', value: 148, xAxis: 1, yAxis: 148 },
                    { name: '2014年违章数量', value: 1437, xAxis: 2, yAxis:1437 },
                    ]
                },
            }
        ]
    };
};

