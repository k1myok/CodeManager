
/*
 * 
 * 创建人：李林峰
 * 
 * 时  间：2009-05-04创建;2012年9月12日整理，添加xml注释;2013年10月19日重购，加入了周的开始与结束
 * 
 * 描  述：日期转换帮助类，常用于报表开发;
 * 可以转换成为：本日开始时间、本日结束时间、本周开始时间、本周结束时间、本月开始时间、本月结束时间、本年开始时间、本年结束时间
 * 
 */

using System;

namespace YunLianLHelp
{
    /// <summary>
    /// 日期转换帮助类
    /// </summary>
    public static class DateTimeHelper
    {
        #region 日
        /// <summary>
        /// 将日期转换为本日的开始时间
        /// </summary>
        /// <param name="value">2001-01-01</param>
        /// <returns>2001-01-01 00:00:00</returns>
        public static DateTime ToDayStart(string value)
        {
            //转换成日期类型
            DateTime date = System.Convert.ToDateTime(value);
            //转换成短日期类型字符
            string shortDate = date.ToShortDateString();
            //返回日期类型
            return System.Convert.ToDateTime(shortDate);
        }

        /// <summary>
        /// 将日期转换为本日的开始时间
        /// </summary>
        /// <param name="value">任意时间</param>
        /// <returns>2001-01-01 00:00:00</returns>
        public static DateTime ToDayStart(DateTime value)
        {
            //转换成短日期类型字符
            string shortDate = value.ToShortDateString();
            //返回日期类型
            return System.Convert.ToDateTime(shortDate);
        }

        /// <summary>
        /// 将日期转换为本日的开始时间
        /// </summary>
        /// <param name="value">2001-01-01</param>
        /// <returns>2001-01-01 23:59:59</returns>
        public static DateTime ToDayEnd(string value)
        {
            //转换成日期类型
            DateTime date = System.Convert.ToDateTime(value);
            //转换成短日期类型
            DateTime shortDate = System.Convert.ToDateTime(date.ToShortDateString());
            //返回日期加一天减一秒
            return shortDate.AddDays(1).AddSeconds(-1);
        }

        /// <summary>
        /// 将日期转换为本日的结束时间
        /// </summary>
        /// <param name="value">任意时间</param>
        /// <returns>2001-01-01 23:59:59</returns>
        public static DateTime ToDayEnd(DateTime value)
        {
            //转换成短日期类型
            DateTime shortDate = System.Convert.ToDateTime(value.ToShortDateString());
            //返回日期加一天减一秒
            return shortDate.AddDays(1).AddSeconds(-1);
        }
        #endregion

        #region 周
        /// <summary>
        /// 将日期转换为本周的开始时间
        /// </summary>
        /// <param name="value">2001-01-01</param>
        /// <returns>2001-01-01 00:00:00</returns>
        public static DateTime ToWeekStart(string value)
        {
            //转换成日期类型
            DateTime date = System.Convert.ToDateTime(value);
            //根据当前时间取出该周周一的当前时间
            DateTime weekStart = ToWeekStart(date, date.Date.DayOfWeek);
            //转换成短日期类型字符
            string shortDate = weekStart.ToShortDateString();
            //返回日期类型
            return System.Convert.ToDateTime(shortDate);
        }

        /// <summary>
        /// 将日期转换为本周的开始时间
        /// </summary>
        /// <param name="value">任意时间</param>
        /// <returns>2001-01-01 00:00:00</returns>
        public static DateTime ToWeekStart(DateTime value)
        {
            //根据当前时间取出该周周一的当前时间
            DateTime weekStart = ToWeekStart(value, value.Date.DayOfWeek);
            //转换成短日期类型字符
            string shortDate = weekStart.ToShortDateString();
            //返回日期类型
            return System.Convert.ToDateTime(shortDate);
        }

        /// <summary>
        /// 将日期转换为本周的结束时间
        /// </summary>
        /// <param name="value">2001-01-01</param>
        /// <returns>2001-01-01 23:59:59</returns>
        public static DateTime ToWeekEnd(string value)
        {
            //转换成日期类型
            DateTime date = System.Convert.ToDateTime(value);
            //根据当前时间取出该周周末的当前时间
            DateTime weekEnd = ToWeekEnd(date, date.Date.DayOfWeek);
            //转换成短日期类型字符
            string shortDate = weekEnd.ToShortDateString();
            //返回日期加一天减一秒
            return Convert.ToDateTime(shortDate).AddDays(1).AddSeconds(-1);
        }

        /// <summary>
        /// 将日期转换为本周的结束时间
        /// </summary>
        /// <param name="value">任意时间</param>
        /// <returns>2001-01-01 23:59:59</returns>
        public static DateTime ToWeekEnd(DateTime value)
        {
            //根据当前时间取出该周周末的当前时间
            DateTime weekEnd = ToWeekEnd(value, value.Date.DayOfWeek);
            //转换成短日期类型字符
            string shortDate = weekEnd.ToShortDateString();
            //返回日期类型
            //返回日期加一天减一秒
            return Convert.ToDateTime(shortDate).AddDays(1).AddSeconds(-1);
        }

        /// <summary>
        /// 将日期转换为本周周一的某一时间
        /// </summary>
        /// <param name="date">将要转换的日期</param>
        /// <param name="week">传入日期的周的枚举类型</param>
        /// <returns>2001-01-01 12:12:12</returns>
        private static DateTime ToWeekStart(DateTime date, DayOfWeek week)
        {
            DateTime WeekStart = new DateTime();
            switch (week)
            {
                case DayOfWeek.Monday:
                    WeekStart = date;
                    break;
                case DayOfWeek.Tuesday:
                    WeekStart = date.AddDays(-1);
                    break;
                case DayOfWeek.Wednesday:
                    WeekStart = date.AddDays(-2);
                    break;
                case DayOfWeek.Thursday:
                    WeekStart = date.AddDays(-3);
                    break;
                case DayOfWeek.Friday:
                    WeekStart = date.AddDays(-4);
                    break;
                case DayOfWeek.Saturday:
                    WeekStart = date.AddDays(-5);
                    break;
                case DayOfWeek.Sunday:
                    WeekStart = date.AddDays(-6);
                    break;
            }
            return WeekStart;
        }

        /// <summary>
        /// 将日期转换为本周周日的某一时间
        /// </summary>
        /// <param name="date">将要转换的日期</param>
        /// <param name="week">传入日期的周的枚举类型</param>
        /// <returns>2001-01-01 12:12:12</returns>
        private static DateTime ToWeekEnd(DateTime date, DayOfWeek week)
        {
            DateTime WeekStart = new DateTime();
            switch (week)
            {
                case DayOfWeek.Monday:
                    WeekStart = date.AddDays(6);
                    break;
                case DayOfWeek.Tuesday:
                    WeekStart = date.AddDays(5);
                    break;
                case DayOfWeek.Wednesday:
                    WeekStart = date.AddDays(4);
                    break;
                case DayOfWeek.Thursday:
                    WeekStart = date.AddDays(3);
                    break;
                case DayOfWeek.Friday:
                    WeekStart = date.AddDays(2);
                    break;
                case DayOfWeek.Saturday:
                    WeekStart = date.AddDays(1);
                    break;
                case DayOfWeek.Sunday:
                    WeekStart = date;
                    break;
            }
            return WeekStart;
        }

        #endregion

        #region 月
        /// <summary>
        /// 将日期转换为本月的开始时间
        /// </summary>
        /// <param name="value">2001-01-01</param>
        /// <returns>2001-01-01 00:00:00</returns>
        public static DateTime ToMonthStart(string value)
        {
            //转换成日期类型
            DateTime date = System.Convert.ToDateTime(value);
            //根据年、月重新创建日期 
            return new DateTime(date.Year, date.Month, 1);
        }

        /// <summary>
        /// 将日期转换为本月的开始时间
        /// </summary>
        /// <param name="value">任意时间</param>
        /// <returns>2001-01-01 00:00:00</returns>
        public static DateTime ToMonthStart(DateTime value)
        {
            //根据年、月重新创建日期
            return new DateTime(value.Year, value.Month, 1);
        }

        /// <summary>
        /// 将日期转换为本月的结束时间
        /// </summary>
        /// <param name="value">2001-01-01</param>
        /// <returns>2001-01-31 23:59:59</returns>
        public static DateTime ToMonthEnd(string value)
        {
            //转换成日期类型
            DateTime date = System.Convert.ToDateTime(value);
            //根据年、月重新创建日期 
            DateTime monthStart = new DateTime(date.Year, date.Month, 1);
            //创建结束日期
            return monthStart.AddMonths(1).AddSeconds(-1);
        }

        /// <summary>
        /// 将日期转换为本月的结束时间
        /// </summary>
        /// <param name="value">任意时间</param>
        /// <returns>2001-01-31 23:59:59</returns>
        public static DateTime ToMonthEnd(DateTime value)
        {
            //根据年、月重新创建日期 
            DateTime monthStart = new DateTime(value.Year, value.Month, 1);
            //创建结束日期
            return monthStart.AddMonths(1).AddSeconds(-1);
        }
        #endregion

        #region 年
        /// <summary>
        /// 将日期转换为本年的开始时间
        /// </summary>
        /// <param name="value">2001-01-01</param>
        /// <returns>2001-01-01 00:00:00</returns>
        public static DateTime ToYearStart(string value)
        {
            //转换成日期类型
            DateTime date = System.Convert.ToDateTime(value);
            //根据年、月重新创建日期 
            return new DateTime(date.Year, 1, 1);
        }

        /// <summary>
        /// 将日期转换为本年的开始时间
        /// </summary>
        /// <param name="value">任意时间</param>
        /// <returns>2001-01-01 00:00:00</returns>
        public static DateTime ToYearStart(DateTime value)
        {
            //根据年、月重新创建日期 
            return new DateTime(value.Year, 1, 1);
        }

        /// <summary>
        /// 将日期转换为本年的结束时间
        /// </summary>
        /// <param name="value">2001-01-01</param>
        /// <returns>2001-12-31 23:59:59</returns>
        public static DateTime ToYearEnd(string value)
        {
            //转换成日期类型
            DateTime date = System.Convert.ToDateTime(value);
            //根据年、月重新创建日期 
            DateTime yearStart = new DateTime(date.Year, 1, 1);
            //创建结束日期
            DateTime yearEnd = new DateTime(date.Year, 1, 1).AddYears(1).AddSeconds(-1);
            return yearEnd;
        }

        /// <summary>
        /// 将日期转换为本年的结束时间
        /// </summary>
        /// <param name="value">任意时间</param>
        /// <returns>2001-12-31 23:59:59</returns>
        public static DateTime ToYearEnd(DateTime value)
        {
            //根据年、月重新创建日期 
            DateTime yearStart = new DateTime(value.Year, 1, 1);
            //创建结束日期
            return new DateTime(value.Year, 1, 1).AddYears(1).AddSeconds(-1);
        }
        #endregion
    }
}
