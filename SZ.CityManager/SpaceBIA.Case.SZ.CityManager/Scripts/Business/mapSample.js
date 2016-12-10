function EMapFactory(mapOption) {
    //base property
    //this.emcContent = null;
    //this.map = null;
    //this.overlayer = null;
    //this.mapOption = _mapOption;

    //base method
    //this.initMap
    //this.initDefaultMapConfig
    //this.mapInitCompleted

    BaseEMapFactory.apply(this, arguments);
    //this.mapInitCompleted = function () {

    //};
    //this.initMap = function (mapDiv) {

    //}
}
EMapFactory.prototype = Object.create(BaseEMapFactory.prototype);
EMapFactory.prototype.constructor = EMapFactory;

function WMapFactory(mapOption) {
    //base property
    //this.emcContent = null;
    //this.map = null;
    //this.overlayer = null;
    //this.mapOption = _mapOption;

    //base method
    //this.initMap
    //this.initDefaultMapConfig
    //this.mapInitCompleted
    BaseWMapFactory.apply(this, arguments);

}
WMapFactory.prototype = Object.create(BaseWMapFactory.prototype);
WMapFactory.prototype.constructor = WMapFactory;

function CMapFactory(mapOption) {
    BaseCMapFactory.apply(this, arguments);
}
CMapFactory.prototype = Object.create(BaseCMapFactory.prototype);
CMapFactory.prototype.constructor = CMapFactory;

/*
 * @function loadECLayerByConfigKey 通过keyName获取数据源加载ECharts图层
 * @author JasonYoung
 * @create date 2016/10/19
 * @param domId echarts初始化所需的dom节点，将echarts初始化在该节点上
 * @param optionFunctionName option函数名支持function.option格式，如‘ChartsOption.BarRainbow’
 * @param chartCategory 图表类型，枚举值(Chart,Map)
 * @param chartType ECharts图表类型，通过类型的不同初始化不同类型图表 chartType详细参数如下
 * *******|Bar(柱状图)|StackBar(堆积柱状图)|Pie(饼状图)|Line(折线图)|Area(面积图)|
 * *******|Scatter(散点图)|Radar(雷达图)|K(K线图)|Chord(和弦图)|Force(力导向图)|Gauge(仪表盘)|Funnel(漏斗图)|EventRiver(事件河流图)|Venn(韦恩图)|TreeMap(矩形树图)|Tree(树图)|WordCloud(字符云)|
 * *******|Timeline_Bar(时间轴结合柱状图)|Timeline_Pie(时间轴结合饼状图)|Timeline_Line(时间轴结合折线图)|Timeline_Area(时间轴结合面积图)|Timeline_Scatter(时间轴结合散点图)|
 * *******|Density(密度图)|HeatMap(热度图)|MarkPoint(点图)|MarkLine(线图)|Miogration(迁徙图)|
 * *******|Timeline_Density(时间轴结合密度图)|Timeline_HeatMap(时间轴结合热度图)|
 * *******|Timeline_MarkPoint(时间轴结合点图)|Timeline_MarkLine(时间轴结合线图)|Timeline_Miogration(时间轴结合迁徙图)|
 * @param keyName 获取数据源的key名称
 * @param themeName echarts主题名称
 * @param initMapExtent 初始化地图范围，类型为Object，支持两种格式，如
 * *******|  格式为：{center:[x,y],zoom:zoomValue} 如：{center:[120.77402598,31.66252874],zoom:5}
 * *******|  格式为：{extent:[XMin, YMin, XMax, YMax],srid:100102} 如：{extent:[13418794, 3697042, 13480925, 3748895],srid:100102}
 * *******|  并支持常规的范围，如:'全国','苏州市','常熟市'……
 */
function loadLayersByConfigKey(domId, optionFunctionName, chartType, keyName, themeName, initMapExtent) {
    //dom节点名称、option函数、图表类型、关键字名称都不为空
    if (!domId || !optionFunctionName || !chartType || !keyName) {
        return;
    }

    var optionArr = optionFunctionName.split('.');
    optionFunctionName = optionArr[0] ? optionArr[0] : '';
    var optionName = optionArr[1] ? optionArr[1] : null;

    var target = new window[optionFunctionName];
    var option = optionName ? target.getOption(optionName) : target.getOption();

    var data = {
        chartType: chartType,
        keyName: keyName
    };
    var readDataSource = function (complete) {
        $.ajax({
            url: '../DataVisSource/CreateDataSourceByConfigKey',
            type: 'get',
            data: data,
            contentType: 'text/json',
            //获取数据源，进行展示
            success: function (dataSource) {
                complete(dataSource);
            },
            error: function (err) {
                console.log(err);
            },
            complete: function () {
            }

        });
    }

    switch (chartType.toLowerCase()) {
        case 'bar':
        case 'stackbar':
        case 'pie':
        case 'line':
        case 'linearea':
        case 'stackline':
        case 'stackarea':
        case 'stacklinearea':
        case 'scatter':
        case 'radar':
        case 'k':
        case 'chord':
        case 'force':
        case 'gauge':
        case 'eventriver':
        case 'venndiagram':
        case 'treemap':
        case 'tree':
        case 'wordcloud':
        case 'timeline_bar':
        case 'timeline_pie':
        case 'timeline_line':
        case 'timeline_area':
        case 'timeline_scatter':
            {
                var factory = new EChartsFactory(themeName);
                readDataSource(function (dataSource) {
                    //通过domId,图表类型,Option,DataSource
                    factory.loadECLayer(domId, chartType, option, dataSource);
                });
                break;
            }
        case 'density':
        case 'heatmap':
        case 'markpoint':
        case 'markline':
        case 'migration':
        case 'timeline_density':
        case 'timeline_heatmap':
        case 'timeline_markpoint':
        case 'timeline_markline':
        case 'timeline_migration':
            {
                var mapFactory = new EMapFactory(option.mapConfig);

                mapFactory.init(domId, function (map, overlay) {
                    var factory = new EChartMapFactory(map, overlay, themeName);

                    //初始化地图基础图层
                    var mapLayers = option.mapConfig ? option.mapConfig.layers : null;
                    if (mapLayers)
                        new mapExtensions(map).addLayersSource(mapLayers, true);
                    else {
                        var defaultLayer = {
                            id: 'PurplishBlue',
                            url: 'http://map.geoq.cn/ArcGIS/rest/services/ChinaOnlineStreetPurplishBlue/MapServer',
                            type: 'Tiled'
                        };
                        new mapExtensions(map).addLayersSource(defaultLayer);
                    }

                    readDataSource(function (dataSource) {
                        factory.loadEMLayer(chartType, option, dataSource);
                    });
                });

                break;
            }
        case 'wmappoint': {
            console.log('loading map:' + domId + ';layerType:' + chartType);

            var mapFactory = new WMapFactory(option.mapConfig);

            mapFactory.init(domId, function (map, mapv) {
                var factory = new WisdomMapFactory(map, mapv);
                //初始化地图基础图层
                var mapLayers = option.mapConfig ? option.mapConfig.layers : null;
                if (mapLayers)
                    new mapExtensions(map).addLayersSource(mapLayers, true);
                else {
                    var defaultLayer = {
                        id: 'PurplishBlue',
                        url: 'http://map.geoq.cn/ArcGIS/rest/services/ChinaOnlineStreetPurplishBlue/MapServer',
                        type: 'Tiled'
                    };
                    new mapExtensions(map).addLayersSource(defaultLayer);
                }

                readDataSource(function (dataSource) {

                    factory.loadWMLayer(chartType, option, dataSource);
                });
            });

            break;
        }
        case 'chartlayer': {

            break;
        }


    }

}




