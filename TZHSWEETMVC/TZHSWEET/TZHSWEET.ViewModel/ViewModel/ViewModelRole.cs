 /*  作者：       tianzh
 *  创建时间：   2012/7/30 16:10:07
 *
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using TZHSWEET.Common;
using TZHSWEET.Entity;
namespace TZHSWEET.ViewModel
{
    public class ViewModelRole:ViewModelBase
    {
        public int RoleID { get; set; }
        public string RoleNo { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public bool IsVisible { get; set; }
        public ViewModelRole()
        { 
        
        }
        public ViewModelRole(HttpContextBase context,bool IsAdd)
        {
            if (IsAdd)
            {
                CreateUserID = SessionHelper.Get("UserID").ObjToIntNull();
                CreateDate = DateTime.Now;
            }
            else
            {
                RoleID = context.Request["RoleID"].ObjToInt();
            }
            RoleNo = context.Request["RoleNo"];
            RoleName = context.Request["RoleName"];
            Description = context.Request["Description"];
            IsVisible = context.Request["IsVisible"] == "1";
            ModifyUserID = SessionHelper.Get("UserID").ObjToIntNull();
            ModifyDate = DateTime.Now;
            RecordStatus = context.Request["RecordStatus"];
            IsDeleted = context.Request["IsDeleted"] == "1";

        }
        public static tbRole ToEntity(ViewModelRole item)
        {
            tbRole role = new tbRole();
            role.RoleID = item.RoleID;
            role.RoleName = item.RoleName;
            role.RoleNo = item.RoleNo;
            role.Description = item.Description;
            role.IsVisible = item.IsVisible;
            role.CreateUserID = item.CreateUserID;
            role.CreateDate = item.CreateDate;
            role.ModifyUserID = item.ModifyUserID;
            role.ModifyDate = item.ModifyDate;
            role.RecordStatus = item.RecordStatus;
            role.IsDeleted = item.IsDeleted;
            return role;
        }
        public static ViewModelRole ToViewModel(tbRole role)
        {
            ViewModelRole item = new ViewModelRole();
            item.RoleID = role.RoleID;
            item.RoleName = role.RoleName;
            item.RoleNo = role.RoleNo;
            item.Description = role.Description;
            item.IsVisible = role.IsVisible.Value;
            item.CreateUserID = role.CreateUserID;
            item.CreateDate = role.CreateDate;
            item.ModifyUserID = role.ModifyUserID;
            item.ModifyDate = role.ModifyDate;
            item.RecordStatus = role.RecordStatus;
            item.IsDeleted = role.IsDeleted.Value;
            return item;
        }
        public static IEnumerable<ViewModelRole> ToListViewModel(IEnumerable<tbRole> roles)
        {
            List<ViewModelRole> list = new List<ViewModelRole>();
            foreach (tbRole role in roles)
            {
                list.Add(ToViewModel(role));
            }
            return list;

        }
    }
}
