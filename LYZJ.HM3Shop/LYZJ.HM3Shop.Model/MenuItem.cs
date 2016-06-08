using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYZJ.HM3Shop.Model
{
    public class MenuItem
    {
        /// <summary>
        /// 菜单名
        /// </summary>
        public string MenuName { get; set; }

        /// <summary>
        /// 菜单URL
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 关联的ID
        /// </summary>
        public int Id { get; set; }
    }

}
