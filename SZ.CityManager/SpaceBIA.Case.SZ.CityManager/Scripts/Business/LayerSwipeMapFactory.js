function LayerSwipeMapFactory(mapDiv, swipeDiv, firtsMap, secondMap) {
    //传如入map和swipe的div
    //console.log("aaaaaaa");
        require([
            "esri/map",
            "esri/SpatialReference",
            "esri/geometry/Extent",
            "esri/layers/ArcGISTiledMapServiceLayer",
            "widgets/emapwidget/EChartsLayerV2.0",
            "esri/dijit/LayerSwipe",
            "esri/layers/ArcGISTiledMapServiceLayer",
            "dojo/domReady!"
        ], function (Map, SpatialReference, Extent, ArcGISTiledMapServiceLayer, EChartsLayer, LayerSwipe, Tiled) {
            //_this.initDefaultMapConfig();
            //initDefaultMapConfig是从mapoption的js中取得
            ////解析Extent
            //for (var k in _this.mapOption.option) {
            //    if (k.toLowerCase() == 'extent') {
            //        _this.mapOption.option[k] = eval(_this.mapOption.option[k]);
            //    }
            //}
            //_this.map = new Map(mapDiv, _this.mapOption.option);
            //加图层
            //console.log("bbbbb");
            
            map = new Map(mapDiv, {
                logo: false,
                zoom: 8,
                extent: new Extent(13456123.171, 3665550.074, 13482581.558, 3696241.803, new SpatialReference({ wkid: 102100 })),
            });
            var swipeFirtsMap = new Tiled(firtsMap);

            var swipeSecondMap = new Tiled(secondMap);
            map.addLayers([swipeFirtsMap, swipeSecondMap]);
            //卷帘
            
            var swipeLayer = map.getLayer(map.layerIds[1]);
            var swipeWidget = new LayerSwipe({
                type: "vertical",  //Try switching to "scope" or "horizontal"
                map: map,
                layers: [swipeLayer]
            }, swipeDiv);
            swipeWidget.startup();

        });
    };
   