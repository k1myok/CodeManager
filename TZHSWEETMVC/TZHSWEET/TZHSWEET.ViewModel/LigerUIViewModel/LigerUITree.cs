/*  作者：       tianzh
*  创建时间：   2012/7/17 21:41:51
*
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TZHSWEET.Entity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TZHSWEET.ViewModel
{

    public class LigerUITree 
    {
        public int id { get; set; }
        public string text { get; set; }
        public string desc { get; set; }
        public List<LigerUITree> children { get; set; }
        public string icon { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented,
new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

        }
        public static LigerUITree ToEntity(tbRole role)
        {
            LigerUITree item = new LigerUITree();
            item.id = role.RoleID;
            item.text = role.RoleName;
            item.desc = role.Description;
            return item;
        }
        /// <summary>
        /// 实体转化
        /// </summary>
        /// <param name="department"></param>
        public static LigerUITree ToEntity(tbDepartment department)
        {
            LigerUITree item = new LigerUITree();
            item.id = department.DeptID;
            item.text = department.DeptName;
            item.desc = department.DeptDescription;
            // gridTree.children = new List<LigerUIGridTree>();
            return item;
        }
        public static IEnumerable<LigerUITree> ToListViewModel(IEnumerable<tbRole> listRoles)
        {
            List<LigerUITree> list = new List<LigerUITree>();
            foreach (tbRole role in listRoles)
            {
                list.Add(ToEntity(role));
            }
            return list;
        
        }
      
    }



}
