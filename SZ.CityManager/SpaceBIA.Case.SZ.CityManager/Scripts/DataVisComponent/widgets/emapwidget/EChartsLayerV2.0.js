define(["dojo/_base/declare", "dojo/_base/lang", "esri/geometry/Point", "esri/geometry/ScreenPoint"], function (e, t, n, r)
{
	return e("EChartsLayer", null,
	{
		name : "EChartsLayer",
		_map : null,
		_ec : null,
		_geoCoord : [],
		_option : null,
		_echartsContainer : null,
		_initOption : null,
		_mapOffset : [0, 0],
		_hasSetOption : !1,
		constructor : function (e, t)
		{
			this._init(e, t)
		},
		_init : function (e, t)
		{
			var a = this;
			a._map = e;
			var o = this._echartsContainer = document.createElement("div");
			o.id = "echarts_for_esri_maps",
			o.style.height = e.height + "px",
			o.style.width = e.width + "px",
			o.style.top = 0,
			o.style.left = 0,
			e.__container.appendChild(o),
			a.getEchartsContainer = function ()
			{
				return a._echartsContainer
			},
			a.getMap = function ()
			{
				return a._map
			},
			a.getInitOption = function ()
			{
				return a._initOption
			},
			a.geoCoord2Pixel = function (e)
			{
				var t = new n(e[0], e[1]),
				r = a._map.toScreen(t);
				return [r.x, r.y]
			},
			a.pixel2GeoCoord = function (e)
			{
				var t = a._map.toMap(new r(e[0], e[1]));
				return [t.x, t.y]
			},
			a.initECharts = function ()
			{
				return a._ec = t.init.apply(a, arguments),
				a._ec.dom.childNodes.item(0).style.position = "",
				a._bindEvent(),
				a._addMarkWrap(),
				a._ec
			},
			a._addMarkWrap = function ()
			{
				function e(e, t, n)
				{
					var r;
					if ("markPoint" == n)
					{
						var r = t.data;
						if (r && r.length)
							for (var o = 0, i = r.length; i > o; o++)
								a._AddPos(r[o])
					}
					else if (r = t.data, r && r.length)
						for (var o = 0, i = r.length; i > o; o++)
							a._AddPos(r[o][0]), a._AddPos(r[o][1]);
					a._ec._addMarkOri(e, t, n)
				}
				a._ec._addMarkOri = a._ec._addMark,
				a._ec._addMark = e
			},
			a.getECharts = function ()
			{
				return a._ec
			},
			a.getMapOffset = function ()
			{
				return a._mapOffset
			},
			a.setOption = function (e, t)
			{
				if (a._hasSetOption = !0, a._initOption || (a._initOption = e), e.timeline)
				{
					var n = e.options || [];
					if (n && n.length > 0)
						for (var r = 0, o = n.length; o > r; r++)
						{
							for (var i, d = n[r].series || {}, _ = 0; i = d[_++]; )
							{
								var f = i.geoCoord;
								if (f)
									for (var s in f)
										a._geoCoord[s] = f[s]
							}
							for (var i, _ = 0; i = d[_++]; )
							{
								var l = i.markPoint || {},
								c = i.markLine || {},
								g = i.heatmap || {},
								p = l.data;
								if (p && p.length)
									for (var s = 0, h = p.length; h > s; s++)
										a._AddPos(p[s]);
								if (p = c.data, p && p.length)
									for (var s = 0, h = p.length; h > s; s++)
										a._AddPos(p[s][0]), a._AddPos(p[s][1]);
								if (p = g.data, p && p.length)
									for (var s = 0, h = p.length; h > s; s++)
										a._AddPos(p[s])
							}
						}
				}
				else
				{
					for (var i, d = e.series || {}, _ = 0; i = d[_++]; )
					{
						var f = i.geoCoord;
						if (f)
							for (var s in f)
								a._geoCoord[s] = f[s]
					}
					for (var i, _ = 0; i = d[_++]; )
					{
						var l = i.markPoint || {},
						c = i.markLine || {},
						g = i.heatmap || {},
						p = l.data;
						if (p && p.length)
							for (var s = 0, h = p.length; h > s; s++)
								a._AddPos(p[s]);
						if (p = c.data, p && p.length)
							for (var s = 0, h = p.length; h > s; s++)
								a._AddPos(p[s][0]), a._AddPos(p[s][1]);
						if (p = g.data, p && p.length)
							for (var s = 0, h = p.length; h > s; s++)
								a._AddPos(p[s])
					}
				}
				a._ec.clear(),
				a._ec.setOption(e, t)
			},
			a._AddPos = function (e)
			{
				if (e.geoCoord)
				{
					var t = e.geoCoord,
					n = this.geoCoord2Pixel(t);
					e.x = n[0] - a._mapOffset[0],
					e.y = n[1] - a._mapOffset[1]
				}
				else if (e.name)
				{
					var t = this._geoCoord[e.name],
					n = this.geoCoord2Pixel(t);
					e.x = n[0] - a._mapOffset[0],
					e.y = n[1] - a._mapOffset[1]
				}
				else if (e instanceof Array && e.length >= 3)
				{
					var t = [e[0], e[1]],
					n = this.geoCoord2Pixel(t);
					e[3] = n[0] - a._mapOffset[0],
					e[4] = n[1] - a._mapOffset[1]
				}
			},
			a._bindEvent = function ()
			{
				a._map.on("zoom-end", function ()
				{
					a.refresh()
				}
				),
				a._map.on("pan-end", function ()
				{
					a.refresh()
				}
				),
				a._ec.getZrender().on("dragstart", function ()
				{
					a._map.disablePan()
				}
				),
				a._ec.getZrender().on("dragend", function ()
				{
					a._map.enablePan()
				}
				),
				a._ec.getZrender().on("mousewheel", function (e)
				{
					a._map.emit("mouse-wheel", e.event)
				}
				)
			},
			a.refresh = function ()
			{
				if (a._hasSetOption && a._ec)
				{
					var e = a._initOption;
					if (e.timeline)
					{
						var t = a._ec.getOption(),
						n = a._ec._timeline || {};
						if (n)
						{
							var r = n.currentIndex;
							e.timeline.autoPlay = !0,
							e.timeline.currentIndex = r
						}
						a._ec.clear(),
						a.setOption(e)
					}
					else
					{
						var t = a._ec.getOption(),
						o = a._ec.component || {},
						i = o.legend,
						d = o.dataRange;
						i && (t.legend.selected = i.getSelectedMap()),
						d && (t.dataRange.range = d._range),
						a._ec.clear(),
						a.setOption(t)
					}
				}
			}
		}
	}
	)
}
);
