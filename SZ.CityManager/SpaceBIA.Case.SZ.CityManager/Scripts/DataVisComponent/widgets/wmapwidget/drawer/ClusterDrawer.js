define(["dojo/_base/declare","esri/geometry/Point","esri/geometry/ScreenPoint","esri/SpatialReference","esri/geometry/webMercatorUtils","../common/Class","../common/WMapUtils","./Drawer"],function(e,t,r,i,a,n,o,s){return e("ClusterDrawer",[s],{min:null,max:null,constructor:function(){this.inherited(arguments)},drawMap:function(){this.beginDrawMap();var e=this.getCtx();max=min=void 0;var r,n=this.getLayer().getData(),o=this.getMapv().getMap(),s=this.zoomUnit=o.extent.getWidth()/o.width,l=this.formatParam(),m=l.size,f=o.position,h=this.pixel2GeoCoord(f);if(4326===h.spatialReference.wkid){var p=a.xyToLngLat(h.x,h.y);r=new t(p[0],p[1],new i(102100))}else r=h;for(var u=m/s,x=parseInt(r.x/m,10)*m,g=(x-r.x)/s,v=[],c=0;g+c*u<o.width;){var d=g+c*u;v.push(d.toFixed(2)),c++}for(var y=parseInt(r.y/m,10)*m+m,b=(r.y-y)/s,w=[],N=0;b+N*u<o.height;)d=b+N*u,w.push(d.toFixed(2)),N++;for(var S={},M=0;M<v.length;M++)for(var P=0;P<w.length;P++){var _=v[M]+"_"+w[P];S[_]=0}for(var M=0;M<n.length;M++){for(var C=n[M].px,D=n[M].py,I=parseInt(n[M].count,10),P=(2*C<v[0],2*D<w[0],C/2>Number(v[v.length-1])+Number(u),D/2>Number(w[w.length-1])+Number(u),0);P<v.length;P++){var z=Number(v[P]);if(C>=z&&z+u>C)for(var k=0;k<w.length;k++){var U=Number(w[k]);D>=U&&U+u>D&&(S[v[P]+"_"+w[k]]+=I,I=S[v[P]+"_"+w[k]])}}min=min||I,max=max||I,min=min>I?I:min,max=I>max?I:max}var L=(max-min+1)/10;for(var M in S)if(0!==S[M]){var O=M.split("_");C=Number(O[0]),D=Number(O[1]);var W=(S[M]+.1-min)/L;W=0>W?0:W;var F=C+u/2,G=D+u/2;e.fillStyle=l.fillStyle||"#fa8b2e",e.beginPath(),e.arc(F,G,5*W+5,0,2*Math.PI),e.fill(),e.lineWidth=8*W/10+1,e.strokeStyle=l.strokeStyle||"#fff",e.stroke(),e.font=30*W/10+5+"px serif",e.textAlign="center",e.textBaseline="middle",l.label&&l.label.show&&(e.fillStyle="#fff",e.fillText(S[M],F,G))}this.endDrawMap()},formatParam:function(){var e=this.getDrawOptions();e=JSON.stringify(e),e=JSON.parse(e);var t=e.size||60;return t+=e.unit||"px",t=/px$/.test(t)?parseInt(t,10)*this.zoomUnit:parseInt(t,10),e.size=t,e},geoCoord2Pixel:function(e){var t=this._map.toScreen(e);return t},pixel2GeoCoord:function(e){var t=this._map.toMap(new r(e.x,e.y));return t}})});