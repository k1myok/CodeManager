
function KSGaugeChart() {
    this.getOption = function () {
        return this.option;
    };

    this.option = {
        tooltip: {
            formatter: "{a} <br/>{b} : {c}%"
        },
        toolbox: {
            show: false,
            feature: {
                mark: { show: true },
                restore: { show: true },
                saveAsImage: { show: true }
            }
        },
        series: [
            {
                name: '',
                type: 'gauge',
                center: ['50%', '50%'],
                detail: { formatter: '{value}' },
                data: []
            }
        ]
    };
};