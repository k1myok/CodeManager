//昆山的Echar都在这里配置

//道路等级option
function KSCharsDLDJ() {
    var KSBaseBarChart = new BaseBarChart();
    //重写Y轴
    KSBaseBarChart.option.yAxis = [
            {
                type: 'value',
                boundaryGap: [0, 0.01],
                axisLabel: {
                    formatter: function (value) {
                        //千分位表示
                        return ((value/1000).toFixed(0) + '').replace(/(?=(?!\b)(\d{3})+$)/g, ',') + '公里'
                    }, textStyle: { color: 'white' },
                }
            }
    ];
    //重写grid
    KSBaseBarChart.option.grid = {
        x: 60,
        y: 40,
        x2: 40,
        y2: 40,
    };
    //被调用方法
    this.getOption = function () {
        return KSBaseBarChart.option;
    };
}


//安置房数量Option
function AZFSLBarChart() {
    var AZFSLBarChart = new BaseBarChart();
    //重写Y轴
    AZFSLBarChart.option.yAxis = [
            {
                type: 'value',
                boundaryGap: [0, 0.01],
                axisLabel: {
                    formatter: function (value) {
                        //千分位表示
                        return (value.toFixed(0) + '').replace(/(?=(?!\b)(\d{3})+$)/g, ',') + '个'
                    }, textStyle: { color: 'white' },
                }

            }
    ];
    //重写grid
    AZFSLBarChart.option.grid = {
        x: 40,
        y: 40,
        x2: 40,
        y2: 40,
    };
    AZFSLBarChart.option.dataZoom = {
        show: true,
        realtime: true,
        //orient: 'vertical',   // 'horizontal'
        //x: 0,
        y: 36,
        //width: 400,
        height: 20,
        backgroundColor: 'rgba(221,160,221,0.5)',
        //dataBackgroundColor: 'rgba(138,43,226,0.5)',
        //fillerColor: 'rgba(38,143,26,0.6)',
        //handleColor: 'rgba(128,43,16,0.8)',
        //xAxisIndex:[],
        //yAxisIndex:[],
        start: 40,
        end: 60
    }
    //被调用方法
    this.getOption = function () {
        return AZFSLBarChart.option;
    };
}


//安置房面积Option
function AZMJBarChart() {
    var AZMJBarChart = new BaseBarChart();
    //重写Y轴
    AZMJBarChart.option.yAxis = [
            {
                type: 'value',
                boundaryGap: [0, 0.01],
                axisLabel: {
                    formatter: function (value) {
                        //千分位表示
                        return (value.toFixed(0) + '').replace(/(?=(?!\b)(\d{3})+$)/g, ',') + '个'
                    }, textStyle: { color: 'white' },
                }

            }
    ];
    //重写grid
    AZMJBarChart.option.grid = {
        x: 80,
        y: 40,
        x2: 40,
        y2: 40,
    };
    //被调用方法
    this.getOption = function () {
        return AZMJBarChart.option;
    };
}

//安置套数
function AZTSBarChart() {
    var AZTSBarChart = new BaseBarChart();
    //重写Y轴
    AZTSBarChart.option.yAxis = [
            {
                type: 'value',
                boundaryGap: [0, 0.01],
                axisLabel: {
                    formatter: function (value) {
                        //千分位表示
                        return (value.toFixed(0) + '').replace(/(?=(?!\b)(\d{3})+$)/g, ',') + '套'
                    },
                    textStyle: { color: 'white' },
                }

            }
    ];
    //重写grid
    AZTSBarChart.option.grid = {
        x: 80,
        y: 40,
        x2: 40,
        y2: 40,
    };
    //被调用方法
    this.getOption = function () {
        return AZTSBarChart.option;
    };
}

//建筑用地Option
function JZYDBarChart() {
    var JZYDBarChart = new BaseBarChart();
    //重写Y轴
    JZYDBarChart.option.yAxis = [
            {
                type: 'value',
                boundaryGap: [0, 0.01],
                axisLabel: {
                    formatter: function (value) {
                        //千分位表示
                        return ((value/1000000).toFixed(0) + '').replace(/(?=(?!\b)(\d{3})+$)/g, ',') + '公顷'
                    }, textStyle: { color: 'white' },    
                },
            }
    ];
    //重写grid
    JZYDBarChart.option.grid = {
        x: 60,
        y: 40,
        x2: 40,
        y2: 40,
    };
    JZYDBarChart.option.series = [{ name: '', type: 'bar', itemStyle: { normal: { color: "#E87C25" } }, smooth: true, data: [] }];
    //被调用方法
    this.getOption = function () {
        return JZYDBarChart.option;
    };
}

//商业用地option
function SYYDBarChart() {
    var SYYDBarChart = new BaseBarChart();
    //重写Y轴
    SYYDBarChart.option.yAxis = [
            {
                type: 'value',
                boundaryGap: [0, 0.01],
                axisLabel: {
                    formatter: function (value) {
                        //千分位表示
                        return ((value / 1000000).toFixed(0) + '').replace(/(?=(?!\b)(\d{3})+$)/g, ',') + '公顷'
                    }, textStyle: { color: 'white' },
                }

            }
    ];
    SYYDBarChart.option.series = [{ name: '', type: 'bar', itemStyle: { normal: { color: "#27727B" } }, smooth: true, data: [] }];

    //重写grid
    SYYDBarChart.option.grid = {
        x: 60,
        y: 40,
        x2: 40,
        y2: 40,
    };
    //被调用方法
    this.getOption = function () {
        return SYYDBarChart.option;
    };
}

//行政办公option
function ZXBGBarChart() {
    var AZMJBarChart = new BaseBarChart();
    //重写Y轴
    AZMJBarChart.option.yAxis = [
            {
                type: 'value',
                boundaryGap: [0, 0.01],
                axisLabel: {
                    formatter: function (value) {
                        //千分位表示
                        return ((value / 1000000).toFixed(1) + '').replace(/(?=(?!\b)(\d{3})+$)/g, ',') + '公顷'
                    }, textStyle: { color: 'white' },
                }

            }
    ];
    //重写grid
    AZMJBarChart.option.grid = {
        x: 60,
        y: 40,
        x2: 40,
        y2: 40,
    };
    //被调用方法
    this.getOption = function () {
        return AZMJBarChart.option;
    };
}
//公共服务ption
function GGFWBarChart() {
    var GGFWBarChart = new BaseBarChart();
    //重写Y轴
    GGFWBarChart.option.yAxis = [
            {
                type: 'value',
                boundaryGap: [0, 0.01],
                axisLabel: {
                    formatter: function (value) {
                        //千分位表示
                        return ((value / 1000000).toFixed(1) + '').replace(/(?=(?!\b)(\d{3})+$)/g, ',') + '公顷'
                    }, textStyle: { color: 'white' },
                }

            }
    ];
    GGFWBarChart.option.series = [{ name: '', type: 'bar', itemStyle: { normal: { color: "#FCCE10" } }, smooth: true, data: [] }];
    //重写grid
    GGFWBarChart.option.grid = {
        x: 60,
        y: 40,
        x2: 40,
        y2: 40,
    };
    //被调用方法
    this.getOption = function () {
        return GGFWBarChart.option;
    };
}

function GXBarChart() {
    var GXBarChart = new BaseBarChart();
    //重写Y轴
    GXBarChart.option.yAxis = [
            {
                type: 'value',
                boundaryGap: [0, 0.01],
                axisLabel: {
                    formatter: function (value) {
                        //千分位表示
                        return ((value / 1000000).toFixed(0) + '').replace(/(?=(?!\b)(\d{3})+$)/g, ',') + '公顷'
                    }, textStyle: { color: 'white' },
                }

            }
    ];
    GXBarChart.option.series = [{ name: '', type: 'bar', itemStyle: { normal: { color: "#FCCE10" } }, smooth: true, data: [] }];
    //重写grid
    GXBarChart.option.grid = {
        x: 60,
        y: 40,
        x2: 40,
        y2: 40,
    };
    GXBarChart.option.series = [
            {
                name: '',
                type: 'bar',
                itemStyle: {
                    normal: {
                        color: function (params) {
                            var colorList = ['#C1232B', '#B5C334', '#FCCE10', '#E87C25', '#27727B',
                                '#FE8463', '#9BCA63', '#FAD860', '#F3A43B', '#60C0DD',
                                '#D7504B', '#C6E579', '#F4E001', '#F0805A', '#26C0C0', '#C1232B', '#B5C334', '#FCCE10', '#E87C25', '#27727B',
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
    //被调用方法
    this.getOption = function () {
        return GXBarChart.option;
    };
}

//道路投资额
function DLTZEBarChart() {
    var DLJEBarChart = new BaseBarChart();
    //重写Y轴
    DLJEBarChart.option.yAxis = [
            {
                type: 'value',
                boundaryGap: [0, 0.01],
                axisLabel: {
                    formatter: function (value) {
                        //千分位表示
                        return ((value / 10000).toFixed(0) + '').replace(/(?=(?!\b)(\d{3})+$)/g, ',') + '亿元'
                    }, textStyle: { color: 'white' },
                }

            }
    ];
    //重写grid
    DLJEBarChart.option.grid = {
        x: 60,
        y: 40,
        x2: 40,
        y2: 40,
    };
    DLJEBarChart.option.series = [{ name: '', type: 'bar', itemStyle: { normal: { color: "#27727B" } }, smooth: true, data: [] }];
    //被调用方法
    this.getOption = function () {
        return DLJEBarChart.option;
    };
}

//MGLDLCBBarChart
//道路投资额
function DLCBBarChart() {
    var DLCBBarChart = new BaseBarChart();
    //重写Y轴
    DLCBBarChart.option.yAxis = [
            {
                type: 'value',
                boundaryGap: [0, 0.01],
                axisLabel: {
                    formatter: function (value) {
                        //千分位表示
                        return ((value).toFixed(0) + '').replace(/(?=(?!\b)(\d{3})+$)/g, ',') + '万元'
                    }, textStyle: { color: 'white' },
                }

            }
    ];
    //重写grid
    DLCBBarChart.option.grid = {
        x: 80,
        y: 40,
        x2: 40,
        y2: 40,
    };
    DLCBBarChart.option.series = [{ name: '', type: 'bar', itemStyle: { normal: { color: "#27727B" } }, smooth: true, data: [] }];
    //被调用方法
    this.getOption = function () {
        return DLCBBarChart.option;
    };
}


function XMZJBarChart() {
    var XMZJBarChart = new BaseBarChart();
    //重写Y轴
    XMZJBarChart.option.yAxis = [
            {
                type: 'value',
                boundaryGap: [0, 0.01],
                axisLabel: {
                    formatter: function (value) {
                        //千分位表示
                        return ((value).toFixed(0) + '').replace(/(?=(?!\b)(\d{3})+$)/g, ',') + '万元'
                    }, textStyle: { color: 'white' },
                }

            }
    ];
    //重写grid
    XMZJBarChart.option.grid = {
        x: 80,
        y: 40,
        x2: 40,
        y2: 40,
    };
    XMZJBarChart.option.series = [{ name: '', type: 'bar', itemStyle: { normal: { color: "#27727B" } }, smooth: true, data: [] }];
    //被调用方法
    this.getOption = function () {
        return XMZJBarChart.option;
    };
}