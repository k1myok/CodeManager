
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TZHSWEET.Entity;

namespace TZHSWEET.IDao
{
    public interface IModuleDao<T> : IBaseDao<T> where T : class
    {
       /// <summary>
        /// 获取最大的父功能id下的子功能ModuleNo
        /// </summary>
        /// <param name="ParentNo">父级ID</param>
        /// <returns></returns>
         string GetMaxModuleNoByParentNo(string ParentNo);
    }
}
