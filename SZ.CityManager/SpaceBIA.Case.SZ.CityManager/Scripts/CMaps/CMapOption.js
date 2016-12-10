/*****************************************************************************************************
 *
 * Copyright (C) 2010-2016, YL Wisdom Co.,Ltd.,All Rights Reserved.
 *
 *****************************************************************************************************
 * FileName:CMapOption.js
 * Create Date: 2016-10-25
 * Author：Jason Young
 * E-Mail:genyong.yang@gmail.com
 * Description:主要记录常熟大数据一套图中用到的图表配置信息，详细信息如下：
 * 1、
 *****************************************************************************************************/
function CMapOption() {


    this.options = {

        CommonCMapPoint: {
            infoTitle: '{name}',
            renderType: 'graduatedSize',//classify(分层设色图),graduatedSize(点大小，只针对点图层)
            symbolSize: 15,
            renderFields: ['户籍人口', '流动人口', '总人口'],
            showInfo: true,
            infoType: 'table',
            defaultUnits: '万元',
            infoFields: ['流动人口', '户籍人口', '总人口'],
            data: [],
            layerType: 'point',
            //minScale:1000000,
            //maxScale: 50000,
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
        AZFDSCMapPoint: {
            infoTitle: '{name}',
            renderType: 'graduatedSize',//classify(分层设色图),graduatedSize(点大小，只针对点图层)
            symbolSize: 15,
            renderFields: ['安置房栋数'],
            showInfo: true,
            infoType: 'table',
            defaultUnits: '栋',
            infoFields: ['安置房栋数'],
            data: [],
            layerType: 'point',
            //minScale:1000000,
            //maxScale: 50000,
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
        AZFTSCMapPoint: {
            infoTitle: '{name}',
            renderType: 'graduatedSize',//classify(分层设色图),graduatedSize(点大小，只针对点图层)
            symbolSize: 15,
            renderFields: ['安置房套数'],
            showInfo: true,
            infoType: 'table',
            defaultUnits: '套',
            infoFields: ['安置房套数'],
            data: [],
            layerType: 'point',
            //minScale:1000000,
            //maxScale: 50000,
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
        AZFJZMJCMapPoint: {
            infoTitle: '{name}',
            renderType: 'graduatedSize',//classify(分层设色图),graduatedSize(点大小，只针对点图层)
            symbolSize: 15,
            renderFields: ['安置房建筑面积'],
            showInfo: true,
            infoType: 'table',
            defaultUnits: '万m²',
            infoFields: ['安置房建筑面积'],
            data: [],
            layerType: 'point',
            //minScale:1000000,
            //maxScale: 50000,
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
        AZFHSMJCMapPoint: {
            infoTitle: '{name}',
            renderType: 'graduatedSize',//classify(分层设色图),graduatedSize(点大小，只针对点图层)
            symbolSize: 15,
            renderFields: ['安置房户室面积'],
            showInfo: true,
            infoType: 'table',
            defaultUnits: '万m²',
            infoFields: ['安置房户室面积'],
            data: [],
            layerType: 'point',
            //minScale:1000000,
            //maxScale: 50000,
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
        CommonCMapPolygon: {
            infoTitle: '{name}',
            renderType: 'bar',
            symbolSize: 10,
            renderFields: ['常州市', '扬州市', '无锡市', '盐城市'],
            showInfo: true,
            infoType: 'bar',
            defaultUnits: '万元',
            infoFields: ['徐州市', '南通市', '南京市'],
            data: [],
            layerType: 'polygon',
            //minScale:1000000,
            //maxScale: 50000,
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
        CMapPolygon: {
            infoTitle: '{Name_CHN}',
            renderType: 'bar',
            symbolSize: 10,
            renderFields: ['梅李镇', '海虞镇', '碧溪新区', '虞山镇', '古里镇', '沙家浜镇', '董浜镇', '辛庄镇', '尚湖镇', '支塘镇'],
            showInfo: true,
            infoType: 'bar',
            defaultUnits: '万元',
            infoFields: ['梅李镇', '海虞镇', '碧溪新区'],
            data: [],
            layerType: 'polygon',
            //minScale:1000000,
            //maxScale: 50000,
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
        GGFWCMapPolygon: {
            infoTitle: '{name}',
            renderType: 'bar',
            symbolSize: 10,
            renderFields: ['公共服务用地分布'],
            showInfo: true,
            infoType: 'table',
            defaultUnits: '平方米',
            infoFields: ['公共服务用地分布'],
            data: [],
            layerType: 'polygon',
            //minScale:1000000,
            //maxScale: 50000,
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
        JZYDCMapPolygon: {
            infoTitle: '{name}',
            renderType: 'bar',
            symbolSize: 10,
            renderFields: ['居住用地分布'],
            showInfo: true,
            infoType: 'table',
            defaultUnits: '平方米',
            infoFields: ['居住用地分布'],
            data: [],
            layerType: 'polygon',
            //minScale:1000000,
            //maxScale: 50000,
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
        SYYDCMapPolygon: {
            infoTitle: '{Name_CHN}',
            renderType: 'bar',
            symbolSize: 10,
            renderFields: ['商业用地分布'],
            showInfo: true,
            infoType: 'table',
            defaultUnits: '平方米',
            infoFields: ['商业用地分布'],
            data: [],
            layerType: 'polygon',
            //minScale:1000000,
            //maxScale: 50000,
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
        XZBGCMapPolygon: {
            infoTitle: '{Name_CHN}',
            renderType: 'bar',
            symbolSize: 10,
            renderFields: ['行政办公用地分布'],
            showInfo: true,
            infoType: 'bar',
            defaultUnits: '平方米',
            infoFields: ['行政办公用地分布'],
            data: [],
            layerType: 'polygon',
            //minScale:1000000,
            //maxScale: 50000,
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