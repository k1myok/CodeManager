using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransformUtility
{
    public  class CoordinateTransformUtility
    {
        public const double X_PI = 3.14159265358979324 * 3000.0 / 180.0;

        public const double PI = 3.1415926535897932384626;
        public const double A = 6378245.0;
        public const double EE = 0.00669342162296594323;

        /// <summary>
        /// 百度坐标系(BD-09)与火星坐标系(GCJ-02)的转换
        /// 即百度转谷歌、高德
        /// </summary>
        /// <param name="BD_Point">百度坐标</param>
        /// <returns>火星坐标</returns>
        public static MapPoint BD09toGCJ02(MapPoint BD_Point)
        {
            MapPoint GCJ_Point = new MapPoint();
            try
            {

                double x = BD_Point.Lon - 0.0065;
                double y = BD_Point.Lat - 0.006;
                double z = Math.Sqrt(x * x + y * y) - 0.00002 * Math.Sin(y * X_PI);
                double theta = Math.Atan2(y, x) - 0.000003 * Math.Cos(x * X_PI);
                GCJ_Point.Lon = z * Math.Cos(theta);
                GCJ_Point.Lat = z * Math.Sin(theta);
            }
            catch
            {
                return null;
            }
            return GCJ_Point;
        }
        /// <summary>
        /// 火星坐标系 (GCJ-02) 与百度坐标系 (BD-09) 的转换
        /// 即谷歌、高德转百度
        /// </summary>
        /// <param name="GCJ_Point"></param>
        /// <returns></returns>
        public static MapPoint GCJ02toBD09(MapPoint GCJ_Point)
        {
            MapPoint BD_Point = new MapPoint();
            try
            {

                double z = Math.Sqrt(GCJ_Point.Lon * GCJ_Point.Lon + GCJ_Point.Lat * GCJ_Point.Lat) + 0.00002 * Math.Sin(GCJ_Point.Lat * X_PI);
                double theta = Math.Atan2(GCJ_Point.Lat, GCJ_Point.Lon) + 0.000003 * Math.Cos(GCJ_Point.Lon * X_PI);
                BD_Point.Lon = z * Math.Cos(theta) + 0.0065;
                BD_Point.Lat = z * Math.Sin(theta) + 0.006;
            }
            catch
            {
                return null;
            }
            return BD_Point;
        }
        /// <summary>
        /// WGS84转GCJ02
        /// 即标准WGS84转火星坐标系
        /// </summary>
        /// <param name="WGS_Point">WGS坐标</param>
        /// <returns>在中国范围内，则返回转化后的坐标；否则，返回原始坐标</returns>
        public static MapPoint WGS84toGCJ02(MapPoint WGS_Point)
        {
            MapPoint GCJ_Point = new MapPoint();
            try
            {
                if (IsPointOutOfChina(WGS_Point))
                {
                    return WGS_Point;
                }
                double dlon = TransformLon(WGS_Point.Lon - 105.0, WGS_Point.Lat - 35.0);
                double dlat = TransformLat(WGS_Point.Lon - 105.0, WGS_Point.Lat - 35.0);
                double radlat = WGS_Point.Lat / 180.0 * PI;
                double magic = Math.Sin(radlat);
                magic = 1 - EE * magic * magic;
                double sqrtMagic = Math.Sqrt(magic);
                dlon = (dlon * 180.0) / (A / sqrtMagic * Math.Cos(radlat) * PI);
                dlat = (dlat * 180.0) / ((A * (1 - EE)) / (magic * sqrtMagic) * PI);
                GCJ_Point.Lon = WGS_Point.Lon + dlon;
                GCJ_Point.Lat = WGS_Point.Lat + dlat;
            }
            catch
            {
                return null;
            }
            return GCJ_Point;
        }
        /// <summary>
        /// GCJ02转换为WGS84
        /// 即火星坐标系转标准WGS84坐标系
        /// </summary>
        /// <param name="GCJ_Point">火星坐标</param>
        /// <returns>在中国范围内，则返回转化后的坐标；否则，返回原始坐标</returns>
        public static MapPoint GCJ02toWGS84(MapPoint GCJ_Point)
        {
            MapPoint WGS_Point = new MapPoint();

            try
            {
                if (IsPointOutOfChina(GCJ_Point))
                {
                    return GCJ_Point;
                }
                double dlon = TransformLon(GCJ_Point.Lon - 105.0, GCJ_Point.Lat - 35.0);
                double dlat = TransformLat(GCJ_Point.Lon - 105.0, GCJ_Point.Lat - 35.0);
                double radlat = GCJ_Point.Lat / 180.0 * PI;
                double magic = Math.Sin(radlat);
                magic = 1 - EE * magic * magic;
                double sqrtMagic = Math.Sqrt(magic);
                dlon = (dlon * 180.0) / (A / sqrtMagic * Math.Cos(radlat) * PI);
                dlat = (dlat * 180.0) / ((A * (1 - EE)) / (magic * sqrtMagic) * PI);
                WGS_Point.Lon = GCJ_Point.Lon * 2 - (GCJ_Point.Lon + dlon);
                WGS_Point.Lat = GCJ_Point.Lat * 2 - (GCJ_Point.Lat + dlat);
            }
            catch
            {
                return null;
            }
            return WGS_Point;
        }



        /// <summary>
        /// 纬度转换函数
        /// </summary>
        /// <param name="Lon"></param>
        /// <param name="Lat"></param>
        /// <returns></returns>
        public static double TransformLat(double Lon, double Lat)
        {
            var ret = -100.0 + 2.0 * Lon + 3.0 * Lat + 0.2 * Lat * Lat + 0.1 * Lon * Lat + 0.2 * Math.Sqrt(Math.Abs(Lon));
            ret += (20.0 * Math.Sin(6.0 * Lon * PI) + 20.0 * Math.Sin(2.0 * Lon * PI)) * 2.0 / 3.0;
            ret += (20.0 * Math.Sin(Lat * PI) + 40.0 * Math.Sin(Lat / 3.0 * PI)) * 2.0 / 3.0;
            ret += (160.0 * Math.Sin(Lat / 12.0 * PI) + 320 * Math.Sin(Lat * PI / 30.0)) * 2.0 / 3.0;
            return ret;
        }
        /// <summary>
        /// 经度转换函数
        /// </summary>
        /// <param name="Lon"></param>
        /// <param name="Lat"></param>
        /// <returns></returns>
        public static double TransformLon(double Lon, double Lat)
        {
            double ret = 300.0 + Lon + 2.0 * Lat + 0.1 * Lon * Lon + 0.1 * Lon * Lat + 0.1 * Math.Sqrt(Math.Abs(Lon));
            ret += (20.0 * Math.Sin(6.0 * Lon * PI) + 20.0 * Math.Sin(2.0 * Lon * PI)) * 2.0 / 3.0;
            ret += (20.0 * Math.Sin(Lon * PI) + 40.0 * Math.Sin(Lon / 3.0 * PI)) * 2.0 / 3.0;
            ret += (150.0 * Math.Sin(Lon / 12.0 * PI) + 300.0 * Math.Sin(Lon / 30.0 * PI)) * 2.0 / 3.0;
            return ret;
        }

        /// <summary>
        /// 判断点是否在中国范围内，在才做偏移，不在不做偏移
        /// </summary>
        /// <param name="p">点</param>
        /// <returns>在，返回False;不在，返回False</returns>
        private static bool IsPointOutOfChina(MapPoint p)
        {
            if ((p.Lon < 72.004 || p.Lon > 137.8347) || (p.Lat < 0.8293 || p.Lat > 55.8271))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }

    /// <summary>
    /// 定义MapPoint类，存储点的经纬度坐标
    /// </summary>
    public class MapPoint
    {
        private double _Lon;
        public double Lon
        {
            get
            {
                return _Lon;
            }
            set
            {
                _Lon = value;
            }
        }
        private double _Lat;
        public double Lat
        {
            get
            {
                return _Lat;
            }
            set
            {
                _Lat = value;
            }
        }


        public MapPoint()
        {
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="lon">经度</param>
        /// <param name="lat">纬度</param>
        public MapPoint(double lon, double lat)
        {
            this._Lon = lon;
            this._Lat = lat;
        }

    }
}
