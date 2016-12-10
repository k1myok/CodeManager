/*****************************************************************************************************
 *
 * Copyright (C) 2010-2016, YL Wisdom Co.,Ltd.,All Rights Reserved.
 *
 *****************************************************************************************************
 * FileName:WMapOption.js
 * Create Date: 2016-10-25
 * Author：Jason Young
 * E-Mail:genyong.yang@gmail.com
 * Description:主要记录常熟大数据一套图中用到的图表配置信息，详细信息如下：
 * 1、
 *****************************************************************************************************/
function WMapOption() {


    this.options = {


        CommonWMapBubble: {
            mapv: {}, // 对应的mapv实例
            zIndex: 1, // 图层层级
            dataType: 'point', // 数据类型，点类型
            data: [], // 数据
            drawType: 'bubble', // 展示形式
            drawOptions: { // 绘制参数
                // splitList数值表示按数值区间来展示不同大小的圆
                splitList: [
                    {
                        start: 0,
                        end: 20000,
                        size: 3
                    }, {
                        start: 20000,
                        end: 50000,
                        size: 6
                    }, {
                        start: 50000,
                        end: 70000,
                        size: 9
                    }, {
                        start: 70000,
                        size: 12
                    }
                ],
                //globalCompositeOperation: 'lighter', //叠加模式
                strokeStyle: 'rgba(255, 255, 255, 0.7)', // 描边颜色，不设置则不描边
                lineWidth: 0.5, // 描边宽度，不设置则不描边
                fillStyle: "rgba(255, 255, 50, 0.6)" // 填充颜色
            },
            mapConfig: {
                option: {
                    extent: 'new Extent(13349400, 3601339, 13511092, 3769327, new SpatialReference({ wkid: 102100 }))',
                    logo: false,
                    slider: false,
                },
                layers: [{
                    id: 'baseMap',
                    type: 'Tiled',
                    url: 'http://map.geoq.cn/ArcGIS/rest/services/ChinaOnlineStreetPurplishBlue/MapServer'
                }, ]
            }
        },

        CommonWMapDensityHive: {
            mapv: {}, // 对应的mapv实例
            zIndex: 1, // 图层层级
            dataType: 'point', // 数据类型，点类型
            data: [], // 数据
            drawType: 'density', // 展示形式
            drawOptions: { // 绘制参数
                type: "honeycomb", // 网格类型，方形网格或蜂窝形
                size: 30, // 网格大小
                globalAlpha: 0.7,
                //opacity: '0.9',
                unit: 'px', // 单位
                drowZero: false,//是否绘制0值网格true or false
                label: { // 是否显示文字标签
                    show: true,
                    minShowLevel: 10,
                    //maxShowLevel:7
                },
                gradient: { // 显示的颜色渐变范围
                    '0': 'blue',
                    '0.05': 'cyan',
                    '0.15': 'lime',
                    '0.5': 'yellow',
                    '1.0': 'red'
                }
            },
            mapConfig: {
                option: {
                    extent: 'new Extent(13349400, 3601339, 13511092, 3769327, new SpatialReference({ wkid: 102100 }))',
                    logo: false,
                    slider: false,
                },
                layers: [{
                    id: 'baseMap',
                    type: 'Tiled',
                    url: 'http://map.geoq.cn/ArcGIS/rest/services/ChinaOnlineStreetPurplishBlue/MapServer'
                }, ]
            }
        },

        CommonWMapDensityRect: {
            mapv: {}, // 对应的mapv实例
            zIndex: 1, // 图层层级
            dataType: 'point', // 数据类型，点类型
            data: [], // 数据
            drawType: 'density', // 展示形式
            drawOptions: { // 绘制参数
                type: "rect", // 网格类型，方形网格或蜂窝形
                size: 30, // 网格大小
                unit: 'px', // 单位
                globalAlpha: 0.9,
                opacity: '0.7',
                drowZero: false,//是否绘制0值网格true or false
                label: { // 是否显示文字标签
                    show: true,
                    minShowLevel: 10,
                    //maxShowLevel:12
                },
                gradient: { // 显示的颜色渐变范围
                    '0.0': 'blue',
                    '0.05': 'cyan',
                    '0.15': 'lime',
                    '0.3': 'yellow',
                    '1.0': 'red'
                }
            },
            mapConfig: {
                option: {
                    extent: 'new Extent(13349400, 3601339, 13511092, 3769327, new SpatialReference({ wkid: 102100 }))',
                    logo: false,
                    slider: false,
                },
                layers: [{
                    id: 'baseMap',
                    type: 'Tiled',
                    url: 'http://map.geoq.cn/ArcGIS/rest/services/ChinaOnlineStreetPurplishBlue/MapServer'
                }, ]
            }
        },
        CommonWMapHeatMap: {
            mapv: {}, // 对应的mapv实例
            zIndex: 1, // 图层层级
            dataType: 'point', // 数据类型，点类型
            data: [], // 数据
            drawType: 'heatmap', // 展示形式
            drawOptions: { // 绘制参数
                //shadowBlur: 15, // 是否有模糊效果
                unit: 'm', // 单位,px:像素(默认),m:米
                max: 50000, // 设置显示的权重最大值
                type: 'circle', // 点形状,可选circle:圆形(默认),rect:矩形
                size: 9000, // 半径大小
                maxOpacity: 0.9,
                gradient: { // 显示的颜色渐变范围
                    '0': 'blue',
                    '0.3': 'cyan',
                    '0.5': 'lime',
                    '0.8': 'yellow',
                    '1.0': 'red'
                }
            },
            mapConfig: {
                option: {
                    extent: 'new Extent(13349400, 3601339, 13511092, 3769327, new SpatialReference({ wkid: 102100 }))',
                    logo: false,
                    slider: false,
                },
                layers: [{
                    id: 'baseMap',
                    type: 'Tiled',
                    url: 'http://map.geoq.cn/ArcGIS/rest/services/ChinaOnlineStreetPurplishBlue/MapServer'
                }, ]
            }
        },
        CommonWMapSimple: {
            mapv: {}, // 对应的mapv实例
            zIndex: 1, // 图层层级
            dataType: 'point', // 数据类型，点类型
            data: [], // 数据
            dataRangeControl: false, // 值阈控件
            drawType: 'simple', // 展示形式
            drawOptions: { // 绘制参数
                fillStyle: 'rgba(200, 200, 50, 1)', // 填充颜色
                //strokeStyle: 'rgba(0, 0, 255, 1)', // 描边颜色
                //lineWidth: 4, // 描边宽度
                shadowColor: 'rgba(255, 255, 255, 1)', // 投影颜色
                shadowBlur: 15,  // 投影模糊级数
                globalCompositeOperation: 'lighter', // 颜色叠加方式
                size: 2 // 半径
            },
            mapConfig: {
                option: {
                    extent: 'new Extent(13456123.171,3665550.074,13482581.558,3696241.803, new SpatialReference({ wkid: 102100 }))',
                    logo: false,
                    slider: false,
                },
                layers: [{
                    id: 'baseMap',
                    type: 'Tiled',
                    url: 'http://map.geoq.cn/ArcGIS/rest/services/ChinaOnlineStreetPurplishBlue/MapServer'
                }, ]
            }
        },


    };

    this.getOption = function (optionName) {
        return this.options[optionName];
    }

}