/*
 * Graphic Chart Layer
 * @author JasonYang
 * @email genyong.yang@gmail.com
 * @version 0.1
 */

//说明：GraphicChartLayer用户地图场渲染图表(柱状图、饼状图、环状图、分层设色图、点大小图)的一个
//轻量级类库，扩展自esri graphicLayer，详细参数说明如下
// var option={
//      title:'税收统计柱状图',//用于显示infoWindow的title
//      renderType:'bar',//默认bar(柱状图),除此之外，还有pie(饼状图),ring(环状图),classify(分层设色图),graduatedSize(点大小，只针对点图层)
//      symbolSize:5,//默认值是5
//      renderFields:['field1','field2','field3','field4'],//数组,配置为英文字段形式
//      defaultUnits:'万元',//默认单位,可设置
//      fieldsMap:{
//                  field1:'字段1',
//                  field1:'字段2',
//                  field1:'字段3',
//                  field1:'字段4',
//                  ……:……
//                },//字段汉化，用于infoWindow和Legend中显示
//      showInfoWindow:true,//false 是否可显示InfoWindow,默认不显示
//      infoTitle:'title',//弹框的title配置,可以直接配置文本也可以通过字段动态获取,如'{Name_CHN}'
//      infoFields:[
//                  {
//                      title: "2012年度分析",      //当为bar或pie时生效
//                      caption: "2012年度当期分析",//当为bar或pie时生效
//
//                      data:[{
//                          field:'field1',
//                          units:'亿元'
//                      },{
//                          field:'field2',
//                          units:'元'
//                      },'field3'],
//                  },{……}//支持多infoFields
//                 ]
//                  //infoFields 配置了infoWindow显示字段值(包括InfoWindow中的图表字段值)
//                  //其值可为对象或字符串,字符时其他采用默认配置,如units采用defaultUnits
//      infoType:'table'//infoWindow中信息展现形式,可选值为table(默认,以字段的形式显示)/bar(以柱状图的形式显示)/pie(以饼状图的形式显示)
//      data:[
//              {
//                  id:'1',name:'全市',field1:234034,field2:129923,……
//              },{
//                  id:'2',name:'市区',field1:234034,field2:129923,……
//              },{
//                  id:'3',name:'区县',field1:234034,field2:129923,……
//              }],
//          //data包含所有需要可视化的数据组成的数组，数组中每一项必须包含id和name字段;
//          //其中id用于关联行政区划，name用于显示，其他字段表示统计指标，用于显示其渲染信息;
//          //attention:数据项中不包含renderFields中的字段时，渲染自动去掉不包含的字段值，并log出错误;
//          //          数据项中不包含infoFields中的字段时，InfoWindow的相关字段值自动设置为0,并log出错误;
//
//
//      renderColors: [[r,g,b,a], [r,g,b,a],……],//数组,用于配置默认渲染颜色数组,支持rgba形式或#形式
//      layerType:'Polygon',//图层类型,可选值为Polygon(默认，面状),Polyline(线状),Point(点状)
//      opacity：0.8,//图层透明度设置(值为0-1),默认为1
//      classify:5,//当图层为分层设色图时,该配置项标示分层级别，默认为10
//      
//      //默认不需赋值属性
//      map:map,            //esri map对象
//      region:region,      //统计的行政区划对象
//      points:points,      //统计的点对象
//      features:[]         //图层中的要素,解析后进行赋值
//  }
//
//  详细用法：
//      1.初始化GraphicChartLayer
//          var graphicChartLayer=new GraphicChartLayer(option);
//      2.将graphicChartLayer加到Map中去
//          map.addLayer(graphicChartLayer);//map为Esri Map对象
//      3.更改option之后直接setOption即可
//          graphicChartLayer.setOption(option);

define(
       ["dojo/_base/declare",
        "dojo/dom-style",
        "dojo/on",
        "dojo/_base/lang",
        "dojo/_base/array",
        "esri/Color",
        "esri/geometry/Extent",
        "esri/geometry/Point",
        "esri/geometry/Polyline",
        "esri/geometry/Polygon",
        "esri/SpatialReference",
        "esri/symbols/SimpleMarkerSymbol",
        "esri/symbols/SimpleLineSymbol",
        "esri/symbols/SimpleFillSymbol",
        "esri/renderers/UniqueValueRenderer",
        "esri/renderers/ClassBreaksRenderer",
        "esri/layers/GraphicsLayer",
        "esri/graphic",
        "esri/geometry/webMercatorUtils",
        "esri/InfoTemplate",
        "esri/dijit/Popup",
        "esri/dijit/PopupTemplate",
        "dojox/charting/Chart",
        "./mapData"
       ],
function (declare, domStyle, on, lang, array, Color,
          Extent, Point, Polyline, Polygon, SpatialReference,
          SimpleMarkerSymbol, SimpleLineSymbol, SimpleFillSymbol,
          UniqueValueRenderer, ClassBreaksRenderer, GraphicsLayer,
          Graphic, webMercatorUtils, InfoTemplate, Popup, PopupTemplate,
          Chart, mapData) {
    var clazz = declare("esri.layers.GraphicChartlayer", [GraphicsLayer], {//继承自esri GraphicsLayer
        //构造函数
        constructor: function (options) {
            /*
             * init params firstly
             * 首先初始化参数
             */
            //标识ChartLayer以区分于esri的其他图层
            this.IsChartLayer = true;
            //esriMap对象：渲染的地图
            this.map = options.map;
            //region用于行政区划统计
            this.region = options.region || {};
            //points用于做点状图
            this.points = options.points || [];
            //Todo:
            this.pilylines = options.polylines || [];
            //title用于显示标题
            //this.title = options.infoTitle||'';
            //defaultUtils用于显示InfoWindow中信息的默认单位
            this.defaultUnits = options.defaultUnits || '';
            //数组：传入的数据，需处理的数据
            this.data = options.data;
            //String:渲染样式（柱状图bar、饼状图pie、环状图ring、classify(分层设色图),graduatedSize(点大小，只针对点图层)）
            this.renderType = options.renderType || "bar";
            //Double 符号大小
            this.renderSize = options.symbolSize || 10;
            //数组：地图渲染字段
            this.renderFields = options.renderFields;
            //数组：地图渲染颜色
            this.renderColors = options.renderColors || [[120, 0, 0, 0.8], [0, 155, 0, 0.8],
                [0, 0, 155, 0.8], [255, 193, 37, 0.8], [255, 106, 106, 0.8],
                [255, 165, 79, 0.8], [153, 50, 204, 0.8], [255, 20, 147, 0.8],
                [56, 199, 120, 0.8], [240, 15, 203, 0.8], [217, 35, 220, 0.8],
                [110, 175, 80, 0.8], [1, 252, 212, 0.8], [140, 115, 112, 0.8], [185, 179, 70, 0.8],
                [198, 116, 57, 0.8], [235, 20, 120, 0.8], [170, 10, 245, 0.8], [6, 180, 249, 0.8],
                [178, 236, 19, 0.8], [240, 176, 15, 0.8]];
            //Double:渲染透明度(0-1)
            this.opacity = options.opacity || 1;
            //Int：数值分级默认10级 数值分级，均值
            this.classify = options.classify || 10;
            //默认不显示图例
            this.showLegend = options.showLegend || false;

            //bool:是否显示弹框信息
            this.showInfoWindow = options.showInfoWindow || false;
            //数组：弹框信息字段
            this.infoFields = options.infoFields || [];
            this.infoType = options.infoType || "table";//table,bar,pie
            this.infoTitle = options.infoTitle || '{Name_CHN}',//弹框的标题

            //汉化
            //this.fieldsMap = options.fieldsMap;

            //String:图层类型，展示行政区划图还是点状图,默认展示行政区划图层
            this.layerType = options.layerType || "Polygon";
            //记录feature
            this.features = [];
            //记录注册的行政区划
            this.registedRegions = {};

            //图例
            this.legend = {
                image: null,
                width: 0,
                height: 0
            };
            this.chartField = "CHART_FIELD";
            this.level = this.map.getLevel();
            this.zoomHandler = null;
            this.drawHandler = null;
            this.drawMode = "default";
            this.intervalLimit = 100;

            var self = this;
            //先注册行政区划
            this.registerAllRegions();

            this.loadFeatures(this.layerType, this.renderType);
        },



        //处理GeoData，解压等
        getRandonColor: function () {
            var r = 0 + parseInt(255 * Math.random());
            var g = 0 + parseInt(255 * Math.random());
            var b = 0 + parseInt(255 * Math.random());
            var a = 0.3 + 0.3 * Math.random();
            return new Color([r, g, b, a]);
        },
        registerAllRegions: function () {
            var geoMapData = new mapData();
            //江苏省
            //this.registedRegions['320000'] = geoMapData.decode('320000');
            ////苏州市
            //this.registedRegions['320500'] = geoMapData.decode('320500');
            ////高新区
            //this.registedRegions['320505'] = geoMapData.decode('320505');
            ////吴中区
            //this.registedRegions['320506'] = geoMapData.decode('320506');
            ////相城区
            //this.registedRegions['320507'] = geoMapData.decode('320507');
            ////姑苏区
            //this.registedRegions['320508'] = geoMapData.decode('320508');
            ////吴江区
            //this.registedRegions['320509'] = geoMapData.decode('320509');
            ////常熟市
            //this.registedRegions['320581'] = geoMapData.decode('320581');
            ////张家港市
            //this.registedRegions['320582'] = geoMapData.decode('320582');
            ////昆山市
            //this.registedRegions['320583'] = geoMapData.decode('320583');
            ////太仓市
            //this.registedRegions['320585'] = geoMapData.decode('320585');
            ////工业园区
            //this.registedRegions['320586'] = geoMapData.decode('320586');
            ////沙家浜镇
            //this.registedRegions['32058101'] = geoMapData.decode('32058101');
            ////辛庄镇
            //this.registedRegions['32058102'] = geoMapData.decode('32058102');
            ////支塘镇
            //this.registedRegions['32058103'] = geoMapData.decode('32058103');
            ////古里镇
            //this.registedRegions['32058104'] = geoMapData.decode('32058104');
            ////董浜镇
            //this.registedRegions['32058105'] = geoMapData.decode('32058105');
            ////尚湖镇
            //this.registedRegions['32058106'] = geoMapData.decode('32058106');
            ////虞山镇
            //this.registedRegions['32058107'] = geoMapData.decode('32058107');
            ////碧溪社区
            //this.registedRegions['32058108'] = geoMapData.decode('32058108');
            ////海虞镇
            //this.registedRegions['32058109'] = geoMapData.decode('32058109');
            ////梅李镇
            //this.registedRegions['32058110'] = geoMapData.decode('32058110');
            //************昆山开发区各街道
            //综保区 32058301
            this.registedRegions['32058301'] = geoMapData.decode('32058301');
            //中华园街道 32058302
            this.registedRegions['32058302'] = geoMapData.decode('32058302');
            //长江街道 32058303
            this.registedRegions['32058303'] = geoMapData.decode('32058303');
            //青阳街道 32058304
            this.registedRegions['32058304'] = geoMapData.decode('32058304');
            //兵希街道 32058305
            this.registedRegions['32058305'] = geoMapData.decode('32058305');
            //蓬朗街道 32058306
            this.registedRegions['32058306'] = geoMapData.decode('32058306');

        },
        loadFeatures: function (layerType, renderType) {
            var self = this;
            switch (layerType.toLowerCase()) {
                case "polygon": {//多边形,一般为行政区划
                    //bar,pie,ring,classfiy
                    //遍历data数据项
                    array.forEach(self.data, function (dataItem) {
                        //判断region是否存在,若没注册则写错误日志
                        if (!self.registedRegions || !self.registedRegions[dataItem.objectid]) {
                            console.error('region(onjectid=dataItem.objectid) is null or region did not registed corrected,plz check!');
                        }
                        var regionData = self.registedRegions[dataItem.objectid];
                        var spatialRef = new SpatialReference(4326);
                        var feature = regionData.features[0];
                        var att = {
                            id: feature.id,
                            name: feature.properties.name
                        };
                        //添加其他属性值
                        $.each(dataItem, function (key) {
                            if (key.toLowerCase() != 'attributes' && key.toLowerCase() != 'objectid' && Object.prototype.toString.call(dataItem[key]) == '[object String]') {
                                att[key] = dataItem[key];
                            } else if (key.toLowerCase() == 'attributes' && Object.prototype.toString.call(dataItem[key]) == '[object Array]') {
                                var items = dataItem[key];
                                array.forEach(items, function (item) {
                                    var name = item.name;
                                    var value = item.value || 0;
                                    if (name) {
                                        att[name] = value
                                    }
                                }, this);
                            }

                        });
                        //$.each(dataItem.attributes, function (i) {
                        //    var key = i;
                        //    var value = dataItem[i];
                        //    if (key.toLowerCase() != "id" && key.toLowerCase() != 'color') {
                        //        att[key] = value;
                        //    }

                        //});
                        var regionGeometry = new Polygon(spatialRef);
                        var geoRings = feature.geometry.coordinates;
                        array.forEach(geoRings, function (ring) {
                            regionGeometry.addRing(ring);
                        });
                        //定制popTemplate
                        var popTemplate = this.getPopTemplate(this.infoFields, this.infoType, this.infoTitle);
                        var graphic = new Graphic(regionGeometry,
                            new SimpleFillSymbol(SimpleFillSymbol.STYLE_SOLID,
                            new SimpleLineSymbol(SimpleLineSymbol.STYLE_SOLID,
                            new Color([255, 255, 255, 0.9]), 2),
                            this.getRandonColor()), att, popTemplate);
                        self.features.push(graphic);
                        self.add(graphic);
                    }, this);

                    break;
                }
                case "polyline": {
                    //Todo:待实现
                    break;
                }
                case "point": {
                    var spatialRef = new SpatialReference(4326);
                    array.forEach(self.data, function (dataItem) {
                        var feaAtt = {};
                        $.each(dataItem, function (key) {
                            if (key.toLowerCase() != 'attributes' && Object.prototype.toString.call(dataItem[key]) == '[object String]') {
                                feaAtt[key] = dataItem[key];
                            } else if (key.toLowerCase() == 'attributes' && Object.prototype.toString.call(dataItem[key]) == '[object Array]') {
                                var items = dataItem[key];
                                array.forEach(items, function (item) {
                                    var name = item.name;
                                    var value = item.value || 0;
                                    if (name) {
                                        feaAtt[name] = value
                                    }
                                }, this);

                            }

                        });

                        var pGeometry = new Point(dataItem.lon, dataItem.lat, spatialRef);
                        var popTemplate = this.getPopTemplate(this.infoFields, this.infoType, this.infoTitle);
                        var graphic = new Graphic(pGeometry);
                        graphic.setAttributes(feaAtt);
                        graphic.setInfoTemplate(popTemplate);
                        self.features.push(graphic);
                        self.add(graphic);

                    }, this);
                    break;
                }
                default: {

                    break;
                }
            }

        },
        _setMap: function (map, surface) {
            this.setOpacity(this.opacity);
            this.level = map.getLevel();
            this.draw(this.renderType, this.level);
            this.zoomHandler = on(map, "zoom-end", lang.hitch(this, function (args) {
                this.level = args.level;
                this.draw(this.renderType, this.level);
            }));
            var div = this.inherited(arguments);
            return div;
        },
        _unsetMap: function () {
            this.zoomHandler.remove();
            this.clearDrawInterval();
            this.inherited(arguments);
        },
        clear: function () {
            this.clearDrawInterval();
            this.inherited(arguments);
        },
        clearDrawInterval: function () {
            if (this.drawHandler) {
                window.clearInterval(this.drawHandler);
                this.drawHandler = null;
            }
        },
        draw: function (type, level) {
            if (!this.features || this.features.length < 1) {
                return;
            }
            this.map.infoWindow.hide();
            this.clear();
            this.reDrawFeatures(this.features);
            //if (!this.legend.image && type != "classify" && this.layerType == "Polygon") {
            if (this.showLegend) {
                if (!this.legend.image && type.toLowerCase() != "classify" && type.toLowerCase() != "graduatedsize") {
                    this.drawLegend(this.renderFields, this.renderColors);
                } else if (!this.legend.image && type.toLowerCase() == "classify") {
                    this.drawClassifyLegend(this.renderFields, this.renderColors, this.renderer);
                } else if (!this.legend.image && type.toLowerCase() == "graduatedsize") {
                    this.drawGraduatedSizeLegend(this.renderFields, this.renderColors, this.renderer);
                }
            }

            switch (type) {
                case "bar":
                    var renderer = this.getRenderer(this.renderFields, this.renderColors);
                    this.setRenderer(renderer);
                    this.bar(level);
                    break;
                case "pie":
                    this.pie(level);
                    break;
                case "ring":
                    this.ring(level);
                    break;
                case "classify": {
                    var renderer = this.getClassifyRenderer(this.renderFields, this.renderColors, this.layerType)
                    this.setRenderer(renderer);
                    break;
                }
                case "graduatedSize": {
                    var renderer = this.getGraduatedSizeRenderer(this.renderFields, this.renderColors, this.layerType)
                    this.setRenderer(renderer);
                    break;
                }
                default:
                    break;
            }
        },
        bar: function (level) {
            var fields = this.renderFields;
            var max = this.getMaxValue(this.features, fields);
            var size = this.getBarSize(level);
            var len = fields.length;
            var half = len / 2.0;
            var graphics = [];
            var attrs;
            var coords;
            var spatialref;
            var i;
            var xmin;
            var xmax;
            var ymin;
            var ymax;
            var value;
            var c;
            var attributes;
            var geometry;
            var graphic;
            var fs;
            var popTemplate;

            array.forEach(this.features, function (f) {
                attrs = f.attributes;
                var point;
                if (f.geometry.type.toLowerCase() == "point") {
                    point = f.geometry;
                }
                else if (f.geometry.type.toLowerCase() == "polygon") {
                    point = f.geometry.getCentroid();
                }
                if (point.spatialReference.wkid == 4326) {//经纬度，需转成webMorcator
                    var xy_Coord = webMercatorUtils.lngLatToXY(point.x, point.y);
                    coords = new Point(xy_Coord[0], xy_Coord[1], new SpatialReference(100102));
                } else {
                    coords = point;
                }

                //spatialref = f.geometry.spatialReference;
                for (i = 0; i < len; ++i) {
                    if (typeof fields[i] == 'object') {
                        value = attrs[fields[i].field] || 0;
                    } else {
                        value = attrs[fields[i]] || 0;
                    }

                    if (value == 0) continue;
                    c = this.getClass(value, max, this.classify);
                    xmin = coords.x + (i - half) * size.w;
                    xmax = xmin + size.w;
                    if (value < 0) {
                        ymax = coords.y;
                        ymin = ymax - c * size.maxh / this.classify;
                    } else if (value > 0) {
                        ymin = coords.y;
                        ymax = ymin + c * size.maxh / this.classify;
                    } else {
                        ymax = ymin = coords.y;
                    }
                    geometry = new Extent(xmin, ymin, xmax, ymax, new SpatialReference(102100));
                    attributes = {};
                    array.forEach(this.infoFields, function (s) {
                        attributes[s] = attrs[s] || '';
                    }, this);
                    if (typeof fields[i] == 'object') {
                        attributes[fields[i].field] = value;
                    }
                    else {
                        attributes[fields[i]] = value;
                    }
                    attributes[this.chartField] = fields[i];
                    graphic = new Graphic(geometry, null, attributes);
                    graphics.push(graphic);
                }
            }, this);
            this.drawGraphics(graphics);
        },
        pie: function (level) {
            if (this.renderFields.length === 1) {
                this.setRenderer(null);
                this.singlePie(level);
            } else {
                var renderer = this.getRenderer(this.renderFields, this.renderColors);
                this.setRenderer(renderer);
                this.pluralCircle(level, this.getCircleSeg);
            }
        },
        ring: function (level) {
            if (this.renderFields.length === 1) {
                this.setRenderer(null);
                this.singleRing(level);
            } else {
                var renderer = this.getRenderer(this.renderFields, this.renderColors);
                this.setRenderer(renderer);
                this.pluralCircle(level, this.getRingSeg);
            }
        },
        singlePie: function (level) {
            var fields = this.renderFields;
            var max = this.getMaxValue(this.features, fields);
            var color = new Color(this.renderColors[0]);
            var borderColor = new Color([255, 255, 255, 1]);
            var field = fields[0];
            var graphics = [];
            var attrs;
            var coords;
            var value;
            var r;
            var attributes;
            var geometry;
            var graphic;
            var fs;
            var symbol;
            var infoTemplate;
            array.forEach(this.features, function (f) {
                attrs = f.attributes;
                value = attrs[field] || 0;
                if (value !== 0) {
                    r = this.getClass(value, max, this.classify) * 60 / this.classify;
                    symbol = this.getMarkerSymbol(r, color, borderColor);
                    attributes = {};
                    array.forEach(this.infoFields, function (s) {
                        attributes[s] = attrs[s] || "";
                    });
                    attributes[field] = value;
                    attributes[this.chartField] = field;
                    //fs = this.infoFields;//.concat(field);
                    //infoTemplate = this.getPopTemplate(fs, this.infoType);
                    if (f.geometry.type == "point") {
                        coords = f.geometry;
                    }
                    else if (f.geometry.type == "polygon") {
                        coords = f.geometry.getCentroid();
                    }
                    geometry = new Point(coords);
                    graphic = new Graphic(geometry, symbol, attributes);//, infoTemplate);
                    graphics.push(graphic);
                }
            }, this);
            this.drawGraphics(graphics);
        },
        singleRing: function (level) {
            var fields = this.renderFields;
            var max = this.getMaxValue(this.features, fields);
            var color = new Color(this.colors[0]);
            var borderColor = new Color([255, 255, 255, 1]);
            var symbol = this.getFillSymbol(color, borderColor);
            var radius = this.getCircleRadius(level);
            var delta = Math.PI / 180;
            var field = fields[0];
            var graphics = [];
            var attrs;
            var coords;
            var value;
            var x;
            var y;
            var r;
            var seg;
            var attributes;
            var geometry;
            var graphic;
            var fs;
            var infoTemplate;
            array.forEach(this.features, function (f) {
                attrs = f.attributes;
                value = attrs[field] || 0;
                if (value !== 0) {
                    if (f.geometry.type == "point") {
                        coords = f.geometry;
                    }
                    else if (f.geometry.type == "polygon") {
                        coords = f.geometry.getCentroid();
                    }
                    x = coords.x;
                    y = coords.y;
                    r = this.getClass(value, max, this.classify) * radius / this.classify;
                    seg = this.getRingSeg({
                        x: x,
                        y: y,
                        radius: r,
                        from: [x + r, y],
                        start: 0,
                        degree: Math.PI * 2,
                        delta: delta
                    }, this.getXY);
                    geometry = new Polygon(new SpatialReference(f.geometry.spatialReference));
                    geometry.addRing(seg.ring);
                    attributes = {};
                    array.forEach(this.infoFields, function (s) {
                        attributes[s] = attrs[s] || "";
                    });
                    attributes[field] = value;
                    attributes[this.chartField] = field;
                    //fs = this.infoFields;//.concat(field);
                    //infoTemplate = this.getPopTemplate(fs, this.infoType);
                    graphic = new Graphic(geometry, symbol, attributes);//, infoTemplate);
                    graphics.push(graphic);
                }
            }, this);
            this.drawGraphics(graphics);
        },
        pluralCircle: function (level, getSeg) {
            var fields = this.renderFields;
            var max = this.getMaxSum(this.features, fields);
            var radius = this.getCircleRadius(level);
            var delta = Math.PI / 180;
            var len = fields.length;
            var graphics = [];
            var coords;
            var attrs;
            var x;
            var y;
            var spatialref;
            var total;
            var abstotal;
            var r;
            var i;
            var value;
            var from;
            var start;
            var degree;
            var seg;
            var attributes;
            var geometry;
            var graphic;
            var fs;
            var infoTemplate;
            array.forEach(this.features, function (f) {
                attrs = f.attributes;
                if (f.geometry.type == "point") {
                    coords = f.geometry;
                }
                else if (f.geometry.type == "polygon") {
                    coords = f.geometry.getCentroid();
                }
                x = coords.x;
                y = coords.y;
                spatialref = f.geometry.spatialReference;
                total = 0;
                abstotal = 0;
                for (i = 0; i < len; ++i) {
                    total += attrs[fields[i]];
                    abstotal += Math.abs(attrs[fields[i]]);
                }
                r = this.getClass(total, max, this.classify) * radius / this.classify;
                from = [x + r, y];
                start = 0;
                for (i = 0; i < len; ++i) {
                    value = Math.abs(attrs[fields[i]] || 0);
                    if (value === 0) {
                        continue;
                    }
                    degree = value * Math.PI * 2 / abstotal;
                    seg = getSeg(
                        {
                            x: x,
                            y: y,
                            radius: r,
                            from: from,
                            start: start,
                            degree: degree,
                            delta: delta
                        }, this.getXY);
                    from = seg.to;
                    start += degree;
                    geometry = new Polygon(new SpatialReference(spatialref));
                    geometry.addRing(seg.ring);
                    attributes = {};
                    array.forEach(this.infoFields, function (s) {
                        attributes[s] = attrs[s] || "";
                    });
                    attributes[fields[i]] = value;
                    attributes[this.chartField] = fields[i];
                    graphic = new Graphic(geometry, null, attributes);
                    graphics.push(graphic);
                }
            }, this);
            this.drawGraphics(graphics);
        },
        drawGraphics: function (graphics) {
            var that = this;
            var count = graphics.length;
            var i = 0;
            if (this.drawMode === "interval" && count > this.intervalLimit) {
                that.drawHandler = window.setInterval(function () {
                    if (i < count) {
                        that.add(graphics[i++]);
                    } else {
                        window.clearInterval(that.drawHandler);
                        that.drawHandler = null;
                    }
                }, 1);
            } else {
                for (; i < count; ++i) {
                    that.add(graphics[i]);
                }
            }
        },
        getCircleSeg: function (obj, getXY) {
            var x = obj.x;
            var y = obj.y;
            var r = obj.radius;
            var start = obj.start;
            var degree = obj.degree;
            var delta = obj.delta;
            var ring = [[x, y]];
            var to = [];
            var i;
            ring.push(obj.from);
            for (i = start; i < start + degree; i += delta) {
                to = getXY(x, y, i, r);
                ring.push(to);
            }
            if (i < start + degree) {
                to = getXY(x, y, start + degree, r);
                ring.push(to);
            }
            ring.push([x, y]);
            return {
                ring: ring,
                to: to
            };
        },
        getRingSeg: function (obj, getXY) {
            var x = obj.x;
            var y = obj.y;
            var r = obj.radius;
            var ir = r / 2.0;
            var start = obj.start;
            var degree = obj.degree;
            var delta = obj.delta;
            var ring = [obj.from];
            var to = [];
            var i;
            var inner;
            for (i = start; i < start + degree; i += delta) {
                to = getXY(x, y, i, r);
                ring.push(to);
            }
            if (i < start + degree) {
                to = getXY(x, y, start + degree, r);
                ring.push(to);
            }
            for (i = start + degree; i >= start; i -= delta) {
                inner = getXY(x, y, i, ir);
                ring.push(inner);
            }
            if (i > start) {
                inner = getXY(x, y, start, ir);
                ring.push(inner);
            }
            return {
                ring: ring,
                to: to
            };
        },
        getMaxValue: function (features, fields) {
            var values = [];
            var attrs;
            array.forEach(features, function (f) {
                attrs = f.attributes;
                array.forEach(fields, function (fieldItem) {
                    if (typeof fieldItem == 'object') {
                        values.push(Math.abs(attrs[fieldItem.field] || 0));
                    }
                    else {
                        values.push(Math.abs(attrs[fieldItem] || 0));
                    }

                });
            });
            return Math.max.apply(Math, values);
        },
        getMaxSum: function (features, fields) {
            var totals = [], attrs, total;
            array.forEach(features, function (f) {
                attrs = f.attributes;
                total = 0;
                array.forEach(fields, function (d) {
                    total += attrs[d] || 0;
                });
                totals.push(Math.abs(total));
            });
            return Math.max.apply(Math, totals);
        },
        getClass: function (value, max, classify) {
            return Math.ceil(Math.abs(value) * classify * 1.0 / max);
        },
        getBarSize: function (level) {
            var width = Math.pow(2, this.map.getMaxZoom() - level) * (1 + level * 0.5) * this.renderSize;
            return { w: width, maxh: width * 5 };
        },
        getCircleRadius: function (level) {
            return Math.pow(2, this.map.getMaxZoom() - level) * (1 + level * 0.1) * this.renderSize;
        },
        getXY: function (x, y, degree, radius) {
            var coords = [], d = 0;
            if (degree <= Math.PI * 0.5) {
                coords[0] = x + Math.cos(degree) * radius;
                coords[1] = y + Math.sin(degree) * radius;
            } else if (degree <= Math.PI) {
                d = degree - Math.PI * 0.5;
                coords[0] = x - Math.sin(d) * radius;
                coords[1] = y + Math.cos(d) * radius;
            } else if (degree <= Math.PI * 1.5) {
                d = degree - Math.PI;
                coords[0] = x - Math.cos(d) * radius;
                coords[1] = y - Math.sin(d) * radius;
            } else {
                d = degree - Math.PI * 1.5;
                coords[0] = x + Math.sin(d) * radius;
                coords[1] = y - Math.cos(d) * radius;
            }
            return coords;
        },
        getRenderer: function (renderFields, colors) {
            var borderColor = new Color([255, 255, 255, 0.9]);
            var symbols = [];
            var i = 0;
            var c;
            var symbol;
            var renderer;
            array.forEach(colors, function (color) {
                c = color;
                symbol = this.getFillSymbol(new Color(c), borderColor);
                symbols.push(symbol);
            }, this);
            renderer = new UniqueValueRenderer(symbols[0], this.chartField);
            array.forEach(renderFields, function (fieldItem) {
                if (typeof fieldItem == 'object') {
                    renderer.addValue(fieldItem.field, symbols[i++]);
                }
                else {
                    renderer.addValue(fieldItem, symbols[i++]);
                }
            }, this);
            return renderer;
        },
        getGraduatedSizeRenderer: function (renderFields, colors, layerType) {
            if (layerType.toLowerCase() == 'point') {
                var symbol = new SimpleMarkerSymbol();
                symbol.setColor(new Color([0, 150, 0, 0.5]));
                var renderer = new ClassBreaksRenderer(symbol, (typeof renderFields[0] == 'object') ? renderFields[0].field : renderFields[0]);
                var maxValue = this.getMaxValue(this.features, renderFields.slice(0, 1));
                //默认分五级
                var levelValue = maxValue / 5;
                var val = 0;
                for (var i = 0; i < 5; i++) {
                    renderer.addBreak(val, val + levelValue, new SimpleMarkerSymbol().setSize((i + 1) * 5).setColor(new Color(this.renderColors[0])).setOutline(new SimpleLineSymbol().setColor(new Color([255, 255, 255, 0.8]))));
                    val = val + levelValue;
                }
                return renderer;
            }
        },
        getClassifyRenderer: function (renderFields, colors, layerType) {
            if (layerType.toLowerCase() == "point") {
                var symbol = new SimpleMarkerSymbol();
                symbol.setColor(new Color(colors[0]));
                var renderer = new ClassBreaksRenderer(symbol, (typeof renderFields[0] == 'object') ? renderFields[0].field : renderFields[0]);
                var maxValue = this.getMaxValue(this.features, renderFields.slice(0, 1));
                //默认分五级
                var levelValue = maxValue / 5;
                var val = 0;
                for (var i = 0; i < 5; i++) {
                    renderer.addBreak(val, val + levelValue, new SimpleMarkerSymbol().setColor(new Color(colors[i])).setOutline(new SimpleLineSymbol().setColor(new Color([255, 255, 255, 0.8]))));
                    val = val + levelValue;
                }
                return renderer;
            } else if (layerType.toLowerCase() == "polyline") {
                //Todo:待实现
            }
            else if (layerType.toLowerCase() == "polygon") {
                var symbol = new SimpleFillSymbol();
                symbol.setColor(new Color(colors[0]));
                var renderer = new ClassBreaksRenderer(symbol, (typeof renderFields[0] == 'object') ? renderFields[0].field : renderFields[0]);
                var maxValue = this.getMaxValue(this.features, renderFields.slice(0, 1));
                //默认分五级
                var levelValue = maxValue / 5;
                var val = 0;
                for (var i = 0; i < 5; i++) {
                    renderer.addBreak(val, val + levelValue, new SimpleFillSymbol().setColor(new Color(colors[i])).setOutline(new SimpleLineSymbol().setColor(new Color([255, 255, 255, 0.8]))));
                    val = val + levelValue;
                }
                return renderer;
            }

        },
        getFillSymbol: function (color, borderColor) {
            return new SimpleFillSymbol(
                SimpleFillSymbol.STYLE_SOLID,
                new SimpleLineSymbol(SimpleLineSymbol.STYLE_SOLID, borderColor, 1),
                color);
        }, getMarkerSymbol: function (size, color, borderColor) {
            return new SimpleMarkerSymbol(
                SimpleMarkerSymbol.STYLE_CIRCLE,
                size,
                new SimpleLineSymbol(SimpleLineSymbol.STYLE_SOLID, borderColor, 1),
                color);
        }, getPopTemplate: function (infoFields, infoType, infoTitle) {
            var fieldsInfos = [];
            var mediaInfos = [];
            var self = this;
            //infofields like ['fieldA','fieldB','fieldC']
            //infoType like bar,pie,table

            array.forEach(infoFields, function (fieldName) {
                var fieldItemFields = [];
                var item = {};
                item.fieldName = fieldName;
                item.label = fieldName;
                fieldItemFields.push(fieldName);

                item.format = { places: 2, digitSeparator: true };
                item.visible = true;
                fieldsInfos.push(item);

                var mediaItem = {};
                switch (infoType.toLowerCase()) {
                    case "bar": {
                        mediaItem.type = "barchart";
                        break;
                    }
                    case "pie": {
                        mediaItem.type = "piechart";
                        break;
                    }
                    case "table": {
                        break;
                    }
                    default: {
                        break;
                    }
                }
                if (mediaItem.type) {
                    var fieldItem = {};
                    mediaItem.title = infoTitle;
                    mediaItem.caption = infoTitle;
                    fieldItem.fields = fieldItemFields;
                    mediaItem.value = fieldItem;
                    mediaInfos.push(mediaItem);
                }


            }, this);

            var templateString = null;
            if (mediaInfos && mediaInfos.length > 0 && mediaInfos[0].type) {
                templateString = {
                    title: infoTitle || "详细分析",
                    fieldInfos: fieldsInfos,
                    mediaInfos: mediaInfos
                };
            }
            else {
                templateString = {
                    title: infoTitle || "详细分析",
                    fieldInfos: fieldsInfos
                };
            }
            var infoTemplate = new PopupTemplate(templateString);
            return infoTemplate;
        },
        getPointPopTemplate: function (dataItem, type, title, fieldsMap) {
            var fieldsInfos = [];
            var mediaInfos = [];
            var fields = [];
            var self = this;
            $.each(dataItem, function (i) {
                var key = i;
                var value = dataItem[i];
                if (key != "id" && key.toLowerCase() != "objectid" && key.toLowerCase() != "name") {
                    var obj = {};
                    obj.fieldName = key;
                    obj.label = fieldsMap[key] || key + self.defaultUnits ? ("(" + self.defaultUnits + ")") : "";
                    obj.format = { places: 2, digitSeparator: true };
                    obj.visible = true;
                    fields.push(key);
                    fieldsInfos.push(obj);
                }
            });

            var mediaItem = {};

            switch (type) {
                case "bar": {
                    mediaItem.type = "barchart";
                    break;
                }
                case "pie": {
                    mediaItem.type = "piechart";
                    break;
                }
                case "table": {
                    break;
                }
                default: {
                    break;
                }

            }
            var fieldItem = {};
            //mediaItem.title =title||"详细指标";
            fieldItem.fields = this.infoFields;
            mediaItem.value = fieldItem;
            mediaInfos.push(mediaItem);

            var templateString = null;
            if (mediaInfos && mediaInfos.length > 0 && mediaInfos[0].type) {
                templateString = {
                    title: title || "详细分析",
                    fieldInfos: fieldsInfos,
                    mediaInfos: mediaInfos
                };
            } else {
                templateString = {
                    title: title || "详细分析",
                    fieldInfos: fieldsInfos,
                };
            }
            var infoTemplate = new PopupTemplate(templateString);
            return infoTemplate;
        },
        drawLegend: function (renderFields, colors) {
            if (!this.canvas) {
                this.canvas = document.createElement("canvas");
                if (!this.canvas.getContext) {
                    return null;
                }

            }
            var canvas = this.canvas;
            var ctx = this.canvas.getContext("2d");
            var colorSize = { w: 20, h: 10, space: 10 };
            var size = this.getLegendSize(renderFields, colorSize);
            var i = 0;
            var rgbas = colors;
            var y;
            canvas.width = size.w;
            canvas.height = size.h;
            ctx.clearRect(0, 0, size.w, size.h);
            ctx.fillStyle = "rgba(255, 255, 255, 0)";
            ctx.fillRect(0, 0, size.w, size.h);
            ctx.font = "14px 微软雅黑";
            array.forEach(renderFields, function (fieldItem) {
                y = colorSize.space + i * (colorSize.space + colorSize.h);
                ctx.fillStyle = "rgba(" + rgbas[i++].join(",") + ")";
                ctx.fillRect(colorSize.space, y, colorSize.w, colorSize.h);
                if (typeof fieldItem == 'object') {
                    ctx.fillText(this.fieldsMap[fieldItem.field] || fieldItem.field, colorSize.space * 2 + colorSize.w, y + colorSize.h);
                } else {
                    ctx.fillText(fieldItem, colorSize.space * 2 + colorSize.w, y + colorSize.h);
                }

            }, this);
            this.legend.image = canvas.toDataURL();
            this.legend.width = size.w;
            this.legend.height = size.h;
            var legendDiv = document.getElementById("legendDiv");
            if (!legendDiv) {
                var root = this.map.root;
                legendDiv = document.createElement('div');
                legendDiv.id = 'legendDiv';
                legendDiv.style.position = 'absolute';
                legendDiv.style.float = 'right';
                legendDiv.style.right = '5px';
                legendDiv.style.bottom = '5px';
                legendDiv.style.borderRadius = '5px';//边框
                legendDiv.style.backgroundColor = 'rgba(255,255,255,0.6)';//北京透明度
                root.appendChild(legendDiv);
            }
            legendDiv.innerHTML = "";
            legendDiv.appendChild(canvas);

        },
        drawGraduatedSizeLegend: function (fields, colors, renderer) {
            if (!renderer) {
                renderer = this.getGraduatedSizeRenderer(this.renderFields, this.renderColors, this.layerType);
                //this.setRenderer(renderer);
            }
            if (!this.canvas) {
                this.canvas = document.createElement("canvas");
                if (!this.canvas.getContext) {
                    return null;
                }

            }
            var rendererBreaks = renderer.breaks;
            var labels = [];
            array.forEach(rendererBreaks, function (rendererBreak) {
                var label = rendererBreak[0].toFixed(0) + " - " + rendererBreak[1].toFixed(0);
                labels.push(label);
            });

            var canvas = this.canvas;
            var ctx = this.canvas.getContext("2d");
            var colorSize = { w: 30, h: 10, space: 15 };
            var size = this.getLegendSize(labels, colorSize);
            var i = 2;
            var rgbas;
            var y;
            size.h = size.h + 40;
            canvas.width = size.w;
            canvas.height = size.h;
            ctx.clearRect(0, 0, size.w, size.h);
            ctx.fillStyle = "rgba(255, 255, 255, 0.9)";
            ctx.fillRect(0, 0, size.w, size.h);
            ctx.font = "16px 微软雅黑";
            rgbas = array.map(colors, function (c) {
                return c;//.concat(1);
            });
            ctx.fillStyle = "rgba(0,0,0,1)";
            ctx.fillText(fields[0], colorSize.space + colorSize.w, 15 + colorSize.h);
            ctx.font = "14px 微软雅黑";
            array.forEach(labels, function (f) {
                y = colorSize.space + i * (colorSize.space + colorSize.h);

                //ctx.fillRect(colorSize.space, y, colorSize.w, colorSize.h);
                ctx.beginPath();
                ctx.fillStyle = "rgba(" + this.renderColors[0].join(",") + ")";
                ctx.arc(colorSize.space + 10, y, 2 + 1.5 * i, 0, 2 * Math.PI);
                ctx.closePath();
                ctx.fill();
                ctx.fillText(f, colorSize.space * 2 + colorSize.w, y + colorSize.h);
                i++;
            }, this);
            this.legend.image = canvas.toDataURL();
            this.legend.width = size.w;
            this.legend.height = size.h;
            var legendDiv = document.getElementById("legendDiv");
            if (!legendDiv) {
                var root = this.map.root;
                legendDiv = document.createElement('div');
                legendDiv.id = 'legendDiv';
                legendDiv.style.position = 'absolute';
                legendDiv.style.float = 'right';
                legendDiv.style.right = '5px';
                legendDiv.style.bottom = '5px';
                legendDiv.style.borderRadius = '5px';//边框
                legendDiv.style.backgroundColor = 'rgba(255,255,255,0.6)';//北京透明度
                root.appendChild(legendDiv);
            }
            legendDiv.innerHTML = "";
            legendDiv.appendChild(canvas);
        },
        drawClassifyLegend: function (fields, colors, renderer) {
            if (!renderer) {
                renderer = this.getClassifyRenderer(this.renderFields, this.renderColors, this.layerType);
                //this.setRenderer(renderer);
            }
            if (!this.canvas) {
                this.canvas = document.createElement("canvas");
                if (!this.canvas.getContext) {
                    return null;
                }

            }
            var rendererBreaks = renderer.breaks;
            var labels = [];
            array.forEach(rendererBreaks, function (rendererBreak) {
                var label = rendererBreak[0].toFixed(0) + " - " + rendererBreak[1].toFixed(0);
                labels.push(label);
            });

            var canvas = this.canvas;
            var ctx = this.canvas.getContext("2d");
            var colorSize = { w: 20, h: 10, space: 10 };
            var size = this.getLegendSize(labels, colorSize);
            var i = 2;
            var j = 0;
            var rgbas = colors;
            var y;
            size.h = size.h + 40;
            canvas.width = size.w;
            canvas.height = size.h;
            ctx.clearRect(0, 0, size.w, size.h);
            ctx.fillStyle = "rgba(255, 255, 255, 0)";
            ctx.fillRect(0, 0, size.w, size.h);
            ctx.font = "16px 微软雅黑";
            ctx.fillStyle = "rgba(0,0,0,1)";
            ctx.fillText(fields[0], colorSize.space + colorSize.w, 15 + colorSize.h);
            ctx.font = "14px 微软雅黑";
            array.forEach(labels, function (f) {
                y = colorSize.space + (i++) * (colorSize.space + colorSize.h);
                ctx.fillStyle = "rgba(" + rgbas[j++].join(",") + ")";
                ctx.fillRect(colorSize.space, y, colorSize.w, colorSize.h);
                ctx.fillText(f, colorSize.space * 2 + colorSize.w, y + colorSize.h);
            }, this);
            this.legend.image = canvas.toDataURL();
            this.legend.width = size.w;
            this.legend.height = size.h;
            var legendDiv = document.getElementById("legendDiv");
            if (!legendDiv) {
                var root = this.map.root;
                legendDiv = document.createElement('div');
                legendDiv.id = 'legendDiv';
                legendDiv.style.position = 'absolute';
                legendDiv.style.float = 'right';
                legendDiv.style.right = '5px';
                legendDiv.style.bottom = '5px';
                legendDiv.style.borderRadius = '5px';//边框
                legendDiv.style.backgroundColor = 'rgba(255,255,255,0.6)';//北京透明度
                root.appendChild(legendDiv);
            }
            legendDiv.innerHTML = "";
            legendDiv.appendChild(canvas);

        },
        //fields为对象或字符串
        getLegendSize: function (fields, colorSize) {
            var count = fields.length;
            var lens = [];
            var len;
            var maxlen;
            array.forEach(fields, function (fieldItem) {
                if (typeof fieldItem == 'object') {
                    len = this.getByteLength(this.fieldsMap[fieldItem.field] || fieldItem.field);
                    lens.push(len);
                } else {
                    len = this.getByteLength(fieldItem);
                    lens.push(len);
                }

            }, this);
            maxlen = Math.max.apply(Math, lens);
            return {
                w: maxlen * 8 + colorSize.w + colorSize.space * 3,
                h: count * colorSize.h + (count + 1) * colorSize.space
            };
        },
        getByteLength: function (s) {
            return (s || "").toString().trim().replace(/[^\x00-\xff]/g, "**").length;
        },
        showTip: function (tip, type) {
            //new gistech.widgets.tip.Tip({ tip: tip, type: type }).startup();
        },
        reDrawFeatures: function (features) {
            array.forEach(features, function (feature) {
                this.add(feature);
            }, this);
        }


    });

    return clazz;

});

