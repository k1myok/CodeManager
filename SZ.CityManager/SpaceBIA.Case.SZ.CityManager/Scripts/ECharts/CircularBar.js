function CircularBar() {
    this.getOption = function () {
        return this.option;
    };
    this.option = {
        tooltip: {
            trigger: 'item',
            formatter: "{a} <br/>{b} : {c}家({d}%)"
        },
        legend: {
            show:false,
            orient: 'vertical',
            x: 'left',
            data: []
        },
        toolbox: {
            show: false,
            feature: {
                mark: { show: true },
                dataView: { show: true, readOnly: false },
                magicType: {
                    show: false,
                    type: ['pie', 'funnel'],
                    option: {
                        funnel: {
                            x: '25%',
                            width: '50%',
                            funnelAlign: 'center',
                            max: 1548
                        }
                    }
                },
                restore: { show: true },
                saveAsImage: { show: true }
            }
        },
        calculable: false,
        series: [
            {
                name: '',
                type: 'pie',
                radius: ['50%', '70%'],
                itemStyle: {
                    normal: {
                        label: {
                            show: true
                        },
                        labelLine: {
                            show: true
                        }
                    },
                    emphasis: {
                        label: {
                            show: true,
                            position: 'center',
                            textStyle: {
                                fontSize: '30',
                                fontWeight: 'bold'
                            }
                        }
                    }
                },
                data: []
            }
        ]
    };
}