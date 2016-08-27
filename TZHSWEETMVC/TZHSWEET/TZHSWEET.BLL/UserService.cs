using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TZHSWEET.IBLL;
using TZHSWEET.Entity;
using TZHSWEET.IDao;
using TZHSWEET.Common;
using Spring.Context;
using Spring.Context.Support;
using System.Data.Objects;
using TZHSWEET.ViewModel;
namespace TZHSWEET.BLL
{
    /// <summary>
    /// 服务层
    /// </summary>
    public class UserService : BaseService<tbUser>, IUserService
    {
        #region 依赖注入
        /// <summary>
        /// 该接口负责用户自定义的功能实现
        /// </summary>
        private IUserDao<tbUser> myDao = null;

        /// <summary>
        /// 构造函数(接口转换,Dao只负责基类的增删改查)
        /// </summary>
        public UserService()
        {
            //spring.net注入
            IApplicationContext ctx = ContextRegistry.GetContext();

            myDao = ctx.GetObject("UserDao") as IUserDao<tbUser>;
            // DaoFactory.GetUserDao();
            Dao = myDao;
        } 
        #endregion
        #region 登录信息
        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="Number"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public tbUser Login(string Number, string Password)
        {
            return myDao.GetEntity(p => p.UserName == Number && p.UserPassword == Password);
            //  throw new NotImplementedException();
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="requestInfo"></param>
        /// <returns></returns>
        public bool ChangePassword(UserRequest requestInfo, int UserID)
        {
            tbUser user = myDao.GetEntity(p => p.UserID == UserID && p.UserPassword == requestInfo.OldPassord);
            if (!user.IsNullOrEmpty()) return myDao.ChangePassword(user, requestInfo.NewPassword);
            return false;
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="requestInfo"></param>
        /// <returns></returns>
        public tbUser Login(UserRequest requestInfo)
        {
            return Login(requestInfo.UserName, requestInfo.Password);
        } 
        #endregion
        #region 获取用户所有的角色(用户默认角色和用户角色组角色组合)
        /// <summary>
        /// 获取用户所有的角色(用户默认角色和用户角色组角色组合)
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public string GetUserAllRole(tbUser User)
        {
            string Roles = "";
            //添加默认角色
            if (User.RoleID.HasValue)
                Roles = User.RoleID.Value.ToString();

            UserRoleService userRoleService = new UserRoleService();
            //获取用户角色信息
            IEnumerable<tbUserRole> UserRoles = userRoleService.GetEntities(p => p.UserID == User.UserID);
            foreach (tbUserRole userRole in UserRoles)
            {
                //如果存在相同的角色,则无需再次添加
                if (User.RoleID == userRole.RoleID)
                {
                    continue;
                }
                else
                {//拼接权限
                    if (!Roles.IsNullOrEmpty())
                        Roles += ",";
                    Roles += userRole.RoleID.Value.ToString();
                }
            }
            return Roles;

        }
        public bool Update(tbUser user, string Roles)
        {
            return myDao.Update(user, Roles);
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="user"></param>
        /// <param name="Roles"></param>
        /// <returns></returns>
        public bool Insert(tbUser user, string Roles)
        {
            return myDao.Insert(user, Roles);
        }
        /// <summary>
        /// 获取用户角色信息
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <returns></returns>
        public string GetUserAllRole(int UserID)
        {
           
                //获取用户信息
                var user = GetEntity(p => p.UserID == UserID);

                string UserRoles= GetUserAllRole(user);
                return UserRoles;
        }
        #endregion
        #region 获取用户实体
        /// <summary>
        /// 获取用户实体
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public tbUser GetUserInfo(string UserID)
        {
            int Id = Convert.ToInt32(UserID);
            return myDao.GetEntity(p => p.UserID == Id);
        }
        /// <summary>
        /// 根据条件获取所有用户
        /// </summary>
        /// <param name="request">请求条件</param>
        /// <param name="Count">记录总数</param>
        /// <returns></returns>
        public IEnumerable<tbUser> GetAllUsers(LigerUIGridRequest request, out int Count)
        {
            //wher语句
            string commandText = "";
            if (!request.Where.IsNullOrEmpty())
            {
                //做Where的翻译处理工作
                FilterTranslator whereTranslator = new FilterTranslator();
                //反序列化Filter Group JSON
                whereTranslator.Group = JsonHelper.FromJson<FilterGroup>(request.Where);
                //开始翻译sql语句
                whereTranslator.Translate();
                commandText = FilterParam.AddParameters(whereTranslator.CommandText, whereTranslator.Parms);
            }
            return myDao.GetEntitiesForPaging("tbUser", request.PageNumber, request.PageSize, request.SortName, request.SortOrder, commandText, out Count);
        }
        /// <summary>
        /// 根据条件获取所有菜单
        /// </summary>
        /// <param name="request">请求条件</param>
        /// <returns></returns>
        public LigerUIGrid GetAllUsers(LigerUIGridRequest request)
        {
            int total = 0;
            return new LigerUIGrid()
            {
                Rows = ViewModelUser.ToListViewModel(GetAllUsers(request, out total)),
                Total = total
            };
        }
        #endregion
        #region 权限管理
        
        /// <summary>
        /// 获取指定角色(多个)的菜单(请在ui层做字符串安全性过滤)
        /// </summary>
        /// <param name="Roles">角色组(多个角色用,分隔开)</param>
        /// <returns></returns>
        public IEnumerable<VRoleMenuPermission> GetRoleMenu(string Roles)
        {
            return myDao.GetUserMenuPermission("it.RoleID in {" + Roles + "} or it.ParentNo=0");
        }

        #region 获取用户的菜单
        /// <summary>
        /// 获取用户的菜单
        /// </summary>
        /// <param name="Roles"></param>
        /// <returns></returns>
        public IEnumerable<ViewModelMenu> GetUserPermissionMenus(string Roles)
        {
            List<ViewModelMenu> menusList = new List<ViewModelMenu>();
            //获取当前角色的json菜单数据
            IEnumerable<VRoleMenuPermission> RolesMenus = GetRoleMenu(Roles).Distinct(p => p.ModuleID);
            //首先找出父级菜单
            var ParentMenus = RolesMenus.Where(p => p.ParentNo == 0);
            foreach (VRoleMenuPermission roleMenu in ParentMenus)
            {
                //实体转化
                ViewModelMenu menu = ViewModelMenu.ToEntity(roleMenu);
                //添加子菜单
                foreach (VRoleMenuPermission childMenu in RolesMenus)
                {
                    if (childMenu.ParentNo == roleMenu.ModuleID)
                    {
                        ViewModelMenu child = ViewModelMenu.ToEntity(childMenu);
                        if (menu.children == null)
                            menu.children = new List<ViewModelMenu>();
                        menu.children.Add(child);
                    }
                }
                menusList.Add(menu);
            }
            return menusList;

        } 
        #endregion

        #region 菜单信息加载处理
        #region 请求菜单
        /// <summary>
        /// 请求一级菜单树
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public IEnumerable<ViewModelMenu> GetUserTreeMenus(LigerUITreeRequest request, string userRoles)
        {
            //返回ui层的菜单
            IEnumerable<ViewModelMenu> rootMenus = new List<ViewModelMenu>(){
            new ViewModelMenu()
            {

                icon = request.RootIcon,
                MenuIcon = request.RootIcon,
                MenuParentNo = request.PidField,
                MenuName = "主菜单",
                MenuNo = request.IdField,
                text = "主菜单",
                children = (List<ViewModelMenu>)ViewModelMenu.ToListViewModel(new ModuleService().GetEntities(p=>p.ParentNo==0)) 
            
            }
        };
            return rootMenus;

        }
        
        /// <summary>
        /// 组合用户角色规则
        /// </summary>
        /// <param name="userRoles">用户角色</param>
        /// <param name="group">组合</param>
        /// <returns></returns>
        private FilterGroup GetUserRolesFilter(string userRoles, FilterGroup group)
        {
            FilterRule roleRule = new FilterRule("RoleID", userRoles, "in");
            if (group == null)
                return new FilterGroup()
                {
                    groups = new List<FilterGroup> { group }
                };
            else
                return new FilterGroup()
                  {
                      groups = new List<FilterGroup> { group },
                      op = "and",
                      rules = new List<FilterRule>() { roleRule }
                  };
        }
        /// <summary>
        /// 获取请求的Grid菜单列表
        /// </summary>
        /// <param name="request">请求</param>
        /// <param name="userRoles">角色组</param>
        /// <param name="Count">总记录数</param>
        /// <returns></returns>
        public IEnumerable<VRoleMenuPermission> GetUserGridMenus(LigerUIGridRequest request, string userRoles, out int Count)
        {
        
            //wher语句
          string commandText = "";
            //做Where的翻译处理工作
            FilterTranslator whereTranslator = new FilterTranslator();
            //当前角色规则转化(用户角色in (xxx)中
            string where = ViewModelMenu.FilterParamsToEntityParams(request.Where);
            //反序列化Filter Group JSON,并把角色权限组所在的规则合并
            FilterGroup group = JsonHelper.FromJson<FilterGroup>(where);
            whereTranslator.Group= GetUserRolesFilter(userRoles,group);
            //开始翻译sql语句
            whereTranslator.Translate();
            commandText = FilterParam.AddParameters(whereTranslator.CommandText, whereTranslator.Parms);

            return myDao.GetAllMenusForPaging(request.PageNumber, request.PageSize, request.SortName, request.SortOrder, commandText, out Count).Distinct(p => p.ModuleID);
        }



       
        /// <summary>
        /// 获取请求的Grid菜单列表的LigeruiGrid格式
        /// </summary>
        /// <param name="request"></param>
        /// <param name="userRoles"></param>
        /// <param name="AdminUserRoleID">超级管理员角色ID</param>
        /// <returns></returns>
        public LigerUIGrid GetUserGridMenus(LigerUIGridRequest request, string userRoles,string AdminUserRoleID)
        {
            int total = 0;
            LigerUIGrid grid = new LigerUIGrid();
           //验证是否拥有超级管理员角色,如果是则返回所有菜单
            if (!Tools.CheckStringHasValue(userRoles, ',', AdminUserRoleID))
                grid.Rows = ViewModelMenu.ToListViewModel(GetUserGridMenus(request, userRoles, out total));
            else
                grid.Rows =ViewModelMenu.ToListViewModel(new ModuleService().GetAdminMenus(request, out total));
               
            //记录总数
                grid.Total = total;
                //返回ui层的菜单
                return grid;
           
        }
        /// <summary>
        /// 根据whereFilter条件获取子集数据
        /// </summary>
        /// <param name="request">where条件</param>
        /// <param name="userRoles">角色组</param>
        /// <returns></returns>
        public IEnumerable<ViewModelMenu> GetMenusByParentNo(string whereFilter, string userRoles)
        {
            string commandText = "";
            //做Where的翻译处理工作
            FilterTranslator whereTranslator = new FilterTranslator();
            //注入当前用户的ID和UserRole角色配置信息
            FilterTranslator.RegCurrentParmMatch("{CurrentUserID}", () => SessionHelper.Get("UserID").ObjToInt());
            FilterTranslator.RegCurrentParmMatch("{CurrentRoleID}", () => SessionHelper.Get("UserRoles")[0].ObjToInt());
            //当前角色规则转化(用户角色in (xxx)中
            FilterRule roleRule = new FilterRule("RoleID", userRoles, "in");
            if (!whereFilter.IsNullOrEmpty())//没有where条件
            {
                string where = ViewModelMenu.FilterParamsToEntityParams(whereFilter);
                //反序列化Filter Group JSON,并把角色权限组所在的规则合并
                FilterGroup group = JsonHelper.FromJson<FilterGroup>(where);
                whereTranslator.Group = new FilterGroup()
                {
                    groups = new List<FilterGroup> { group },
                    op = "and",
                    rules = new List<FilterRule>() { roleRule }
                };
            }
            //开始翻译sql语句
            whereTranslator.Translate();
            commandText = FilterParam.AddParameters(whereTranslator.CommandText, whereTranslator.Parms);

            List<ViewModelMenu> menusList = new List<ViewModelMenu>();
            //获取当前角色的json菜单数据
            IEnumerable<VRoleMenuPermission> RolesMenus = myDao.GetUserMenuPermissionBySql(commandText).Distinct(p=>p.ModuleID);
            foreach (VRoleMenuPermission roleMenu in RolesMenus)
            {
                //实体转化
                ViewModelMenu menu = ViewModelMenu.ToEntity(roleMenu);
                menusList.Add(menu);
            }
            return menusList;
        }
        #endregion 
        #endregion

        #region 根据角色和控制器查找该控制器中的Action(也就是按钮动作信息)
        /// <summary>
        /// 根据角色和控制器查找该控制器中的Action(也就是按钮动作信息)
        /// </summary>
        /// <param name="Roles">角色组</param>
        /// <param name="MenuNo">控制器(也就是菜单信息)</param>
        /// <param name="AdminUserRoleID">超级管理员角色ID</param>
        /// <returns>返回角色的该菜单下的按钮信息(</returns>
        public IEnumerable<VRoleModulePermission> GetRolesModulesActions(string Roles, string MenuNo,string AdminUserRoleID)
        {
            ObjectParameter[] objParams = new ObjectParameter[]{ 
                new ObjectParameter("ModuleController",MenuNo)
            };
            string commandText="";
            //如果不是超级角色就需要加上角色组限制条件
            if (!Tools.CheckStringHasValue(Roles, ',', AdminUserRoleID))
                commandText += " it.RoleID in {" + Roles + "} and ";
            return myDao.GetUserModulePermission(commandText+" it.ModuleController='" + MenuNo+"'").OrderByDescending(p => p.Sort).Distinct(p => p.PermissionAction); 
        } 
        #endregion
        #region 获取角色指定菜单的按钮信息
        /// <summary>
        /// 获取角色指定菜单的按钮信息
        /// </summary>
        /// <param name="Roles">角色信息</param>
        /// <param name="RequestController">请求信息</param>
        /// <param name="AdminUserRoleID">超级管理员角色ID</param>
        /// <returns></returns>
        public IEnumerable<ViewModelMyButton> GetButtons(string Roles, ButtonRequest RequestController, string AdminUserRoleID)
        {
            List<ViewModelMyButton> listButtons = new List<ViewModelMyButton>();
            //获取当前角色的按钮模块数组
            IEnumerable<VRoleModulePermission> Actions = GetRolesModulesActions(Roles, RequestController.MenuNo,AdminUserRoleID).Where(p=>p.IsButton==true);
            foreach (VRoleModulePermission data in Actions)
            {
                //添加到按钮数组
                listButtons.Add(ViewModelMyButton.ToEntity(data));
            }
            return listButtons;
        } 
        #endregion
        #region 权限校验
        /// <summary>
        /// 角色是否拥有权限
        /// </summary>
        /// <param name="Roles">角色组</param>
        /// <param name="Controller">控制器</param>
        /// <param name="Action">动作</param>
        /// <returns></returns>
        public bool RoleHasOperatePermission(string Roles, string Controller, string Action)
        {
            ObjectParameter[] objParams = new ObjectParameter[]{ 
                new ObjectParameter("Controller",Controller),
               new ObjectParameter("Action",Action)
            };
            return myDao.GetUserModulePermission("it.RoleID in {" + Roles + "} and it.ModuleController=@Controller and it.PermissionAction=@Action", objParams).Count() > 0;

        }
        /// <summary>
        /// 角色是否拥有该菜单模块权限
        /// </summary>
        /// <param name="Roles">角色组</param>
        /// <param name="Controller">控制器</param>
        /// <param name="Action">动作</param>
        /// <returns></returns>
        public bool RoleHasMenusPermission(string Roles, string Controller, string Action)
        {
            ObjectParameter[] objParams = new ObjectParameter[]{ 
                new ObjectParameter("Controller",Controller)
              // ,new ObjectParameter("Action",Action)
            };
            //菜单权限控制(不需要对动作进行处理)
            return myDao.GetUserMenuPermission("it.RoleID in {" + Roles + "} and it.ModuleController=@Controller", objParams).Count() > 0;
        } 
        #endregion

        #endregion

       
     
    }
}
