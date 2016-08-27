 /*  作者：       tianzh
 *  创建时间：   2012/7/21 23:52:25
 *
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TZHSWEET.Entity;
using TZHSWEET.IBLL;
using TZHSWEET.IDao;
using Spring.Context;

using Spring.Context.Support;
using TZHSWEET.ViewModel;
using TZHSWEET.Common;
namespace TZHSWEET.BLL
{
    public class OperateLogService : BaseService<tbOperateLog>, IOperateLogService
    {
        #region 依赖注入
        /// <summary>
        /// 该接口负责用户自定义的功能实现
        /// </summary>
        private IOperateLogDao<tbOperateLog> myDao = null;

        /// <summary>
        /// 构造函数(接口转换,Dao只负责基类的增删改查)
        /// </summary>
        public OperateLogService()
        {
            //spring.net注入
            IApplicationContext ctx = ContextRegistry.GetContext();

            myDao = ctx.GetObject("OperateLogEFDao") as IOperateLogDao<tbOperateLog>;
            Dao = myDao;
        } 
        #endregion

        /// <summary>
        /// 获取树形的Grid的json数据
        /// </summary>
        /// <param name="gridData"></param>
        /// <returns></returns>
        public LigerUIGrid GetAllLogsToGrid(LigerUIGridRequest gridData)
        {
            int total = 0;
            IEnumerable<tbOperateLog> list = GetAllEntitiesByPaging(gridData, out total);
            List<ViewModelOperateLog> listLogs = new List<ViewModelOperateLog>();
            foreach (tbOperateLog log in list)
            {
                listLogs.Add(ViewModelOperateLog.ToViewModel(log));
            }
            LigerUIGrid grid = new LigerUIGrid();
            grid.Rows = listLogs;
            grid.Total = total;
            return grid;

        }
        /// <summary>
        /// 写系统操作日志
        /// </summary>
        /// <param name="Log"></param>
        /// <returns></returns>
        public bool WriteOperateLog(ViewModelOperateLog Log)
        {
            return myDao.Insert(ViewModelOperateLog.ToEntity(Log));
           
        }
        /// <summary>
        /// 清空三个月以前的日志
        /// </summary>
        /// <returns></returns>
        public bool DeleteThreeMonthLogs()
        {
            int month = 3;
            return myDao.DeleteByMonth(month);
        }
    }
}
