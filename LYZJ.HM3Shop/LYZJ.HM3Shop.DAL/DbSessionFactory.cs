using LYZJ.HM3Shop.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace LYZJ.HM3Shop.DAL
{
    public class DbSessionFactory
    {
        /// <summary>
        /// 保证了线程内DbSession实例唯一
        /// </summary>
        /// <returns></returns>
        public static IDbSession GetCurrentDbSession()
        {
            IDbSession _dbSession = CallContext.GetData("DbSession") as IDbSession;
            if (_dbSession == null)
            {
                _dbSession = new DbSession();
                CallContext.SetData("DbSession", _dbSession);
            }
            return _dbSession;
        }
    }
}
