define(["dojo/_base/declare"],function(t){return t("WMapUtils",null,{constructor:function(){},isPlainObject:function(t){var e,o={},n=o.hasOwnProperty;if(!t||"object"!=typeof t||t.nodeType)return!1;var r=!n.call(t,"constructor"),i=!n.call(t.constructor.prototype,"isPrototypeOf");if(t.constructor&&r&&i)return!1;for(e in t);return void 0===e||n.call(t,e)},extend:function(t,e){var o,n=this,r=Object.prototype.toString,i="[object Array]";t=t||{};for(o in e)e.hasOwnProperty(o)&&(n.isPlainObject(e[o])?(t[o]=r.call(e[o])===i?[]:{},n.extend(t[o],e[o]),t[o]=e[o]):t[o]=e[o]);return t},copy:function(t){return this.extend({},t)},inherits:function(t,e){var o,n,r=t.__proto__,i=new Function;i.__proto__=e.__proto__,n=t.__proto__=new i;for(o in r)n[o]=r[o];t.__proto__.constructor=t,t.superClass=e.__proto__},addCssByStyle:function(t){var e=document,o=e.createElement("style");if(o.setAttribute("type","text/css"),o.styleSheet)o.styleSheet.cssText=t;else{var n=e.createTextNode(t);o.appendChild(n)}var r=e.getElementsByTagName("head");r.length?r[0].appendChild(o):e.documentElement.appendChild(o)},getGeoCenter:function(t){for(var e=t[0][0],o=t[0][1],n=t[0][0],r=t[0][1],i=1;i<t.length;i++)e=Math.min(e,t[i][0]),n=Math.max(n,t[i][0]),o=Math.min(o,t[i][1]),r=Math.max(r,t[i][1]);return[e+(n-e)/2,o+(r-o)/2]},getPixelRatio:function(t){var e=t.backingStorePixelRatio||t.webkitBackingStorePixelRatio||t.mozBackingStorePixelRatio||t.msBackingStorePixelRatio||t.oBackingStorePixelRatio||t.backingStorePixelRatio||1;return(window.devicePixelRatio||1)/e}})});