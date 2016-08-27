 /*  作者：       tianzh
 *  创建时间：   2012/7/19 9:16:11
 *
 */
 /*  作者：       tianzh
 *  创建时间：   2012/7/18 23:31:01
 *
 */
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TZHSWEET.IBLL;
using TZHSWEET.BLL;
using TZHSWEET.Entity;
using TZHSWEET.ViewModel;
using TZHSWEET.UI;
using System.Web;
using TZHSWEET.Common;
using TZHSWEET.WebUI.Areas.Admin.Controllers;
using System.Reflection;

namespace TZHSWEET.MethodTest
{
    [TestClass]
    public class TestUserService
    {
        /// <summary>
        /// 测试获取用户所有角色
        /// </summary>
        [TestMethod]
        public void TestGetUserAllRole()
        {
            IUserService user = new UserService();
            tbUser usr = new tbUser();
            usr.UserID = 1;
            usr.RoleID = 1;

          Assert.AreEqual(user.GetUserAllRole(usr),"1,2,3,4");
        
                    
        
        }
        [TestMethod]
        public void TestDepart()
        {

            var result = new List<MVCController>();
            var types = Assembly.Load("TZHSWEET.WebUI").GetTypes();

            foreach (var type in types)
            {
                if (type.BaseType.Name == "BaseController")//如果是Controller
                {
                    var controller = new MVCController();
                    controller.ControllerName = type.Name.Replace("Controller", "");//去除Controller的后缀
                    object[] attrs = type.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), true);
                    if (attrs.Length > 0)
                        controller.Description = (attrs[0] as System.ComponentModel.DescriptionAttribute).Description;

                    controller.LinkUrl = "/Admin/" + controller.ControllerName + "/Index";
                    result.Add(controller);
                }
            }
           // Assert.AreEqual(result, "23");
            Assert.AreEqual(ConfigSettings.GetAdminUserID(), "3");
        
        }
        [TestMethod]
        public void TestFilter()
        {
            //var whereTranslator = new FilterTranslator();
            //string where = HttpUtility.UrlDecode("%7B%22rules%22%3A%5B%7B%22field%22%3A%22CompanyName%22%2C%22op%22%3A%22equal%22%2C%22value%22%3A%2244%22%2C%22type%22%3A%22string%22%7D%5D%2C%22groups%22%3A%5B%7B%22rules%22%3A%5B%7B%22field%22%3A%22ContactName%22%2C%22op%22%3A%22equal%22%2C%22value%22%3A%2255%22%2C%22type%22%3A%22string%22%7D%5D%2C%22op%22%3A%22and%22%7D%5D%2C%22op%22%3A%22and%22%7D");
        
            //    //反序列化Filter Group JSON
            //    whereTranslator.Group = JsonHelper.FromJson<FilterGroup>(where);
            //    //合并条件权限规则
            //   // whereTranslator.Group = dpRule.GetRuleGroup(view, whereTranslator.Group);
           
            //whereTranslator.Translate();
            //where = whereTranslator.CommandText;
            //where += "      " + whereTranslator.Parms.ToArray();
            //Assert.AreEqual(where, "22");
            tbUser user = new tbUser ();
           // Assert.AreEqual(user.GetType().Name, "22");
            //   PaginationHelper pagin=new PaginationHelper ("tbDepartment","DeptID",10,1,"desc","IsDeleted=0");
        
            
          //  Assert.AreEqual(pagin.GetRowNumberPagination(15), "5");
            //sql = sql.FormatWith(sqlselect, view, where.IsNullOrEmpty() ? " 1=1 " : where);
        }

    }
}
