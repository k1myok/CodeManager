function KSHeart() {

    this.getOption = function () {
        return this.option;
    };
    this.option = {
        backgroundColor: '#1b1b1b',
        title: {
            show: false,
            text: '热度图',
            x: 'right',
            y: '10',
            textStyle: {
                color: '#fff'
            }
        },
        legend: {
            orient: 'vertical',
            x: 'right',
            y: '40%',
            data: [],
            //selectedMode: 'single',
            selected: {},
            textStyle: {
                color: '#fff'
            }
        },
        series: [
            {
                name: '',
                type: 'map',
                mapType: 'none',
                hoverable: false,
                roam: false,
                zlevel: 5,
                data: [],
                heatmap: {
                    needsTransform: false,//必须
                    minAlpha: 0.005,
                    blurSize: 30,
                    gradientColors: [
                        'blue',
                        'cyan',
                        'lime',
                        'yellow',
                        'red'
                    ],
                    valueScale: 0.01,
                    opacity: 0.5,
                    data: []
                },
                itemStyle: {
                    normal: {
                        borderColor: 'rgba(100,149,237,0.6)',
                        borderWidth: 0.5,
                        areaStyle: {
                            color: '#1b1b1b'
                        }
                    }
                }
            }],
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
            }, ]
        }
    };
};

