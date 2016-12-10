define(["dojo/_base/declare","esri/geometry/Point","esri/geometry/ScreenPoint","esri/SpatialReference","esri/geometry/webMercatorUtils","../common/Class","../common/WMapUtils","../common/MVCObject","../component/Transitions","../component/Animation","../control/DataRangeControl","../control/DrawScale","../drawer/BubbleDrawer","../drawer/BrushDrawer","../drawer/CategoryDrawer","../drawer/ChoroplethDrawer","../drawer/ClusterDrawer","../drawer/DensityDrawer","../drawer/HeatmapDrawer","../drawer/IntensityDrawer","../drawer/LineDrawer","../drawer/SimpleDrawer","../drawer/TrafficDrawer","./CanvasLayer"],function(declare,Point,ScreenPoint,SpatialReference,webMercatorUtils,Class,WMapUtils,MVCObject,Transitions,Animation,DataRangeControl,DrawScale,BubbleDrawer,BrushDrawer,CategoryDrawer,ChoroplethDrawer,ClusterDrawer,DensityDrawer,HeatmapDrawer,IntensityDrawer,LineDrawer,SimpleDrawer,TrafficDrawer,CanvasLayer){return declare("WMapVLayer",[Class],{constructor:function(e){this._drawer={},this.initOptions($.extend({ctx:null,animationCtx:null,mapv:null,paneName:"labelPane",map:null,context:"2d",data:[],dataType:"point",animationOptions:{size:5},coordType:"bd09ll",drawType:"simple",animation:!1,geometry:null,dataRangeControl:!0,zIndex:1},e)),this._map=this.getMap()||this.getMapv().getMap(),this.dataRangeControl=new DataRangeControl(this.getMapv().getMap()),this.Scale=new DrawScale(this.getMapv().getMap()),this.notify("data"),this.notify("mapv")},initialize:function(){if(!this.canvasLayer){this.bindTo("map",this.getMapv());var e=this;this.canvasLayer=new CanvasLayer({map:this.getMap(),context:this.getContext(),zIndex:this.getZIndex(),paneName:this.getPaneName(),update:function(){e.draw()},elementTag:"canvas"}),this.setCtx(this.canvasLayer.getContainer().getContext(this.getContext())),this.getAnimation()&&(this.animationLayer=new CanvasLayer({map:this.getMap(),zIndex:this.getZIndex(),elementTag:"canvas"}),this.setAnimationCtx(this.animationLayer.getContainer().getContext(this.getContext())))}},draw:function(){var e=this;if(this.getMapv()){var t=this.getCtx();if(!t)return!1;this._calculatePixel(),"time"!==this.getAnimation()&&("2d"==this.getContext()&&t.clearRect(0,0,t.canvas.width,t.canvas.height),this._getDrawer().drawMap()),"polyline"===this.getDataType()&&this.getAnimation()&&!this._animationFlag&&(this.drawAnimation(),this._animationFlag=!0);var a=this.getAnimationOptions()||{};if("polyline"===this.getDataType()&&this.getAnimation()&&!this._animationTime){this._animationTime=!0;var i=this.timeline=new Animation({duration:a.duration||1e4,fps:a.fps||30,delay:a.delay||"INFINITE",transition:(new Transitions).Transitions[a.transition||"linear"],onStop:a.onStop||function(){},render:function(i){"2d"==e.getContext()&&t.clearRect(0,0,t.canvas.width,t.canvas.height);var n=parseInt(parseFloat(e._minTime)+(e._maxTime-e._minTime)*i);e._getDrawer().drawMap(n),a.render&&a.render(n)}});i.setFinishCallback(function(){i.start()}),i.start()}this.dispatchEvent("draw")}},drawAnimation:function(){var e=this.getAnimationCtx();if(!e)return!1;e.clearRect(0,0,e.canvas.width,e.canvas.height);var t=this;this._getDrawer().drawAnimation(),this.getAnimation()&&requestAnimationFrame(function(){t.drawAnimation()})},animation_changed:function(){this.getAnimation()&&this.drawAnimation()},mapv_changed:function(){return this.getMapv()?(this.canvasLayer&&this.canvasLayer.show(),this.initialize(),this.updateControl(),void this.draw()):void(this.canvasLayer&&this.canvasLayer.hide())},drawType_changed:function(){this.updateControl(),this.draw()},drawOptions_changed:function(){this.draw()},updateControl:function(){var e=this.getMapv();if(e){{var t=this._getDrawer();this.getMap()}t.scale&&this.getDataRangeControl()?(t.scale(this.Scale),this.Scale.show()):this.Scale.hide(),this.getMapv().OptionalData&&this.getMapv().OptionalData.initController(this,this.getDrawType())}},_getDrawer:function(){var drawType=this.getDrawType();if(!this._drawer[drawType]){var funcName=drawType.replace(/(\w)/,function(e){return e.toUpperCase()});funcName+="Drawer";var drawer=this._drawer[drawType]=eval("(new "+funcName+"(this))");drawer.scale?this.getMapv()&&(drawer.scale(this.Scale),this.Scale.show()):this.Scale.hide()}return this._drawer[drawType]},_calculatePixel:function(){var e=this.getMapv().getMap(),t=e.extent.getWidth()/e.width,a=e.position,i=this.pixel2GeoCoord(a);if(4326===i.spatialReference.wkid){var n=webMercatorUtils.lngLatToXY(i.x,i.y),r=new Point(n[0],n[1],new SpatialReference(102100));i=r}for(var o=this.getData(),s=0;s<o.length;s++){if(o[s].lng&&o[s].lat&&!o[s].x&&!o[s].y){var h=new Point(o[s].lng,o[s].lat),l=webMercatorUtils.lngLatToXY(h.x,h.y);o[s].x=l[0],o[s].y=l[1]}if(o[s].x&&o[s].y&&(o[s].px=(o[s].x-i.x)/t+a.x,o[s].py=(i.y-o[s].y)/t+a.y),o[s].geo){var g=[];if(e.spatialReference&&4326==e.spatialReference.wkid||!e.spatialReference&&e.getLayer("baseMap")&&4326==e.getLayer("baseMap").spatialReference.wkid)for(var c=0;c<o[s].geo.length;c++){var l=new Point(o[s].geo[c][0],o[s].geo[c][1]),p=this.geoCoord2Pixel(l);g.push([p.x,p.y,parseFloat(o[s].geo[c][2])])}else if(e.spatialReference&&102100==e.spatialReference.wkid||!e.spatialReference&&e.getLayer("baseMap")&&102100==e.getLayer("baseMap").spatialReference.wkid)if(Math.round(o[s].geo[0][0])>180)for(var c=0;c<o[s].geo.length;c++){var l=new Point(o[s].geo[c][0],o[s].geo[c][1],new SpatialReference(102100)),n=webMercatorUtils.lngLatToXY(l.x,l.y),p=this.geoCoord2Pixel(l);g.push([p.x,p.y,parseFloat(o[s].geo[c][2])])}else for(var c=0;c<o[s].geo.length;c++){var l=new Point(o[s].geo[c][0],o[s].geo[c][1]),n=webMercatorUtils.lngLatToXY(l.x,l.y),p=this.geoCoord2Pixel(l);g.push([p.x,p.y,parseFloat(o[s].geo[c][2])])}o[s].pgeo=g}}},geoCoord2Pixel:function(e){var t=this._map.toScreen(e);return t},pixel2GeoCoord:function(e){var t=this._map.toMap(new ScreenPoint(e.x,e.y));return t},data_changed:function(){var e=this.getData();if(e){if("polyline"===this.getDataType()&&this.getAnimation())for(var t=0;t<e.length;t++)e[t].index=parseInt(Math.random()*e[t].geo.length,10);if("polyline"===this.getDataType()&&"time"===this.getAnimation()){this._minTime=e[0]&&e[0].geo[0][2],this._maxTime=this._minTime;for(var t=0;t<e.length;t++)for(var a=e[t].geo,i=0;i<a.length;i++){var n=a[i][2];n<this._minTime&&(this._minTime=n),n>this._maxTime&&(this._maxTime=n)}}e.length>0&&(this._min=e[0].count,this._max=this._max);for(var t=0;t<e.length;t++)(void 0===e[t].count||null===e[t].count)&&(e[t].count=1),this._max=Math.max(this._max,e[t].count),this._min=Math.min(this._min,e[t].count);this.draw()}},getDataRange:function(){return{minTime:this._minTime,maxTime:this._maxTime,min:this._min,max:this._max}},zIndex_changed:function(){var e=this.getZIndex();this.canvasLayer.setZIndex(e)},dataRangeControl_changed:function(){this.updateControl(),this._getDrawer().notify("drawOptions")}})});