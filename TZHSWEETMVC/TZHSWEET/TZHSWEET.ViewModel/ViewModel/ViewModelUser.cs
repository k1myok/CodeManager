/*  作者：       tianzh
*  创建时间：   2012/7/30 23:58:56
*
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using TZHSWEET.Entity;
using TZHSWEET.Common;
namespace TZHSWEET.ViewModel
{
    public class ViewModelUser : ViewModelBase
    {
        #region - 属性 -

        public int UserID { get; set; }

        public string UserName { get; set; }

        public string UserPassword { get; set; }

        public string RoleIDs { get; set; }
        public int? DeptID { get; set; }

        public string Phone { get; set; }

        public string Fax { get; set; }

        public string Email { get; set; }

        public string QQ { get; set; }

        public string NickName { get; set; }

        public string Address { get; set; }

        public string RealName { get; set; }

        public string Sex { get; set; }

        public bool Enabled { get; set; }

        #endregion

        #region - 构造函数 -

        public ViewModelUser()
        {
        }

        public ViewModelUser(HttpContextBase context,bool IsAdd)
        {
            if (!IsAdd)
            {
                UserID = context.Request["UserID"].ObjToInt();
                ModifyDate = DateTime.Now;
                ModifyUserID = SessionHelper.Get("UserID").ObjToIntNull();
                Enabled = context.Request["Enabled"] == "1";
            }
            else
            {
                CreateDate = DateTime.Now;
                CreateUserID = SessionHelper.Get("UserID").ObjToIntNull();
                Enabled =true;
            }
            UserName = context.Request["UserName"];
            UserPassword = context.Request["UserPassword"];
            RoleIDs = context.Request["RoleIDs"];
            DeptID = context.Request["DeptID"].ObjToIntNull();
            Phone = context.Request["Phone"];
            Fax = context.Request["Fax"];
            QQ = context.Request["QQ"];
            NickName = context.Request["NickName"];
            Address = context.Request["Address"];
            RealName = context.Request["RealName"];
            Sex = context.Request["Sex"];
            
            RecordStatus = context.Request["RecordStatus"];

        }

        #endregion

        #region - 方法 -
        public static tbUser ToEntity(ViewModelUser user)
        {
            tbUser item = new tbUser();
            item.UserID = user.UserID;
            item.DeptID = user.DeptID;
            item.UserName = user.UserName;
            item.UserPassword = user.UserPassword;
            item.Phone = user.Phone;
            item.Fax = user.Fax;
            item.QQ = user.QQ;
            item.NickName = user.NickName;
            item.Address = user.Address;
            item.RealName = user.RealName;
            item.Sex = user.Sex=="1"?true :false;
            item.Enabled = user.Enabled;
            item.RecordStatus = user.RecordStatus;
            item.CreateDate = user.CreateDate;
            item.CreateUserID = user.CreateUserID;
            item.ModifyDate = user.ModifyDate;
            item.ModifyUserID = user.ModifyUserID;
            item.RecordStatus = user.RecordStatus;
            return item;
        }

        public static ViewModelUser ToViewModel(tbUser user)
        {
            ViewModelUser item = new ViewModelUser();
            item.UserID = user.UserID;
            item.DeptID = user.DeptID;
            item.UserName = user.UserName;
            item.UserPassword = user.UserPassword;
            item.Phone = user.Phone;
            item.Fax = user.Fax;
            item.QQ = user.QQ;
            item.NickName = user.NickName;
            item.Address = user.Address;
            item.RealName = user.RealName;
            item.Sex = user.Sex.Value?"1":"0";
            item.Enabled = user.Enabled.Value;
            item.RecordStatus = user.RecordStatus;
            return item;
        }

        public static IEnumerable<ViewModelUser> ToListViewModel(IEnumerable<tbUser> users)
        {
            List<ViewModelUser> listModel = new List<ViewModelUser>();
            foreach (tbUser user in users)
            {
                listModel.Add(ToViewModel(user));
            }
            return listModel;
        }

        #endregion

    }
}
