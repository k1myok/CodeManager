/*  作者：       tianzh
*  创建时间：   2012/7/21 9:34:11
*
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using TZHSWEET.Entity;

namespace TZHSWEET.ViewModel
{
    /// <summary>
    /// LigerUI中Select的实体类
    /// </summary>
    public class LigerUISelect
    {
        #region - 属性 -

        /// <summary>
        /// ID
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public int value { get; set; }

        /// <summary>
        /// 显示内容
        /// </summary>

        public string text { get; set; }

        #endregion

        #region - 方法 -

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented,
  new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, });

        }

        /// <summary>
        /// 将实体转为为Select列表
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        public static LigerUISelect ToViewModel(tbDepartment department)
        {
            LigerUISelect item = new LigerUISelect();
            item.id = department.DeptID;
            item.value = department.DeptID;
            item.text = department.DeptName;
            return item;
        }

        public static LigerUISelect ToViewModel(tbRole role)
        {
            LigerUISelect item = new LigerUISelect();
            item.id = role.RoleID;
            item.text = role.RoleName;
            item.value = role.RoleID;
            return item;
        }

        public static IEnumerable<LigerUISelect> ToListModel(IEnumerable<tbRole> roles)
        {
            var selectList = new List<LigerUISelect>();
            foreach (var item in roles)
            {
                selectList.Add(LigerUISelect.ToViewModel(item));
            }
            return selectList;
        }

        public static IEnumerable<LigerUISelect> ToListModel(IEnumerable<tbDepartment> list)
        {
            var selectList = new List<LigerUISelect>();
            foreach (var item in list)
            {
                selectList.Add(LigerUISelect.ToViewModel(item));
            }
            return selectList;
        }

        #endregion

    }

}
