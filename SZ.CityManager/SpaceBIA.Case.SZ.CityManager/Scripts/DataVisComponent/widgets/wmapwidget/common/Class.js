define(["dojo/_base/declare","./MVCObject","./WMapUtils"],function(t,e,n){return t("Class",[e],{constructor:function(){this.__listeners={}},addEventListener:function(t,e){return"object"!=typeof this.__listeners[t]&&(this.__listeners[t]=[]),this.__listeners[t].push(e),this},removeEventListener:function(t,e){var n=this.__listeners[t];if(!n)return!1;for(var s=n.length;s>=0;s--)n[s]===e&&n.splice(s,1);return this},dispatchEvent:function(t,e){var s=(new n).extend({},e),i=this.__listeners[t];if(!i)return!1;for(var r=i.length-1;r>=0;r--)i[r].call(this,s);return this},dispose:function(){}})});