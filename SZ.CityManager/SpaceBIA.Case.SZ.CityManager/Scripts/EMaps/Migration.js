function SFMigration() {
    this.getOption = function () {
        return this.option;
    };

    this.option = {
        backgroundColor: '#1b1b1b',
        color: ['gold', 'aqua', 'lime'],
        title: {
            show: false,
            text: '迁徙图',
            subtext: '',
            x: 'right',
            y: '10',
            textStyle: {
                color: '#fff'
            }
        },
        tooltip: {

        },
        legend: {
            orient: 'vertical',
            x: 'right',
            y: '40%',
            data: [],
            selectedMode: 'single',
            selected: {
            },
            textStyle: {
                color: '#fff'
            }
        },
        dataRange: {
            min: 0,
            max: 120000,
            x: '15px',
            bottom: '10%',
            calculable: true,
            color: ['#ff3333', 'orange', 'yellow', 'lime', 'aqua'],
            textStyle: {
                color: '#fff'
            }
        },
        animationDurationUpdate: 2000, // for update animation, like legend selected.
        series: [
            {
                name: '',
                type: 'map',
                //roam: true,
                //hoverable: false,
                mapType: 'none',
                //itemStyle:{
                //    normal:{
                //        borderColor:'rgba(100,149,237,1)',
                //        borderWidth:0.5,
                //        areaStyle:{
                //            color: '#1b1b1b'
                //        }
                //    }
                //},
                data: [],
                //geoCoord: {
                //},
                markPoint: {
                    symbol: 'emptyCircle',
                    symbolSize: function (v) {
                        return 3 + v / 200000;
                    },

                    effect: {
                        show: true,
                        shadowBlur: 0
                    },
                    itemStyle: {
                        normal: {
                            label: { show: false }
                        }
                    },
                    data: [
                    ]
                },
                markLine: {
                    smooth: true,
                    symbol: ['star', 'arrow'],
                    symbolSize: function (v) {
                        return 4 + v / 200000;
                    },
                    effect: {
                        show: true,
                        scaleSize: 1,
                        period: 30,
                        color: '#fff',
                        shadowBlur: 10
                    },
                    itemStyle: {
                        normal: {
                            borderWidth: 1.5,
                            label: {
                                show: false,
                                position: 'inside'
                            },
                            lineStyle: {
                                type: 'solid',
                                shadowBlur: 10
                            }
                        }
                    },
                    data: [
                    ]
                }
            }
        ],
        mapConfig: {
            option: {
                extent: 'new Extent(8272070, 2435804, 14899896, 6510395, new SpatialReference({ wkid: 102100 }))',
                logo: false,
                slider: false,
            },
            layers: [{
                id: 'baseMap',
                type: 'Tiled',
                url: KSConfig.baseMap
            }, ]
        }
    };
};

//城市迁徙修改地图范围
function CityMigration() {
    this.getOption = function () {
        return this.option;
    };

    this.option = {
        backgroundColor: '#1b1b1b',
        color: ['gold', 'aqua', 'lime'],
        title: {
            show: false,
            text: '迁徙图',
            subtext: '',
            x: 'right',
            y: '10',
            textStyle: {
                color: '#fff'
            }
        },
        tooltip: {

        },
        legend: {
            orient: 'vertical',
            x: 'right',
            y: '40%',
            data: [],
            selectedMode: 'single',
            selected: {
            },
            textStyle: {
                color: '#fff'
            }
        },
        dataRange: {
            min: 0,
            max: 120000,
            x: '15px',
            bottom: '10%',
            calculable: true,
            color: ['#ff3333', 'orange', 'yellow', 'lime', 'aqua'],
            textStyle: {
                color: '#fff'
            }
        },
        animationDurationUpdate: 2000, // for update animation, like legend selected.
        series: [
            {
                name: '',
                type: 'map',
                //roam: true,
                //hoverable: false,
                mapType: 'none',
                //itemStyle:{
                //    normal:{
                //        borderColor:'rgba(100,149,237,1)',
                //        borderWidth:0.5,
                //        areaStyle:{
                //            color: '#1b1b1b'
                //        }
                //    }
                //},
                data: [],
                //geoCoord: {
                //},
                markPoint: {
                    symbol: 'emptyCircle',
                    symbolSize: function (v) {
                        return 3 + v / 200000;
                    },

                    effect: {
                        show: true,
                        shadowBlur: 0
                    },
                    itemStyle: {
                        normal: {
                            label: { show: false }
                        }
                    },
                    data: [
                    ]
                },
                markLine: {
                    smooth: true,
                    symbol: ['star', 'arrow'],
                    symbolSize: function (v) {
                        return 4 + v / 200000;
                    },
                    effect: {
                        show: true,
                        scaleSize: 1,
                        period: 30,
                        color: '#fff',
                        shadowBlur: 10
                    },
                    itemStyle: {
                        normal: {
                            borderWidth: 1.5,
                            label: {
                                show: false,
                                position: 'inside'
                            },
                            lineStyle: {
                                type: 'solid',
                                shadowBlur: 10
                            }
                        }
                    },
                    data: [
                    ]
                }
            }
        ],
        mapConfig: {
            option: {
                extent: 'new Extent(116.830950, 31.587880, 121.961565, 34.007121, new SpatialReference({ wkid: 4326 }))',
                logo: false,
                slider: false,
            },
            layers: [{
                id: 'baseMap',
                type: 'Tiled',
                url: 'http://map.geoq.cn/ArcGIS/rest/services/ChinaOnlineStreetPurplishBlue/MapServer'
            }, ]
        }
    };
};

function CSMigration() {
    this.getOption = function () {
        return this.option;
    };

    this.option = {
        backgroundColor: '#1b1b1b',
        color: ['gold', 'aqua', 'lime'],
        title: {
            show: false,
            text: '迁徙图',
            subtext: '',
            x: 'right',
            y: '10',
            textStyle: {
                color: '#fff'
            }
        },
        tooltip: {

        },
        legend: {
            orient: 'vertical',
            x: 'right',
            y: '40%',
            data: [],
            selectedMode: 'single',
            selected: {
            },
            textStyle: {
                color: '#fff'
            }
        },
        dataRange: {
            min: 0,
            max: 120000,
            x: '15px',
            bottom: '10%',
            calculable: true,
            color: ['#ff3333', 'orange', 'yellow', 'lime', 'aqua'],
            textStyle: {
                color: '#fff'
            }
        },
        animationDurationUpdate: 2000, // for update animation, like legend selected.
        series: [
            {
                name: '',
                type: 'map',
                //roam: true,
                //hoverable: false,
                mapType: 'none',
                //itemStyle:{
                //    normal:{
                //        borderColor:'rgba(100,149,237,1)',
                //        borderWidth:0.5,
                //        areaStyle:{
                //            color: '#1b1b1b'
                //        }
                //    }
                //},
                data: [],
                //geoCoord: {
                //},
                markPoint: {
                    symbol: 'emptyCircle',
                    symbolSize: function (v) {
                        return 3 + v / 200000;
                    },

                    effect: {
                        show: true,
                        shadowBlur: 0
                    },
                    itemStyle: {
                        normal: {
                            label: { show: false }
                        }
                    },
                    data: [
                    ]
                },
                markLine: {
                    smooth: true,
                    symbol: ['star', 'arrow'],
                    symbolSize: function (v) {
                        return 4 + v / 200000;
                    },
                    effect: {
                        show: true,
                        scaleSize: 1,
                        period: 30,
                        color: '#fff',
                        shadowBlur: 10
                    },
                    itemStyle: {
                        normal: {
                            borderWidth: 1.5,
                            label: {
                                show: false,
                                position: 'inside'
                            },
                            lineStyle: {
                                type: 'solid',
                                shadowBlur: 10
                            }
                        }
                    },
                    data: [
                    ]
                }
            }
        ],
        mapConfig: {
            option: {
                extent: 'new Extent(13349400, 3601339, 13511092, 3769327, new SpatialReference({ wkid: 102100 }))',
                logo: false,
                slider: false,
            },
            layers: [{
                id: 'baseMap',
                type: 'Tiled',
                url: 'http://10.36.172.231:6080/arcgis/rest/services/BASE/sz84_blue/MapServer'
            }, ]
        }
    };
};
