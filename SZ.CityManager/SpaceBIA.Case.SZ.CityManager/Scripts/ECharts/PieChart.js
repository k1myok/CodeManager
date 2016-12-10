
function PieChart() {
    this.getOption = function () {
        return this.option;
    };

    this.option = {
        title: {
            //text: '饼状图',
            subtext: '',
            x: 'center'
        },
        tooltip: {
            trigger: 'item',
            backgroundColor: 'rgba(255,255,0,0.4)',
            textStyle: { color: 'blue' },
            formatter: "{b} : {c} <br/>({d}%)"
        },
        legend: {
            show: false,
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
                    show: true,
                    type: ['pie', 'funnel'],
                    option: {
                        funnel: {
                            x: '25%',
                            width: '50%',
                            funnelAlign: 'left',
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
                radius: ['0%', '50%'],
                selectedMode: 'single',
                selectedOffset: 10,
                center: ['50%', '60%'],
                data: [],
                itemStyle: {
                    normal: {
                        label: {
                            show: true,
                            formatter: '{b} 企业：{c} '
                        },
                        labelLine: { show: true }
                    }
                }
            }
        ]
    };
};
 //企业饼状图
function CorPieChart() {
    this.getOption = function () {
        return this.option;
    };

    this.option = {
        title: {
            //text: '饼状图',
            subtext: '',
            x: 'center'
        },
        tooltip: {
            trigger: 'item',
            backgroundColor: 'rgba(255,255,0,0.4)',
            textStyle: { color: 'blue' },
            formatter: "{b} 企业数量: {c} 家<br/>({d}%)"
        },
        legend: {
            orient: 'vertical',
            x: 'left',
            data: [],
            textStyle: { color: 'white' }
        },
        toolbox: {
            show: false,
            feature: {
                mark: { show: true },
                dataView: { show: true, readOnly: false },
                magicType: {
                    show: true,
                    type: ['pie', 'funnel'],
                    option: {
                        funnel: {
                            x: '25%',
                            width: '50%',
                            funnelAlign: 'left',
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
                radius: ['0%', '50%'],
                selectedMode: 'single',
                selectedOffset: 10,
                center: ['50%', '60%'],
                data: [],
                itemStyle: {
                    normal: {
                        label: {
                            show: false
                        },
                        labelLine: {
                            show: false
                        }
                    },
                    emphasis: {
                        label: {
                            show: true,
                            formatter: '{b} 企业：{c}家 ',
                            position: 'outer'
                        },
                        labelLine: {
                            show: true,
                            lineStyle: {
                                color: 'red'
                            }
                        }
                    }
                }
            }
        ]
    };
};
function KSPieChart() {
    this.getOption = function () {
        return this.option;
    };

    this.option = {
        title: {
            //text: '饼状图',
            subtext: '',
            x: 'center'
        },
        tooltip: {
            trigger: 'item',
            backgroundColor: 'rgba(255,255,0,0.4)',
            textStyle: { color: 'blue' },
            formatter: "{b} : {c} <br/>({d}%)"
        },
        legend: {
            show: false,
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
                    show: true,
                    type: ['pie', 'funnel'],
                    option: {
                        funnel: {
                            x: '25%',
                            width: '50%',
                            funnelAlign: 'left',
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
                radius: ['0%', '50%'],
                selectedMode: 'single',
                selectedOffset: 10,
                center: ['50%', '60%'],
                data: [],
                itemStyle: {
                    normal: {
                        label: {
                            show: true,
                            formatter: '{b}：{c} '
                        },
                        labelLine: { show: true }
                    }
                }
                
            }
        ]
    };
};
//颜色修改
function KSPieChart_2() {
    this.getOption = function () {
        return this.option;
    };

    this.option = {
        title: {
            //text: '饼状图',
            subtext: '',
            x: 'center'
        },
        tooltip: {
            trigger: 'item',
            backgroundColor: 'rgba(255,255,0,0.4)',
            textStyle: { color: 'blue' },
            formatter: "{b} : {c} <br/>({d}%)"
        },
        color: ['#679CA2', '#FEA891'],
        legend: {
            show: false,
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
                    show: true,
                    type: ['pie', 'funnel'],
                    option: {
                        funnel: {
                            x: '25%',
                            width: '50%',
                            funnelAlign: 'left',
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
                radius: ['0%', '50%'],
                selectedMode: 'single',
                selectedOffset: 10,
                center: ['50%', '60%'],
                data: [],
                itemStyle: {
                    normal: {
                        label: {
                            show: true,
                            formatter: '{b}：{c} '
                        },
                        labelLine: { show: true }
                    }
                }

            }
        ]
    };
};