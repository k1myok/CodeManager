/*
 * DensityMapOption 密度图option
 * 可以多series，模板定义了默认的series，seriesName默认为空，
 * 当需要将带有样式(自定义样式)的series配置到option时，seriesName不要为空，
   且数据源有相应的类别(Model中的Category)指定该seriesName，这样才能正确匹配，否则会出问题。
 *
 *
 */
function QYLHLMap() {
    //获取option
    this.getOption = function () {
        return this.option;
    };
    this.option = {
        //背景色
        backgroundColor: '#1b1b1b',
        //颜色数组，不填使用默认颜色数组，详见echarts配置
        color: [
            'rgba(255, 255, 255, 0.8)',
            'rgba(14, 241, 242, 0.8)',
            'rgba(37, 140, 249, 0.8)'
        ],
        //标题
        title: {
            show: false,
            text: '人口数据点亮苏州',
            x: 'right',
            y: '10',
            textStyle: {
                color: '#fff'
            }
        },
        //图例，一般需要图例且要selected节点，否则可能报错
        legend: {
            orient: 'vertical',
            x: 'right',
            y: '40%',
            data: [],
            selected: {},
            textStyle: {
                color: '#fff'
            }
        },
        //核心：数据series 为数组，每个数组元素为对象，配置了不同系列的名称、类型、样式、数据等
        series: [
            {
                name: '',           //series名称，不填则为模板样式
                type: 'map',        //series的样式'line'（折线图） | 'bar'（柱状图） | 'scatter'（散点图） | 'k'（K线图） 
                //'pie'（饼图） | 'radar'（雷达图） | 'chord'（和弦图） | 'force'（力导向布局图） | 'map'（地图）
                mapType: 'none',    //地图类型：使用esri地图直接设置'none'，echarts自带地图设置如'world|Brazil'(世界，巴西)，'china|广东'(中国，广东)
                hoverable: false,   //默认false 关闭区域悬浮
                roam: false,        //false 对于echarts地图一定要设置成false，不然会出错
                //样式配置
                itemStyle: {
                    normal: {
                        borderColor: 'rgba(100,149,237,1)',
                        borderWidth: 1.5,
                        areaStyle: {
                            color: '#1b1b1b'
                        }
                    }
                },
                data: [],       //需要，不然会报错，但不要填值
                markPoint: {
                    symbol: 'circle',
                    symbolSize: 1.5,
                    large: true,
                    effect: {
                        show: true,
                        period: 40
                    },
                    data: []    //用于填充具体值进行显示
                }
            },
            {
                name: '',
                type: 'map',
                mapType: 'none',
                hoverable: false,
                roam: false,
                data: [],
                markPoint: {
                    symbol: 'circle',
                    symbolSize: 2,
                    large: true,
                    effect: {
                        show: true,
                        period: 30
                    },
                    data: []
                }
            },
            {
                name: '',
                type: 'map',
                mapType: 'none',
                hoverable: false,
                roam: false,
                data: [],
                markPoint: {
                    symbol: 'diamond',
                    symbolSize: 2,
                    large: true,
                    effect: {
                        show: true,
                        period: 20
                    },
                    data: []
                }
            }
        ],
        mapConfig: {
            option: {
                extent: 'new Extent(13456123.171,3665550.074,13482581.558,3696241.803, new SpatialReference({ wkid: 102100 }))',
                logo: false,
                slider: false,
            },
            layers: [{
                id: 'baseMap',
                type: 'Tiled',
                url: KSConfig.baseMap
            },
            {
                id: 'KS',
                type: 'Dynamic',
                url: 'http://172.16.9.100:6080/arcgis/rest/services/KS/QYLHL1/MapServer'
            },
            ]
        }
    };
};
