using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TZHSWEET.Entity;
using TZHSWEET.IDao;
using System.Data.Objects;
namespace TZHSWEET.EFDao
{
    public class ModuleEFDao : BaseEFDao<tbModule>, IModuleDao<tbModule>
    {

        
        
        /// <summary>
        /// 获取最大的父功能id下的子功能ModuleNo
        /// </summary>
        /// <param name="ParentNo">父级ID</param>
        /// <returns></returns>
        public string GetMaxModuleNoByParentNo(string ParentNo)
        {
            using (BaseManageEntities Entities = new BaseManageEntities())
            {
                return Entities.ExecuteStoreQuery<string>("select Cast((Max(ModuleNo)+5) as nvarchar) as MaxModuleNo from  tbModule where ParentNo='" + ParentNo + "'").SingleOrDefault();
            }
        }
      
        
        
    }
}
